using System;
using System.Collections.Generic;
using Zadatak_1.View;
using Zadatak_1.Model;
using Zadatak_1.Tools;
using Zadatak_1.Command;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Type of user patient is created in this class
    /// </summary>
    class CreatePatientViewModel : ViewModelBase
    {
        CreatePatient createPat;
        Entity context = new Entity();
        Methods methods = new Methods();
        GetLists getLists = new GetLists();

        public CreatePatientViewModel(CreatePatient patOpen)
        {
            createPat = patOpen;
            DoctorList = getLists.GetDoctors();
            Doctor = new vwDoctor();
            Patient = new tblPatient();
            User = new tblUser();
        }

        private vwDoctor doctor;

        public vwDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private List<vwDoctor> doctorList;

        public List<vwDoctor> DoctorList
        {
            get { return doctorList; }
            set { doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

        private tblPatient patient;

        public tblPatient Patient
        {
            get { return patient; }
            set { patient = value;
                OnPropertyChanged("Patient");
            }
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
            createPat.Close();
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
                    newUser.CreatedClinic = false;
                    tblPatient newPatient = new tblPatient();
                    newPatient.CardNumber = Patient.CardNumber;
                    newPatient.DateExpire = Patient.DateExpire;
                    
                    newPatient.DoctorID = Doctor.DoctorID;

                    if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == false)
                    {
                        MessageBox.Show("Username,password and IdCard number must be unique");
                    }
                    else if (methods.ValidateAdmin(newUser.Username, newUser.Pasword, newUser.IdCard) == true && methods.ValidateGender(newUser.Gender) == true && methods.ValidatePassword(newUser.Pasword) == true && Patient.DateExpire > DateTime.Now && methods.PatientUniqueNumber(Patient.CardNumber) == true)
                    {
                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        newPatient.UserID = newUser.UserId;
                        context.tblPatients.Add(newPatient);
                        context.SaveChanges();
                        MessageBox.Show("Patient is saved");
                        User = new tblUser();
                        First = "";
                        Second = "";
                        Patient = new tblPatient();
                        Update = true;
                    }
                    else if (Patient.DateExpire < DateTime.Now)
                    {
                        MessageBox.Show("Health insurance expire date can not be in past.");
                    }
                    else if (methods.PatientUniqueNumber(Patient.CardNumber) == false)
                    {
                        MessageBox.Show("Insurance health card number must be unique");
                    }
                    else if (methods.ValidateGender(newUser.Gender) == false)
                    {
                        MessageBox.Show("Gender can be only \"M\" or \"Z\"");

                    }
                    else if (methods.ValidatePassword(newUser.Pasword) == false)
                    {
                        MessageBox.Show("Password must contain:\n8 characters\n1 number\n1 upper letter\n1 small letter\n1 special character");
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
            if (String.IsNullOrEmpty(First) || String.IsNullOrEmpty(Second) || String.IsNullOrEmpty(User.Citizenship) || String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Pasword) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.IdCard) || User.Birthdate == null || String.IsNullOrEmpty(Patient.CardNumber) || String.IsNullOrEmpty(Doctor.UniqueNumber))
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
