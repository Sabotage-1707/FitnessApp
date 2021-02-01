using FintnessAppBusinessLogic.Model;
using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Control
{
    public class ExerciseController : BaseController
    {
        private readonly User user;
        public List<Exercise> Exercises { get; }
        public List<Activity> Activities { get; }
        private IDataSaver manager;

        public ExerciseController(User user, IDataSaver manager)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.manager = manager ?? throw new ArgumentNullException("Способ работы не может быть пустым или null", nameof(manager));
            Exercises = GetAllExercises();
            Activities = GetAllActivities();
        }

        private List<Activity> GetAllActivities()
        {
            return Load<Activity>(manager) ?? new List<Activity>();
        }

        public void Add(Activity activity, DateTime begin, DateTime end)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);

            if (act == null)
            {
                Activities.Add(activity);

                var exercise = new Exercise(begin, end, activity, user);
                Exercises.Add(exercise);
            }
            else
            {
                var exercise = new Exercise(begin, end, act, user);
                Exercises.Add(exercise);
            }
            Save();
        }

        private List<Exercise> GetAllExercises()
        {
            return Load<Exercise>(manager) ?? new List<Exercise>();
        }

        private void Save()
        {
            Save(Exercises, manager);
            Save(Activities, manager);
        }
    }
}
