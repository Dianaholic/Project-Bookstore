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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ManageBooks.xaml
    /// </summary>
    public partial class ManageBooks : Window
    {
        private ObservableCollection<Book> books;

        public ManageBooks()
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // ตรวจสอบว่ามีแถวที่เลือกใน Datagrid หรือไม่
            if (dataGrid.SelectedItem != null)
            {
                // รับข้อมูลของแถวที่เลือกจาก Datagrid
                Book selectedBook = (Book)dataGrid.SelectedItem;

                // สร้างหน้าต่าง EditBookWindow และส่งค่า selectedBook
                EditBookWindow editWindow = new EditBookWindow(selectedBook);

                // กำหนดข้อมูลในหน้าต่าง EditBookWindow
                editWindow.txtTitle.Text = selectedBook.Title;
                editWindow.txtDescription.Text = selectedBook.Description;
                editWindow.txtPrice.Text = selectedBook.Price.ToString();

                // ตรวจสอบว่าผู้ใช้กดปุ่ม Save หรือไม่
                if (editWindow.ShowDialog() == true)
                {
                    // อัพเดทข้อมูลในแถวที่เลือก
                    selectedBook.Title = editWindow.txtTitle.Text;
                    selectedBook.Description = editWindow.txtDescription.Text;
                    selectedBook.Price = float.Parse(editWindow.txtPrice.Text);

                    // อัพเดทแสดงผลใน Datagrid
                    dataGrid.Items.Refresh();
                }
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            DataGridRow dataGridRow = FindVisualParent<DataGridRow>(deleteButton);

            if (dataGridRow != null)
            {
                Book selectedBook = (Book)dataGridRow.Item;

                // เรียกใช้งานฐานข้อมูล SQLite เพื่อลบข้อมูล
                using (SqliteConnection db = new SqliteConnection("Data Source=sqliteSample.db"))
                {
                    db.Open();

                    string deleteQuery = "DELETE FROM Books WHERE ISBN = @isbn";
                    using (SqliteCommand deleteCommand = new SqliteCommand(deleteQuery, db))
                    {
                        deleteCommand.Parameters.AddWithValue("@isbn", selectedBook.ISBN);
                        deleteCommand.ExecuteNonQuery();
                    }
                }

                // ลบ Book ที่ถูกเลือกออกจาก ObservableCollection
                books.Remove(selectedBook);
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



        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();

            LoadData();
        }
    }

    public class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
