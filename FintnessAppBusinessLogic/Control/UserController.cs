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
    public class UserController : BaseController
    {
       
        public const string USER_FILE_NAME = "users.dat";
        /// <summary>
        /// Все пользователи.
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public User CurrentUser { get; }
        public bool IsNewUser { get; set; } = false;


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
            CurrentUser = new User(userName, new Gender(genderName), userBirthday, userWeight, userHeight);
        }
        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Пользователь не может быть null.", nameof(userName));
            }
            Users = GetUsersData();
            
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }
        /// <summary>
        /// Сохранение данных пользователей в файл.
        /// </summary>
        public void Save()
        {
            Save(USER_FILE_NAME, Users);
        }
        public void SetNewUserData(string genderName, DateTime birthDay, double weight = 1, double height = 1)
        {
            if(birthDay == null)
            {
                throw new ArgumentNullException("Дата рождения не можеть быть пустой или null.", nameof(birthDay));
            }
            var gen = new Gender(genderName);
            CurrentUser.Gender = gen;
            CurrentUser.Birthday = birthDay;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
            
        }
        /// <summary>
        /// Получить данные пользователя.
        /// </summary>
        /// <returns>Пользователи приложения.</returns>
        public List<User> GetUsersData()
        {
            return Load<List<User>>(USER_FILE_NAME) ?? new List<User>();
        }
    }
}
