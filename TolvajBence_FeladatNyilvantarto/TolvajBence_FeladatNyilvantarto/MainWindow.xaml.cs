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
using System.IO;

namespace TolvajBence_FeladatNyilvantarto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            feladatok.ItemsSource = feladatokListaja;

        }
        List<CheckBox> feladatokListaja = new List<CheckBox>();

        private void uj_btn_Click(object sender, RoutedEventArgs e)
        {
            if (feladatSzovege.Text != "") { 
            CheckBox uj = new CheckBox();
            uj.Content = feladatSzovege.Text;
            uj.Checked += new RoutedEventHandler(CheckEvent);
            uj.Unchecked += new RoutedEventHandler(CheckEvent);
            feladatokListaja.Add(uj);
            feladatok.Items.Refresh();
            }
            
        }
        private static void CheckEvent(object sender, RoutedEventArgs e)
        {
            CheckBox vizsgalando = (CheckBox)sender;
            switch (vizsgalando.IsChecked)
            {
                case true:
                    vizsgalando.Foreground = Brushes.Gray;
                    vizsgalando.FontStyle = FontStyles.Italic;
                    break;
                case false:
                    vizsgalando.Foreground = Brushes.Black;
                    vizsgalando.FontStyle = FontStyles.Normal;
                    break;
            }
           
        }

        private void modosito_btn_Click(object sender, RoutedEventArgs e)
        {
            if (feladatok.SelectedItems.Count > 0 && feladatSzovege.Text!="")
            {
                feladatokListaja[feladatok.SelectedIndex].Content = feladatSzovege.Text;
                feladatok.Items.Refresh();
            }
            if (feladatok.SelectedItems.Count < 0)
            {
                MessageBox.Show("Nincs kijelölve egy feladat se!");
                return;
            }
            if (feladatSzovege.Text == "")
            {
                MessageBox.Show("Nincs szöveg megadva!");
                return;
            }

        }
    }
}
