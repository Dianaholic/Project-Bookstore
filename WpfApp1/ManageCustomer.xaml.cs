using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ManageCustomer.xaml
    /// </summary>
    public partial class ManageCustomer : Window
    {
        private ObservableCollection<Customer> customers;
        public ManageCustomer()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            customers = new ObservableCollection<Customer>();

            using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
            {
                db.Open();

                string selectQuery = "SELECT * FROM Customers";
                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, db))
                {
                    using (SqliteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerid = reader.GetInt32(0);
                            string customername = reader.GetString(1);
                            string customeraddress = reader.GetString(2);
                            string customeremail = reader.GetString(3);

                            customers.Add(new Customer { Customer_ID = customerid, Customer_Name = customername, Customer_Address = customeraddress, Customer_Email = customeremail });
                        }
                    }
                }
            }

            dataGrid.ItemsSource = customers;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่เลือกใน Datagrid หรือไม่
            if (dataGrid.SelectedItem != null)
            {
                // รับข้อมูลของแถวที่เลือกจาก Datagrid
                Customer selectedCustomer = (Customer)dataGrid.SelectedItem;

                // สร้างหน้าต่าง EditBookWindow และส่งค่า selectedBook
                EditFormWindow editWindow = new EditFormWindow(selectedCustomer);

                // กำหนดข้อมูลในหน้าต่าง EditBookWindow
                editWindow.txtTitle.Text = selectedCustomer.Customer_Name;
                editWindow.txtDescription.Text = selectedCustomer.Customer_Address;
                editWindow.txtPrice.Text = selectedCustomer.Customer_Email;

                // ตรวจสอบว่าผู้ใช้กดปุ่ม Save หรือไม่
                if (editWindow.ShowDialog() == true)
                {
                    // อัพเดทข้อมูลในแถวที่เลือก
                    selectedCustomer.Customer_Name = editWindow.txtTitle.Text;
                    selectedCustomer.Customer_Address = editWindow.txtDescription.Text;
                    selectedCustomer.Customer_Email =editWindow.txtPrice.Text;

                    // อัพเดทแสดงผลใน Datagrid
                    dataGrid.Items.Refresh();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            DataGridRow dataGridRow = FindVisualParent<DataGridRow>(deleteButton);

            if (dataGridRow != null)
            {
                Customer selectedCustomer = (Customer)dataGridRow.Item;

                // เรียกใช้งานฐานข้อมูล SQLite เพื่อลบข้อมูล
                using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
                {
                    db.Open();

                    string deleteQuery = "DELETE FROM Customers WHERE Customer_ID = @Customer_ID";
                    using (SqliteCommand deleteCommand = new SqliteCommand(deleteQuery, db))
                    {
                        deleteCommand.Parameters.AddWithValue("@Customer_ID", selectedCustomer.Customer_ID);
                        deleteCommand.ExecuteNonQuery();
                    }
                }

                // ลบ Book ที่ถูกเลือกออกจาก ObservableCollection
                customers.Remove(selectedCustomer);
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.ShowDialog();

            LoadData();
        }

    }

    public class Customer
    {
        public int Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Email { get; set; }
    }
}
