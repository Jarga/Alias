using System;
using Xunit.Sdk;

namespace Automation.Common.Executors.XUnit.TraitAttributes
{
    [TraitDiscoverer("Automation.Common.Executors.XUnit.TraitAttributes.CustomTraitDiscoverer", "Automation.Common")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomTraitAttribute : Attribute, ITraitAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Automation.Common.Executors.XUnit.TraitAttributes.CustomTraitAttribute"/> class.
        /// 
        /// </summary>
        /// <param name="name">The trait name</param><param name="value">The trait value</param>
        public CustomTraitAttribute(string name, string value)
        {
        }
    }
}
