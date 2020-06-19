using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration.Internal;

namespace NFZ_PRIVATE
{
    class DataBaseConnect
    {
        //private const string userLogin = "User ID = sa;";
        //private const string userPassword = "Password = password;";
        private const string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog = PATIENT_DB; Integrated Security = True";//"Data Source = localhost\\SQLEXPRESS; Initial Catalog = main;" + userLogin + userPassword;
        private static SqlConnection connection = new SqlConnection(connectionString);

        //Update the database 
        public static void UpdateDataBase(string query, out bool confirm)
        {
            confirm = false;
            try 
            { 
                if (OpenConnectSQL())
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnectSQL();
                    confirm = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Download data about SignIn
        public static void DownloadDataBase(string query, string column, out bool confirm, out string data)
        {
            data = "";
            confirm = false;
            try
            {
                if (OpenConnectSQL())
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data = dataReader[column].ToString();
                    }

                    dataReader.Close();
                    CloseConnectSQL();
                    if (data != "")
                    {
                        confirm = true;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            } 
        }

        //Download Patient personals details
        public static void DownloadDataBase(string query, string[] column, out bool confirm, out Patient PatientInfo)
        {
            //column = { "PatientName", "PatientSurname", "Id", "PatientMail", "PatientPhone"};
            confirm = false;
            bool check = false;
            Patient patient = new Patient();
            try
            {
                if (OpenConnectSQL())
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        patient.Name = dataReader[column[0]].ToString();
                        patient.Surname = dataReader[column[1]].ToString();
                        patient.Index = Int32.Parse(dataReader[column[2]].ToString());
                        patient.Email = dataReader[column[3]].ToString();
                        patient.Phone = Int32.Parse(dataReader[column[4]].ToString());
                        check = true;
                    }

                    dataReader.Close();
                    CloseConnectSQL();

                    if (check)
                    {
                        confirm = true;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            PatientInfo = patient;
        }

        //Search for information about the doctor
        public static void DownloadDataBase(string query, string[] column, out bool confirm, out Doctor DoctorInfo)
        {
            //column = { "PatientName", "PatientSurname", "Id", "PatientMail", "PatientPhone"};
            confirm = false;
            bool check = false;
            Doctor doctor = new Doctor();
            try
            {
                if (OpenConnectSQL())
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        doctor.Name = dataReader[column[0]].ToString();
                        doctor.Surname = dataReader[column[1]].ToString();
                        doctor.Index = Int32.Parse(dataReader[column[2]].ToString());
                        doctor.Specialization = dataReader[column[3]].ToString();
                        check = true;
                    }

                    dataReader.Close();
                    CloseConnectSQL();

                    if (check)
                    {
                        confirm = true;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            DoctorInfo = doctor;
        }

        //Download next Visit Control
        public static void DownloadDataBase(string query, string[] column, out bool confirm, out Dictionary<int, ControlVisit> timevisit)
        {
            //column = { "Id", "DoctorId", "Specialization", "Date", "Time", "PrivateNFZ", "PatientId" };
            Dictionary<int, ControlVisit> ControlVisitInfo = new Dictionary<int, ControlVisit>();
            confirm = false;
            bool check = false;
            try
            {
                if (OpenConnectSQL())
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ControlVisit VisitInfo = new ControlVisit();

                        VisitInfo.VisitIndex = Int32.Parse(dataReader[column[0]].ToString());
                        VisitInfo.DoctorIndex = Int32.Parse(dataReader[column[1]].ToString());
                        VisitInfo.Specialization = dataReader[column[2]].ToString();
                        VisitInfo.DateVisit = DateTime.Parse(dataReader[column[3]].ToString());
                        VisitInfo.TimeVisit = TimeSpan.Parse(dataReader[column[4]].ToString());
                        VisitInfo.PrivateNFZ = dataReader[column[5]].ToString();
                        VisitInfo.PatientIndex = Int32.Parse(dataReader[column[6]].ToString());

                        ControlVisitInfo.Add(VisitInfo.VisitIndex, VisitInfo);
                        check = true;
                    }

                    dataReader.Close();
                    CloseConnectSQL();
                    if (check)
                    {
                        confirm = true;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            timevisit = ControlVisitInfo;
        }

        //Open Connect SQL
        private static bool OpenConnectSQL()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 1042:
                        MessageBox.Show("Nie można połączyć się z serwerem");
                        break;
                    default:
                        MessageBox.Show("Nieznany błąd połaczenia");
                        break;
                }
                return false;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
                CloseConnectSQL();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.GetType().ToString());
                return false;
            }
        }

        //Close Connect SQL
        private static bool CloseConnectSQL()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
