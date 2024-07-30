using DAL;
using DAL.Models;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for SuitableCandidate.xaml
    /// </summary>
    public partial class SuitableCandidate : Window
    {
        DBConnection conn;
        private Dictionary<string, int> candidateDictionary = new Dictionary<string, int>();


        public SuitableCandidate()
        {
            InitializeComponent();
        }
        private void fillCandidateCmb()
        {
            IEnumerable<Candidate> all_candidates = conn.GetAllCandidates();
            myComboBox.Items.Clear(); // מנקה את הפריטים הקיימים
            candidateDictionary.Clear(); // מנקה את ה-Dictionary

            foreach (Candidate c in all_candidates)
            {
                string fullName = c.FirstName + " " + c.LastName;
                if (!candidateDictionary.ContainsKey(fullName))
                {
                    candidateDictionary[fullName] = c.Id;
                    myComboBox.Items.Add(fullName);
                }
            }
        }


    private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Interview> allInterviews = conn.GetAllInterviews();


            if (myComboBox.SelectedItem is string selectedName && candidateDictionary.TryGetValue(selectedName, out int selectedId))
            {
                var interviewsByCandidate = GetInterviewsByCandidate();

                // בדיקת אם יש ראיונות למועמד עם ה-Id שנבחר
                if (interviewsByCandidate.TryGetValue(selectedId, out List<Interview>? interviews))
                {
                    myDataGrid.ItemsSource = interviews;
                }
            }

        }


        //פונקציה ההופכת את כל הראיונות למילון
        private Dictionary<int, List<Interview>> GetInterviewsByCandidate()
        {
            // קבלת כל הראיונות
            List<Interview> allInterviews = conn.GetAllInterviews();


            return interviewsByCandidate;
        }
    }
}
