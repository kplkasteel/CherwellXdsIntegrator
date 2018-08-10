using System.Xml;

namespace CherwellXdsIntegrator.Helpers
{
    public class XmlReaders
    {
        public static string FindXmlValue(string myMatch, string element)
        {
            var result = string.Empty;
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(myMatch);
            var nodeList = xmldoc.GetElementsByTagName(element);
            foreach (XmlNode node in nodeList)
            {
                result = node.InnerText;
            }
            return result;
        }

        public static int FindJudgements(string myMatch, string element)
        {
            var result = 0;
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(myMatch);
            var nodeList = xmldoc.GetElementsByTagName(element);
            foreach (XmlNode node in nodeList)
            {
                result = result + int.Parse(node.InnerText);
            }
            return result;
        }
    }
}