using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Model
{
    [Serializable]
    public class Activity
    {
        public string Name { get; }

        public double CaloriesPerMinute { get; set; }

        public Activity() { }

        public Activity(string name, double caloriesPerMinute)
        {
           if(string.IsNullOrWhiteSpace(name))
           {
                throw new ArgumentNullException("Название активности не може быть пустым или null.", nameof(name));
           }
           if(caloriesPerMinute <= 0)
           {
                throw new ArgumentException("Потраченные каллории не могут быть меньше или равны нулю.", nameof(caloriesPerMinute));
           }

            Name = name;
            CaloriesPerMinute = caloriesPerMinute;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
