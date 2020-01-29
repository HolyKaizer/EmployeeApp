using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    /// <summary>
    /// Основной класс для всех элементов интерфейса
    /// </summary>
    public abstract class BaseClass : INotifyPropertyChanged
    {
        string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        // Пустой конструктор класса
        public BaseClass()
        {

        }

        //Конструктор класса
        public BaseClass(string Name)
        {
            this.Name = Name;
        }

        //Реализация интерфейса 
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
