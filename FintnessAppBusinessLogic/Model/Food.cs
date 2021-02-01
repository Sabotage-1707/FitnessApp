using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Model
{
    [Serializable]
    public class Food
    {
        public int Id { get; set; }
        /// <summary>
        /// Название продукта.
        /// </summary>
        public string Name { get;}

        /// <summary>
        /// Белки.
        /// </summary>
        public double Proteins { get;}
        /// <summary>
        /// Калории.
        /// </summary>
        public double Calloeries { get; }

        /// <summary>
        /// Жиры
        /// </summary>
        public double Fats { get; }

        /// <summary>
        /// Углеводы
        /// </summary>
        public double Carbohydrates { get; }
        public virtual ICollection<Eating> Eatings { get; set; }
        public Food() { }

        public Food(string name) : this(name, 0, 0, 0, 0) { }

        public Food(string name, double proteins, double calloeries, double fats, double carbohydrates)
        {
            #region Проверка вхрдных параметров
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Название еды не может быть пустым или null.", nameof(name));
            }
            if (proteins < 0) 
            {
                throw new ArgumentException("Количество протеинов еды не может быть меньше нуля.", nameof(proteins));
            }
            if (calloeries < 0)
            {
                throw new ArgumentException("Количество каллорий еды не может быть меньше нуля.", nameof(calloeries));
            }
            if (carbohydrates < 0)
            {
                throw new ArgumentException("Количество углеводов еды не может быть меньше нуля.", nameof(carbohydrates));
            }
            if (fats < 0)
            {
                throw new ArgumentException("Количество жиров еды не может быть меньше нуля.", nameof(fats));
            }
            #endregion
            Name = name;
            Proteins = proteins / 100.0;
            Calloeries = calloeries / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
