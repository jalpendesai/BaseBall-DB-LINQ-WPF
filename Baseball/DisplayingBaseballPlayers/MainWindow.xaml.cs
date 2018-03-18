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
using BaseballLibrary;
using System.Data.Entity;

namespace DisplayingBaseballPlayers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseballEntities db = new BaseballEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

             System.Windows.Data.CollectionViewSource playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // playerViewSource.Source = [generic data source]

            //db.Players.OrderBy(players => players.LastName).ThenBy(players => players.FirstName).Load();
            //Loading data into Memory
            db.Players.Load();
            playerDataGrid.ItemsSource = db.Players.Local;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            playerDataGrid.ItemsSource = db.Players.Local;
            btnBack.Visibility = Visibility.Hidden;
            errortxt.Text = "";
            txtLastName.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Checking if input String is Empty
            if(txtLastName.Text == null)
            {
                errortxt.Text = "Enter lastName";
            }

            //Check if the input String is in DataBase
            var errorCheck = (db.Players.Any(n => n.LastName.ToLower() == txtLastName.Text.ToLower()));
            if (errorCheck)
            {
                errortxt.Text = "Success";
            }
            else
            {
                
                errortxt.Text = "Sorry! Cannot Find";
            }

           //Displaying Player Details
                playerDataGrid.ItemsSource = from baseball in db.Players.Local
                                             where baseball.LastName.ToLower() == txtLastName.Text.ToLower()
                                             orderby baseball.FirstName
                                             select baseball;
                
           
            
            txtLastName.Text = "";
            btnBack.Visibility = Visibility.Visible;
        }
    }
}

//-Jalpen Desai