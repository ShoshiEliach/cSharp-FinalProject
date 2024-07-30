using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using DAL.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeesBL eb=new EmployeesBL();
        AddEmployee addEmployeeWin = new AddEmployee();

        public MainWindow()
        {
            InitializeComponent();
            var res = eb.desineEmployee();
            CandidateDataGrid.ItemsSource = res;
            fillCmb();
            //CandidateDataGrid.ItemsSource = employeeManagerBL.GetAllEmployee();
            //ComboBoxFilterOptions.ItemsSource = employeeManagerBL.GetAllJobTitle();
        }

        


    private bool addEmployee(Employee emp)
    {
        
            var res = eb.desineEmployee();
            CandidateDataGrid.ItemsSource = res;
            
        return true; 
    }
    private void Button_Click(object sender, RoutedEventArgs e)
        {

            //addEmployeeWin.d1
            addEmployeeWin.addEvent += addEmployee;
            addEmployeeWin.ShowDialog();
            
                var res = eb.desineEmployee();
                CandidateDataGrid.ItemsSource = res;
            
        }

       

        private void ComboBoxFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var res = eb.FilterRoleInCompany(ComboBoxFilterCategory.SelectedValue.ToString());
            CandidateDataGrid.ItemsSource = res;
        }

        private void fillCmb()
        {
            IEnumerable<string> rolesInCompany = eb.RoleInCompany();
            foreach (string role in rolesInCompany)
            {
                if (!ComboBoxFilterCategory.Items.Cast<string>().Contains(role))

                    ComboBoxFilterCategory.Items.Add(role);
            }

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SuitableCandidate suitableCandidate = new SuitableCandidate();
            suitableCandidate.ShowDialog();
        }
    }
}
