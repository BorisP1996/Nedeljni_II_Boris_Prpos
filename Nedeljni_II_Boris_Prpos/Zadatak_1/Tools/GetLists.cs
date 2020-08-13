using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Tools
{
    class GetLists
    {
        public List<tblShift> GetShifts()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblShift> list = new List<tblShift>();
                    list = context.tblShifts.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public List<vwManager> GetManagers()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwManager> list = new List<vwManager>();
                    list = context.vwManagers.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public List<vwDoctor> GetDoctors()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwDoctor> list = new List<vwDoctor>();
                    list = context.vwDoctors.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// If return more than 1 - invalid, because there must be only one admin
        /// </summary>
        /// <returns></returns>
        public int GetAdmins()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    int count = 0;
                    List<tblUser> adminList = context.tblUsers.ToList();
                    foreach (tblUser item in adminList)
                    {
                        if (item.Manager==true)
                        {
                            count++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return count;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return 10;
            }
        }
        public List<vwMaintance> GetMaintance()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwMaintance> list = new List<vwMaintance>();
                    list = context.vwMaintances.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public List<vwPatient> GetPatients()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwPatient> list = new List<vwPatient>();
                    list = context.vwPatients.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
