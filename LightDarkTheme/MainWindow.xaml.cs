using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Iwwerall;

namespace LightDarkTheme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlgemeneFuncties AF = new AlgemeneFuncties();
        public App CurrentApplication { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ChangeTheme(object sender, RoutedEventArgs e)
        //{
        //    switch (int.Parse(((MenuItem)sender).Uid))
        //    {
        //        // light
        //        case 1: CurrentApplication.SetTheme(App.Theme.Light); break;
        //        // colourful light
        //        // dark
        //        case 2: CurrentApplication.SetTheme(App.Theme.Dark); break;
        //        // colourful dark
        //    }
        //    e.Handled = true;
        //}
    }
}
