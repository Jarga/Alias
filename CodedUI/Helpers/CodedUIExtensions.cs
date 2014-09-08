using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace TestAutomation.CodedUI.Helpers
{
    public static class CodedUiExtension
    {
        private static readonly HashSet<string> DefaultFindProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Id", "Name"
        };

        private static readonly List<string> TagIdentifierProperties = new List<string>() { "Tag", "TagName" };
        private static readonly List<string> TypeIdentifierProperties = new List<string>() { "Type", "ElementType" }; 

        public static UITestControl Find(this UITestControl _this, IDictionary<string, string> searchProperties)
        {
            var ctrl = InitializeElementFromProperties(_this, searchProperties);

            AddFindProperties(ctrl, searchProperties);

            return ctrl;
        }

        public static UITestControlCollection FindAll(this UITestControl _this, IDictionary<string, string> searchProperties)
        {
            var ctrl = InitializeElementFromProperties(_this, searchProperties);

            AddFindProperties(ctrl, searchProperties);

            return ctrl.FindMatchingControls();
        }

        public static void AddFindProperties(UITestControl control, IDictionary<string, string> searchProperties)
        {
            if (control == null) throw new ArgumentNullException("control");
            if (searchProperties == null) throw new ArgumentNullException("searchProperties");

            foreach (var item in searchProperties.Keys)
            {
                string propertyValue = searchProperties[item];
                if (DefaultFindProperties.Contains(item))
                {
                    if (propertyValue.StartsWith("contains="))
                    {
                        control.SearchProperties.Add(item, propertyValue.Substring(9), PropertyExpressionOperator.Contains);
                    }
                    else
                    {
                        control.SearchProperties.Add(item, propertyValue);
                    }
                }
                else
                {
                    if (propertyValue.StartsWith("contains="))
                    {
                        control.FilterProperties.Add(item, propertyValue.Substring(9), PropertyExpressionOperator.Contains);
                    }
                    else
                    {
                        control.SearchProperties.Add(item, propertyValue);
                    }
                }
            }
        }


        private static HtmlControl InitializeElementFromProperties(UITestControl parent, IDictionary<string, string> searchProperties)
        {
            ICollection<string> propNames = searchProperties.Keys;
            string tagKey = propNames.FirstOrDefault(name => TagIdentifierProperties.Contains(name));
            string typeKey = propNames.FirstOrDefault(name => TypeIdentifierProperties.Contains(name));
            string tag, type;
            //If no identifiers found then just return the default type
            if (string.IsNullOrEmpty(tagKey))
            {
                return new HtmlControl(parent);
            }
            else
            {
                //consume tag property so it is not used to search
                tag = searchProperties[tagKey];
                searchProperties.Remove(tagKey);
            }

            switch (tag.ToLower() + (string.IsNullOrEmpty(typeKey) ? "" : "." + searchProperties[typeKey].ToLower()))
            {
                case "input.text":
                    searchProperties.Remove(typeKey);
                    return new HtmlEdit(parent);
                case "input.button":
                case "input.submit":
                    searchProperties.Remove(typeKey);
                    return new HtmlInputButton(parent);
                case "span":
                    return new HtmlSpan(parent);
                default:
                    return new HtmlControl(parent);
            }
        }
    }

}
