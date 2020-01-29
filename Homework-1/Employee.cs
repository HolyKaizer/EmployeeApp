namespace Homework_1
{
    public class Employee : BaseClass
    {
        int age;
        string secondName;
        Department dept;
        int id;
        decimal salary;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public string SecondName
        {
            get => secondName;
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }


        public decimal Salary 
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        public Department Dept 
        {
            get => dept;
            set
            {
                dept = value;
                OnPropertyChanged("Dept");
            }
        }

        public Employee(int id, string name, string secondName, int age, Department department, decimal salary) : base(name)
        {
            Id = id;
            SecondName = secondName;
            Age = age;
            Dept = department;
            Salary = salary;
        }

        public Employee() : base()
        {
        }

        public void ChangeDataFrom(Employee emp)
        {
            Name = emp.Name;
            SecondName = emp.SecondName;
            Age = emp.Age;
            Dept = emp.Dept;
            Salary = emp.Salary;
        }
    }
}

