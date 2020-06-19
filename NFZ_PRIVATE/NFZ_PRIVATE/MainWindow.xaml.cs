using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NFZ_PRIVATE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //Hide all grids
        private void Collapsed_Grid()
        {
            Grid_Hello.Visibility = Visibility.Collapsed;
            Grid_SignIn.Visibility = Visibility.Collapsed;
            Grid_InfoPatient.Visibility = Visibility.Collapsed;
            Grid_EditData.Visibility = Visibility.Collapsed;
            Grid_ShowVisit.Visibility = Visibility.Collapsed;
            Grid_AddVisit.Visibility = Visibility.Collapsed;
            Grid_FindDoctor.Visibility = Visibility.Collapsed;
            Grid_SignOut.Visibility = Visibility.Collapsed;
        }

        //Hide all text errors
        private void Collapsed_Error()
        {
            Text_ErrorSignIn.Visibility = Visibility.Collapsed;
            Text_ErrorAdd.Visibility = Visibility.Collapsed;
            Text_ErrorShow.Visibility = Visibility.Collapsed;
            Text_ErrorEdit.Visibility = Visibility.Collapsed;
            Text_ErrorFindDoctor.Visibility = Visibility.Collapsed;
        }

        //Clear textbox regarding patient personal details 
        private void Clear_Data()
        {
            Get_Login.Text = "";
            Get_Password.Clear();
            Show_Name.Text = "";
            Show_Surname.Text = "";
            Show_ID.Text = "";
            Show_Phone.Text = "";
            Show_Email.Text = "";
            Get_PasswordConfirm.Clear();
            Get_Name.Text = "";
            Get_Surname.Text = "";
            Get_Phone.Text = "";
            Get_Email.Text = "";
            Clear_FindDoctor();
            Clear_DateTime();
        }

        private void Clear_FindDoctor()
        {
            Get_DoctorID.Text = "";
            Show_DoctorName.Text = "";
            Show_DoctorSurname.Text = "";
            Show_Specialization.Text = "";
        }

        //Clear textbox regarding visit date setting 
        private void Clear_DateTime()
        {
            Get_Day.Text = "";
            Get_Month.Text = "";
            Get_Year.Text = "";
        }

        //Operation of all buttons
        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            Collapsed_Error();
            Button button = (Button)sender;
            switch (button.Content.ToString())
            {
                case "Start":
                    GoToSignIn();
                    break;
                case "Sign In":
                    CheckLogin();
                    break;
                case "Sign In again":
                    Collapsed_Grid();
                    Collapsed_Error();
                    Clear_Data();
                    Grid_SignIn.Visibility = Visibility.Visible;
                    break;
                case "Show your visit control":
                    GetVisitInfo();
                    break;
                case "Delete this visit":
                    DeleteNextVisit();
                    break;
                case "Edit your data":
                    GoToEditData();
                    break;
                case "Change Data":
                    EditData();
                    break;
                case "Add next visit control":
                    GoToAddNextVisit();
                    break;
                case "Add visit":
                    AddNextVisit();
                    break;
                case "Find visit":
                    SearchNextVisit();
                    break;
                case "Find a doctor":
                    Collapsed_Grid();
                    Collapsed_Error();
                    Clear_FindDoctor();
                    Grid_FindDoctor.Visibility = Visibility.Visible;
                    break;
                case "Search doctor":
                    SearchDoctor();
                    break;
                case "Back":
                    Collapsed_Grid();
                    Collapsed_Error();
                    Grid_InfoPatient.Visibility = Visibility.Visible;
                    break;
                case "Sign Out":
                    SignOut();
                    Clear_Data();
                    Clear_DateTime();
                    break;
                case "Exit":
                    Clear_Data();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("How did you do that?", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        //Show Grid_SignIn and download a lists of doctor's specializations and prices visit control
        private void GoToSignIn()
        {
            Collapsed_Grid();
            Grid_SignIn.Visibility = Visibility.Visible;
            List<string> Specialization = new List<string>();
            PatientController.ListSpecialization(out Specialization);
            List<string> Price = new List<string>();
            PatientController.ListPrice(out Price);
            Get_Specialization.ItemsSource = Specialization;
            Get_Specialization.Items.Refresh();
            Get_Price.ItemsSource = Price;
            Get_Price.Items.Refresh();
        }

        //Check the correct login and password
        private void CheckLogin()
        {
            Collapsed_Error();
            SignIn signIn = new SignIn();
            signIn.Login = Get_Login.Text;
            signIn.Password = Get_Password.Password;
            bool confirm = false;
            if (signIn.Login == "" | signIn.Password == "")
            {
                Text_ErrorSignIn.Visibility = Visibility.Visible;
                Text_ErrorSignIn.Text = "Login or Password is incorrect!";
            }
            else
            {
                PatientController.CheckLogin(out confirm, ref signIn);
                if (confirm)
                {
                    Show_ID.Text = signIn.PatientIndex.ToString();
                    Collapsed_Grid();
                    Collapsed_Error();
                    GetInfoPatient();

                }
                else
                {
                    Text_ErrorSignIn.Visibility = Visibility.Visible;
                    Text_ErrorSignIn.Text = "Login or Password is incorrect!";
                }
            }
        }

        //Download patient personal details
        private void GetInfoPatient()
        {
            Patient patient = new Patient();
            SignIn signIn = new SignIn();
            signIn.PatientIndex = Int32.Parse(Show_ID.Text.ToString());
            bool confirm = false;
            PatientController.GetInfoPatient(out confirm, signIn, out patient);
            if(confirm)
            {
                Show_Name.Text = patient.Name;
                Show_Surname.Text = patient.Surname;
                Show_ID.Text = patient.Index.ToString();
                Show_Phone.Text = patient.Phone.ToString();
                Show_Email.Text = patient.Email;
                Grid_InfoPatient.Visibility = Visibility.Visible;
            }
            else
            {
                Text_ErrorSignIn.Visibility = Visibility.Visible;
                Text_ErrorSignIn.Text = "I can't download your data!";
            }
        }

        //Show Grid_AddVisit and set default value for the visit control list - List_AddVisit
        private void GoToAddNextVisit()
        {
            Dictionary<int, ControlVisit> controlvisit = new Dictionary<int, ControlVisit>();
            ControlVisit control = new ControlVisit();
            control.VisitIndex = 0;
            controlvisit.Add(0, control);
            Text_ErrorAdd.Text = "Find next visit control!";
            Text_ErrorAdd.Visibility = Visibility.Visible;
            List_AddVisit.ItemsSource = controlvisit;
            List_AddVisit.Items.Refresh();
            Collapsed_Grid();
            Grid_AddVisit.Visibility = Visibility.Visible;
            Clear_DateTime();
        }

        //Download information about subsequent patient visit control
        private void GetVisitInfo()
        {
            bool confirm = false;
            Dictionary<int, ControlVisit> ShowControlVisit = new Dictionary<int, ControlVisit>();
            PatientController.GetInfoControl(out confirm, out ShowControlVisit);
            if(!confirm)
            {
                ControlVisit control = new ControlVisit();
                control.VisitIndex = 0;
                ShowControlVisit.Add(0, control);
                Text_ErrorShow.Visibility = Visibility.Visible;
                Text_ErrorShow.Text = "You don't have next visit control!";
            }
            List_ShowVisit.ItemsSource = ShowControlVisit;
            List_ShowVisit.Items.Refresh();
            Collapsed_Grid();
            Grid_ShowVisit.Visibility = Visibility.Visible;
        }

        //Show Grid_SignOut and clear patient personal details
        private void SignOut()
        {
            PatientController.ClearInfoPatient();
            Collapsed_Grid();
            Collapsed_Error();
            Grid_SignOut.Visibility = Visibility.Visible;
        }

        //Find the next control visit in the chosen date
        private void SearchNextVisit()
        {
            try
            {
                string specialization = Get_Specialization.SelectedItem.ToString();
                string privateNFZ = Get_Price.SelectedItem.ToString();
                int day = Int32.Parse(Get_Day.Text);
                int month = Int32.Parse(Get_Month.Text);
                int year = Int32.Parse(Get_Year.Text);
                DateTime timevisit = new DateTime(year, month, day);
                bool confirm = false;
                Dictionary<int, ControlVisit> AddControlVisit = new Dictionary<int, ControlVisit>();
                PatientController.SearchNextVisit(out confirm, specialization, privateNFZ, timevisit, out AddControlVisit);
                if(!confirm)
                {
                    Text_ErrorAdd.Text = "We don't have time for you on this day!";
                    Text_ErrorAdd.Visibility = Visibility.Visible;
                    ControlVisit control = new ControlVisit();
                    control.VisitIndex = 0;
                    AddControlVisit.Add(0, control);
                }
                List_AddVisit.ItemsSource = AddControlVisit;
                List_AddVisit.Items.Refresh();
            }
            catch(Exception e)
            {
                Text_ErrorAdd.Text = "You entered incorrect date next visit!";
                Text_ErrorAdd.Visibility = Visibility.Visible;
                MessageBox.Show(e.Message);
            }
        }

        //Add the next control visit in the chosen date
        private void AddNextVisit()
        {
            bool confirm = false;
            if(List_AddVisit.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose anything!");
            }
            else
            {
                var visit = (KeyValuePair<int, ControlVisit>)List_AddVisit.SelectedItem;
                int idvisit = visit.Key;
                PatientController.AddNextVisit(out confirm, idvisit);
                if(confirm)
                {
                    GoToAddNextVisit();
                    Text_ErrorAdd.Text = "Added successfully!";
                    Text_ErrorAdd.Visibility = Visibility.Visible;
                }
                else
                {
                    Text_ErrorAdd.Text = "We have a problem adding data!";
                    Text_ErrorAdd.Visibility = Visibility.Visible;
                }
            }
        }

        //Delete the date of patient next control visit in the chosen date
        private void DeleteNextVisit()
        {
            bool confirm = false;
            if (List_ShowVisit.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose anything!");
            }
            else
            {
                var visit = (KeyValuePair<int, ControlVisit>)List_ShowVisit.SelectedItem;
                int idvisit = visit.Key;
                PatientController.DeleteNextControl(out confirm, idvisit);
                if (confirm)
                {
                    Text_ErrorShow.Text = "Removed successfully!";
                    Text_ErrorShow.Visibility = Visibility.Visible;
                    GetVisitInfo();
                }
                else
                {
                    Text_ErrorShow.Text = "We have a problem deleting data!";
                    Text_ErrorShow.Visibility = Visibility.Visible;
                }
            }
        }

        //Show Grid_EditData and set patient's personal data for editing
        private void GoToEditData()
        {
            Collapsed_Error();
            Collapsed_Grid();
            Get_PasswordConfirm.Clear();
            Grid_EditData.Visibility = Visibility.Visible;
            Get_Name.Text = Show_Name.Text;
            Get_Surname.Text = Show_Surname.Text;
            Get_Phone.Text = Show_Phone.Text;
            Get_Email.Text = Show_Email.Text;
        }

        //Change patient personal details
        private void EditData()
        {
            if(Get_Password.Password == Get_PasswordConfirm.Password)
            {
                bool confirm = false;
                Patient patient = new Patient();
                try
                {
                    patient.Index = Int32.Parse(Show_ID.Text);
                    patient.Phone = Int32.Parse(Get_Phone.Text);
                    patient.Name = Get_Name.Text;
                    patient.Surname = Get_Surname.Text;
                    patient.Email = Get_Email.Text;
                    PatientController.EditInfoPatient(out confirm, patient);
                }
                catch(Exception e)
                {
                    Text_ErrorAdd.Text = "You entered incorrect data!";
                    Text_ErrorAdd.Visibility = Visibility.Visible;
                    MessageBox.Show(e.Message);
                }
                if (confirm)
                {
                    Text_ErrorEdit.Text = "We changed your data!";
                    Text_ErrorEdit.Visibility = Visibility.Visible;
                    GetInfoPatient();
                    Collapsed_Grid();
                    Grid_EditData.Visibility = Visibility.Visible;
                }
                else
                {
                    Text_ErrorEdit.Text = "We have a problem with change your data!";
                    Text_ErrorEdit.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Text_ErrorEdit.Text = "You entered the wrong password!";
                Text_ErrorEdit.Visibility = Visibility.Visible;
            }
        }

        //Search for information about the doctor
        private void SearchDoctor()
        {
            try
            {
                Doctor doctor = new Doctor();
                doctor.Index = Int32.Parse(Get_DoctorID.Text);
                bool confirm = false;
                Clear_FindDoctor();
                Get_DoctorID.Text = doctor.Index.ToString();
                PatientController.GetInfoDoctor(out confirm, ref doctor);
                if (!confirm)
                {
                    Text_ErrorFindDoctor.Text = "We can't find information about this doctor";
                    Text_ErrorFindDoctor.Visibility = Visibility.Visible;
                }
                else
                {
                    Show_DoctorName.Text = doctor.Name;
                    Show_DoctorSurname.Text = doctor.Surname;
                    Show_Specialization.Text = doctor.Specialization;
                }
            }
            catch (Exception e)
            {
                Text_ErrorFindDoctor.Text = "You entered incorrect date next visit!";
                Text_ErrorFindDoctor.Visibility = Visibility.Visible;
                MessageBox.Show(e.Message);
            }
        }
    }
}
