using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Model
{
    [Serializable]
    public class Exercise
    {
       
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public Activity Activity { get; set; }
        public User User { get; set; }

        public Exercise() { }

        public Exercise(DateTime start, DateTime finish, Activity activity, User user)
        {
            if(start > DateTime.Now)
            {
                throw new ArgumentException("Время начала упражнения не может быть больше текущего.", nameof(start));
            }
            if (finish > DateTime.Now)
            {
                throw new ArgumentException("Время окончания упражнения не может быть больше текущего.", nameof(finish));
            }
            if(activity == null)
            {
                throw new ArgumentNullException("Активность не может быть null", nameof(activity));
            }
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть null", nameof(activity));
            }
           

            Start = start;
            Finish = finish;
            Activity = activity;
            User = user;
        }
    }
}
