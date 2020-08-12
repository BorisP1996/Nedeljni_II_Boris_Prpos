using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AdminViewModel : ViewModelBase
    {
        Admin admin;

        public AdminViewModel(Admin adminOpen)
        {
            admin = adminOpen;
        }

        #region Command
        private ICommand manager;
        public ICommand Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new RelayCommand(param => ManagerExecute(), param => CanManagerExecute());
                }
                return manager;
            }
        }

        private bool CanManagerExecute()
        {
            return true;
        }

        private void ManagerExecute()
        {
            try
            {
                CreateManager cm = new CreateManager();
                cm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private ICommand maintance;
        public ICommand Maintance
        {
            get
            {
                if (maintance == null)
                {
                    maintance = new RelayCommand(param => MaintanceExecute(), param => CanMaintanceExecute());
                }
                return maintance;
            }
        }

        private bool CanMaintanceExecute()
        {
            return true;
        }

        private void MaintanceExecute()
        {
            try
            {
                CreateMaintance cm = new CreateMaintance();
                cm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private ICommand doctor;
        public ICommand Doctor
        {
            get
            {
                if (doctor == null)
                {
                    doctor = new RelayCommand(param => DoctorExecute(), param => CanDoctorExecute());
                }
                return doctor;
            }
        }

        private bool CanDoctorExecute()
        {
            return true;
        }

        private void DoctorExecute()
        {
            try
            {
                CreateDoctor createDoc = new CreateDoctor();
                createDoc.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }   
        private ICommand patient;
        public ICommand Patient
        {
            get
            {
                if (patient == null)
                {
                    patient = new RelayCommand(param => PatientExecute(), param => CanPatientExecute());
                }
                return patient;
            }
        }

        private bool CanPatientExecute()
        {
            return true;
        }

        private void PatientExecute()
        {
            try
            {
                CreatePatient createPat = new CreatePatient();
                createPat.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
       
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
            admin.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
