using System;
using System.Reflection;

namespace UTIL
{
    public class Reflections

    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Object create(Type obj)
        {
            object objecto = Activator.CreateInstance(obj);
            return objecto;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public object getPropertyValue(object obj, string property)
        {
            Type type = obj.GetType();
            PropertyInfo numberPropertyInfo = type.GetProperty(property);
            return numberPropertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void setPropertyValue(object obj, string property, object value)
        {
            if (value != null)
            {
                Type type = obj.GetType();

                PropertyInfo numberPropertyInfo = type.GetProperty(property);

                if(numberPropertyInfo != null)
                {
                    numberPropertyInfo.SetValue(obj, value, null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        public void setPropertyValue(object obj, PropertyInfo propertyInfo, object value)
        {
            if (value != null)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public PropertyInfo[] getPropertys(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            return propertyInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public PropertyInfo getProperty(object obj, string property)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(property);
            return propertyInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isString(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(String)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isDecimal(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(Decimal)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isDouble(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(Double)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isLong(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(long)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isInt32(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(System.Int32)))
            {
                resultado = true;
            }
            return resultado;
        }

        public Boolean isInt64(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(System.Int64)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isChar(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(Char)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isDateTime(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(DateTime)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isBoolean(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(Boolean)))
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public Boolean isByte(PropertyInfo propertyInfo)
        {
            Boolean resultado = false;
            if (propertyInfo.PropertyType.Equals(typeof(Byte)))
            {
                resultado = true;
            }
            return resultado;
        }

    }
}
