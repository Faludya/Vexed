using Microsoft.Extensions.Localization;
using System.Reflection;
using Vexed.Models;

namespace Vexed.Services
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo()!.Assembly!.FullName!);
            _localizer = factory.Create("SharedResource", assemblyName.Name!);
        }

        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }
    }
}
