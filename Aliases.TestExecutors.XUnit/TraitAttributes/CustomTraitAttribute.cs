using System;
using Xunit.Sdk;

namespace Aliases.TestExecutors.XUnit.TraitAttributes
{
    [TraitDiscoverer("Aliases.TestExecutors.XUnit.TraitAttributes.CustomTraitDiscoverer", "Aliases.TestExecutors.XUnit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomTraitAttribute : Attribute, ITraitAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Aliases.TestExecutors.XUnit.TraitAttributes.CustomTraitAttribute"/> class.
        /// 
        /// </summary>
        /// <param name="name">The trait name</param><param name="value">The trait value</param>
        public CustomTraitAttribute(string name, string value)
        {
        }
    }
}
