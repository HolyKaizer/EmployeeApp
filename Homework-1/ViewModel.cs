using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Homework_1
{
    public class ViewModel : INotifyPropertyChanged
    {
        //Поля списков
        ObservableCollection<Department> departments;
        ObservableCollection<Employee> employees;

        //Объявление списков департаментов и сотрудников 
        public ObservableCollection<Department> Departments
        {
            get => departments;
            set
            {
                departments = value;
                OnPropertyChanged("Departments");
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        //Команды 
        RelayCommand addEmployee;
        RelayCommand editEmployee;
        RelayCommand deleteEmployee;
        RelayCommand showDepartments;
        RelayCommand addDepartment;
        RelayCommand editDepartment;
        RelayCommand deleteDepartment;

        //Конструктор класса 
        public ViewModel()
        {
            //Инициализация списков 
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();

            //Заполняем списки
            DepartmentsFilling();
            EmployeesFilling();
        }

        //Метод заполняет список департаментов
        private void DepartmentsFilling()
        {
            if (Departments == null) Departments = new ObservableCollection<Department>();
            Departments.Add(new Department("Разработка", 1));
            Departments.Add(new Department("Дизайн", 2));
            Departments.Add(new Department("Маркетинг", 3));
        }

        private void EmployeesFilling()
        {
            if (Employees == null) Employees = new ObservableCollection<Employee>();
            Employees.Add(new Employee(1, "Иванов", "Иван", 30, Departments[0], 1900));
            Employees.Add(new Employee(2, "Петров", "Петр", 34, Departments[1], 1300));
            Employees.Add(new Employee(3, "Васильев", "Василий", 24, Departments[2], 2500));
        }


        /// <summary>
        /// Команда добавления сотрудника 
        /// </summary>
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                    (addEmployee = new RelayCommand((o) =>
                    {
                        EmployeeEditWindow editWindow = new EmployeeEditWindow(new Employee(), Departments);
                        if (editWindow.ShowDialog() == true)
                        {
                            Employee employee = editWindow.CurrentEmployee;

                            //Проверяем может ли идентификатор быть заданным пользователем
                            int maxEmpId = 0;
                            bool isValidId = true;
                            foreach (var e in Employees)
                            {
                                if (e.Id > maxEmpId)
                                    maxEmpId = e.Id;
                                if (employee.Id == e.Id)
                                    isValidId = false;
                            }

                            if (!isValidId)
                                employee.Id = maxEmpId + 1;
                            employee.Dept = Departments[editWindow.DepartmentsCB.SelectedIndex];
                            Employees.Add(employee);
                        }
                    }));
            }
        }
        
        /// <summary>
        /// Команда для удаления сотрудника
        /// </summary>
        public RelayCommand EditEmployee 
        { 
            get
            {
                return editEmployee ??
                    (editEmployee = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Employee employee = selectedItem as Employee;

                        Employee emp = new Employee(employee.Id,
                                                    employee.Name,
                                                    employee.SecondName,
                                                    employee.Age,
                                                    employee.Dept,
                                                    employee.Salary);
                        EmployeeEditWindow editWindow = new EmployeeEditWindow(emp, Departments);

                        if(editWindow.ShowDialog() == true)
                        {
                            employee = null;
                            try
                            {
                                employee = Employees.First(x => x.Id == emp.Id);
                            }
                            catch { }
                            if (employee != null)
                            {
                                employee.ChangeDataFrom(emp);
                                employee.Dept = Departments[editWindow.DepartmentsCB.SelectedIndex];
                            }
                        }
                    }));
            } 
        }

        /// <summary>
        /// Команда удаляющая текущего сотрудника
        /// </summary>
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                    (deleteEmployee = new RelayCommand((selectedItem) =>
                    {
                        DelWindow delWindow = new DelWindow();

                        if(delWindow.ShowDialog() == true)
                        {
                            if (selectedItem == null) return;
                            try 
                            {
                                Employees.Remove(Employees.First(x => x.Id == (selectedItem as Employee).Id)); 
                            } catch { }
                        }
                    }));
            }
        }

        /// <summary>
        /// Команда показывающая список всех текущих департмаентов
        /// </summary>
        public RelayCommand ShowDepartments
        {
            get
            {
                return showDepartments ??
                    (showDepartments = new RelayCommand((o) => 
                    {
                        DepartmentsWindow departmentsWindow = new DepartmentsWindow(this);
                        departmentsWindow.ShowDialog();
                    }));
            }
        }
        
        /// <summary>
        /// Команда добавляющая новый департамент
        /// </summary>
        public RelayCommand AddDepartments
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand((o) =>
                    {
                        DepartmentsEditWindow editWindow = new DepartmentsEditWindow(new Department());
                        if(editWindow.ShowDialog() == true)
                        {
                            Department department = editWindow.Department;
                            department.Id = Departments.Last().Id + 1;
                            Departments.Add(department);
                        }
                    }));
            }
        }

        /// <summary>
        /// Команда редактирующая выбранный департамент
        /// </summary>
        public RelayCommand EditDepartment
        {
            get
            {
                return addDepartment ??
                    (addDepartment = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Department department = selectedItem as Department;
                        Department dept = new Department( department.Name, department.Id);

                        DepartmentsEditWindow editWindow = new DepartmentsEditWindow(dept);
                        if (editWindow.ShowDialog() == true)
                        {
                            if(department != null)
                            {
                                //Обновляет департамент у всех сотрудников ранее имеющих его в качестве параметра
                                foreach (var e in Employees.Where((e) => e.Dept.Name == department.Name))
                                    e.Dept = new Department(dept.Name, dept.Id);

                                department.Name = dept.Name;
                            }
                        }
                    }));
            }
        }

        /// <summary>
        /// Команда удаляющая выбранный департамент
        /// </summary>
        public RelayCommand DeleteDepartment
        {
            get
            {
                return deleteDepartment ??
                    (deleteDepartment = new RelayCommand((selectedItem) =>
                    {
                        DelWindow delWindow = new DelWindow();
                        if (delWindow.ShowDialog() == true)
                        {
                            if (selectedItem == null) return;
                            try { Departments.Remove(Departments.First(x => x.Name == (selectedItem as Department).Name)); } catch { }
                        }
                    }));
            }
        }

        //Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
