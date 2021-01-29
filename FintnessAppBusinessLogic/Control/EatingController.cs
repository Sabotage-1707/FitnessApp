using FintnessAppBusinessLogic.Model;
using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Control
{
    public class EatingController:BaseController
    {
        private const string EATING_FILE_NAME = "eating.dat";

        private const string FOODS_FILE_NAME = "foods.dat";
        private readonly User user;
        public List<Food> Foods;
        public Eating Eating;


        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым",nameof(user));

            Foods = GetAllFoods();

            Eating = GetEating();
        }

        private List<Food> GetAllFoods()
        {
            return Load<List<Food>>(FOODS_FILE_NAME) ?? new List<Food>();
        }
        private Eating GetEating()
        {
            return Load<Eating>(EATING_FILE_NAME) ?? new Eating(user);
        }
        public void Add(Food food, double weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product == null)
            {
                Foods.Add(food);
                Eating.Add(food, weight);
                Save();
            }
            else
            {
                Eating.Add(product, weight);
                Save();
            }
        }
        public void Save()
        {
            Save(FOODS_FILE_NAME, Foods);
            Save(EATING_FILE_NAME, Eating);
        }
    }
}
