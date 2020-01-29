using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        public Employee CurrentEmployee { get; set; }

        public ObservableCollection<Department> Departments { get; set; }

        public EmployeeEditWindow(Employee employee, ObservableCollection<Department> departments)
        {
            CurrentEmployee = employee;
            this.Departments = departments;
            this.DataContext = CurrentEmployee;
            InitializeComponent();
            DepartmentsCB.ItemsSource = Departments;
            try { 
                DepartmentsCB.SelectedIndex = DepartmentsCB.Items.IndexOf(Departments.First(x => x.Name == CurrentEmployee.Dept.Name)); 
            } catch { }
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
