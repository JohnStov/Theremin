﻿using System.Windows.Controls;

namespace Theremin.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            DataContext = new HomeViewModel();
        }
    }
}
