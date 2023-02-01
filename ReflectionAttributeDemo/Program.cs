using System;
using System.Reflection;

namespace ReflectionAttributeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();


            var methods = manager.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    var instance = (IAttribute)Activator.CreateInstance(attribute.GetType());
                    instance.RunAttribute();
                }
                method.Invoke(manager, null);
            }
        }
    }

    public class CustomAttribute : Attribute, IAttribute
    {
        public void RunAttribute()
        {
            Console.WriteLine("Run Attribute!");
        }
    }

    public class InterceptAttribute : Attribute, IAttribute
    {
        public void RunAttribute()
        {
            Console.WriteLine("Intercept Attribute!");
        }
    }

    public class Manager
    {
        [Custom]
        [Intercept]
        public void Run()
        {
            Console.WriteLine("Run!");
        }

        [Intercept]
        public void Deneme()
        {
            Console.WriteLine("Deneme");
        }
    }
    public interface IAttribute
    {
        void RunAttribute();
    }
}
