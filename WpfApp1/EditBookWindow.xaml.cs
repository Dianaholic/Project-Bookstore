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
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private Book _book;

        public EditBookWindow(Book book)
        {
            InitializeComponent();
            _book = book;
        }



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // เชื่อมต่อฐานข้อมูล SQLite
            using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
            {
                db.Open();

                // สร้าง SQL query เพื่ออัพเดทข้อมูลในตาราง Books
                string updateQuery = "UPDATE Books SET Title = @Title, Description = @Description, Price = @Price WHERE ISBN = @ISBN";
                using (SqliteCommand updateCommand = new SqliteCommand(updateQuery, db))
                {
                    // กำหนดค่าพารามิเตอร์ใน SQL query
                    updateCommand.Parameters.AddWithValue("@Title", txtTitle.Text);
                    updateCommand.Parameters.AddWithValue("@Description", txtDescription.Text);
                    updateCommand.Parameters.AddWithValue("@Price", float.Parse(txtPrice.Text));
                    updateCommand.Parameters.AddWithValue("@ISBN", _book.ISBN);

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
