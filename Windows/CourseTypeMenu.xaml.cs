﻿using POP_SF7.DB;
using POP_SF7.Helpers;
using POP_SF7.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for LanguagesCourseTypesMenu.xaml
    /// </summary>
    /// 

    public partial class CourseTypeMenu : Window
    {
        public ICollectionView view{ get; set; }

        public CourseTypeMenu()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(ApplicationA.Instance.CourseTypes);
            dynamicdg.ItemsSource = view;
            dynamicdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType newCourseType = new CourseType();
            CourseTypeAddEdit add = new CourseTypeAddEdit(newCourseType, Decider.ADD);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType selectedCourseType =(CourseType) dynamicdg.SelectedItem;
            if(selectedCourseType == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili tip kursa!");
            }
            else
            {
                CourseType backup = (CourseType)selectedCourseType.Clone();
           
                CourseTypeAddEdit edit = new CourseTypeAddEdit(selectedCourseType, Decider.EDIT);
                if(edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.CourseTypes.IndexOf(selectedCourseType);
                    ApplicationA.Instance.CourseTypes[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
            if (selectedCourseType == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili tip kursa!");
            }
            else if(selectedCourseType.Deleted == true)
            {
                MessageBox.Show("Selektovani tip kursa je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj tip kursa?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {                   
                    selectedCourseType = view.CurrentItem as CourseType;
                    if(CourseTypeDAO.Delete(selectedCourseType))
                    {
                        selectedCourseType.Deleted = true;
                    }
                    
                }
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dynamicdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourseType(e);
        }
    }
}
