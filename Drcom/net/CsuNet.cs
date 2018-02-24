using System;
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
                try
                {
                    string content = "DDDDD=" + uid + "@zndx&upass=" + pwd + "&0MKKey= +Login";
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
                    using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
                    {
                        result = reader.ReadToEnd();
                    }
                    if (result.Contains("您已经成功登录"))
                    {
                        return "您已成功登陆";
                    }
                    else
                    {
                        return "请检查账号密码及IP是否正确\r\n并确认为未登录状态";
                    }
                    
                }
                catch (Exception e)
                {
                    return "发生未知错误";
                }
            }
        }

        //注销
        public static string LogoutCsuNet(string nip)
        {
            try
            {
                string gethost = "http://" + nip + "/F.htm";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(gethost);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
                {
                    var result = reader.ReadToEnd();
                    return "我也不知道有没有注销成功\r\n下个版本尝试解决";
                }
            }
            catch (Exception e)
            {
                return "发生未知错误";
            }
            
        }
    }
}
