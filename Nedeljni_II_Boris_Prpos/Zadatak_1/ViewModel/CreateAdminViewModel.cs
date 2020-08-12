using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.Tools;
using Zadatak_1.Command;
using Zadatak_1.View;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class CreateAdminViewModel : ViewModelBase
    {
        CreateAdmin createAdmin;
        Entity context = new Entity();
        Methods methods = new Methods();
        GetLists getLists = new GetLists();

        public CreateAdminViewModel(CreateAdmin adminOpen)
        {
            createAdmin = adminOpen;
            User = new tblUser();
        }

        private tblUser user;

        public tblUser User
        {
            get { return user; }
            set { user = value;
                OnPropertyChanged("User");
            }
        }

        private string first;

        public string First
        {
            get { return first; }
            set { first = value;
                OnPropertyChanged("First");
            }
        }

        private string second;

        public string Second
        {
            get { return second; }
            set { second = value;
                OnPropertyChanged("Second");
            }
        }

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
            createAdmin.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    if (getLists.GetAdmins()>0)
                    {
                        MessageBox.Show("Admin already exists. You can not create another one.");
                    }
                    else
                    {
                        tblUser newUser = new tblUser();
                        newUser.Birthdate = User.Birthdate;
                        newUser.FullName = First + " " + Second;
                        newUser.Citizenship = User.Citizenship;
                        newUser.IdCard = User.IdCard;
                        newUser.Username = User.Username;
                        newUser.Pasword = User.Pasword;
                        newUser.Gender = User.Gender;
                        newUser.Manager = true;
                        newUser.CreatedClinic = false;
                        if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == false)
                        {
                            MessageBox.Show("Username,password and IdCard number must be unique");
                        }
                        else if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == true && methods.ValidateGender(newUser.Gender) == true && methods.ValidatePassword(newUser.Pasword) == true && newUser.Birthdate < DateTime.Now.AddYears(-18))
                        {
                            context.tblUsers.Add(newUser);
                            context.SaveChanges();
                            MessageBox.Show("Admin is saved");
                            User = new tblUser();
                            First = "";
                            Second = "";
                        }
                        else if (methods.ValidateGender(newUser.Gender) == false)
                        {
                            MessageBox.Show("Gender can be only \"M\" or \"Z\"");

                        }
                        else if (methods.ValidatePassword(newUser.Pasword) == false)
                        {
                            MessageBox.Show("Password must contain:\n8 characters\n1 number\n1 upper letter\n1 small letter\n1 special character");
                        }
                        else if (newUser.Birthdate > DateTime.Now.AddYears(-18))
                        {
                            MessageBox.Show("Admin must be at least 18 years old");
                        }
                        else
                        {
                            MessageBox.Show("Wrong input");
                        }
                    }             
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(First) || String.IsNullOrEmpty(Second) || String.IsNullOrEmpty(User.Citizenship) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.IdCard) || User.Birthdate==null )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}
