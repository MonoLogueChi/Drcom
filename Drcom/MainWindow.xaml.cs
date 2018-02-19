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
            if (Seeting.GetSetting("nip") != "")
            {
                SaveIp.IsChecked = true;
                TextIp.Text = Seeting.GetSetting("nip");
            }
            if (Seeting.GetSetting("uid") != "")
            {
                SaveUid.IsChecked = true;
                TextUid.Text = Seeting.GetSetting("uid");
            }
            if (Seeting.GetSetting("pwd") != "")
            {
                SavePwd.IsChecked = true;
                TextPwd.Password = Seeting.GetSetting("pwd");
            }
        }

        //登陆按钮
        private void LoginNet(object sender, RoutedEventArgs e)
        {
            //获取账号密码ip
            string nip = TextIp.Text;
            string uid = TextUid.Text;
            string pwd = TextPwd.Password;

            //保存ip账号密码
            if (SaveIp.IsChecked == true)
            {
                Seeting.UpdateSetting("nip", nip);
            }
            if (SaveUid.IsChecked == true)
            {
                Seeting.UpdateSetting("uid", uid);
            }
            if (SavePwd.IsChecked == true)
            {
                Seeting.UpdateSetting("pwd", pwd);
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
            Process.Start("http://url.xxwhite.com?id=5a884de19f5454543ef4201e");
        }
        //检查更新
        private void FindNew(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NewApp.IsNew())
            {
                MessageBox.Show("已经是最新版本");
            }
            else
            {
                Process.Start("https://github.com/MonoLogueChi/Drcom/releases");
            }
        }
    }
}
