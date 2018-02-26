using System;
using System.Reflection;
using System.Xml.Linq;

namespace Drcom.net
{
    public class NewApp
    {
        public static string[] IsNew()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            try
            {
                string versionxml = "http://v.xxwhite.com/version/Drcom.xml?t=" + DateTime.Now.ToFileTimeUtc().ToString();
                XDocument oXDoc = XDocument.Load(versionxml);
                XElement root = oXDoc.Root;
                XElement lastversion = root.Element("version");
                XElement data = root.Element("data");

                string[] versiondata = new string[3] { version, lastversion.Value, data.Value };

                return versiondata;
            }
            catch (Exception e)
            {
                return (new string[3]{version, "未检测到最新版本" ,"未检测到更新时间"});
            }
        }
    }
}
