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

namespace ex.ui
{
    /// <summary>
    /// Логика взаимодействия для StudentDetailWindow.xaml
    /// </summary>
    public partial class StudentDetailWindow : Window
    {

        StudentsWindow _window;
        private Student _student;
        private ScreenMode _screenMode;
        private int _classId;

        public StudentDetailWindow(StudentsWindow w, Student student = null)
        {
            _window = w;
            if (student == null)
            {
                _student = new Student();
                _screenMode = ScreenMode.AddMode;
            }
            else
            {
                _student = student;
                _screenMode = ScreenMode.EditMode;
                _classId = student.ClassId;
            }
            InitializeComponent();
            using (SchoolDB db = new SchoolDB())
            {
                var classes = db.Class.ToList();
                cb_class.DisplayMemberPath = "Name";
                cb_class.ItemsSource = classes;
            }
            grid_student.DataContext = _student;
        }

        private void cb_class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clas = (Class)cb_class.SelectedItem;
            _classId = clas.Id;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (_screenMode)
            {
                case ScreenMode.EditMode:
                    updateStudent();
                    break;
                case ScreenMode.AddMode:
                    createStudent();
                    break;
                default:
                    break;
            }
        }

        private void createStudent()
        {
            using (SchoolDB db = new SchoolDB())
            {
                var student = new Student();
                student.FIO = tb_fio.Text;
                student.Address = tb_address.Text;
                student.ParentNumber = tb_number.Text;
                student.DateOfBirthday = dp_post.SelectedDate.Value.Date;
                student.ClassId = _classId;
                db.Student.Add(student);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("ученик добавлен");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
                _window.loadStudents();
            }

            this.Close();
        }

        private void updateStudent()
        {
            using (SchoolDB db = new SchoolDB())
            {

                var student = db.Student.FirstOrDefault(p => p.Id == _student.Id);
                student.FIO = tb_fio.Text;
                student.Address = tb_address.Text;
                student.ParentNumber = tb_number.Text;
                student.DateOfBirthday = dp_post.SelectedDate.Value.Date;
                student.ClassId = _classId;
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("изменено");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
                _window.loadStudents();
            }
            this.Close();
        }
    }

}
