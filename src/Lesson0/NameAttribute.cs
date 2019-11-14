using System;
using System.Collections.Generic;

namespace Tours.Attribute.Lesson0
{
    class Main
    {
        public void Run()
        {
            ShowCClassAttrs();
        }
        void ShowCClassAttrs() {
            foreach (NameAttribute attr in GetClassAttrs<C,NameAttribute>()) {
                Console.WriteLine($"{attr.Name}");
            }
        }
        A[] GetClassAttrs<T,A>() {
            object[] objs = typeof(T).GetCustomAttributes(typeof(A), false);
            return Array.ConvertAll(objs, o => {
                return (A)o;
            });
        }
        /*
        void ShowAttrs()
        {
            object[] attrs = typeof(C).GetCustomAttributes(typeof(NameAttribute), false);
            foreach (NameAttribute attr in attrs) {
                Console.WriteLine($"{attr.Name}");
            }
        }
        */
    }
    class NameAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public NameAttribute(string name) => Name = name;
    }
    [Name("CCC")]
    class C {}
}

