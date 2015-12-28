using System;
using System.Collections.Generic;
using System.Linq;

namespace Alias.Common.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddElementDefinition(this IDictionary<string, Types.Alias> rootDictionary, string elementName, params string[] elementProperties)
        {
            var elementAlias = new Types.Alias();
            if (elementProperties.Length < 2 || elementProperties.Length % 2 != 0)
            {
                throw new ArgumentException("elementProperties must contain only pairs of Field Property and Field Value strings");
            }

            for (int i = 0; i < elementProperties.Length; i += 2)
            {
                if (elementProperties.ElementAt(i).Equals("parentelement", StringComparison.OrdinalIgnoreCase))
                {
                    elementAlias.ParentElement = elementProperties.ElementAt(i + 1);
                }
                else
                {
                    elementAlias.Definition.Add(elementProperties.ElementAt(i), elementProperties.ElementAt(i + 1));
                }
            }

            if (rootDictionary.ContainsKey(elementName))
            {
                rootDictionary[elementName] = elementAlias;
            }
            else
            {
                rootDictionary.Add(elementName, elementAlias);
            }
        }
    }
}
