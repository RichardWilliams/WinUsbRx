// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbForm.cs" company="None">
//   Some copyright TODO:
// </copyright>
// <summary>
//   Defines the UsbForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using System.Reactive;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Handle;

    /// <summary>
    /// The usb form. This wraps up a control class so its not exposed and Application.ExitThread can't be called on it. It is also private. 
    /// </summary>
    internal sealed class UsbForm : IUsbForm
    {
        /// <summary>
        /// This is used in creating the CreatedHandle or DestroyedHandle.
        /// </summary>
        private readonly IHandleFactory _handleFactory;

        /// <summary>
        /// The _control.
        /// </summary>
        private readonly TheControl _control;

        /// <summary>
        /// This is the main task that the application.Run is on.
        /// </summary>
        private Task _mainTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsbForm"/> class. This creates a private an instance of a private class here to do the monitoring of the usb device change events.
        /// </summary>
        /// <param name="handleFactory">
        /// The handle factory.
        /// </param>
        public UsbForm(IHandleFactory handleFactory)
        {
            _control = new TheControl();
            _handleFactory = handleFactory;
        }

        /// <summary>
        /// The run.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     to when the handle is created.
        /// </returns>
        public IObservable<IHandle> Run()
        {
            return Observable.Create<IHandle>(ob =>
            {
                var handleCreated = new ManualResetEventSlim(false);
                var startFailed = new ManualResetEventSlim(false);
                var handleDestroyed = new ManualResetEventSlim(false);

                // Subscribe to the event handlers and transform them to an IHandle (Created or Destroyed).
                var observableHandleDestroyedSubscription = ObserveHandleDestroyed(handleDestroyed, startFailed, ob);
                var observableHandleCreatedSubscription = ObserveHandleCreated(handleCreated, startFailed, ob);

                // This kicks off the application to start running and listening for usb device changes which are notified on teh event handlers of this. 
                _mainTask = Task.Factory.StartNew(() => Start(handleCreated, startFailed));

                // This will block until message loop has started i.e. handleCreated is set. Or the Start failed due to an exception.
                CheckMessageLoopHasStarted(handleCreated, startFailed);

                // Setting up of the cleanup to happen when the subscription of this observable is disposed.
                var cleanUpDisposable = Disposable.Create(() => CleanUp(handleCreated, startFailed, handleDestroyed));
                return new CompositeDisposable(cleanUpDisposable, observableHandleCreatedSubscription, observableHandleDestroyedSubscription, handleDestroyed, startFailed, handleCreated);
            });
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            _control.Dispose();
        }

        /// <summary>
        /// This will set up an observable on the Event Handle Destroyed and also subscribe to that observable. This monitors for exceptions also and fires an onError if there are any.
        /// </summary>
        /// <param name="handleDestroyed">
        /// The handle Destroyed.
        /// </param>
        /// <param name="startFailed">
        /// The start Failed.
        /// </param>
        /// <param name="handleDestroyedObserver">
        /// The handle Destroyed Observer.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        private IDisposable ObserveHandleDestroyed(ManualResetEventSlim handleDestroyed, ManualResetEventSlim startFailed, IObserver<IHandle> handleDestroyedObserver)
        {
            var observer = Observer.Create<EventPattern<object>>(
                next =>
                {
                    try
                    {
                        handleDestroyedObserver.OnNext(_handleFactory.CreateDestroyedHandle(_control.Handle));
                        handleDestroyed.Set();
                    }
                    catch (Exception e)
                    {
                        handleDestroyedObserver.OnError(e);
                        startFailed.Set();
                    }
                },
                handleDestroyedObserver.OnError,
                handleDestroyedObserver.OnCompleted);

            return Observable.FromEventPattern(ev => _control.HandleDestroyed += ev, ev => _control.HandleDestroyed -= ev).Subscribe(observer);
        }

        /// <summary>
        /// This will set up an observable on the Event Handle Created and also subscribe to that observable. This monitors for exceptions also and fires an onError if there are any.
        /// This will also set the 
        /// </summary>
        /// <param name="handleCreated">
        /// The handle created.
        /// </param>
        /// <param name="startFailed">
        /// The start Failed.
        /// </param>
        /// <param name="handleCreatedObserver">
        /// The handle Created Observer.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        private IDisposable ObserveHandleCreated(ManualResetEventSlim handleCreated, ManualResetEventSlim startFailed, IObserver<IHandle> handleCreatedObserver)
        {
            var observer = Observer.Create<EventPattern<object>>(
                next =>
                {
                    try
                    {
                        var handle = _handleFactory.CreateCreatedHandle(_control.Handle);
                        handleCreatedObserver.OnNext(handle);
                        handleCreated.Set();
                    }
                    catch (Exception e)
                    {
                        handleCreatedObserver.OnError(e);
                        startFailed.Set();
                    }
                },
                handleCreatedObserver.OnError,
                handleCreatedObserver.OnCompleted);

            return Observable.FromEventPattern(ev => _control.HandleCreated += ev, ev => _control.HandleCreated -= ev).Subscribe(observer);
        }

        /// <summary>
        /// This will Create the handle for the control and also call Application.Run that should start the message loop.
        /// This will block on Application.Run until the message loop receives a WM_QUIT. When the message loop is stopped
        /// DestroyHandle will be called. When the Application exits it will call the ThreadExit on the ApplicationContext, this is required to perform
        /// some necessary steps to preventing the disposing of the Run Observable from also trying to send a message to exit application thread.
        /// </summary>
        /// <param name="handleCreated">
        /// The handle Created.
        /// </param>
        /// <param name="startFailed">
        /// The start Failed.
        /// </param>
        private void Start(ManualResetEventSlim handleCreated, ManualResetEventSlim startFailed)
        {
            try
            {
                _control.CreateControl();

                if (!handleCreated.IsSet)
                {
                    startFailed.Set();
                }
                else
                {
                    Application.Run();
                }
            }
            catch (Exception)
            {
                startFailed.Set();
            }
        }

        /// <summary>
        /// This should block until all events off the event queue have completed. Since ExitThread event needs to complete before continuing.
        /// </summary>
        private void SafeExit()
        {
            _control.Invoke(new Action(() =>
            {
                Application.ExitThread();
                Application.DoEvents();
            }));
        }

        /// <summary>
        /// This will stop the message loop running and and wait for the mainTask to complete. It will only stop the message loop from running if it was created and not destroyed already via
        /// an external call to Application.ExitThread.
        /// </summary>
        /// <param name="handleCreated">
        /// The handle Created.
        /// </param>
        /// <param name="startFailed">
        /// The start Failed.
        /// </param>
        /// <param name="handleDestroyed">
        /// The handle Destroyed.
        /// </param>
        private void CleanUp(ManualResetEventSlim handleCreated, ManualResetEventSlim startFailed, ManualResetEventSlim handleDestroyed)
        {
            // Only block until a handle was created or a the starting of the thread failed.
            WaitHandle.WaitAny(new[] { handleCreated.WaitHandle, startFailed.WaitHandle });

            // We only need perform a safe exit, i.e. an ExitThread if the handle was created and not destroyed already. 
            if (handleCreated.IsSet && !handleDestroyed.IsSet && !startFailed.IsSet)
            {
                SafeExit();
            }

            // Wait for the main task to finish, this will only finish if the Application was exited or it never started due to an exception.
            if (_mainTask != null)
            {
                _mainTask.Wait();
            }
        }

        /// <summary>
        /// This is used to check if the message loop has started. We can not start the checking until we know that the handle
        /// has been created, or the main task failed. This will block until the Application.MessageLoop has started.
        /// </summary>
        /// <param name="handleCreated">
        /// The handle created.
        /// </param>
        /// <param name="startFailed">
        /// The start Failed.
        /// </param>
        private void CheckMessageLoopHasStarted(ManualResetEventSlim handleCreated, ManualResetEventSlim startFailed)
        {
            WaitHandle.WaitAny(new[] { handleCreated.WaitHandle, startFailed.WaitHandle });

            if (!startFailed.IsSet)
            {
                // Invoke the while check on the same thread as the Application is running on.
                _control.Invoke(new Action(() =>
                {
                    // The while check is executed on the same thread as Application.Run as we require
                    // to see the state of the Application.MessageLoop.
                    while (Application.MessageLoop == false)
                    {
                        Console.WriteLine("Waiting for message loop to start...");
                        Thread.Sleep(200);
                    }
                }));                
            }
        }

        /// <summary>
        /// The control used to watch for usb events.
        /// </summary>
        private class TheControl : Control
        {
        }
    }
}
