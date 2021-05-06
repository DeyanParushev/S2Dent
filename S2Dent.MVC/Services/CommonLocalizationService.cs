namespace S2Dent.MVC.Services
{
    using System.Reflection;
    using Microsoft.Extensions.Localization;
    using S2Dent.MVC.Resources;

    public class CommonLocalizationService
    {
        private readonly IStringLocalizer localizer;

        public CommonLocalizationService(IStringLocalizerFactory factory)
        {
            var assemblyName = new AssemblyName(typeof(CommonResources).GetTypeInfo().Assembly.FullName);
            localizer = factory.Create(nameof(CommonResources), assemblyName.Name);
        }

        public string Get(string key)
        {
            return localizer[key];
        }
    }
}
