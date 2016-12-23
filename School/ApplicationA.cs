﻿using System.Collections.ObjectModel;

namespace POP_SF7
{
    public class ApplicationA
    {
        public const string CONNECTION_STRING = @"Integrated Security=SSPI;
                                          Initial Catalog=TheLanguageSchool;
                                          Data Source=DUSAN\SQLEXPRESS";

        public const string DATABASE_ERROR_MESSAGE = "Doslo je do greske sa bazom!" + "\n" + "Greska ";

        public School School { get; set; }
        public int UserId { get; set; }

        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<CourseType> CourseTypes { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Payment> Payments { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        private static ApplicationA instance = new ApplicationA();

        public static ApplicationA Instance
        {
            get
            {
                return instance;
            }
        }

        private ApplicationA()
        {
            School = School.LoadSchool();
            Languages = new ObservableCollection<Language>();
            CourseTypes = new ObservableCollection<CourseType>();
            Courses = new ObservableCollection<Course>();
            Payments = new ObservableCollection<Payment>();
            Teachers = new ObservableCollection<Teacher>();
            Users = new ObservableCollection<User>();
            Students = new ObservableCollection<Student>();
        }

    }
}
