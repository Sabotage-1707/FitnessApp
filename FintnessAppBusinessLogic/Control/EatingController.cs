using FintnessAppBusinessLogic.Model;
using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Control
{
    public class EatingController : BaseController
    {
        private readonly User user;
        public List<Food> Foods;
        public Eating Eating;
        private IDataSaver manager;

        public EatingController(User user, IDataSaver manager)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым",nameof(user));
            this.manager = manager ?? throw new ArgumentNullException("Способ работы не может быть пустым или null", nameof(manager));
            Foods = GetAllFoods();

            Eating = GetEating();
        }

        private List<Food> GetAllFoods()
        {
            return Load<Food>(manager) ?? new List<Food>();
        }
        private Eating GetEating()
        {
            return Load<Eating>(manager).FirstOrDefault() ?? new Eating(user);
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
            Save(Foods, manager);
            Save(new List<Eating>() { Eating}, manager);
        }
    }
}
