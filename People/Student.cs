﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public class Student : Person, ICloneable
    {
        public ObservableCollection<Payment> ListOfPayments { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }

        public Student() { }

        public Student(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfPayments = new ObservableCollection<Payment>();
            ListOfCourses = new ObservableCollection<Course>();
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand loadCommand = connection.CreateCommand();
                loadCommand.CommandText = @"Select * From Student;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(loadCommand);
                
                try
                {
                    dataAdapter.Fill(dataSet, "Student");

                    foreach (DataRow row in dataSet.Tables["Student"].Rows)
                    {
                        Student student = new Student();
                        student.Id = (int)row["Student_Id"];
                        student.FirstName = (string)row["Student_FirstName"];
                        student.LastName = (string)row["Student_LastName"];
                        student.Jmbg = (string)row["Student_Jmbg"];
                        student.Address = (string)row["Student_Address"];
                        student.Deleted = (bool)row["Student_Deleted"];

                        ApplicationA.Instance.Students.Add(student);
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + e.Number + " u liniji " + e.LineNumber);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + a.HResult);
                }
            }
        }

        public static void Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Insert Into Student Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted);";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", student.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", student.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", student.Deleted));

                try
                {
                    addCommand.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + e.Number + " u liniji " + e.LineNumber);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + a.HResult);
                }
            }
        }

        public static void Edit(Student student)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update Student Set Student_FirstName=@FirstName, Student_LastName=@LastName, Student_Jmbg=@Jmbg, Student_Address=@Address, Student_Deleted=@Deleted Where Student_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", student.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", student.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", student.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@Id", student.Id));

                try
                {
                    addCommand.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + e.Number + " u liniji " + e.LineNumber);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + a.HResult);
                }
            }
        }

        public static void Delete(Student student)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update Student Set Student_Deleted=1 Where Student_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Id", student.Id));

                try
                {
                    addCommand.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + e.Number + " u liniji " + e.LineNumber);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + "\n" + "Greska " + a.HResult);
                }
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            Student studentCopy = new Student();
            studentCopy.Id = Id;
            studentCopy.FirstName = FirstName;
            studentCopy.LastName = LastName;
            studentCopy.Jmbg = Jmbg;
            studentCopy.Address = Address;
            studentCopy.Deleted = Deleted;
            studentCopy.ListOfPayments = ListOfPayments;
            studentCopy.ListOfCourses = ListOfCourses;

            return studentCopy;
        }

        #endregion

    }
}
