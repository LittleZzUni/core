using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JasonStorey
{
    public static class ExtensionsReflection
    {
        public static TProperty GetValue<TSource, TProperty>(this TSource source,
            Expression<Func<TSource, TProperty>> expression)
        {
            var value = expression.Compile()(source);
            return value;
        }

        public static string GetFieldName<T, TX>(this Expression<Func<T, TX>> field)
        {
            return
                (field.Body as MemberExpression ?? ((UnaryExpression) field.Body).Operand as MemberExpression).Member
                .Name;
        }

        public static TX GetPropertyValue<TX, T>(this T t, string name)
        {
            return (TX) t.GetPropertyValue(name);
        }

        public static object GetPropertyValue<T>(this T src, string propName, bool includePrivateProperties = true)
        {
            var options = includePrivateProperties
                ? src.AllProperties()
                : src.PublicProperties();

            var prop = options.FirstOrDefault(x => x.Name.Equals(propName));
            return prop != null ? prop.GetValue(src, null) : null;
        }

        public static IEnumerable<PropertyInfo> AllProperties<T>(this T src)
        {
            return src.PublicProperties().Union(src.PrivateProperties());
        }

        public static IEnumerable<PropertyInfo> PrivateProperties<T>(this T src)
        {
            return src.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static IEnumerable<PropertyInfo> PublicProperties<T>(this T src)
        {
            return src.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        ///     Takes a method name on an object and converts the method call into a delegate action
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="method">The method.</param>
        /// <returns>Action.</returns>
        public static Action CreateActionFromMethod(this object o, string method)
        {
            var t = o.GetType();
            var me = t.GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return me != null ? Delegate.CreateDelegate(typeof(Action), o, method) as Action : null;
        }
    }
}