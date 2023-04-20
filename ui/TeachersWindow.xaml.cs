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
using ex.database;

namespace ex.ui
{
    /// <summary>
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        private Teacher _teacher;

        public TeachersWindow()
        {
            InitializeComponent();
        }

        public void loadTeachers()
        {
            using(SchoolDB db = new SchoolDB())
            {
                var teachers = db.Teacher.Include(p => p.Post).Include(p => p.Subject).Where(t => t.PostId == 3 || t.PostId == 2).ToList();
                dg_teachers.ItemsSource = teachers;
            }
        }

        private void dg_teachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _teacher = (Teacher)dg_teachers.SelectedItem;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadTeachers();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            TeacherDetailWindow w = new TeacherDetailWindow(this);
            w.ShowDialog();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if(_teacher == null)
            {
                MessageBox.Show("Учитель не выбран");
                return;
            }
            TeacherDetailWindow w = new TeacherDetailWindow(this, _teacher);
            w.ShowDialog();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (_teacher == null)
            {
                MessageBox.Show("Учитель не выбран");
                return;
            }
            using(SchoolDB db = new SchoolDB())
            {
                var teacher = db.Teacher.FirstOrDefault(p => p.Id == _teacher.Id);
                db.Teacher.Remove(teacher);
                db.SaveChanges();
                loadTeachers();
            }
        }

    }
}
