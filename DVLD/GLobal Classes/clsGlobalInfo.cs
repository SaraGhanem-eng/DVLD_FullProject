using DVLD.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVLD.GLobal_Classes
{
    public class clsGlobalInfo
    {
        public static clsUsers CurrentUser { get; set; }


        static public readonly string _RememberMeFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DVLD", "LoginInfo.txt");


        static public void LoadRememberMeInfo(ref string UserName , ref string Password , ref bool IsRememberMeChecked)
        {
            if (!File.Exists(_RememberMeFilePath))
                return;
            // thre return will maintain the default values .

            string[] UserInfo = File.ReadAllLines(_RememberMeFilePath);

            if (UserInfo.Length != 2)
                return;

            UserName = UserInfo[0];
            Password= UserInfo[1];
            IsRememberMeChecked = true;
        }

        static public void SaveRememberMeInfo(string UserName , string Password )
        {
            Directory.CreateDirectory(
                Path.GetDirectoryName(_RememberMeFilePath));

            File.WriteAllLines(_RememberMeFilePath, new string[]
            {
              UserName,
              Password
            });
        }

        static public void ClearRememberMeInfo()
        {
            if (File.Exists(_RememberMeFilePath))
            {
                File.Delete(_RememberMeFilePath);
            }
        }


    }
}
