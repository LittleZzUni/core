using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JasonStorey 
{
    public class AssemblyLoader
    {
        public static IEnumerable<T> LoadAllInstances<T>()
        {
            return LoadAllInstancesFromAssemblies<T>(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void LoadAssembliesFrom(string directory)
        {
            var getAssemblyDlls = Directory.GetFiles(directory, "*.dll");
            foreach (var dll in getAssemblyDlls)
                Assembly.LoadFrom(dll);
        }

        private static T CreateInstance<T>(Type t)
        {
            if (t.IsAbstract)
                return default(T);
            try
            {
                return (T) Activator.CreateInstance(t);
            }
            catch
            {
                return default(T);
            }
        }

        private static IEnumerable<T> CreateInstancesAs<T>(IEnumerable<Type> types)
        {
            foreach (var type in types)
                yield return CreateInstance<T>(type);
        }

        private static IEnumerable<T> LoadAllInstancesFromAssemblies<T>(IEnumerable<Assembly> assemblies)
        {
            var matchingTypes = new List<Type>();

            var ass = assemblies.ToList();

            foreach (var assembly in ass)
                matchingTypes.AddRange(GetTypesImplementing<T>(assembly));

            return CreateInstancesAs<T>(matchingTypes);
        }

        private static IEnumerable<Type> GetTypesImplementing<T>(Assembly assembly)
        {
            return assembly.GetTypes().Where(IsAssignableTo<T>).ToList();
        }

        private static bool IsAssignableTo<T>(Type t)
        {
            var ttype = typeof(T);
            return t != ttype && ttype.IsAssignableFrom(t) && !t.IsAbstract;
        }
    }
}