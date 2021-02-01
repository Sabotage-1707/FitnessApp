using System;
using System.Collections.Generic;

namespace FitnessAppBusinessLogic.Model
{
    /// <summary>
    /// Гендер пользователя
    /// </summary>
    [Serializable]
    public class Gender
    {
        public int Id { get; set; }
        /// <summary>
        /// Название пола.
        /// </summary>
        public string Name{ get;}
        public virtual ICollection<User> Users { get; set; }

        public Gender() { }

        /// <summary>
        /// Создание пола
        /// </summary>
        /// <param name="name">Название пола.</param>
        public Gender(string name)
        {
            Name = name ?? throw new ArgumentNullException("Название пола не может быть пустым или Null.",nameof(name));
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
