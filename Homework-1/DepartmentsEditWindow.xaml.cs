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

namespace Homework_1
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsEditWindow.xaml
    /// </summary>
    public partial class DepartmentsEditWindow : Window
    {
        public Department Department { get; set; }
        public DepartmentsEditWindow(Department department)
        {
            InitializeComponent();
            this.Department = department;
            this.DataContext = Department;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
