using ex.database;
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
using System.Data.Entity;

namespace ex.ui
{
    /// <summary>
    /// Логика взаимодействия для VedomostWindow.xaml
    /// </summary>
    public partial class VedomostWindow : Window
    {
        private Student _student;

        public VedomostWindow(Student student)
        {
            _student = student;
            InitializeComponent();
            using(SchoolDB db = new SchoolDB())
            {
                var journals = db.Journal.Include(p => p.Class).Where(p=> p.ClassId == _student.ClassId).ToList();
                dg_journal.ItemsSource = journals;
            }

        }

        private void dg_journal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using(SchoolDB db = new SchoolDB())
            {
                var journal = dg_journal.SelectedItem as Journal;
                var otsenki = db.Otsenka.Include(p => p.Subject).Where(p => p.JournalId == journal.Id && p.StudentId == _student.Id).ToList();
                dg_otsenki.DataContext = otsenki;
            }
        }
    }
}
