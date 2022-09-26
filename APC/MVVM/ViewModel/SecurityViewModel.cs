using APC.Command;
using APC.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.IO;
using APC.Properties;
using System.Runtime.InteropServices;

namespace APC.MVVM.ViewModel
{
    public class SecurityViewModel : BaseViewModel
    {
        private Window currentWindow;
        private Window changePassWindow;
        private bool isChangePassword = false;
        public SecurityViewModel()
        {
            LoadWindows();
        }

        public SecurityViewModel(bool isChange)
        {
            isChangePassword = isChange;
            LoadWindows();
        }

        private void LoadWindows()
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item is SecurityWindow)
                {
                    currentWindow = item;
                }
                if (item is ChangeSecurity)
                {
                    changePassWindow = item;
                }
            }
        }

        private string _passHelper;

        public string PassHelper
        {
            get
            {
                if (string.IsNullOrEmpty(_passHelper) || string.IsNullOrWhiteSpace(_passHelper))
                {
                    _passHelper = Settings.Default.PassHelp;
                }
                return _passHelper;
            }
            set
            {
                _passHelper = value;
                OnPropertyChanged("PassHelper");
            }
        }


        private RelayCommand _checkPassCommand;
        public RelayCommand CheckPassCommand
        {
            get
            {
                return _checkPassCommand ?? (_checkPassCommand = new RelayCommand(obj =>
                {

                    byte[] entropy = Convert.FromBase64String(Settings.Default.Salt);
                    byte[] protectedHash = File.ReadAllBytes(Environment.CurrentDirectory + "/blob");
                    byte[] unprotectedHash = ProtectedData.Unprotect(protectedHash, null, DataProtectionScope.CurrentUser);
                    byte[] passwordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(((PasswordBox)obj).Password));
                   
                    if (passwordHash.SequenceEqual(unprotectedHash)==true)
                    {
                        if (isChangePassword == false)
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                        }
                        else
                        {
                            ChangeSecurity changeSecurity = new ChangeSecurity();
                            changeSecurity.Show();
                        }
                        ((SecurityWindow)currentWindow).Close();
                    }
                    else
                    {
                        MessageBox.Show("Не верный пароль!");
                    }

                    entropy = null;
                    protectedHash = null;
                    unprotectedHash = null;
                    passwordHash = null;
                }));
            }
        }

        private RelayCommand _changePasswordCommand;
        public RelayCommand ChangePasswordCommand
        {
            get
            {
                return _changePasswordCommand ?? (_changePasswordCommand = new RelayCommand(obj =>
                {
                    byte[] entropy = new byte[256];
                    RNGCryptoServiceProvider.Create().GetBytes(entropy);

                    Settings.Default.IsFirstLaunch = false;
                    Settings.Default.PassHelp = PassHelper;
                    Settings.Default.Salt = Convert.ToBase64String(entropy);
                    Settings.Default.Save();

                    using (FileStream fs = new FileStream("blob", FileMode.OpenOrCreate))
                    {   
                        byte[] passwordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(((TextBox)obj).Text));
                        byte[] protectedHash = ProtectedData.Protect(passwordHash, null, DataProtectionScope.CurrentUser);
                        for (int i = 0; i < protectedHash.Length; i++)
                        {
                            fs.WriteByte(protectedHash[i]);
                        }
                    }

                    MessageBox.Show("Пароль успешно установлен");
                    if (isChangePassword == false)
                    {
                        MainWindow workspace = new MainWindow();
                        workspace.Show();
                    }
                    ((ChangeSecurity)changePassWindow).Close();
                }));
            }
        }

    }
}
