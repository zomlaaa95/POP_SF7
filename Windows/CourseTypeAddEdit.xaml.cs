﻿using POP_SF7.DB;
using POP_SF7.Windows;
using System.Linq;
using System.Windows;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for LanguageCourseTypeAddEdit.xaml
    /// </summary>
    public partial class CourseTypeAddEdit : Window
    {
        public CourseType CourseTypeC { get; set; }
        public Decider Decider { get; set; }

        public string labelAddCourseType = "Dodavanje novog tipa kursa";
        public string labelEditCourseType = "Izmena postojeceg tipa kursa";

        public CourseTypeAddEdit(CourseType courseType, Decider decider)
        {
            InitializeComponent();

            CourseTypeC = courseType;
            Decider = decider;

            DataContext = CourseTypeC;

            userControl.descriptionlbl.Text = (decider == Decider.ADD) ? labelAddCourseType : labelEditCourseType;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(userControl.nametb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    if (CourseTypeDAO.Add(CourseTypeC))
                    {
                        CourseTypeC.Id = ApplicationA.Instance.CourseTypes.Count() + 1;
                        ApplicationA.Instance.CourseTypes.Add(CourseTypeC);
                    }
                }
                else
                {
                    if (CourseTypeDAO.Edit(CourseTypeC))
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
