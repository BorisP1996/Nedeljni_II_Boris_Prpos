using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Tools
{
    class Methods
    {
        /// <summary>
        /// Compares input with master parametres in file
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pasword"></param>
        /// <returns></returns>
        public bool CompareFileCredentials(string username,string pasword)
        {
            try
            {
                string path = @"../../MasterCredentials.txt";
                StreamReader sr = new StreamReader(path);
                string line = "";
                List<string> lineList = new List<string>();
                //take data from file and insert into list
                while ((line=sr.ReadLine())!=null)
                {
                    lineList.Add(line);
                }
                sr.Close();

                List<string> credentials = new List<string>();
                
                //format is:
                //username: xxxxx
                //password: xxxxx 
                //and because of that=>split by ':'
                for (int i = 0; i < lineList.Count; i++)
                {
                    string[] array = lineList[i].Split(':').ToArray();
                    credentials.Add(array[1]);
                }
                //compare input with data that was ripped out of the file
                if (credentials[0]==username && credentials[1]==pasword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.ToString());
                return false;
            }
        }

        public void WriteNewCredentials(string newUser,string newPas)
        {
            try
            {
                string path = @"../../MasterCredentials.txt";
                StreamWriter sw = new StreamWriter(path,false);
                sw.WriteLine("Username:" + newUser + "\nPassword:" + newPas);
                sw.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pasword"></param>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public bool ValidateAdmin(string username,string pasword,string idcard)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblUser> userList = context.tblUsers.ToList();
                    List<string> usernameList = new List<string>();
                    List<string> paswordList = new List<string>();
                    List<string> cardList = new List<string>();

                    foreach (tblUser item in userList)
                    {
                        usernameList.Add(item.Username);
                        paswordList.Add(item.Pasword);
                        cardList.Add(item.IdCard);
                    }

                    if (!usernameList.Contains(username) && !paswordList.Contains(pasword) && !cardList.Contains(idcard))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
                
            }
        }
        public bool ValidateGender(string gender)
        {
            if (gender=="M" || gender =="m" || gender =="z" || gender=="Z")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Must contain 1 small letter,1 big letter,8 char length and one number
        /// </summary>
        /// <param name="pasword"></param>
        /// <returns></returns>
        public bool ValidatePassword(string pasword)
        {
            try
            {
                bool length=false;
                bool small=false;
                bool big=false;
                bool number=false;

                char[] array = pasword.ToCharArray();

                for (int i = 0; i < array.Length; i++)
                {
                    if (Char.IsLower(array[i]))
                    {
                        small = true;
                    }
                    if (Char.IsUpper(array[i]))
                    {
                        big = true;
                    }
                    if (Char.IsDigit(array[i]))
                    {
                        number = true;
                    }
                }
                if (pasword.Length>=8)
                {
                    length = true;
                }

                if (small == true && big == true && number == true && length == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public int DeterminLoger(string username,string pasword)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblUser> userList = context.tblUsers.ToList();
                    List<tblManager> managerList = context.tblManagers.ToList();
                    List<tblMaintance> maintanceList = context.tblMaintances.ToList();
                    List<tblPatient> patienceList = context.tblPatients.ToList();
                    List<tblDoctor> doctorList = context.tblDoctors.ToList();

                    foreach (tblUser item in userList)
                    {
                        //admin
                        if (item.Username==username && item.Pasword==pasword && item.Manager==true)
                        {
                            return 1;
                        }
                        else
                        {
                            continue;
                        }

                    }
                    foreach (tblUser item in userList)
                    {
                        if (item.Username==username && item.Pasword==pasword)
                        {
                            foreach (tblDoctor itemDoc in doctorList)
                            {
                                //doctor logged
                                if (item.UserId==itemDoc.UserID)
                                {
                                    return 2;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    foreach (tblUser item in userList)
                    {
                        if (item.Username==username && item.Pasword==pasword)
                        {
                            foreach (tblMaintance itemMan  in maintanceList)
                            {
                                //maintance logged
                                if (itemMan.UserID==item.UserId)
                                {
                                    return 3;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    foreach (tblUser item in userList)
                    {
                        if (item.Username == username && item.Pasword == pasword)
                        {
                            foreach (tblManager itemMan in managerList)
                            {
                                //manager logged
                                if (itemMan.UserID == item.UserId)
                                {
                                    return 4;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    foreach (tblUser item in userList)
                    {
                        if (item.Username == username && item.Pasword == pasword)
                        {
                            foreach (tblPatient itemPat in patienceList)
                            {
                                //patient logged
                                if (itemPat.UserID == item.UserId)
                                {
                                    return 5;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return 10;
            }
        }
        public bool UniqueDoctorNumber(string number)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblDoctor> doctorList = context.tblDoctors.ToList();
                    List<string> numbers = new List<string>();
                    foreach (tblDoctor item in doctorList)
                    {
                        numbers.Add(item.UniqueNumber);
                    }

                    if (!numbers.Contains(number) && number.Length==5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool UniqueDoctorAccount(string number)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblDoctor> doctorList = context.tblDoctors.ToList();
                    List<string> accounts = new List<string>();
                    foreach (tblDoctor item in doctorList)
                    {
                        accounts.Add(item.AccountNumber);
                    }

                    if (!accounts.Contains(number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool PatientUniqueNumber(string Number)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblPatient> patientList = context.tblPatients.ToList();

                    List<string> numberList = new List<string>();

                    foreach (tblPatient item in patientList)
                    {
                        numberList.Add(item.CardNumber);
                    }

                    if (!numberList.Contains(Number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Find maintance who was created first=>has smallest id
        /// </summary>
        /// <returns></returns>
        public int FindMinMaintance()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblMaintance> list = context.tblMaintances.ToList();

                    List<int> maintanceIDlist = new List<int>();

                    foreach (tblMaintance item in list)
                    {
                        maintanceIDlist.Add(item.MaintanceID);
                    }

                    if (maintanceIDlist.Count==0)
                    {
                        return 0;
                    }
                    else
                    {
                        int minId = maintanceIDlist.Min();
                        return minId;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// Calculate how many doctor is monitored by same manager
        /// </summary>
        /// <param name="managerID"></param>
        /// <returns></returns>
        public bool HowManyDoctorsMonitored(int managerID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblDoctor> doctorList = new List<tblDoctor>();
                    doctorList = context.tblDoctors.ToList();
                    int count=0;

                    foreach (tblDoctor item in doctorList)
                    {
                        if (item.ManagerID==managerID)
                        {
                            count++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    tblManager viaManager = (from r in context.tblManagers where r.ManagerID == managerID select r).FirstOrDefault();
                    if (viaManager.MaxDoctors>count)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Find how many erors manager has in order to determine if he can monitor any doctor (5 is the limit)
        /// </summary>
        /// <param name="managerID"></param>
        /// <returns></returns>
        public int FindManagerErors(int managerID)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    tblManager viaManager = (from r in context.tblManagers where r.ManagerID == managerID select r).FirstOrDefault();

                    int erorCount = viaManager.Erors.GetValueOrDefault();

                    return erorCount;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return 10;
            }
        }
    }
}
