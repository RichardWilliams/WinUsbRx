// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbDeviceWatcher.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UsbDeviceWatcher type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Windows.Forms;
    using Handle;
    using UsbDeviceNotifications;
    using Wrappers;

    /// <summary>
    /// The usb device watcher.
    /// </summary>
    internal class UsbDeviceWatcher : NativeWindow, IUsbDeviceWatcher
    {
        /// <summary>
        /// The _marshal wrapper.
        /// </summary>
        private readonly IMarshalWrapper _marshalWrapper;

        /// <summary>
        /// This is the usb device notification factory, this will help in creating the UsbDeviceNotification when a WM_DEVICECHANGE has come in.
        /// </summary>
        private readonly IUsbDeviceNotificationFactory _usbDeviceNotificationFactory;

        /// <summary>
        /// The usb form that is started to for the application.run.
        /// </summary>
        private readonly IUsbForm _usbForm;

        /// <summary>
        /// The _observable where all the usb notifications come from.
        /// </summary>
        private readonly IObservable<IUsbDeviceNotification> _observable;

        /// <summary>
        /// The observer when UsbDeviceWatcher is started, it's stored so that the WndProc can use it to send new notifications.
        /// </summary>
        private IObserver<IUsbDeviceNotification> _observer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsbDeviceWatcher"/> class.
        /// </summary>
        /// <param name="marshalWrapper">
        /// The marshal Wrapper.
        /// </param>
        /// <param name="usbDeviceNotificationFactory">
        /// The usb device notification factory.
        /// </param>
        /// <param name="usbForm">
        /// The usb form.
        /// </param>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public UsbDeviceWatcher(IMarshalWrapper marshalWrapper, IUsbDeviceNotificationFactory usbDeviceNotificationFactory, IUsbForm usbForm, Guid guid)
        {
            _marshalWrapper = marshalWrapper;
            _usbDeviceNotificationFactory = usbDeviceNotificationFactory;
            _usbForm = usbForm;
            DeviceGuid = guid;
            RegisteredCreatedHandle = null;
            _observable = ConnectableObservable().RefCount();
        }

        /// <summary>
        /// Gets the guid of the device to register for notifications.
        /// </summary>
        public Guid DeviceGuid { get; private set; }

        /// <summary>
        /// Gets the handle used when we have registered for notifications on a device.
        /// </summary>
        public CreatedHandle RegisteredCreatedHandle { get; private set; }

        /// <summary>
        /// Gets the WM_DEVICECHANGE constant.
        /// </summary>
        private int WmDeviceChange
        {
            get { return 0x219; }
        }

        /// <summary>
        /// The all device notifications.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IObservable<IUsbDeviceNotification> AllDeviceNotifications()
        {
            return _observable;
        }

        /// <summary>
        /// The attached devices.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IObservable<IUsbDeviceNotification> ArrivedDeviceNotificationsOnly()
        {
            return _observable.Where(x => x.IsArrivalNotification);
        }

        /// <summary>
        /// Overriding the WndProc as we want to notify the observer with all notifications from the usb device, that is a DEV_BROADCAST_DEVINTERFACE and is the same Guid.
        /// </summary>
        /// <param name="m">
        /// The m.
        /// </param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmDeviceChange)
            {
                try
                {
                    var devBroadCastDeviceInterface = _marshalWrapper.PointerToStructure<BroadcastDeviceInterface>(m.LParam, typeof(BroadcastDeviceInterface));
                    if (devBroadCastDeviceInterface != null && devBroadCastDeviceInterface.Guid == DeviceGuid)
                    {
                        _observer.OnNext(_usbDeviceNotificationFactory.Create(DeviceGuid, m, devBroadCastDeviceInterface.Name));
                    }
                }
                catch (Exception e)
                {
                    _observer.OnError(e);
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// This will start the process of watching for devices.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        private IConnectableObservable<IUsbDeviceNotification> ConnectableObservable()
        {
            // This observes on another thread so it can take away any further processing on this observer chained higher up, from the next notifications
            // from the usb.
            return Observable.Create<IUsbDeviceNotification>(observer =>
            {
                // TODO: Now this is something I am unsure if I should be doing or not, I am storing away the observer so I can let the
                // TODO: WndProc fire new messages out.
                _observer = observer;

                var runSubscribedDisposable = _usbForm.Run().Subscribe(
                    next =>
                    {
                        try
                        {
                            HandleNotification(next);
                        }
                        catch (Exception e)
                        {
                            _observer.OnError(e);
                        }
                    },
                    _observer.OnError,
                    _observer.OnCompleted);

                var cleanUpObserver = Disposable.Create(() => _observer = null);
                return new CompositeDisposable(runSubscribedDisposable, cleanUpObserver);
            }).ObserveOn(NewThreadScheduler.Default).Publish();
        }

        /// <summary>
        /// Handle notification, this could be a Created or Destroyed handle.
        /// </summary>
        /// <param name="watchedDevice">
        /// The watched device.
        /// </param>
        private void HandleNotification(IHandle watchedDevice)
        {
            var processHandleResult = watchedDevice.ProcessFor(this);
            processHandleResult.SuccessTest(_observer);
        }
    }
}