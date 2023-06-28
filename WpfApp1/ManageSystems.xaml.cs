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

namespace WpfApp1
{

    public partial class ManageSystems : Window
    {
        public ManageSystems()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            ManageBooks ManageBooks = new ManageBooks();
            ManageBooks.Show();

            this.Close();
        }

        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomer manageCustomers = new ManageCustomer();
            manageCustomers.Show();

            this.Close();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderBook orderBook = new OrderBook();
            orderBook.Show();

            this.Close();
        }
    }
}
