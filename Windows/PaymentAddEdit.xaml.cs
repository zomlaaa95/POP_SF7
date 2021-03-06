﻿using POP_SF7.DB;
using POP_SF7.Helpers;
using POP_SF7.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PaymentAddEdit.xaml
    /// </summary>
    public partial class PaymentAddEdit : Window
    {
        public ICollectionView PaymentsView { get; set; }

        public double Paid { get; set; }
        public double LeftToPay { get; set; }

        public Payment SelectedPayment { get; set; }
        public Decider Decider { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

        public string labelAddPayment = "Dodavanje nove uplate";
        public string labelEditPayment = "Izmena postojece uplate";

        public bool paymentsLoaded = false;

        public PaymentAddEdit(Payment payment, Decider decider)
        {
            InitializeComponent();
            SelectedPayment = payment;
            Decider = decider;

            paidtb.DataContext = Paid;
            lefttb.DataContext = LeftToPay;

            DataContext = SelectedPayment;
            descriptionlbl.Text = (payment == null) ? labelAddPayment : labelEditPayment;
            setupCourseAndStudent();
        }

        public PaymentAddEdit(Payment payment, Student student)
        {
            InitializeComponent();
            SelectedPayment = payment;
            Decider = Decider.ADD;
            Student = student;
            studenttb.Text = Student.FirstName + " " + Student.LastName;

            paidtb.DataContext = Paid;
            lefttb.DataContext = LeftToPay;

            DataContext = SelectedPayment;
            descriptionlbl.Text = (payment == null) ? labelAddPayment : labelEditPayment;
            setupCourseAndStudent();
        }

        private void setupCourseAndStudent()
        {
            if (Decider == Decider.EDIT)
            {
                paymentsLoaded = true;

                PaymentsView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Payments);
                paymentsdg.ItemsSource = PaymentsView;
                paymentsdg.IsSynchronizedWithCurrentItem = true;

                Course = ApplicationA.Instance.Courses[SelectedPayment.Course.Id - 1];
                Student = ApplicationA.Instance.Students[SelectedPayment.Student.Id - 1];

                studenttb.Text = Student.FirstName + " " + Student.LastName;
                coursetb.Text = Course.StartDate.ToShortDateString() + "-" + Course.EndDate.ToShortDateString() + ", " + Course.Price.ToString();
                setupPaidAndLeft();
            }
            else
            {
                coursebtn.IsEnabled = false;
                paymentsLoaded = false;
            }
        }

        private void setupPaidAndLeft()
        {
            Paid = 0; LeftToPay = 0;
            PaymentsView.Filter = new Predicate<object>(courseSearchCondition) + new Predicate<object>(studentSearchCondition);
            foreach (Payment p in PaymentsView)
            {
                Paid += p.Amount;
            }
            LeftToPay = Course.Price - Paid;
            paidtb.Text = Paid.ToString();
            lefttb.Text = LeftToPay.ToString();
        }

        private bool studentSearchCondition(object s)
        {
            Payment p = s as Payment;
            return p.Student.Id == Student.Id;
        }

        private bool courseSearchCondition(object s)
        {
            Payment p = s as Payment;
            return p.Course.Id == Course.Id;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            bool valid = Double.TryParse(amounttb.Text, out amount);

            if(string.IsNullOrEmpty(coursetb.Text) || string.IsNullOrEmpty(studenttb.Text) || string.IsNullOrEmpty(amounttb.Text))
            {
                MessageBox.Show("Morate da popunite sva polja!");
            }
            else if(!valid)
            {
                MessageBox.Show("Morate da unesete brojeve za iznos!");
                amounttb.Text = 0.ToString();
            }
            else if (amount > LeftToPay)
            {
                MessageBox.Show("Iznos uplate ne moze biti veci od preostalog iznosa za uplatu!");
                amounttb.Text = 0.ToString();
            }
            else
            {
                SelectedPayment.Course = Course;
                SelectedPayment.Student = Student;

                if (Decider == Decider.ADD)
                {
                    if(PaymentDAO.Add(SelectedPayment))
                    {
                        SelectedPayment.Id = ApplicationA.Instance.Payments.Count() + 1;
                        ApplicationA.Instance.Payments.Add(SelectedPayment);
                    }
                }
                else
                {
                    if(PaymentDAO.Edit(SelectedPayment))
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

        private void coursebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.ADD, CourseStudentDecider.COURSE, this);
            window.Show();
        }

        private void studentbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.ADD, CourseStudentDecider.STUDENT, this);
            window.Show();
        }

        private void studenttb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(!paymentsLoaded)
            {
                PaymentsView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Payments);
                paymentsdg.ItemsSource = PaymentsView;
                paymentsdg.IsSynchronizedWithCurrentItem = true;
            }
            try
            {
                if (Student.ListOfCourses.Count == 0)
                {
                    MessageBox.Show("Izabrani student ne pohadja nijedan kurs!");
                    studenttb.Text = "";
                    coursebtn.IsEnabled = false;
                }
                else
                {
                    coursebtn.IsEnabled = true;
                    coursetb.Text = "";
                }
                PaymentsView.Filter = new Predicate<object>(studentSearchCondition);
                paidtb.Text = "";
                lefttb.Text = "";
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }

        private void paymentsdg_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadPayment(e);
        }

        private void coursetb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                Paid = 0; LeftToPay = 0;
                PaymentsView.Filter = new Predicate<object>(courseSearchCondition);
                foreach (Payment p in PaymentsView)
                {
                    Paid += p.Amount;
                }
                LeftToPay = Course.Price - Paid;
                paidtb.Text = Paid.ToString();
                lefttb.Text = LeftToPay.ToString();
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }

        private void ClosingFunction(object sender, CancelEventArgs e)
        {
            try
            {
                PaymentsView.Filter = null;
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
