using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace Drcom.net
{
    public class CsuNet
    {
        //获取登陆IP
        public static string LoginIP()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.qq.com/");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
                {
                    string result = reader.ReadToEnd();

                    if (result.Contains("v4serip"))
                    {
                        string[] Fnip = Regex.Split(result, "v4serip='", RegexOptions.IgnoreCase);
                        string[] nip = Regex.Split(Fnip[1], "'", RegexOptions.IgnoreCase);
                        return nip[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

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
                        return LoginCaes(result);
                    }
                }
                catch (Exception e)
                {
                    return "发生未知错误\r\n请检查登陆IP是否填写正确";
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
                    string result = reader.ReadToEnd();
                    return LoginCaes(result);
                }
            }
            catch (Exception e)
            {
                return "发生未知错误";
            }

        }

        //错误代码
        public static string LoginCaes(string result)
        {
            if (result.Contains("Msg="))
            {
                string[] FMsg = Regex.Split(result, "Msg=", RegexOptions.IgnoreCase);
                int Msg = Convert.ToInt32(FMsg[1].Substring(0, 2));

                switch (Msg)
                {
                    case 0:
                        return "未知错误";
                    case 1:
                        string[] Fmsga = Regex.Split(FMsg[1], "msga=", RegexOptions.IgnoreCase);
                        string msga = Fmsga[1].Substring(1, 1);
                        if (msga != "\'")
                        {
                            return "错误代码：" + msga;
                        }
                        else
                        {
                            return "账号或密码错误";
                        }
                    case 2:
                        return "该账号正在使用中，请您与网管联系";
                    case 3:
                        return "本账号只能在指定地址使用";
                    case 4:
                        return "本账号费用超支或时长流量超过限制";
                    case 5:
                        return "本账号暂停使用";
                    case 6:
                        return "System buffer full";
                    case 8:
                        return "本账号正在使用,不能修改";
                    case 7:
                        return "未知错误";
                    case 9:
                        return "新密码与确认新密码不匹配,不能修改";
                    case 10:
                        return "密码修改成功";
                    case 11:
                        return "本账号只能在指定地址使用";
                    case 12:
                        return "未知错误";
                    case 13:
                        return "未知错误";
                    case 14:
                        return "注销成功";
                    case 15:
                        return "登录成功";
                }

                return "未知错误";
            }
            else
            {
                return "您应该大概也许可能已经成功登陆了";
            }
        }
    }
}
