using System;
using System.Reflection;

namespace ReflectionExtension
{
    public static class Reflection
    {
        public static void SetFieldValue(Type type, string fieldName, object value)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            field.SetValue(new object(), value);
        }

        public static object GetFieldValue(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return field.GetValue(new object());
        }

        public static void SetPropertyValue(Type type, string propertyName, object value)
        {
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            property.SetValue(new object(), value);
        }

        public static object GetMethodResult(Type type, string methodName, params object[] parameters)
        {
            return type.InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, new object(), parameters);
        }
    }
}
