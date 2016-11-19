﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for LanguageCourseTypeAddEdit.xaml
    /// </summary>
    public partial class LanguageCourseTypeAddEdit : Window
    {
        public string labelAddLanguage = "Dodavanje novog jezika";
        public string labelEditLanguage = "Izmena postojeceg jezika";
        public string labelAddCourseType = "Dodavanje novog tipa kursa";
        public string labelEditCourseType = "Izmena postojeceg tipa kursa";

        public LanguageCourseTypeAddEdit()
        {
            InitializeComponent();
        }
    }
}