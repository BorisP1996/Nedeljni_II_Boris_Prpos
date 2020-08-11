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
    class MasterViewModel : ViewModelBase
    {
        Entity context = new Entity();
        MasterView masterView;

        public MasterViewModel(MasterView masterOpen)
        {
            masterView = masterOpen;
        }

        private ICommand createAdmin;
        public ICommand CreateAdmin
        {
            get
            {
                if (createAdmin==null)
                {
                    createAdmin = new RelayCommand(param => CreateAdminExecute(), param => CanCreateAdminExecute());
                }
                return createAdmin;
            }
        }

        private void CreateAdminExecute()
        {
            try
            {
                CreateAdmin createAdmin = new CreateAdmin();
                createAdmin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCreateAdminExecute()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
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
            masterView.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        private ICommand changeCredentials;
        public ICommand ChangeCredentials
        {
            get
            {
                if (changeCredentials==null)
                {
                    changeCredentials = new RelayCommand(param => ChangeCredentialsExecute(), param => CanChangeCredentialsExecute());
                }
                return changeCredentials;
            }
        }

        private bool CanChangeCredentialsExecute()
        {
            return true;
        }

        private void ChangeCredentialsExecute()
        {
            try
            {
                ChangeCredentials changeCred = new ChangeCredentials();
                changeCred.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
