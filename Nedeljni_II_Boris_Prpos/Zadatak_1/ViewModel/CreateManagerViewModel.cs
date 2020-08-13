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
    class CreateManagerViewModel : ViewModelBase
    {
        CreateManager createMan;
        Entity context = new Entity();
        Methods methods = new Methods();

        public CreateManagerViewModel(CreateManager managerOpen)
        {
            createMan = managerOpen;
            User = new tblUser();
            Manager = new tblManager();
            
        }

        private tblUser user;

        public tblUser User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private tblManager manager;

        public tblManager Manager
        {
            get { return manager; }
            set { manager = value;
                OnPropertyChanged("Manager");
            }
        }


        private string first;

        public string First
        {
            get { return first; }
            set
            {
                first = value;
                OnPropertyChanged("First");
            }
        }

        private string second;

        public string Second
        {
            get { return second; }
            set
            {
                second = value;
                OnPropertyChanged("Second");
            }
        }
        private bool update;

        public bool Update
        {
            get { return update; }
            set { update = value; }
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
            createMan.Close();
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

                    tblUser newUser = new tblUser();
                    newUser.Birthdate = User.Birthdate;
                    newUser.FullName = First + " " + Second;
                    newUser.Citizenship = User.Citizenship;
                    newUser.IdCard = User.IdCard;
                    newUser.Username = User.Username;
                    newUser.Pasword = User.Pasword;
                    newUser.Gender = User.Gender;
                    newUser.Manager = false;
                    tblManager newManager = new tblManager();
                    newManager.HospitalLevel = Manager.HospitalLevel;
                    newManager.MaxDoctors = Manager.MaxDoctors;
                    newManager.Erors = Manager.Erors;
                    if (newManager.Erors>5)
                    {
                        newManager.MinRooms = 0;
                        MessageBox.Show("Error count is bigger than 5, no room can be monitored\nRoom count will be set to zero");
                    }
                    else
                    {
                        newManager.MinRooms = Manager.MinRooms;
                    }

                    if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == false)
                    {
                        MessageBox.Show("Username,password and IdCard number must be unique");
                    }
                    else if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == true && methods.ValidateGender(newUser.Gender) == true && methods.ValidatePassword(newUser.Pasword) == true && newUser.Birthdate < DateTime.Now.AddYears(-18))
                    {
                        context.tblUsers.Add(newUser);
                        newManager.UserID = newUser.UserId;
                        context.tblManagers.Add(newManager);
                        context.SaveChanges();
                        MessageBox.Show("Manager is saved");
                        User = new tblUser();
                        Manager = new tblManager();
                        First = "";
                        Second = "";
                        Update = true;
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
                        MessageBox.Show("Manager must be at least 18 years old");
                    }
                    else
                    {
                        MessageBox.Show("Wrong input");
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
            if (String.IsNullOrEmpty(First) || String.IsNullOrEmpty(Second) || String.IsNullOrEmpty(User.Citizenship) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.IdCard) || User.Birthdate == null || String.IsNullOrEmpty(Manager.Erors.ToString()) || String.IsNullOrEmpty(Manager.MinRooms.ToString()) || String.IsNullOrEmpty(Manager.MaxDoctors.ToString()) || String.IsNullOrEmpty(Manager.HospitalLevel.ToString()))
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
