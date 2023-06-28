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
    /// Interaction logic for OrderBook.xaml
    /// </summary>
    public partial class OrderBook : Window
    {
        private ObservableCollection<Book> books;
        public OrderBook()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            books = new ObservableCollection<Book>();

            using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
            {
                db.Open();

                string selectQuery = "SELECT * FROM Books";
                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, db))
                {
                    using (SqliteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int isbn = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string description = reader.GetString(2);
                            float price = reader.GetFloat(3);

                            books.Add(new Book { ISBN = isbn, Title = title, Description = description, Price = price });
                        }
                    }
                }
            }

            dataGrid.ItemsSource = books;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่เลือกใน Datagrid หรือไม่
            if (dataGrid.SelectedItem != null)
            {
                // รับข้อมูลของแถวที่เลือกจาก Datagrid
                Book selectedBook = (Book)dataGrid.SelectedItem;

                // สร้างหน้าต่าง EditBookWindow และส่งค่า selectedBook
                Window1 buyingWindow = new Window1(selectedBook);

                // กำหนดข้อมูลในหน้าต่าง EditBookWindow
                buyingWindow.txtTitle.Text = selectedBook.Title;
                buyingWindow.txtDescription.Text = selectedBook.Description;
                buyingWindow.txtPrice.Text = selectedBook.Price.ToString();

                // ตรวจสอบว่าผู้ใช้กดปุ่ม Save หรือไม่
                if (buyingWindow.ShowDialog() == true)
                {
                    // อัพเดทข้อมูลในแถวที่เลือก
                    selectedBook.Title = buyingWindow.txtTitle.Text;
                    selectedBook.Description = buyingWindow.txtDescription.Text;
                    selectedBook.Price = float.Parse(buyingWindow.txtPrice.Text);

                    // อัพเดทแสดงผลใน Datagrid
                    dataGrid.Items.Refresh();
                }
            }
        }
    }
}
