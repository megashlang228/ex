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
    public partial class JournalWindow : Window
    {
        private int _classId;
        private int _subjectId;

        public JournalWindow()
        {
            InitializeComponent();
            using(SchoolDB db = new SchoolDB())
            {
                var classes = db.Class.ToList();
                cb_class.DisplayMemberPath = "Name";
                cb_class.ItemsSource = classes;
            }
        }

        private void cb_class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clas = cb_class.SelectedItem as Class;
            _classId = clas.Id;
            loadJournal();
        }

        private void loadJournal()
        {
            using(SchoolDB db = new SchoolDB())
            {
                var journals = db.Journal.Include(p=>p.Class).Where(p=> p.ClassId == _classId).ToList();
                dg_otsenki.DataContext = journals;
            }
        }
    }
}
