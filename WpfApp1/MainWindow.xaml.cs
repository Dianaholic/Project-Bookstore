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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (IsValidLogin(username, password))
            {
                MessageBox.Show("[ระบบการแจ้งเตือน] คุณได้เข้าสู่ระบบเรียบร้อยแล้ว");

                ManageSystems manageSystem = new ManageSystems();
                manageSystem.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("[ระบบการแจ้งเตือน] เกิดข้อผิดพลาด, กรุณากรอก Username และ Password อีกครั้ง");
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            return (username == "admin" && password == "password");
        }
    }
}
