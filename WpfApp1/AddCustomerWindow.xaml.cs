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
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            string price = txtPrice.Text;

            using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
            {
                db.Open();

                string insertQuery = "INSERT INTO Customers (Customer_Name, Customer_Address, Customer_Email) VALUES (@Customer_Name, @Customer_Address, @Customer_Email)";
                using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, db))
                {
                    insertCommand.Parameters.AddWithValue("@Customer_Name", title);
                    insertCommand.Parameters.AddWithValue("@Customer_Address", description);
                    insertCommand.Parameters.AddWithValue("@Customer_Email", price);

                    insertCommand.ExecuteNonQuery();
                }
            }
            Close();
        }
    }
}
