using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;

namespace NFZ_PRIVATE
{
    class PatientController
    {
        private static Patient PatientInfo = new Patient();

        //List of doctor's specializations
        public static void ListSpecialization(out List<string> spec)
        {
            List<string> Specialization = new List<string>();
            Specialization.Add("kardiolog");
            Specialization.Add("ortopeda");
            Specialization.Add("neurolog");
            spec = Specialization;
        }

        //List of price visit control
        public static void ListPrice(out List<string> price)
        {
            List<string> Price = new List<string>();
            Price.Add("NFZ");
            Price.Add("Private");
            price = Price;
        }

        //Check the correct login and password
        public static void CheckLogin(out bool confirm, ref SignIn signIn)
        {
            confirm = false;
            string password = signIn.Password;
            SecurityMD5.PasswordToMD5(ref password, out confirm);
            if(confirm)
            {
                signIn.Password = password;
                DataBase.CheckSignIn(out confirm, ref signIn);
            }
        }

        //Download patient personal details
        public static void GetInfoPatient(out bool confirm, SignIn signIn, out Patient patient)
        {
            confirm = false;
            DataBase.GetInfoPatient(out confirm, signIn, out PatientInfo);
            patient = PatientInfo;
        }

        //Search for information about the doctor
        public static void GetInfoDoctor(out bool confirm, ref Doctor doctor)
        {
            confirm = false;
            DataBase.GetInfoDoctor(out confirm, ref doctor);
        }

        //Download information about subsequent patient visit control
        public static void GetInfoControl(out bool confirm, out Dictionary<int, ControlVisit> control)
        {
            confirm = false;
            DataBase.GetInfoVisit(out confirm, PatientInfo, out control);
        }

        //Find the next control visit in the chosen date
        public static void SearchNextVisit(out bool confirm, string specialization, string privateNFZ, DateTime timevisit, out Dictionary<int, ControlVisit> control)
        {
            confirm = false;
            DataBase.SearchControlVisit(out confirm, specialization, privateNFZ, timevisit, out control);
        }

        //Add the next control visit in the chosen date
        public static void AddNextVisit(out bool confirm, int idvisit)
        {
            int idpatient = PatientInfo.Index;
            DataBase.ChangeControlVisit(out confirm, idvisit, idpatient);
        }

        //Change patient personal details
        public static void EditInfoPatient(out bool confirm, Patient patient)
        {
            confirm = false;
            patient.Index = PatientInfo.Index;
            PatientInfo = patient;
            DataBase.UpdateInfoPatient(out confirm, PatientInfo);
        }

        //Delete the date of patient next control visit in the chosen date
        public static void DeleteNextControl(out bool confirm, int idvisit)
        {
            confirm = false;
            int idpatient = 1;
            DataBase.ChangeControlVisit(out confirm, idvisit, idpatient);
        }

        //Clear all patient personal details
        public static void ClearInfoPatient()
        {
            PatientInfo.Name = "";
            PatientInfo.Surname = "";
            PatientInfo.Email = "";
            PatientInfo.Phone = 0;
            PatientInfo.Index = 0;
        }

    }
}
