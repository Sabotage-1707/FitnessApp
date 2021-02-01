using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Model 
{ 

    /// <summary>
    /// Прием пищи
    /// </summary>
    [Serializable]
    public class Eating
    {
        public int Id { get; set; }
        /// <summary>
        /// Время приема пищи
        /// </summary>
        public DateTime Moment { get; }
        /// <summary>
        /// Список еды.
        /// </summary>
        public Dictionary<Food,double> Foods{ get;}
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public Eating(User user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Moment = DateTime.UtcNow;
            Foods = new Dictionary<Food, double>();
        }
        public void Add(Food food, double weight)
        {
            var product = Foods.Keys.FirstOrDefault(f => f.Name.Equals( food.Name));
            if(product == null)
            {
                Foods.Add(food, weight);
            }
            else
            {
                Foods[product] += weight;
            }
        }
    }
}
