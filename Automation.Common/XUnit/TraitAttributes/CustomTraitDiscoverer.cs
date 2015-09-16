using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Automation.Common.XUnit.TraitAttributes
{
    public class CustomTraitDiscoverer : ITraitDiscoverer
    {
        public virtual IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var ctorArgs = traitAttribute.GetConstructorArguments().Cast<string>().ToList();

            var traits = new List<KeyValuePair<string, string>>();

            traits.Add(new KeyValuePair<string, string>("Category", "All"));

            if (!string.IsNullOrWhiteSpace(ctorArgs[1]))
            {
                var split = ctorArgs[1].Split(';');
                foreach (var s in split)
                {
                    traits.Add(new KeyValuePair<string, string>(ctorArgs[0], s));
                }
            }
            return traits;
        }
    }
}
