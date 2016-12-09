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
    /// Interaction logic for PaymentMenu.xaml
    /// </summary>
    public partial class PaymentMenu : Window
    {
        public ICollectionView view { get; set; }
        public ObservableCollection<Payment> ListOfPayments { get; set; }

        public List<Course> ListOfCourses { get; set; }
        public List<Student> ListOfStudents { get; set; }

        public PaymentMenu()
        {
            InitializeComponent();
            // ucitavanje uplata iz baze
            view = CollectionViewSource.GetDefaultView(ListOfPayments);

            paymentsdg.ItemsSource = view;
            paymentsdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Payment newPayment = new Payment();
            PaymentAddEdit addPayment = new PaymentAddEdit(newPayment, Decider.ADD, ListOfPayments);
            addPayment.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Payment selectedPayment = view.CurrentItem as Payment;
            if(selectedPayment == null)
            {
                MessageBox.Show("Morate da selektujete jednu uplatu da biste je izmenili!");
            }
            else
            {
                Payment backup = (Payment)selectedPayment.Clone();
                PaymentAddEdit edit = new PaymentAddEdit(selectedPayment, Decider.EDIT, ListOfPayments);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfPayments.IndexOf(selectedPayment);
                    ListOfPayments[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Payment selectedPayment = view.CurrentItem as Payment;
            if (selectedPayment == null)
            {
                MessageBox.Show("Morate da selektujete jednu uplatu da biste je izmenili!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovu uplatu?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedPayment = view.CurrentItem as Payment;
                    ListOfPayments.Remove(selectedPayment);
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;

            if (idrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (amountrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (studentidrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (daterb.IsChecked ?? false) ; //funkcija za sortiranje
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool course = coursechb.IsChecked ?? false;
            bool student = studentchb.IsChecked ?? false;

            if (course && student)
            {
                // pokupi podatke iz oba textboxa
                // funkcija
            }
            else if (course)
            {
                // pokupi podatke iz textboxa za kurs
                // funkcija
            }
            else if (student)
            {
                // pokupi podatke iz textboxa za ucenika
                // funkcija
            }
            else
            {
                MessageBox.Show("Morate da otkacite jedan ili oba kriterijuma da biste pretrazili kurseve!");
            }
        }
    }
}
