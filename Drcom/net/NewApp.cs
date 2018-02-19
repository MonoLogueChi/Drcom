using System;
using System.Reflection;
using System.Xml.Linq;

namespace Drcom.net
{
    public class NewApp
    {
        public static string[] IsNew()
        {

            try
            {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                string versionxml = "http://md.xxwhite.com/version/Drcom.xml";
                XDocument oXDoc = XDocument.Load(versionxml);
                XElement root = oXDoc.Root;
                XElement lastversion = root.Element("version");
                XElement data = root.Element("data");

                string[] versiondata = new string[3] { version, lastversion.Value, data.Value };

                return versiondata;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
