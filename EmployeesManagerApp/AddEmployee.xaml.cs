using BL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static BL.EmployeesBL;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
         EmployeesBL empbl = new EmployeesBL();
        public AddEmployee()
        {
           InitializeComponent();
            
        }

        public delegate bool DelegateAddEmployee(Employee emp);
        public event DelegateAddEmployee addEvent;

        protected virtual bool OnEmployeeAdded(Employee emp)
        {
            // בדיקה אם יש מנהל אירוע רשום לאירוע
            if (addEvent != null)
            {
                return addEvent(emp);
            }
            return false;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void newEmployee()
        {
            Employee e1 = new Employee();
            e1.Id=int.Parse(IdTextBox.Text);
            e1.FirstName=FirstNameTextBox.Text;
            e1.LastName=LastNameTextBox.Text;
            e1.Age=int.Parse(AgeTextBox.Text);
            e1.StartOfWorkYear=int.Parse(StartOfWorkingYearTextBox.Text);
            e1.City=CityAddressTextBox.Text;
            e1.Street=StreetAddressTextBox.Text;
            e1.RoleInCompany=JobTitleTextBox.Text;
            e1.PhoneNumber=PhoneNumberTextBox.Text;
            e1.Email=MailAddressTextBox.Text;
            empbl.addEmployee(e1);
            bool success = OnEmployeeAdded(e1);

        }
        private void checkLegal()
        {
            //check id
            if (IdTextBox.Text.Length != 9)
            {
                MessageBox.Show("Validation failed for field – Id");
            }
            else
            {
                //check fName
                if (!Regex.IsMatch(FirstNameTextBox.Text, @"^[a-zA-Z]+$") && FirstNameTextBox.Text.Length > 2)
                {
                    MessageBox.Show("Validation failed for field – first name");

                }
                else
                {
                    //check lName
                    if (!Regex.IsMatch(LastNameTextBox.Text, @"^[a-zA-Z]+$") && LastNameTextBox.Text.Length > 2)
                    {
                        MessageBox.Show("Validation failed for field – last name");

                    }
                    else
                    {
                        //check year
                        int year = int.Parse(DateTime.Now.Year.ToString());
                        int start = int.Parse(StartOfWorkingYearTextBox.Text);
                        if (!(IsDigitsOnly(StartOfWorkingYearTextBox.Text) && year - start < 15))
                            MessageBox.Show("Validation failed for field – Start Of Working Year");
                        else
                        {
                            //check role
                            IEnumerable<string> rolesInCompany = empbl.RoleInCompany();

                            if (!rolesInCompany.Cast<string>().Contains(JobTitleTextBox.Text))
                                MessageBox.Show("Validation failed for field – JobTitle");
                            else {
                                //check phone
                                if (PhoneNumberTextBox.Text.Length != 10)
                                    MessageBox.Show("Validation failed for field – phone");
                                else
                                {//check mail
                                    if (!IsValidEmail(MailAddressTextBox.Text))
                                        MessageBox.Show("Validation failed for field – mail");
                                    else
                                    {
                                        MessageBox.Show("Employee added Successfully");
                                        newEmployee();
                                           
                                    }
                                }

                            }

                        }

                    }


                }

            }



        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            checkLegal();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
