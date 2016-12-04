﻿using POP_SF7.Models.Courses;
using POP_SF7.Models.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace POP_SF7.Models.School
{
    public class School : INotifyPropertyChanged, ICloneable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string webSite;
        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; OnPropertyChanged("WebSite"); }
        }

        private string pib;
        public string Pib
        {
            get { return pib; }
            set { pib = value; OnPropertyChanged("Pib"); }
        }

        private string identificationNumber;
        public string IdentificationNumber
        {
            get { return identificationNumber; }
            set { identificationNumber = value; OnPropertyChanged("IdentificationNumber"); }
        }

        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; OnPropertyChanged("AccountNumber"); }
        }

        public List<Course> ListOfCourses { get; set; }
        public List<CourseType> ListOfCourseTypes { get; set; }
        public List<Language> ListOfLanguages { get; set; }
        public List<Payment> ListOfPayments { get; set; }
        public List<User> ListOfUsers { get; set; }
        public List<Teacher> ListOfTeachers { get; set; }
        public List<Student> ListOfStudents { get; set; }

        public School() { }

        public School(string name, string address, string phoneNumber, string email, string webSite, string PIB, string identificationNumber, string accountNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            WebSite = webSite;
            Pib = PIB;
            IdentificationNumber = identificationNumber;
            AccountNumber = accountNumber;
            ListOfCourses = new List<Course>();
            ListOfCourseTypes = new List<CourseType>();
            ListOfLanguages = new List<Language>();
            ListOfPayments = new List<Payment>();
            ListOfUsers = new List<User>();
            ListOfTeachers = new List<Teacher>();
            ListOfStudents = new List<Student>();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            School schoolCopy = new School();
            schoolCopy.Name = Name;
            schoolCopy.Address = Address;
            schoolCopy.PhoneNumber = PhoneNumber;
            schoolCopy.Email = Email;
            schoolCopy.WebSite = WebSite;
            schoolCopy.Pib = Pib;
            schoolCopy.IdentificationNumber = IdentificationNumber;
            schoolCopy.AccountNumber = AccountNumber;

            return schoolCopy;
        }

        #endregion
    }
}