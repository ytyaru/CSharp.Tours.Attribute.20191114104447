using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tours.Attribute.Lesson1
{
    class Main
    {
        public void Run()
        {
            ShowCClassAttrs();
            ShowCClassMemberAttrs();
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
        void ShowCClassMemberAttrs() {
            foreach (NameAttribute attr in GetMemberAttrs<C,NameAttribute>()) {
                Console.WriteLine($"{attr.Name}");
            }
        }
        A[] GetMemberAttrs<T,A>() {
            List<A> result = new List<A>();
            MemberInfo[] members = typeof(T).GetMembers(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.DeclaredOnly);
            foreach (MemberInfo m in members)
            {
                object[] objs = m.GetCustomAttributes(typeof(A), false);
                result.AddRange(Array.ConvertAll(objs, o => {
                    return (A)o;
                }));
            }
            return result.ToArray();
        }
    }
    class NameAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public NameAttribute(string name) => Name = name;
    }
    [Name("Class")]
    class C
    {
        [Name("Field")]
        private int i;
        [Name("Property")]
        private int I { get; set; }
        [Name("Method")]
        private void M() {}
        [Name("コンストラクタ")]
        public C() {}
        [Name("デストラクタ")]
        ~C() {}
        [Name("列挙体の型定義")]
        enum E { e }
        [Name("構造体の型定義")]
        struct S {}
        [Name("デリゲートの型定義")]
        delegate void D();
    }
}

