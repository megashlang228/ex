using ex.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для TeacherDetailWindow.xaml
    /// </summary>
    public partial class TeacherDetailWindow : Window
    {
        TeachersWindow _window;
        private Teacher _teacher;
        private ScreenMode _screenMode;
        private int _postId;
        private int _subjectId;


        public TeacherDetailWindow(TeachersWindow w, Teacher teacher = null)
        {
            _window = w;
            if(teacher == null)
            {
                _teacher = new Teacher();
                _screenMode = ScreenMode.AddMode;
            }
            else
            {
                _teacher = teacher;
                _screenMode = ScreenMode.EditMode;
                _postId = teacher.PostId;
                _subjectId = teacher.SubjectId;
            }
            InitializeComponent();
            using(SchoolDB db = new SchoolDB())
            {
                var posts = db.Post.ToList();
                cb_post.DisplayMemberPath = "Name";
                cb_post.ItemsSource = posts;
                var subjects = db.Subject.ToList();
                cb_subject.DisplayMemberPath = "Name";
                cb_subject.ItemsSource = subjects;
            }
            grid_teacher.DataContext = _teacher;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (_screenMode)
            {
                case ScreenMode.EditMode:
                    updateTeacher();
                    break;
                case ScreenMode.AddMode:
                    createTeacher();
                    break;
                default:
                    break;
            }
        }

        private void createTeacher()
        {
            using (SchoolDB db = new SchoolDB())
            {
                var teacher = new Teacher();
                teacher.FIO = tb_fio.Text;
                teacher.Rate = Convert.ToInt32(tb_rate.Text);
                teacher.SubjectId = _subjectId;
                teacher.PostId = _postId;
                db.Teacher.Add(teacher);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("учитель добавлен");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
                _window.loadTeachers();
            }

            this.Close();
        }

        private void updateTeacher()
        {
            using (SchoolDB db = new SchoolDB())
            {

                var teacher = db.Teacher.FirstOrDefault(p => p.Id == _teacher.Id);
                teacher.FIO = tb_fio.Text;
                teacher.Rate = Convert.ToInt32(tb_rate.Text);
                teacher.SubjectId = _subjectId;
                teacher.PostId = _postId;

                var card = new PersonalCard();
                card.SubjectId = _subjectId;
                card.PostId = _postId;
                card.TeacherId = _teacher.Id;
                card.Rate = Convert.ToInt32(tb_rate.Text);
                card.Date = DateTime.Now;
                db.PersonalCard.Add(card);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("изменено");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
                _window.loadTeachers();
            }
            this.Close();
        }

        private void cb_subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subject subject = (Subject)cb_subject.SelectedItem;
            _subjectId = subject.Id;
        }

        private void cb_post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Post post = (Post)cb_post.SelectedItem;
            _postId = post.Id;
        }
    }

    enum ScreenMode { EditMode, AddMode}
}
