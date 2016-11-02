namespace Vurdalakov
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(ExtensionCommandPackage.PackageGuid)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ExtensionCommandPackage : Package
    {
        public const String PackageGuid = "cacd46ba-c111-446a-8e30-ce6e9574d9a7";

        public ExtensionCommandPackage()
        {
        }

        protected override void Initialize()
        {
            ExtensionCommandSet.Initialize(this);
            base.Initialize();
        }
    }
}
