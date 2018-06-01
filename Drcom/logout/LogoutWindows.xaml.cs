using System;
using System.Threading;
using System.Windows.Forms;
using Drcom.net;
using mshtml;

namespace Drcom.logout
{
    /// <summary>
    /// LogoutWindows.xaml 的交互逻辑
    /// </summary>
    public partial class LogoutWindows
    {
        public LogoutWindows()
        {
            InitializeComponent();

            nav_login.Navigate(new Uri("http://119.39.119.134:8089/Self/nav_login"));
        }

        private void acc(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                IHTMLDocument2 doc2 = (IHTMLDocument2)nav_login.Document;

                if (Setting.GetSetting("uid") != null && Setting.GetSetting("uid") != "")
                {
                    //获取元素
                    IHTMLElement account = (IHTMLElement)doc2.all.item("account", 0);
                    IHTMLElement password = (IHTMLElement)doc2.all.item("pass", 0);
                    IHTMLElement submit = (IHTMLElement)doc2.all.item("Submit", 0);

                    //填充表单
                    var uid = Setting.GetSetting("uid");
                    var pwd = Setting.GetSetting("pwd");
                    account.setAttribute("value", uid);
                    password.setAttribute("value", pwd);
                    submit.click();

                    Application.DoEvents();
                    Thread.Sleep(500);

                    nav_login.Navigate(new Uri("http://119.39.119.134:8089/Self/nav_offLine"));
                }
                
            }
            catch (Exception){}
        }
    }

}
