using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTool.Config;
using TestTool.Forms;

namespace TestTool.Test
{
    internal class Works
    {
        private static bool isSingleStep = false; 
        private static Queue<Action<object>> methodQueue = new Queue<Action<object>>(); 
        private static Queue<object> parameterQueue = new Queue<object>();
        //private static object parameter; 
        private static readonly object lockObject = new object(); 
        private static SingletonStatus singletonStatus = SingletonStatus.Instance;
        public static void Test()
        {
            Thread workerThread = new Thread(WorkerMethod);
            workerThread.Start();


            Console.WriteLine("Press 's' to single step, 'c' to continue, or 'q' to quit.");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.S)
                {
                    isSingleStep = true;
                }
                else if (key == ConsoleKey.C)
                {
                    isSingleStep = false;
                }
                else if (key == ConsoleKey.Q)
                {
                    break;
                }

            }
        }
        public static void WorksEnQueue(string childName)
        {
            methodQueue = new Queue<Action<object>>();
            parameterQueue = new Queue<object>();
            Parent bc = new Parent();
            bc = JsonDog.ReadChild(childName);
            foreach (Child item in bc.childSteps)
            {
                if (methodDictionary.ContainsKey(item.stepName))
                {
                    object v = item;
                    object k = item.stepParameter;
                    methodQueue.Enqueue(methodDictionary[item.stepName]);
                    parameterQueue.Enqueue(item.stepParameter);
                }
            }

        }
        public static void WorkerMethod()
        {
            singletonStatus.stepNum = 0;
            singletonStatus.cycle = true;
            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {

                lock (lockObject)
                {
                    if (methodQueue.Count == 0)
                    {

                        break; 
                    }

                    var method = methodQueue.Dequeue();
                    var parameter = parameterQueue.Dequeue();
                    if (method != null)
                    {

                        stopwatch.Start();
                        method(parameter);
                        stopwatch.Stop();
                        singletonStatus.t = (int)stopwatch.ElapsedMilliseconds;
                        stopwatch.Restart();
                        singletonStatus.stepNum++;
                        if (!singletonStatus.status)
                        {
                            break;
                            //return;
                        }

                        if (isSingleStep)
                        {

                            Console.WriteLine("Press Enter to continue...");
                            lock (lockObject) 
                            {
                                Console.ReadLine();
                            }
                        }
                    }

                }

            }
            singletonStatus.cycle = false;
        }
        private static void InitDictonary()
        {

            Type tp = typeof(MethodCollection);

            MethodInfo staticMethodInfo;

            List<FormStr> li = new List<FormStr>();
            li = FormInfo.readOnlyList.ToList();
            foreach (FormStr formStr in li)
            {
                staticMethodInfo = tp.GetMethod(formStr.formMethod, BindingFlags.Static | BindingFlags.Public);
            }
        }
        private static void AddMethod(string key, Action<dynamic> method)
        {
            methodDictionary[key] = method;
        }


        public static void InvokeMethod(string key, string parameter)
        {
            if (methodDictionary.ContainsKey(key))
            {
                methodDictionary[key](parameter);
            }
            else
            {
                //Console.WriteLine("方法未找到");
            }
        }

        private static readonly Dictionary<string, Action<dynamic>> methodDictionary = new Dictionary<string, Action<dynamic>>()
        {
            {"变量定义",MethodCollection.Method_DefineVar },
            {"PLC读取",MethodCollection.Method_ReadPLC },
            {"PLC写入",MethodCollection.Method_WritePLC },
            {"延时工具",MethodCollection.Method_DelayTime },
            {"变量赋值",MethodCollection.Mathod_VariableAssignment }
        };
        
    }
}
