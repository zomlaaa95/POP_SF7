﻿using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            loadData();

            view = CollectionViewSource.GetDefaultView(ApplicationA.Instance.CourseTypes);
            dynamicdg.ItemsSource = view;
            dynamicdg.IsSynchronizedWithCurrentItem = true;
        }

        public void loadData()
        {
            if (ApplicationA.Instance.CourseTypes.Count() == 0) CourseType.Load();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType newCourseType = new CourseType();
            CourseTypeAddEdit add = new CourseTypeAddEdit(newCourseType, Decider.ADD);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
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
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj tip kursa?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {                   
                    selectedCourseType = view.CurrentItem as CourseType;
                    CourseType.Delete(selectedCourseType);
                    ApplicationA.Instance.CourseTypes.Remove(selectedCourseType);
                }
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
