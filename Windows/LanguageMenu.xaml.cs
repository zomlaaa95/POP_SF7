﻿using System;
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

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for LanguageMenu.xaml
    /// </summary>
    public partial class LanguageMenu : Window
    {
        public ICollectionView view { get; set; }
        public ObservableCollection<Language> ListOfLanguages { get; set; }

        public LanguageMenu()
        {
            InitializeComponent();
            // ListOfLanguages = pa ucitavanje iz baze
            ListOfLanguages = new ObservableCollection<Language>();

            view = CollectionViewSource.GetDefaultView(ListOfLanguages);

            dynamicdg.ItemsSource = view;
            dynamicdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Language newLanguage = new Language();
            LanguageAddEdit add = new LanguageAddEdit(newLanguage, Decider.ADD, ListOfLanguages);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = (Language)dynamicdg.SelectedItem;
            if (selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili jezik!");
            }
            else
            {
                Language backup = (Language) selectedLanguage.Clone();
                LanguageAddEdit edit = new LanguageAddEdit(selectedLanguage, Decider.EDIT, ListOfLanguages);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfLanguages.IndexOf(selectedLanguage);
                    ListOfLanguages[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = (Language)dynamicdg.SelectedItem;
            if (selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili jezik!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj jezik?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedLanguage = view.CurrentItem as Language;
                    ListOfLanguages.Remove(selectedLanguage);
                }
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}