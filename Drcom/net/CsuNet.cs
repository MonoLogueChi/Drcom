using System.IO;
using System.Net;
using System.Text;


namespace Drcom.net
{
    public class CsuNet
    {
        //登陆
        public static string LoginCsuNet(string nip, string uid, string pwd)
        {
            if (nip == "" || uid == "" || pwd == "")
            {
                return "请检查登陆ip，账号和密码是否输入正确";
            }
            else
            {
                string content = "DDDDD=" + uid + "upass=" + pwd + "0MKKey=  Login";
                string posthost = "http://" + nip + "/";
                string result = null;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(posthost);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
        }

        //注销
        public static string LogoutCsuNet(string nip)
        {
            string gethost = "http://" + nip + "/F.htm";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(gethost);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return null;
        }
    }
}
