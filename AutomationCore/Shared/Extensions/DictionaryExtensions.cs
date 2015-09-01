using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationCore.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddElementDefinition(this  IDictionary<string, IDictionary<string, string>> rootDictionary, string elementName, params string[] elementProperties)
        {
            IDictionary<string, string> elementProps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (elementProperties.Length < 2 || elementProperties.Length % 2 != 0)
            {
                throw new ArgumentException("elementProperties must contain only pairs of Field Property and Field Value strings");
            }

            for (int i = 0; i < elementProperties.Length; i += 2)
            {
                elementProps.Add(elementProperties.ElementAt(i), elementProperties.ElementAt(i + 1));
            }

            if (rootDictionary.ContainsKey(elementName))
            {
                rootDictionary[elementName] = elementProps;
            }
            else
            {
                rootDictionary.Add(elementName, elementProps);
            }
        }
    }
}
