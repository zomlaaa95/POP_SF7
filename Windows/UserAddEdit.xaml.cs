﻿using POP_SF7.DB;
using POP_SF7.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class UserAddEdit : Window
    {
        public User UserU { get; set; }
        public Decider Decider { get; set; }
        public string UserName { get; set; }

        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelEditUser = "Izmena postojeceg korisnika";
        
        public UserAddEdit(User userU, Decider decider)
        {
            InitializeComponent();
            UserU = userU;
            Decider = decider;
            DataContext = UserU;

            personInfo.descriptionlbl.Text = (decider == Decider.ADD) ? labelAddUser : labelEditUser;
            UserName = (decider == Decider.EDIT) ? UserU.UserName : "";
            setRadioButton();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = checkUsername();
            bool deletingSelf = checkSelfDeletion();

            if(personInfo.nametb.Text.Equals("") || personInfo.lastnametb.Text.Equals("") || personInfo.addresstb.Text.Equals("") || personInfo.jmbgtb.Text.Equals("") || usernametb.Text.Equals("") || passwordtb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else if (deletingSelf)
            {
                MessageBox.Show("Ne mozete da obrisete sebe!");
                personInfo.deletedcb.IsChecked = false;
            }
            else if(valid)
            {
                if (Decider == Decider.ADD)
                {
                    if (UserDAO.Add(UserU))
                    {
                        UserU.Id = ApplicationA.Instance.Users.Count() + 1;
                        ApplicationA.Instance.Users.Add(UserU);
                    }
                    else
                    {
                        DialogResult = false;
                    }
                    Close();
                }
                else
                {   
                    if(UserDAO.Edit(UserU))
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        cancelbtn_Click(null, null);
                    }
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Postoji korisnik sa unetim korisnickim imenom! Unesite neko drugo.");
                usernametb.Text = "";
            }
        }

        private bool checkUsername()
        {
            bool valid = true;
            foreach (User u in ApplicationA.Instance.Users)
            {
                if (u.UserName.Equals(UserU.UserName))
                {
                    if(Decider == Decider.EDIT)
                    {
                        if(!UserU.UserName.Equals(UserName) || UserU.Id != u.Id)
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }
            }
            return valid;
        }

        private bool checkSelfDeletion()
        {
            if(UserU.Id == ApplicationA.Instance.UserId && UserU.Deleted == true)
            {
                return true;
            }
            return false;
        }
        
        private void setRadioButton()
        {
            if(Decider == Decider.EDIT)
            {
                if (UserU.UserRole == Role.ADMINISTRATOR)
                {
                    administratorrb.IsChecked = true;
                }
                else
                {
                    employeerb.IsChecked = true;
                }
            }
            else
            {
                UserU.UserRole = Role.ADMINISTRATOR;
            }
        }

        private void administratorrb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            try
            {
                UserU.UserRole = (rb.Name.Equals("administratorrb")) ? Role.ADMINISTRATOR : Role.EMPLOYEE;
            }
            catch(NullReferenceException a)
            {
                Console.WriteLine(a.StackTrace);
            }     
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
