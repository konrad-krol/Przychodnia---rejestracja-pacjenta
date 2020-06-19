using System;
using System.Collections.Generic;
using System.Text;

namespace NFZ_PRIVATE
{
    class DataBase : DataBaseConnect
    {
        //Check correct SignIn
        public static void CheckSignIn(out bool confirm, ref SignIn signin)
        {
            confirm = false;
            string data = "";
            string column = "PatientIndex";
            string query = $"SELECT PatientIndex FROM LoginPatient WHERE PatientLogin=\'{signin.Login}\' " +
                $"and PatientPassword=\'{signin.Password}\'";
            DownloadDataBase(query, column, out confirm, out data);
            if (confirm)
            {
                signin.PatientIndex = Int32.Parse(data);
            }
        }

        //Download Info Patient
        public static void GetInfoPatient(out bool confirm, SignIn signin, out Patient patient)
        {
            confirm = false;
            string index = signin.PatientIndex.ToString();
            //string[] column = { "PatientName", "PatientSurname", "Id", "PatientMail", "PatientPhone" };
            string[] column = { "PatientName", "PatientSurname", "PatientIndex", "PatientMail", "PatientPhone" };
            //string query = $"SELECT * FROM Patients WHERE Id=\'{index}\'";
            string query = $"SELECT * FROM Patients WHERE PatientIndex=\'{index}\'";
            DownloadDataBase(query, column, out confirm, out patient);
        }

        //Search for information about the doctor
        public static void GetInfoDoctor(out bool confirm, ref Doctor doctor)
        {
            confirm = false;
            string index = doctor.Index.ToString();
            string[] column = { "DoctorName", "DoctorSurname", "Id", "DoctorSpecialization"};
            string query = $"SELECT * FROM Doctors WHERE Id=\'{index}\'";
            DownloadDataBase(query, column, out confirm, out doctor);
        }

        //Download next visit control
        public static void GetInfoVisit(out bool confirm, Patient patient, out Dictionary<int, ControlVisit> timevisit)
        {
            confirm = false;
            //string query = $"SELECT * FROM Visits WHERE PatientId=\'{patient.Index}\'";
            string query = $"SELECT * FROM Visits WHERE PatientIndex=\'{patient.Index}\'";
            //string[] column = { "Id", "DoctorId", "Specialization", "Date", "Time", "PrivateNFZ", "PatientId" };
            string[] column = { "VisitIndex", "DoctorId", "Specialization", "VisitDate", "VisitTime", "PrivateNFZ", "PatientIndex" };
            int index = patient.Index;
            DownloadDataBase(query, column, out confirm, out timevisit);
        }

        //Find next visit control
        public static void SearchControlVisit(out bool confirm, string specialization, string privateNFZ, DateTime date, out Dictionary<int, ControlVisit> timevisit)
        {
            string onlydata = date.Date.ToString("yyyy-MM-dd");
            confirm = false;
            /*string query = $"SELECT * FROM Visits WHERE PatientId=1 and Date=\'{onlydata}\' " + 
                $"and Specialization=\'{specialization}\' and PrivateNFZ=\'{privateNFZ}\'";*/
            string query = $"SELECT * FROM Visits WHERE PatientIndex=1 and VisitDate=\'{onlydata}\' " +
            $"and Specialization=\'{specialization}\' and PrivateNFZ=\'{privateNFZ}\'";
            //string[] column = { "Id", "DoctorId", "Specialization", "Date", "Time", "PrivateNFZ", "PatientId" };
            string[] column = { "VisitIndex", "DoctorId", "Specialization", "VisitDate", "VisitTime", "PrivateNFZ", "PatientIndex" };
            DownloadDataBase(query, column, out confirm, out timevisit);
        }

        //Edit next visit control
        public static void ChangeControlVisit(out bool confirm, int idvisit, int idpatient)
        {
            confirm = false;
            //string query = $"UPDATE Visits SET PatientId=\'{idpatient}\' WHERE Id=\'{idvisit}\'";
            string query = $"UPDATE Visits SET PatientIndex=\'{idpatient}\' WHERE VisitIndex=\'{idvisit}\'";
            UpdateDataBase(query, out confirm);

        }

        //Change personal details
        public static void UpdateInfoPatient(out bool confirm, Patient patientinfo)
        {
            confirm = false;
            /*string query = $"UPDATE Patients SET PatientName=\'{patientinfo.Name}\', PatientSurname=\'{patientinfo.Surname}\', " +
                $"PatientPhone=\'{patientinfo.Phone}\', PatientMail=\'{patientinfo.Email}\' WHERE Id=\'{patientinfo.Index}\'";*/
            string query = $"UPDATE Patients SET PatientName=\'{patientinfo.Name}\', PatientSurname=\'{patientinfo.Surname}\', " +
            $"PatientPhone=\'{patientinfo.Phone}\', PatientMail=\'{patientinfo.Email}\' WHERE PatientIndex=\'{patientinfo.Index}\'";
            UpdateDataBase(query, out confirm);
        }
    }
}
