using System;
using System.Reflection;

namespace ReflectionExtension
{
    public static class Reflection
    {
        public static void SetFieldValue(Type type, string fieldName, object value, object typeInstance = null)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            field.SetValue(typeInstance, value);
        }

        public static object GetFieldValue(Type type, string fieldName, object typeInstance = null)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return field.GetValue(typeInstance);
        }

        public static void SetPropertyValue(Type type, string propertyName, object value, object typeInstance = null)
        {
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            property.SetValue(typeInstance, value);
        }

        public static object GetMethodResult(Type type, string methodName, object typeInstance, params object[] parameters)
        {
            return type.InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, typeInstance, parameters);
        }
    }
}
