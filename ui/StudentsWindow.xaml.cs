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
    /// Логика взаимодействия для StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        public StudentsWindow()
        {
            InitializeComponent();
        }

        private void dg_students_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadStudents();
        }

        public void loadStudents()
        {
            using (SchoolDB db = new SchoolDB())
            {
                var students = db.Student.Include(p => p.Class).ToList();
                dg_students.ItemsSource = students;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            StudentDetailWindow w = new StudentDetailWindow(this);
            w.ShowDialog();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var student = dg_students.SelectedItem as Student;
            if(student != null)
            {
                StudentDetailWindow w = new StudentDetailWindow(this, student);
                w.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ученик не выбран");
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

            var student = dg_students.SelectedItem as Student;
            if (student != null)
            {
                using(SchoolDB db = new SchoolDB())
                {
                    var st = db.Student.FirstOrDefault(p => p.Id == student.Id);
                    db.Student.Remove(st);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Ученик не выбран");
            }
        }

        private void vedomost_Click(object sender, RoutedEventArgs e)
        {
            var student = dg_students.SelectedItem as Student;
            VedomostWindow w = new VedomostWindow(student);
            w.ShowDialog();
        }
    }
}
