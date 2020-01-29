using System.Collections.Generic;

namespace Homework_1
{
    public class Department : BaseClass
    {
        int id;
        public int Id 
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        } 

        public Department(string name, int id): base(name)
        {
            Id = id;
        }
        public Department() : base()
        {

        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

