using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace EmployeeRecord.Utilities
{
    public static class DataReader
    {
        public static List<T> MapToList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (reader.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                    {
                        try
                        {
                            prop.SetValue(obj, reader[prop.Name], null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
