using System;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.CTe
{
    internal static class CTeUtils
    {
        public static string RemoverDeclaracaoXml(this string xml)
        {
            if (xml.IsEmpty()) return xml;

            var posIni = xml.IndexOf("<?", StringComparison.Ordinal);
            if (posIni < 0) return xml;

            var posFinal = xml.IndexOf("?>", StringComparison.Ordinal);
            return posFinal < 0 ? xml : xml.Remove(posIni, (posFinal + 2) - posIni);
        }
    }
}