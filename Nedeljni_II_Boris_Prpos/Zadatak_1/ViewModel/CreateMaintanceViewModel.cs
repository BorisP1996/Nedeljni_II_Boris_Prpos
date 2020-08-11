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
    class CreateMaintanceViewModel : ViewModelBase
    {
        CreateMaintance createMan;
        Entity context = new Entity();
        Methods methods = new Methods();

        public CreateMaintanceViewModel(CreateMaintance manOpen)
        {
            createMan = manOpen;
            User = new tblUser();
            Maintance = new tblMaintance();
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

        private tblMaintance maintance;

        public tblMaintance Maintance
        {
            get { return maintance; }
            set { maintance = value;
                OnPropertyChanged("Maintance");
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
                    tblMaintance newMaintance = new tblMaintance();
                    newMaintance.InvalidDuty = Maintance.InvalidDuty;
                    if (newMaintance.InvalidDuty==true)
                    {
                        newMaintance.AmbulanceDuty = false;
                    }
                    else
                    {
                        newMaintance.AmbulanceDuty = true;
                    }
                    newMaintance.GrowPermision = Maintance.GrowPermision;
                    if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == false)
                    {
                        MessageBox.Show("Username,password and IdCard number must be unique");
                    }
                    else if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == true && methods.ValidateGender(newUser.Gender) == true && methods.ValidatePassword(newUser.Pasword) == true && newUser.Birthdate < DateTime.Now.AddYears(-18))
                    {
                        context.tblUsers.Add(newUser);
                        newMaintance.UserID = newUser.UserId;
                        context.tblMaintances.Add(newMaintance);
                        context.SaveChanges();
                        MessageBox.Show("Maintance is saved");
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
                        MessageBox.Show("Maintance must be at least 18 years old");
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
            if (String.IsNullOrEmpty(First) || String.IsNullOrEmpty(Second) || String.IsNullOrEmpty(User.Citizenship) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.IdCard) || User.Birthdate == null)
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
