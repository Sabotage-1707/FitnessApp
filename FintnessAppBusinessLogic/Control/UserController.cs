using FitnessAppBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Control
{
    /// <summary>
    /// Контроллер пользователя приложения.
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="genderName">Пол пользоваетя.</param>
        /// <param name="userBirthday">Дата рождения.</param>
        /// <param name="userWeight">Вес.</param>
        /// <param name="userHeight">Рост.</param>
        public UserController(string userName, string genderName, DateTime userBirthday, double userWeight, double userHeight)
        {
            User = new User(userName, new Gender(genderName), userBirthday, userWeight, userHeight);
        }
        
        /// <summary>
        /// Сохранение данных пользователя в файл.
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();
            using(var fs = new FileStream("users.bin", FileMode.OpenOrCreate))
            {
                    formatter.Serialize(fs, User);
            }
        }
        /// <summary>
        /// Получить данные пользователя.
        /// </summary>
        /// <returns>Пользователь приложения.</returns>
        public UserController()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.bin", FileMode.OpenOrCreate))
            {
                if (formatter.Deserialize(fs) is User user)
                {
                    User = user;
                }
            }
        }
    }
}
