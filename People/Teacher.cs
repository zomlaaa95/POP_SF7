﻿using System;
using System.Collections.ObjectModel;

namespace POP_SF7
{
    public class Teacher : Person, ICloneable
    {
        public ObservableCollection<Language> ListOfLanguages { get; set; }
        public ObservableCollection<Language> ListOfDeletedLanguages { get; set; }

        public ObservableCollection<Course> ListOfCourses { get; set; }
        public ObservableCollection<Course> ListOfDeletedCourses { get; set; }

        public string FullName { get; set; }

        public Teacher()
        {
            FirstName = ApplicationA.FILL_FIELD;
            LastName = ApplicationA.FILL_FIELD;
            Address = ApplicationA.FILL_FIELD;
            Jmbg = "1234567890123";
            ListOfCourses = new ObservableCollection<Course>();
            ListOfDeletedCourses = new ObservableCollection<Course>();
            ListOfLanguages = new ObservableCollection<Language>();
            ListOfDeletedLanguages = new ObservableCollection<Language>();
        }

        public Teacher(int id)
        {
            Id = id;
        }

        public Teacher(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfLanguages = new ObservableCollection<Language>();
            ListOfDeletedLanguages = new ObservableCollection<Language>();
            ListOfCourses = new ObservableCollection<Course>();
            ListOfDeletedCourses = new ObservableCollection<Course>();
            FullName = FirstName + " " + LastName;
        }

        #region ICloneable

        public object Clone()
        {
            Teacher teacherCopy = new Teacher();
            teacherCopy.Id = Id;
            teacherCopy.FirstName = FirstName;
            teacherCopy.LastName = LastName;
            teacherCopy.Jmbg = Jmbg;
            teacherCopy.Address = Address;
            teacherCopy.Deleted = Deleted;
            teacherCopy.ListOfLanguages = ListOfLanguages;
            teacherCopy.ListOfCourses = ListOfCourses;
            teacherCopy.ListOfDeletedLanguages = ListOfDeletedLanguages;
            teacherCopy.ListOfDeletedCourses = ListOfDeletedCourses;

            return teacherCopy;
        }

        #endregion

    }
}
