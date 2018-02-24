using System;
using System.Configuration;

namespace Drcom.net
{
    public class Setting
    {
        //获取设置
        public static string GetSetting(string key)
        {
            try
            {
                string value = ConfigurationManager.AppSettings[key].ToString();
                return value;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //写入设置
        public static void UpdateSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings.Remove(key);
            }
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }

}
