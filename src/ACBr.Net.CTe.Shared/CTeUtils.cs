using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace ACBr.Net.CTe
{
    internal static class CTeUtils
    {
        public static void RemoveDuplicateNamespaceDeclarations(this XmlDocument xmlDoc)
        {
            var xmlElem = xmlDoc.DocumentElement;
            if (xmlElem == null) return;

            var namespaceMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            RemoveDuplicateNamespaceDeclarations(xmlElem, namespaceMgr);
        }

        private static void RemoveDuplicateNamespaceDeclarations(
            XmlElement xmlElem, XmlNamespaceManager namespaceMgr)
        {
            namespaceMgr.PushScope();

            // xpath on "@xmlns:*" seams not to work so…
            var namespaceDefinitions = (from xmlAttr in xmlElem.Attributes.OfType<XmlAttribute>()
                                        where "xmlns".Equals(xmlAttr.Prefix, StringComparison.Ordinal)
                                        select new
                                        {
                                            Prefix = xmlAttr.LocalName,
                                            Url = xmlAttr.Value,
                                            Node = xmlAttr
                                        }).ToArray();

            foreach (var namespaceDefinition in namespaceDefinitions)
            {
                var currentUrl = namespaceMgr.LookupNamespace(namespaceDefinition.Prefix);
                if (!string.IsNullOrEmpty(currentUrl))
                {
                    Debug.Assert(namespaceDefinition.Url.Equals(currentUrl, StringComparison.OrdinalIgnoreCase));
                    xmlElem.RemoveAttributeNode(namespaceDefinition.Node);
                }
                else
                {
                    namespaceMgr.AddNamespace(namespaceDefinition.Prefix, namespaceDefinition.Url);
                }
            }

            // Recurse into Child Elements
            foreach (XmlNode xmlChildNode in xmlElem.ChildNodes)
                if (xmlChildNode.NodeType == XmlNodeType.Element)
                    RemoveDuplicateNamespaceDeclarations((XmlElement)xmlChildNode, namespaceMgr);

            namespaceMgr.PopScope();
        }
    }
}