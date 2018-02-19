using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Drcom.net
{
    public class NewApp
    {
        public static bool IsNew()
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            string versionxml = "http://md.xxwhite.com/version/Drcom.xml";
            XDocument oXDoc = XDocument.Load(versionxml);
            XElement root = oXDoc.Root;
            XElement lastversion = root.Element("version");

            return (version == lastversion.Value);

        }
    }
}
