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
            if (myComboBox.SelectedItem is string selectedName && candidateDictionary.TryGetValue(selectedName, out int selectedId))
            {
                var interviewsByCandidate = GetInterviewsByCandidate();

                // בדיקת אם יש ראיונות למועמד עם ה-Id שנבחר
                if (interviewsByCandidate.TryGetValue(selectedId, out List<Interview>? interviews))
                {
                    var sortedInterviews = interviews
             .OrderBy(i => i.InterviewDate) // מיין לפי תאריך הראיון
             .ToList();

                    myDataGrid.ItemsSource = sortedInterviews;
                }

            }

        }


        //פונקציה ההופכת את כל הראיונות למילון
        private Dictionary<int, List<Interview>> GetInterviewsByCandidate()
        {
            // קבלת כל הראיונות
            List<Interview> allInterviews = conn.GetAllInterviews();

            // שימוש ב-LINQ כדי לקבץ את הראיונות לפי CandidateId
            var interviewsByCandidate = allInterviews
                .GroupBy(i => i.CandidateId) // קיבוץ לפי CandidateId
                .ToDictionary(
                    g => g.Key,               // המפתח של המילון - CandidateId
                    g => g.ToList()           // הערך של המילון - רשימה של Interviews
                );

            return interviewsByCandidate;
        }
    }
}
