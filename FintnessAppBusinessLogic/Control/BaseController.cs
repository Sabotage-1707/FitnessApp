using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FintnessAppBusinessLogic.Control
{
    public abstract class BaseController
    {
        /// <summary>
        /// Сохранение в файл.
        /// </summary>
        /// <param name="fileName"> Название файла. </param>
        /// <param name="item"> Сохроняемый объект. </param>
        protected void Save(string fileName, object item)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }
        protected T Load<T>(string fileName)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is T item)
                {
                    return item;
                }
                else
                {
                    return default(T);
                }
               
            }
        }
    }
}
