// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidModule.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The guid module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Ninject
{
    using System;
    using global::Ninject.Modules;

    /// <summary>
    /// The guid module.
    /// </summary>
    internal class GuidModule : NinjectModule
    {
        /// <summary>
        /// The _guid.
        /// </summary>
        private readonly Guid _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidModule"/> class.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public GuidModule(Guid guid)
        {
            _guid = guid;
        }

        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<Guid>().ToConstant(_guid).InSingletonScope();
        }
    }
}