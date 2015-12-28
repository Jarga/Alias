using System;
using System.Collections.Generic;

namespace Alias.Common.Shared.Types
{
    public class Alias
    {
        public IDictionary<string, string> Definition = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Alias _parent { get; set; }
        private string _parentname { get; set; }

        public string AliasName { get; }

        public Alias RelativeTo
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public string ParentElement
        {
            get { return _parentname; }
            set { _parentname = value; }
        }

        public string Class
        {
            get { return Definition["class"] ?? Definition["classname"]; }
            set { Definition["class"] = value; }
        }

        public string Css
        {
            get { return Definition["css"] ?? Definition["cssselector"]; }
            set { Definition["css"] = value; }
        }

        public string Id
        {
            get { return Definition["id"]; }
            set { Definition["id"] = value; }
        }

        public string LinkText
        {
            get { return Definition["linktext"]; }
            set { Definition["linktext"] = value; }
        }

        public string Name
        {
            get { return Definition["name"]; }
            set { Definition["name"] = value; }
        }

        public string TagName
        {
            get { return Definition["tagname"]; }
            set { Definition["tagname"] = value; }
        }

        public string XPath
        {
            get { return Definition["xpath"]; }
            set { Definition["xpath"] = value; }
        }

        public string Text
        {
            get { return Definition["text"]; }
            set { Definition["text"] = value; }
        }

        public string Type
        {
            get { return Definition["type"]; }
            set { Definition["type"] = value; }
        }

        public string Href
        {
            get { return Definition["href"]; }
            set { Definition["href"] = value; }
        }

        public Alias(string aliasName = null)
        {
            AliasName = aliasName;
        }
    }
}
