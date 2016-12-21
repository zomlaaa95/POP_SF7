﻿
using POP_SF7.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public class CourseType : INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public CourseType() { }

        public CourseType(int id, string name, bool deleted)
        {
            Id = id;
            Name = name;
            Deleted = deleted;
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand loadCommand = connection.CreateCommand();
                loadCommand.CommandText = @"Select * From CourseType;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(loadCommand);

                try
                {
                    dataAdapter.Fill(dataSet, "CourseType");

                    foreach (DataRow row in dataSet.Tables["CourseType"].Rows)
                    {
                        CourseType type = new CourseType();
                        type.Id = (int)row["CourseType_Id"];
                        type.Name = (string)row["CourseType_Name"];
                        type.Deleted = (bool)row["CourseType_Deleted"];

                        ApplicationA.Instance.CourseTypes.Add(type);
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

        public static void Add(CourseType type)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Insert Into CourseType Values(@Name, @Deleted);";

                addCommand.Parameters.Add(new SqlParameter("@Name", type.Name));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", type.Deleted));

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

        public static void Edit(CourseType type)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update CourseType Set CourseType_Name=@Name, CourseType_Deleted=@Deleted Where CourseType_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Name", type.Name));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", type.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@Id", type.Id));

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

        public static void Delete(CourseType type)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update CourseType Set CourseType_Deleted=1 Where CourseType_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Id", type.Id));

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

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                return "";
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (ValidationHelper.EmptyField(Name)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Name, 30)) return ValidationHelper.returnMessageMaxLength(30);
                        break;
                }
                return "";
            }
        }

        #endregion 

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
            CourseType courseCopy = new CourseType();
            courseCopy.Id = Id;
            courseCopy.Name = Name;
            courseCopy.Deleted = Deleted;

            return courseCopy;
        }

        #endregion

    }
}
