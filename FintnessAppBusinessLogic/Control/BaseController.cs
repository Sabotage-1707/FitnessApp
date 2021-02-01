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

        protected void Save<T>(List<T> item, IDataSaver manager) where T : class
        {
            manager.Save(item);
        }

        protected List<T> Load<T>(IDataSaver manager) where T : class
        {
            return manager.Load<T>();
        }
    }
}
