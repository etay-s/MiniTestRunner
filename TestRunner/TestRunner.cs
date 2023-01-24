using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using TestRunner.Attributes;

namespace TestRunner
{
    internal class TestRunner
    {
        /* Divide methods into call order categories */
        private static void CategorizeMethods(
            Type t,
            List<MethodInfo> init,
            List<MethodInfo> test,
            List<MethodInfo> final
            )
        {
            foreach (MethodInfo m in t.GetMethods())
            {
                // Run before all tests
                if (Attribute.GetCustomAttribute(m, typeof(TestInitAttribute)) != null)
                {
                    init.Add(m);
                }
                // Tests to run
                else if (Attribute.GetCustomAttribute(m, typeof(TestMethodAttribute)) != null)
                {
                    test.Add(m);
                }
                // Run after all tests
                else if (Attribute.GetCustomAttribute(m, typeof(TestFinalAttribute)) != null)
                {
                    final.Add(m);
                }
                else
                {
                    // Skip method.
                }
            }
        }

        /* Invoke all methods in list */
        private static void InvokeMethods(List<MethodInfo> methods)
        {
            foreach (MethodInfo m in methods)
            {
                m.Invoke(null, null);
            }
        }

        /* Execute test for a TestClass */
        private static void ExecuteTest(Type t)
        {
            List<MethodInfo> initMethods, testMethods, finalMethods;

            if (Attribute.GetCustomAttribute(t, typeof(TestClassAttribute)) != null)
            {
                Console.WriteLine("Running tests of: " + t.FullName);

                initMethods = new List<MethodInfo>();
                testMethods = new List<MethodInfo>();
                finalMethods = new List<MethodInfo>();

                CategorizeMethods(t, initMethods, testMethods, finalMethods);

                InvokeMethods(initMethods);
                InvokeMethods(testMethods);
                InvokeMethods(finalMethods);
            }
        }

        static void Main(string[] args)
        {
            Assembly assembly;

            foreach (string test in args)
            {
                try
                {
                    assembly = Assembly.LoadFile(Path.GetFullPath(test));
                    foreach (Type t in assembly.GetTypes())
                    {
                        try
                        {
                            ExecuteTest(t);
                        }
                        catch (TargetInvocationException e)
                        {
                            Console.WriteLine("An exception interuppted the test\n" + e.Message);
                        }
                        catch (MethodAccessException e)
                        {
                            Console.WriteLine("Permission to test denied\n" + e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid test\n" + e.Message);
                        }
                    }
                }
                catch (ReflectionTypeLoadException e)
                {
                    Console.WriteLine("Type(s) cannot be loaded\n" + e.Message + ":\n");
                    foreach (Exception? ex in e.LoaderExceptions)
                    {
                        if (ex != null)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid path or permission of test assembly (DLL)\n"
                        + e.Message);
                }
            }
        }
    }
}