using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;
using Zadatak_1.View;
using Zadatak_1.Tools;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        Entity context = new Entity();
        Methods methods = new Methods();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

#endregion

        #region Commands
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        private void LoginExecute()
        {
            try
            {
                if (methods.CompareFileCredentials(Username,Password)==true)
                {
                    MessageBox.Show("Welcome master account");
                    MasterView master = new MasterView();
                    master.ShowDialog();
                }
                else if (methods.DeterminLoger(Username,Password)==1)
                {
                    MessageBox.Show("Welcome admin");
                    tblUser viaUser = (from r in context.tblUsers where r.Username == Username && r.Pasword == Password select r).FirstOrDefault();
                    // if database filed "create clinic" in tblUser is false or null=>open window for creating clinic and than open admin window
                    if (viaUser.CreatedClinic!=true)
                    {
                        CreateClinic createClinic = new CreateClinic(Username);
                        createClinic.ShowDialog();
                        Admin admin = new Admin();
                        admin.ShowDialog();
                        viaUser.CreatedClinic = true;
                    }
                    //if admin already created clinic, just open admin window
                    else
                    {
                        Admin admin = new Admin();
                        admin.ShowDialog();
                    }                  
                }
                else if (methods.DeterminLoger(Username, Password) == 2)
                {
                    MessageBox.Show("Welcome doctor");
                }
                else if (methods.DeterminLoger(Username,Password)==3)
                {
                    MessageBox.Show("Welcome maintance");
                }
                else if (methods.DeterminLoger(Username,Password)==4)
                {
                    MessageBox.Show("Welcome manager");
                }
                else if (methods.DeterminLoger(Username,Password)==5)
                {
                    MessageBox.Show("Welcome patient");
                }
                else
                {
                    MessageBox.Show("Invalid parametres");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand createPat;
        public ICommand CreatePat
        {
            get
            {
                if (createPat == null)
                {
                    createPat = new RelayCommand(param => CreatePatExecute(), param => CanCreatePatExecute());
                }
                return createPat;
            }
        }
        private void CreatePatExecute()
        {
            try
            {
                CreatePatient createPat = new CreatePatient();
                createPat.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private bool CanCreatePatExecute()
        {         
                return true;      
        }
        #endregion
    }
}
