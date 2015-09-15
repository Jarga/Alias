using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AutomationCore.XUnit.TraitAttributes
{
    [TraitDiscoverer("AutomationCore.XUnit.TraitAttributes.CustomTraitDiscoverer", "AutomationCore")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomTraitAttribute : Attribute, ITraitAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:AutomationCore.XUnit.TraitAttributes.CustomTraitAttribute"/> class.
        /// 
        /// </summary>
        /// <param name="name">The trait name</param><param name="value">The trait value</param>
        public CustomTraitAttribute(string name, string value)
        {
        }
    }
}
