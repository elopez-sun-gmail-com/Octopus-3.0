using System;
using System.Reflection;

namespace UTIL
{
    public class EntityTransform
    {
        /// <summary>
        /// Copia de un objeto a objeto de destino, sólo los campos coincidentes se copiarán
        /// </summary>
        /// <typeparam name="T">Objeto gnerico</typeparam>
        /// <param name="entityObject">Objeto Entity</param>
        /// <returns>Instancia generica</returns>
        public static T Transform<T>(object entityObject)
        {
            //Crea una instancia generica del objeto
            T classObject = Activator.CreateInstance<T>();
            //Si el entityObjet es nulo, retorna una valor nulo
            if (entityObject == null)
            {
                return default(T);
            }

            //Obtiene el tipo de cada objeto
            Type sourceType = entityObject.GetType();
            Type targetType = classObject.GetType();

            //Hace un bucle a través de las propiedades de entityObject
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //Consigue la propiedad correspondiente en el objeto de destino (classObject)
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //Si no hay ninguno, lo omite
                if (targetObj == null)
                    continue;

                //Se establece el valor en el classObject
                targetObj.SetValue(classObject, p.GetValue(entityObject, null), null);
            }
            //Se retorna la instancia creada
            return classObject;
        }


        public static T Transform<T>(T classObject, object entityObject)
        {
            //Crea una instancia generica del objeto
            //  T classObject = Activator.CreateInstance<T>();
            //Si el entityObjet es nulo, retorna una valor nulo
            if (entityObject == null)
            {
                return default(T);
            }

            //Obtiene el tipo de cada objeto
            Type sourceType = entityObject.GetType();
            Type targetType = classObject.GetType();

            //Hace un bucle a través de las propiedades de entityObject
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //Consigue la propiedad correspondiente en el objeto de destino (classObject)
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //Si no hay ninguno, lo omite
                if (targetObj == null)
                    continue;

                //Se establece el valor en el classObject
                targetObj.SetValue(classObject, p.GetValue(entityObject, null), null);
            }
            //Se retorna la instancia creada
            return classObject;
        }

        public static T TransformIsNotNull<T>(T classObject, object entityObject)
        {
            //Crea una instancia generica del objeto
            //  T classObject = Activator.CreateInstance<T>();
            //Si el entityObjet es nulo, retorna una valor nulo
            if (entityObject == null)
            {
                return default(T);
            }

            //Obtiene el tipo de cada objeto
            Type sourceType = entityObject.GetType();
            Type targetType = classObject.GetType();

            //Hace un bucle a través de las propiedades de entityObject
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //Consigue la propiedad correspondiente en el objeto de destino (classObject)
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //Si no hay ninguno, lo omite
                if (targetObj == null)
                    continue;

                //Se establece el valor en el classObject

                object value = p.GetValue(entityObject, null);

                if (value != null)
                {
                    targetObj.SetValue(classObject, p.GetValue(entityObject, null), null);
                }


            }
            //Se retorna la instancia creada
            return classObject;
        }
    }
}
