using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Tools
{
    /// <summary>
    /// Class contains method that deletes different types of users, taking in consideration foreign key 
    /// ona manager can be referenced in many doctors and one doctor can be referenced in many users
    /// and each and everyone of them is referenced in table users
    /// </summary>
    class Deleting
    {
        public void DeleteUser(int userID)
        {
            try
            {
                using (Entity context = new Entity()) 
                {
                    List<tblUser> userList = context.tblUsers.ToList();

                    foreach (tblUser item in userList)
                    {
                        if (item.UserId==userID)
                        {
                            context.tblUsers.Remove(item);
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public void DeletePat(int UserID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblPatient> patientList = context.tblPatients.ToList();

                    foreach (tblPatient item in patientList)
                    {
                        if (item.UserID==UserID)
                        {
                            context.tblPatients.Remove(item);
                            DeleteUser(item.UserID.GetValueOrDefault());
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                    //context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// deletes patients monitored by doctor (and in user tbl)
        /// </summary>
        /// <param name="DocID"></param>
        public void DeletePatientForDoctor(int DocID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblPatient> patientList = context.tblPatients.ToList();

                    foreach (tblPatient item in patientList)
                    {
                        if (item.DoctorID==DocID)
                        {
                            context.tblPatients.Remove(item);
                            DeleteUser(item.UserID.GetValueOrDefault());
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// deletes doctor and in user tbl
        /// </summary>
        /// <param name="DocID"></param>
        public void DeleteDoctor(int DocID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblDoctor> doctorList = context.tblDoctors.ToList();

                    foreach (tblDoctor item in doctorList)
                    {
                        if (item.DoctorID == DocID)
                        {
                            context.tblDoctors.Remove(item);
                            DeleteUser(item.UserID);
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void DeleteDocForManager(int managerID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblDoctor> doctorList = context.tblDoctors.ToList();

                    foreach (tblDoctor item in doctorList)
                    {
                        if (item.ManagerID==managerID)
                        {
                            DeletePatientForDoctor(item.DoctorID);
                            context.tblDoctors.Remove(item);
                            DeleteUser(item.UserID);
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void DeleteManager(int managerID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblManager> managerList = context.tblManagers.ToList();

                    foreach (tblManager item in managerList)
                    {
                        if (item.ManagerID==managerID)
                        {
                            DeleteDocForManager(item.ManagerID);
                            context.tblManagers.Remove(item);
                            DeleteUser(item.UserID);
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception exx)
            {

                MessageBox.Show(exx.ToString());
            }
        }


    }
}
