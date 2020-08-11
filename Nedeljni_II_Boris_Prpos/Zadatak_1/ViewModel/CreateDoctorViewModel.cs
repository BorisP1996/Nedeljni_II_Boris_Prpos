using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.View;
using Zadatak_1.Tools;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class CreateDoctorViewModel : ViewModelBase
    {
        CreateDoctor createDoc;
        Entity context = new Entity();
        Methods methods = new Methods();
        GetLists getList = new GetLists();

        public CreateDoctorViewModel(CreateDoctor docOpen)
        {
            createDoc = docOpen;
            User = new tblUser();
            Doctor = new tblDoctor();
            Manager = new vwManager();
            Shift = new tblShift();
            ShiftList = getList.GetShifts();
            ManagerList = getList.GetManagers();
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

        private tblDoctor doctor;

        public tblDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private tblShift shift;

        public tblShift Shift
        {
            get { return shift; }
            set { shift = value;
                OnPropertyChanged("Shift");
            }
        }
        private List<tblShift> shiftList;

        public List<tblShift> ShiftList
        {
            get { return shiftList; }
            set { shiftList = value;
                OnPropertyChanged("ShiftList");
            }
        }

        private vwManager manager;

        public vwManager Manager
        {
            get { return manager; }
            set { manager = value;
                OnPropertyChanged("Manager");
            }
        }
        private List<vwManager> managerList;

        public List<vwManager> ManagerList
        {
            get { return managerList; }
            set { managerList = value;
                OnPropertyChanged("ManagerList");
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
            createDoc.Close();
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
                    tblDoctor newDoctor = new tblDoctor();
                    newDoctor.Department = Doctor.Department;
                    newDoctor.ManagerID = Manager.ManagerID;
                    newDoctor.ShiftID = Shift.ShiftID;
                    if (Doctor.Reception==null || Doctor.Reception==false)
                    {
                        newDoctor.Reception = false;
                    }
                    else
                    {
                        newDoctor.Reception = true;
                    }
                    newDoctor.UniqueNumber = Doctor.UniqueNumber;
                    newDoctor.AccountNumber = Doctor.AccountNumber;
                    if (methods.UniqueDoctorNumber(newDoctor.UniqueNumber)==false)
                    {
                        MessageBox.Show("Unique number must be unique and 5 characters long");
                    }
                    if (methods.UniqueDoctorAccount(newDoctor.AccountNumber)==false)
                    {
                        MessageBox.Show("Account number must be unique");
                    }
                    if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == false)
                    {
                        MessageBox.Show("Username,password and IdCard number must be unique");
                    }
                    else if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == true && methods.ValidateGender(newUser.Gender) == true && methods.ValidatePassword(newUser.Pasword) == true && newUser.Birthdate < DateTime.Now.AddYears(-18) && methods.UniqueDoctorAccount(newDoctor.AccountNumber)==true && methods.UniqueDoctorNumber(newDoctor.UniqueNumber)==true)
                    {
                        context.tblUsers.Add(newUser);
                        newDoctor.UserID = newUser.UserId;
                        context.tblDoctors.Add(newDoctor);
                        context.SaveChanges();
                        MessageBox.Show("Doctor is saved");
                        User = new tblUser();
                        Doctor = new tblDoctor();
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
                        MessageBox.Show("Doctor must be at least 18 years old");
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
            if (String.IsNullOrEmpty(First) || String.IsNullOrEmpty(Second) || String.IsNullOrEmpty(User.Citizenship) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.IdCard) || User.Birthdate == null || String.IsNullOrEmpty(Doctor.UniqueNumber) || String.IsNullOrEmpty(Doctor.AccountNumber) || String.IsNullOrEmpty(Doctor.Department) || String.IsNullOrEmpty(Manager.FullName) || String.IsNullOrEmpty(Shift.ShiftName))
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
