using System.IO;
using System.Linq;
using System.Windows;


namespace PalmaEtterem
{

    public partial class MainWindow : Window
    {
        private List<Desszert> desszertek = new List<Desszert>();
        public MainWindow()
        {
            InitializeComponent();
            Beolvasas();
            LegolcsobbLegdragabb();
        }
        private void Beolvasas()
        {
            try
            {
                string[] lines = File.ReadAllLines(@"..\..\..\src\palma.txt");
                desszertek = lines.Select(line => new Desszert(line)).ToList();
            }
            catch
            {
                MessageBox.Show("Hiba");
            }
            Random rnd = new();
            int randomNap = rnd.Next(0, desszertek.Count);
            lblRandomDessert.Text = desszertek[randomNap].Nev;
        }
        private void LegolcsobbLegdragabb()
        {
            var mostExpensive = desszertek.OrderByDescending(d => d.Ar).First();
            var cheapest = desszertek.OrderBy(d => d.Ar).First();
            lblMostExpensive.Text = $"{mostExpensive.Nev}, {mostExpensive.Ar} Ft";
            lblCheapest.Text = $"{cheapest.Nev}, {cheapest.Ar} Ft";
        }
        private void ArAjanlat_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = lblType.Text;

            var filteredDesserts = desszertek.Where(d => d.Tipus.Equals(selectedType, StringComparison.OrdinalIgnoreCase)).ToList();
            if (filteredDesserts.Any())
            {

                File.WriteAllLines(@"..\..\..\src\ajanlat.txt", filteredDesserts.Select(d => $"{d.Nev};{d.Ar} Ft;{d.Egyseg}"));
                var averagePrice = filteredDesserts.Average(d => d.Ar);
                MessageBox.Show($"{filteredDesserts.Count} db desszertet írtunk ki.\nÁtlagár: {averagePrice} Ft");
            }
            else
            {
                MessageBox.Show("Nincs ilyen típusú desszertünk. Kérjük, válasszon mást!");
            }
        }
        private void UjDesszert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nev = txtName.Text;
                string tipus = txtType.Text;
                bool dijazott = chkAwardWinning.IsChecked ?? false;
                int ar = int.Parse(txtPrice.Text);
                string egyseg = txtUnit.Text;

                if (string.IsNullOrWhiteSpace(nev) || string.IsNullOrWhiteSpace(tipus) || string.IsNullOrWhiteSpace(egyseg) || ar <= 0)
                {
                    MessageBox.Show("Minden mezőt megfelelően ki kell tölteni!");
                    return;
                }

                var newDessert = new Desszert($"{nev};{tipus};{dijazott};{ar};{egyseg}");
                desszertek.Add(newDessert);
                File.AppendAllText(@"..\..\..\src\cuki.txt", $"{nev};{tipus};{dijazott};{ar};{egyseg}\n");
                MessageBox.Show("Az új desszert sikeresen hozzá lett adva.");
            }
            catch
            {
                MessageBox.Show("Hiba történt az adatok mentése közben.");
            }
        }
        private void Dijazott_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Dijnyertes = desszertek
                .Where(d => d.Dijazott)
                .GroupBy(d => new { d.Nev, d.Egyseg })
                .Count();
                MessageBox.Show($"{Dijnyertes} díjazott desszertek közül választhat.");
            }
            catch
            {
                MessageBox.Show("Hiba történt az adatok beolvasása közben.");
            }

        }
        private void SaveDessertList()
        {
            var uniqueDesserts = desszertek.GroupBy(d => d.Nev).Select(g => g.First()).ToList();
            var lines = uniqueDesserts.Select(d => $"{d.Nev} {d.Tipus}").ToList();
            File.WriteAllLines(@"..\..\..\src\lista.txt", lines);
            MessageBox.Show("A lista.txt fájl elkészült.");
        }
        private void SaveDessertStatistics()
        {
            var typeGroups = desszertek.GroupBy(d => d.Tipus)
                .Select(g => $"{g.Key} - {g.Count()}").ToList();
            File.WriteAllLines(@"..\..\..\src\stat.txt", typeGroups);
            MessageBox.Show("A stat.txt fájl elkészült.");
        }

    }
}


















/*
       Az „Új süti felvétele” nyomógomb hatására adja hozzá adatszerkezetéhez és a cuki.txt 
állományhoz a felhasználó által, a bemeneti vezérlő elemek segítségével megadott 
desszertet! Amennyiben valamelyik adat hiányzik vagy nem megfelelő formátumú, felugró 
ablakban küldjön hibaüzenetet, sikeres mentés esetén is ilyen módon tájékoztassa a 
felhasználót!


         
        private void SaveDessertList()
        {
            var uniqueDesserts = desszertek.GroupBy(d => d.Nev).Select(g => g.First()).ToList();
            var lines = uniqueDesserts.Select(d => $"{d.Nev} {d.Tipus}").ToList();
            File.WriteAllLines(@"..\..\..\src\lista.txt", lines);
            MessageBox.Show("A lista.txt fájl elkészült.");
        }
        private void SaveDessertStatistics()
        {
            var typeGroups = desszertek.GroupBy(d => d.Tipus)
                .Select(g => $"{g.Key} - {g.Count()}").ToList();
            File.WriteAllLines(@"..\..\..\src\stat.txt", typeGroups);
            MessageBox.Show("A stat.txt fájl elkészült.");
        }
*/