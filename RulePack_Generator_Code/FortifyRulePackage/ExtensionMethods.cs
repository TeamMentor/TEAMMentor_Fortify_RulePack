using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace FortifyPackageGenerator
{
    public static class ExtensionMethods
    {
        public static String ToXml(this object source)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof (RulePack));
            serializer.Serialize(stringwriter, source);
            return stringwriter.ToString();
        }

        public static String CleanUpHtmlMarkUps(this StringBuilder source)
        {
            source = source.Replace("<h1>Description</h1>",
                                    "<b>Description Powered by Security Innovation <a href=\"https://teammentor.net/teamMentor\" target=\"_blank\">TeamMentor.net </a> \r\n\r\n</b>");
            source = source.Replace("<h1>", "");
            source = source.Replace("</h1>", "\r\n\r\n");
            source = source.Replace("<h2>", "");
            source = source.Replace("</h2>", "\r\n\r\n");
            source = source.Replace("<h3>", "");
            source = source.Replace("</h3>", "\r\n\r\n");

            return source.ToString();
        }

        public static Stream GetResourceAsFile(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
                if (stream != null)
                {
                    return stream;
                }
                else return null;
        }
    }
}