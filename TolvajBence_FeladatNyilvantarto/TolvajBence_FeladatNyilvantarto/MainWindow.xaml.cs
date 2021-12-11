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
            Application.Current.Exit += Application_Exit;
            Betoltes();
            feladatok.ItemsSource = feladatokListaja;
            toroltFeladatok.ItemsSource = toroltFeladatokListaja;


        }

        private void Betoltes()
        {
            foreach (var x in File.ReadAllLines(@"feladatok.txt"))
            {
                string[] tempArray = x.Split(" ");
                CheckBox tempCheckbox = new CheckBox();
                tempCheckbox.Content = tempArray[0];
                if (tempArray[1] == "False")
                {
                    tempCheckbox.IsChecked = false;
                }
                else
                {
                    tempCheckbox.IsChecked = true;
                    tempCheckbox.Foreground = Brushes.Gray;
                    tempCheckbox.FontStyle = FontStyles.Italic;
                }
                feladatokListaja.Add(tempCheckbox);


            }
            foreach (var x in File.ReadAllLines(@"toroltFeladatok.txt"))
            {
                string[] tempArray = x.Split(" ");
                CheckBox tempCheckbox = new CheckBox();
                tempCheckbox.Content = tempArray[0];
                if (tempArray[1] == "False")
                {
                    tempCheckbox.IsChecked = false;
                }
                else
                {
                    tempCheckbox.IsChecked = true;
                    tempCheckbox.Foreground = Brushes.Red;
                }
                toroltFeladatokListaja.Add(tempCheckbox);


            }
        }

        static List<CheckBox> feladatokListaja = new List<CheckBox>();
        static List<CheckBox> toroltFeladatokListaja = new List<CheckBox>();

        private void uj_btn_Click(object sender, RoutedEventArgs e)
        {
            if (feladatSzovege.Text != "") { 
            CheckBox uj = new CheckBox();
            uj.Checked += new RoutedEventHandler(CheckEvent);
            uj.Unchecked += new RoutedEventHandler(CheckEvent);
            uj.Content = feladatSzovege.Text;
           
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

        private void torol_btn_Click(object sender, RoutedEventArgs e)
        {
            if (feladatok.SelectedItems.Count > 0)
            {
                toroltFeladatokListaja.Add(feladatokListaja[feladatok.SelectedIndex]);
                feladatokListaja.Remove(feladatokListaja[feladatok.SelectedIndex]);
                feladatok.Items.Refresh();
                toroltFeladatok.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Nincs kijelölve egy feladat se!");

            }
        }

        private void visszaallit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (toroltFeladatok.SelectedItems.Count > 0)
            {
                feladatokListaja.Add(toroltFeladatokListaja[toroltFeladatok.SelectedIndex]);
                toroltFeladatokListaja.Remove(toroltFeladatokListaja[toroltFeladatok.SelectedIndex]);
                feladatok.Items.Refresh();
                toroltFeladatok.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Nincs kijelölve egy feladat se!");

            }
        }

        private void veglegTorol_btn_Click(object sender, RoutedEventArgs e)
        {
            if (toroltFeladatok.SelectedItems.Count > 0)
            {
                toroltFeladatokListaja.Remove(toroltFeladatokListaja[toroltFeladatok.SelectedIndex]);
                toroltFeladatok.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Nincs kijelölve egy feladat se!");

            }
        }
        private static void FeladatTxtFrissites(string fajlnev, List<CheckBox> lista)
        {
            using (TextWriter tw = new StreamWriter(@fajlnev))
            {
                foreach (var x in lista)
                {
                    tw.WriteLine(x.Content + " " + x.IsChecked);
                }
            }
        }
        private static void Application_Exit(object sender, ExitEventArgs e)
        {

            FeladatTxtFrissites("feladatok.txt", feladatokListaja);
            FeladatTxtFrissites("toroltFeladatok.txt", toroltFeladatokListaja);
        }
       
    }
}
