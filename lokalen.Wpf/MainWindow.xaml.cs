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
using lokalen.Lib;

namespace lokalen.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Lokaal> lokalen;
        bool isNieuw;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lokalen = new List<Lokaal>();
            BeeldNormaal();
            VulVerdiepingen();
            VulVleugels();
        }

        private void BeeldNormaal()
        {
            grpDetails.IsEnabled = false;
            btnBewaren.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
            btnNieuw.Visibility = Visibility.Visible;
            btnWijzig.Visibility = Visibility.Visible;
            btnVerwijder.Visibility = Visibility.Visible;
            lstLokalen.IsEnabled = true;
        }
        private void BeeldBewerking()
        {
            grpDetails.IsEnabled = true;
            btnBewaren.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
            btnNieuw.Visibility = Visibility.Hidden;
            btnWijzig.Visibility = Visibility.Hidden;
            btnVerwijder.Visibility = Visibility.Hidden;
            lstLokalen.IsEnabled = false;
        }
        private void VulVerdiepingen()
        {
            cmbVerdieping.Items.Add(-1);
            cmbVerdieping.Items.Add(0);
            cmbVerdieping.Items.Add(1);
            cmbVerdieping.Items.Add(2);
        }
        private void VulVleugels()
        {
            cmbVleugel.Items.Add('A');
            cmbVleugel.Items.Add('B');
            cmbVleugel.Items.Add('C');
            cmbVleugel.Items.Add('D');
        }

        private void VulLstLokalen()
        {
            lstLokalen.Items.Clear();
            foreach(Lokaal lok in lokalen)
            {
                lstLokalen.Items.Add(lok);
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            isNieuw = true;
            BeeldBewerking();
            txtNaam.Text = "";
            cmbVerdieping.SelectedItem = 0;
            cmbVleugel.SelectedItem = 'A';
            txtPlaatsen.Text = "1";
            chkInformaticalokaal.IsChecked = false;
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            BeeldNormaal();
        }

        private void btnBewaren_Click(object sender, RoutedEventArgs e)
        {
            // waarden uitlezen en eventueel reageren bij fouten
            string naam = txtNaam.Text.Trim();
            if(naam.Length == 0)
            {
                MessageBox.Show("Naam invoeren aub");
                txtNaam.Focus();
                return;
            }
            sbyte verdieping = sbyte.Parse(cmbVerdieping.SelectedItem.ToString());
            char vleugel = char.Parse(cmbVleugel.SelectedItem.ToString());
            int plaatsen = 1;
            try
            {
                 plaatsen = int.Parse(txtPlaatsen.Text);
            }
            catch
            {
                MessageBox.Show("Geldig aantal plaatsen invoeren aub (1-250)");
                txtPlaatsen.Text = "1";
                txtPlaatsen.Focus();
                txtPlaatsen.SelectAll();
                return;
            }
            if(plaatsen < 1 || plaatsen > 250)
            {
                MessageBox.Show("Geldig aantal plaatsen invoeren aub (1-250)");
                txtPlaatsen.Focus();
                txtPlaatsen.SelectAll();
                return;
            }
            bool informaticaLokaal = (bool)chkInformaticalokaal.IsChecked;

            if(isNieuw)
            {
                // nieuw object aanmaken indien het gaat om een nieuw lokaal
                Lokaal lok = new Lokaal(naam, verdieping, vleugel, plaatsen, informaticaLokaal);
                lokalen.Add(lok);
            }
            else
            {
                // object aanpassen bij wijziging
                Lokaal lok = (Lokaal)lstLokalen.SelectedItem;
                lok.Naam = naam;
                lok.Verdieping = verdieping;
                lok.Vleugel = vleugel;
                lok.Plaatsen = plaatsen;
                lok.Informaticalokaal = informaticaLokaal;

            }
            VulLstLokalen();
            BeeldNormaal();
        }

        private void lstLokalen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNaam.Text = "";
            cmbVerdieping.SelectedItem = 0;
            cmbVleugel.SelectedItem = 'A';
            txtPlaatsen.Text = "1";
            chkInformaticalokaal.IsChecked = false;

            if (lstLokalen.SelectedIndex == -1) return;


            Lokaal lok = (Lokaal)lstLokalen.SelectedItem;
            txtNaam.Text = lok.Naam;
            cmbVerdieping.SelectedItem = lok.Verdieping;
            cmbVleugel.SelectedItem = lok.Vleugel;
            txtPlaatsen.Text = lok.Plaatsen.ToString();
            chkInformaticalokaal.IsChecked = lok.Informaticalokaal;

        }

        private void btnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstLokalen.SelectedIndex == -1)
                return;
            isNieuw = false;
            BeeldBewerking();
            txtNaam.Focus();
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (lstLokalen.SelectedIndex == -1)
                return;
            Lokaal lok = (Lokaal)lstLokalen.SelectedItem;
            lokalen.Remove(lok);
            VulLstLokalen();
        }
    }
}
