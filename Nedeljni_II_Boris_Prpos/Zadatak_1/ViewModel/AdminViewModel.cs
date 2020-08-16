using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;
using Zadatak_1.Model;
using Zadatak_1.Tools;

namespace Zadatak_1.ViewModel
{
    class AdminViewModel : ViewModelBase
    {
        Admin admin;
        GetLists listGetter = new GetLists();
        Entity context = new Entity();
        Deleting deleteing = new Deleting();

        public AdminViewModel(Admin adminOpen)
        {
            admin = adminOpen;
            DoctorList = listGetter.GetDoctors();
            MaintanceList = listGetter.GetMaintance();
            ManagerList = listGetter.GetManagers();
            PatientList = listGetter.GetPatients();
            HospitalList = listGetter.GetHospitals();
            Doctorvw = new vwDoctor();
            Maintancevw = new vwMaintance();
            Patientvw = new vwPatient();
            Managervw = new vwManager();
            Hospital = new tblHospital();
        }

        #region Properties

        private vwDoctor doctorvw;

        public vwDoctor Doctorvw
        {
            get { return doctorvw; }
            set { doctorvw = value;
                OnPropertyChanged("Doctorvw");
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

        private vwMaintance maintancevw;

        public vwMaintance Maintancevw
        {
            get { return maintancevw; }
            set { maintancevw = value;
                OnPropertyChanged("Maintancevw");
            }
        }

        private List<vwMaintance> maintanceList     ;

        public List<vwMaintance> MaintanceList
        {
            get { return maintanceList; }
            set { maintanceList = value;
                OnPropertyChanged("MaintanceList");
            }
        }

        private vwPatient patientvw;

        public vwPatient Patientvw
        {
            get { return patientvw; }
            set { patientvw = value;
                OnPropertyChanged("Patientvw");
            }
        }
        private List<vwPatient> patientList;

        public List<vwPatient> PatientList
        {
            get { return patientList; }
            set { patientList = value;
                OnPropertyChanged("PatientList");
            }
        }

        private vwManager managervw;

        public vwManager Managervw
        {
            get { return managervw; }
            set { managervw = value;
                OnPropertyChanged("Managervw");
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

        private tblHospital hospital;

        public tblHospital Hospital
        {
            get { return hospital; }
            set { hospital = value;
                OnPropertyChanged("Hospital");
            }
        }

        private List<tblHospital> hospitalList;

        public List<tblHospital> HospitalList
        {
            get { return hospitalList; }
            set { hospitalList = value;
                OnPropertyChanged("HospitalList");
            }
        }

        #endregion
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
                if ((cm.DataContext as CreateManagerViewModel).Update==true)
                {
                    ManagerList = listGetter.GetManagers();
                }
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
                if ((cm.DataContext as CreateMaintanceViewModel).Update==true)
                {
                    MaintanceList = listGetter.GetMaintance();
                }
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
                if ((createDoc.DataContext as CreateDoctorViewModel).Update==true)
                {
                    DoctorList = listGetter.GetDoctors();
                }
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
                if ((createPat.DataContext as CreatePatientViewModel).Update==true)
                {
                    PatientList = listGetter.GetPatients();
                }
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

        /// <summary>
        /// Command for deleting each and everyone from user table
        /// Foreign key is taken in consideration when deleting different type odf users
        /// manager is referenced by many doctors
        /// doctor is referenced by manu patients
        /// everyone is referenced in tbl user
        /// </summary>
        #region DeleteComands
        private ICommand deleteMan;
        public ICommand DeleteMan
        {
            get
            {
                if (deleteMan==null)
                {
                    deleteMan = new RelayCommand(param => DeleteManExecute(), param => CanDeleteManExecute());
                }
                return deleteMan;
            }
        }

        private void DeleteManExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete manager?\nYou also need to delete doctors which is monitored by this manager, and all patients monitored by that doctor.", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    tblManager viaManager = (from r in context.tblManagers where r.ManagerID == Managervw.ManagerID select r).FirstOrDefault();
                    int managerID = viaManager.ManagerID;

                    

                    deleteing.DeleteDocForManager(managerID);
                    
                    deleteing.DeleteManager(managerID);

                    context.SaveChanges();
                    ManagerList = listGetter.GetManagers();
                    DoctorList = listGetter.GetDoctors();
                    PatientList = listGetter.GetPatients();

                    MessageBox.Show("Manager is deleted");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDeleteManExecute()
        {
            if (Managervw!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICommand deleteDoc;
        public ICommand DeleteDoc
        {
            get
            {
                if (deleteDoc==null)
                {
                    deleteDoc = new RelayCommand(param => DeleteDocExecute(), param => CanDeleteDocExecute());
                }
                return deleteDoc;
            }
        }

        private bool CanDeleteDocExecute()
        {
            if (Doctorvw!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteDocExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete doctor?\nYou also need to delete patients which are monitored by this doctor.", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    tblDoctor viaDoctor = (from r in context.tblDoctors where r.DoctorID == Doctorvw.DoctorID select r).FirstOrDefault();
                    int docID = viaDoctor.DoctorID;

                    deleteing.DeletePatientForDoctor(docID);

                    deleteing.DeleteDoctor(docID);
                    context.SaveChanges();
                    MessageBox.Show("Doctor is deleted");
                    DoctorList = listGetter.GetDoctors();
                    PatientList = listGetter.GetPatients();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand deletePat;
        public ICommand DeletePat
        {
            get
            {
                if (deletePat==null)
                {
                    deletePat = new RelayCommand(param => CanDeletePat(), param => CanDeletePatExecute());
                }
                return deletePat;
            }
        }

        private bool CanDeletePatExecute()
        {
            if (Patientvw!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CanDeletePat()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete patient?", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    tblPatient viaPatient = (from r in context.tblPatients where r.PatientID == Patientvw.PatientID select r).FirstOrDefault();
                    int patientID = viaPatient.PatientID;

                    context.tblPatients.Remove(viaPatient);

                    tblUser viaUser = (from r in context.tblUsers where r.UserId == Patientvw.UserId select r).FirstOrDefault();
                    context.tblUsers.Remove(viaUser);
                    

                    context.SaveChanges();
                    MessageBox.Show("Patient is deleted");
                    
                    PatientList = listGetter.GetPatients();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand deleteMaint;
        public ICommand DeleteMaint
        {
            get
            {
                if (deleteMaint==null)
                {
                    deleteMaint = new RelayCommand(param => DeleteMaintExecute(), param => CanDeleteMaintExecute());
                }
                return deleteMaint;
            }
        }

        private bool CanDeleteMaintExecute()
        {
            if (Maintancevw!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteMaintExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete maintance?", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    tblMaintance viaMaintace = (from r in context.tblMaintances where r.UserID == Maintancevw.UserId select r).FirstOrDefault();

                    context.tblMaintances.Remove(viaMaintace);

                    tblUser viaUser = (from r in context.tblUsers where r.UserId == Maintancevw.UserId select r).FirstOrDefault();
                    context.tblUsers.Remove(viaUser);

                    context.SaveChanges();
                  
                    MessageBox.Show("Maintance is deleted");
                    MaintanceList = listGetter.GetMaintance();
                }
            }
            catch (Exception ec)
            {

                MessageBox.Show(ec.ToString());
            }
        }
        #endregion

    }
}
