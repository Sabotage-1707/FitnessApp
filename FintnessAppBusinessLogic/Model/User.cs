using System;


namespace FitnessAppBusinessLogic.Model
{
    /// <summary>
    /// Пользователь приложения
    /// </summary>
    [Serializable]
    public class User
    {
        
        #region Свойства
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get;}
        /// <summary>
        /// Пол.
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Вес.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Рост.
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age
        {
            get
            {
                DateTime dateNow = DateTime.Today;
                int age = dateNow.Year - Birthday.Year;
                if (Birthday > dateNow.AddYears(-age)) age--;
                return age;
            }
        }
        #endregion
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthday">Дата рождения.</param>
        /// <param name="weight">Вес.</param>
        /// <param name="height">Рост.</param>
        public User(string name, Gender gender, DateTime birthday, double weight, double height)
        {
            #region Проверка входных значений
            if (birthday <= DateTime.Parse("01.01.1900") || birthday >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата рождения.", nameof(birthday));
            }
            if(weight <= 0)
            {
                throw new ArgumentException("Вес не может быть меньше или равен нулю",nameof(weight));
            }
            if (height <= 0)
            {
                throw new ArgumentException("Рост не может быть меньше или равен нулю", nameof(height));
            }
            #endregion

            Name = name ?? throw new ArgumentNullException("Имя не может быть пустым или Null.",nameof(name));
            Gender = gender ?? throw new ArgumentNullException("Пол не может быть пустым или Null.", nameof(gender));
            Birthday = birthday;
            Weight = weight;
            Height = height;
        }

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя не может быть пустым или null.", nameof(name));
            }
            Name = name;
        }

        public override string ToString()
        {
            return Name + " " + Age + "(лет/год)";
        }
    }
}
