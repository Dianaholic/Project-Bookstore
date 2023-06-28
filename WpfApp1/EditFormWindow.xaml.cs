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
    /// Interaction logic for EditFormWindow.xaml
    /// </summary>
    public partial class EditFormWindow : Window
    {
        private Customer _customer;

        public EditFormWindow(Customer customers)
        {
            InitializeComponent();
            _customer = customers;
        }



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // เชื่อมต่อฐานข้อมูล SQLite
            using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
            {
                db.Open();

                // สร้าง SQL query เพื่ออัพเดทข้อมูลในตาราง Books
                string updateQuery = "UPDATE Customers SET Customer_Name = @Customer_Name, Customer_Address = @Customer_Address, Customer_Email = @Customer_Email WHERE Customer_ID = @Customer_ID";
                using (SqliteCommand updateCommand = new SqliteCommand(updateQuery, db))
                {
                    // กำหนดค่าพารามิเตอร์ใน SQL query
                    updateCommand.Parameters.AddWithValue("@Customer_Name", txtTitle.Text);
                    updateCommand.Parameters.AddWithValue("@Customer_Address", txtDescription.Text);
                    updateCommand.Parameters.AddWithValue("@Customer_Email", txtPrice.Text);
                    updateCommand.Parameters.AddWithValue("@Customer_ID", _customer.Customer_ID);

                    // อัพเดทข้อมูลในฐานข้อมูล
                    updateCommand.ExecuteNonQuery();
                }
            }

            // ปิดหน้าต่าง EditBookWindow และส่งค่า true กลับไปยัง EditButton_Click
            DialogResult = true;
            Close();
        }
    }
}

