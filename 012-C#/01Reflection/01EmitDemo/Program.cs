using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _01EmitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyName assemblyName = new AssemblyName("EmitDemo01");

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("SayHelloModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("EmitDemo01.SayHelloTool", TypeAttributes.Public,
                                                               typeof(object), new Type[] { typeof(ISayHello) });

            Console.WriteLine(typeBuilder.Assembly.FullName);
            Console.WriteLine(typeBuilder.Module.Name);
            Console.WriteLine(typeBuilder.Namespace);
            Console.WriteLine(typeBuilder.Name);

            MethodBuilder sayHelloMethodBuilder = typeBuilder.DefineMethod("SayHello", MethodAttributes.Public, typeof(object), new Type[] { typeof(int) });
            MethodBuilder sayHiMethodBuilder = typeBuilder.DefineMethod("SayHi", MethodAttributes.Public, typeof(object), new Type[] { typeof(string), typeof(DateTime) });

             //Type providerType = typeBuilder.CreateType();
             //ISayHello provider = Activator.CreateInstance(providerType) as ISayHello;

            Console.ReadKey();
        }
    }

    interface ISayHello
    {
        void SayHello(int times);
        void SayHi(string name, DateTime dateTime);
    }
}
