using System.Diagnostics;
using System.Windows;

using Drcom.net;

namespace Drcom
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Settings();
        }

        //初始化配置
        private void Settings()
        {
            if (Setting.GetSetting("nip") == "" || Setting.GetSetting("nip") == null)
            {
                GetIp.IsChecked = true;
            }
            else
            {
                TextIp.Text = Setting.GetSetting("nip");
            }
            if (Setting.GetSetting("uid") != "")
            {
                SaveUid.IsChecked = true;
                TextUid.Text = Setting.GetSetting("uid");
            }
            if (Setting.GetSetting("pwd") != "")
            {
                SavePwd.IsChecked = true;
                TextPwd.Password = Setting.GetSetting("pwd");
            }
        }

        //登陆按钮
        private void LoginNet(object sender, RoutedEventArgs e)
        {
            //获取账号密码ip
            string nip = TextIp.Text;
            string uid = TextUid.Text;
            string pwd = TextPwd.Password;

            //获取登陆IP
            if (GetIp.IsChecked == true)
            {
                TextIp.Text = CsuNet.LoginIP();
                nip = TextIp.Text;
            }
            //保存IP，账号和密码
            if (TextIp.Text != null & TextIp.Text != "")
            {
                Setting.UpdateSetting("nip", nip);
            }
            if (SaveUid.IsChecked == true)
            {
                Setting.UpdateSetting("uid", uid);
            }
            if (SavePwd.IsChecked == true)
            {
                Setting.UpdateSetting("pwd", pwd);
            }

            var relust = CsuNet.LoginCsuNet(nip, uid, pwd);

            MessageBox.Show(relust);
        }

        //注销按钮
        private void LogoutNet(object sender, RoutedEventArgs e)
        {
            string nip = TextIp.Text;

            var relust = CsuNet.LogoutCsuNet(nip);

            MessageBox.Show(relust);
        }

        //关于软件
        private void About(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("http://www.xxwhite.com/2018/Drcom.html");
        }
        //检查更新
        private void FindNew(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string[] version = NewApp.IsNew();
            MessageBox.Show("当前版本为：" + version[0] + "\r\n" +
                            "最新版本为：" + version[1] + "\r\n" +
                            "最后更新时间为：" + version[2]);
            if (version[0] != version[1])
            {
                Process.Start("https://gitee.com/monologuechi/Drcom/releases");
            }
        }
    }
}
