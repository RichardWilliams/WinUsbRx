// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WrappersModule.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the WrappersModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WinUsbRx.Ninject
{
    using global::Ninject.Modules;
    using Wrappers;

    /// <summary>
    /// The wrappers module.
    /// </summary>
    internal class WrappersModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<IMarshalWrapper>().To<MarshalWrapper>();
        }
    }
}