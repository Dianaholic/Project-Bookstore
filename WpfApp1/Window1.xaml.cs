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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Book _book;
        public Window1(Book book)
        {
            InitializeComponent();
            _book = book;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            float price = float.Parse(txtPrice.Text);
            int amount = int.Parse(txtAmount.Text);

            float totalPrice = price * amount;

            MessageBox.Show($"คุณได้ซื้อหนังสือ {title} จำนวน {amount} เล่ม ราคารวมทั้งหมดคือ {totalPrice} เรียบร้อยแล้ว");

            DialogResult = true;
            Close();
        }

    }
}
