using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;
using Zadatak_1.Model;
using Zadatak_1.Tools;
using Zadatak_1.Command;
using Zadatak_1.View;
using System.Windows.Input;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class CreateClinicViewModel : ViewModelBase
    {
        CreateClinic createClinic;

        public CreateClinicViewModel(CreateClinic clinicOpen,string username)
        {
            createClinic = clinicOpen;
            Username = username;
            Hospital = new tblHospital();
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        private tblHospital hospital;

        public tblHospital Hospital
        {
            get { return hospital; }
            set { hospital = value;
                OnPropertyChanged("Hospital");
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
            createClinic.Close();
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

                    tblHospital newHospital = new tblHospital();
                    newHospital.Adress = Hospital.Adress;
                    newHospital.Owns = Hospital.Owns;
                    newHospital.Levels = Hospital.Levels;
                    newHospital.InvalidPoint = Hospital.InvalidPoint;
                    newHospital.AmbulancePoint = Hospital.AmbulancePoint;
                    newHospital.HospitalName = Hospital.HospitalName;
                    newHospital.StartDate = Hospital.StartDate;
                    newHospital.Flors = Hospital.Flors;
                    if (Hospital.Yard==true)
                    {
                        newHospital.Yard = true;
                    }
                    else
                    {
                        newHospital.Yard = false;
                    }
                    if (Hospital.Balcony==true)
                    {
                        newHospital.Balcony = true;
                    }
                    else
                    {
                        newHospital.Balcony = false;
                    }
                    if (newHospital.StartDate > DateTime.Now)
                    {
                        MessageBox.Show("Clinic establishment date can not be bigger than present date");
                    }
                    else
                    {
                        context.tblHospitals.Add(newHospital);
                        context.SaveChanges();
                        MessageBox.Show("Clinic is created");
                        Hospital = new tblHospital();

                        //set created clinic = true to the user who created the clinic
                        tblUser viaUser = (from r in context.tblUsers where r.Username == Username select r).FirstOrDefault();
                        viaUser.CreatedClinic = true;
                        context.SaveChanges();

                        createClinic.Close();
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
            if (String.IsNullOrEmpty(Hospital.HospitalName) || String.IsNullOrEmpty(Hospital.Owns) || String.IsNullOrEmpty(Hospital.Adress) || String.IsNullOrEmpty(Hospital.Levels.ToString()) || String.IsNullOrEmpty(Hospital.Flors.ToString()) || String.IsNullOrEmpty(Hospital.AmbulancePoint.ToString()) || String.IsNullOrEmpty(Hospital.InvalidPoint.ToString()))
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
