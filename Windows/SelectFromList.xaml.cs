﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for SelectFromList.xaml
    /// </summary>

    public enum SelectFromMenuOrAddDecider { MENU, ADD }
    public enum CourseStudentDecider { STUDENT, COURSE }

    public partial class SelectFromList : Window
    {
        public SelectFromMenuOrAddDecider MenuAddDecider { get; set; }
        public CourseStudentDecider CourseStudentDecider { get; set; }

        public PaymentMenu MenuWindow { get; set; }
        public PaymentAddEdit AddEditWindow { get; set; }

        public string selectStudent = "Izaberite ucenika";
        public string selectCourse = "Izaberite kurs";

        public SelectFromList(SelectFromMenuOrAddDecider decider, CourseStudentDecider decider2, PaymentMenu menu)
        {
            InitializeComponent();

            MenuAddDecider = decider;
            CourseStudentDecider = decider2;

            MenuWindow = menu;

            setupWindow();
        }

        public SelectFromList(SelectFromMenuOrAddDecider decider, CourseStudentDecider decider2, PaymentAddEdit addEdit)
        {
            InitializeComponent();

            MenuAddDecider = decider;
            CourseStudentDecider = decider2;

            AddEditWindow = addEdit;

            setupWindow();
        }

        private void setupWindow()
        {
            if (CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                description.Text = selectCourse;
                dataGrid.ItemsSource = ApplicationA.Instance.Courses;
            }
            else
            {
                description.Text = selectStudent;
                dataGrid.ItemsSource = ApplicationA.Instance.Students;
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                Course selectedCourse = (Course)dataGrid.SelectedItem;
                if(selectedCourse == null)
                {
                    MessageBox.Show("Morate da selektujete red u tabeli da biste ga preuzeli!");
                }
                else
                {
                    if(MenuAddDecider == SelectFromMenuOrAddDecider.MENU)
                    {
                        MenuWindow.SearchCourse = selectedCourse;
                        MenuWindow.coursetb.Text = selectedCourse.StartDate.ToShortDateString() + "-" + selectedCourse.EndDate.ToShortDateString() + ", " + selectedCourse.Price.ToString();
                    }
                    else
                    {
                        AddEditWindow.Course = selectedCourse;
                        AddEditWindow.coursetb.Text = selectedCourse.StartDate.ToShortDateString() + "-" + selectedCourse.EndDate.ToShortDateString() + ", " + selectedCourse.Price.ToString();
                    }
                    Close();
                }
            }
            else
            {
                Student selectedStudent = (Student)dataGrid.SelectedItem;
                if (selectedStudent == null)
                {
                    MessageBox.Show("Morate da selektujete red u tabeli da biste ga preuzeli!");
                }
                else
                {
                    if (MenuAddDecider == SelectFromMenuOrAddDecider.MENU)
                    {
                        MenuWindow.SearchStudent = selectedStudent;
                        MenuWindow.studenttb.Text = selectedStudent.FirstName + " " + selectedStudent.LastName;
                    }
                    else
                    {
                        AddEditWindow.Student = selectedStudent;
                        AddEditWindow.studenttb.Text = selectedStudent.FirstName + " " + selectedStudent.LastName;
                    }
                    Close();
                }
            }
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                switch((string)e.Column.Header)
                {
                    case "Id":
                        e.Cancel = true;
                        break;
                    case "Language":
                        e.Cancel = true;
                        break;
                    case "CourseType":
                        e.Cancel = true;
                        break;
                    case "Price":
                        e.Column.Header = "Cena";
                        break;
                    case "ListOfStudents":
                        e.Cancel = true;
                        break;
                    case "Teacher":
                        e.Cancel = true;
                        break;
                    case "StartDate":
                        e.Column.Header = "Datum pocetka";
                        break;
                    case "EndDate":
                        e.Column.Header = "Datum kraja";
                        break;
                    case "Deleted":
                        e.Column.Header = "Obrisan";
                        break;
                    case "Error":
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                switch ((string)e.Column.Header)
                {
                    case "Id":
                        e.Cancel = true;
                        break;
                    case "FirstName":
                        e.Column.Header = "Ime";
                        break;
                    case "LastName":
                        e.Column.Header = "Prezime";
                        break;
                    case "Address":
                        e.Column.Header = "Adresa";
                        break;
                    case "Jmbg":
                        e.Column.Header = "JMBG";
                        break;
                    case "Deleted":
                        e.Column.Header = "Obrisan";
                        break;
                    case "ListOfPayments":
                        e.Cancel = true;
                        break;
                    case "ListOfCourses":
                        e.Cancel = true;
                        break;
                    case "Error":
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
