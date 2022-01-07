using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(PrintArray);
            Task task4 = task1.ContinueWith(action1);

            Action<Task<int>> action2 = new Action<Task<int>>(Print);
            Task task5 = task2.ContinueWith(action2);

            Action<Task<int>> action3 = new Action<Task<int>>(Print);
            Task task6 = task3.ContinueWith(action3);

            task1.Start();
            Console.ReadKey();
        }
        static int [] GetArray (object a)
        {
            int n = (int)a;
            int[] array = new int[n];
                Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
           
        }
        static void PrintArray (Task<int[]> task)

        {
            int[] array = task.Result;
            for (int i=0; i<array.Count();i++)
            Console.Write($"{array[i]} "); }
        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int summa = 0;
            
            for (int i = 0; i < array.Length; i++)
            
                summa += array[i];
            
            return summa;
               
        }
        static int  MaxArray(Task<int[]> task)

        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (max<array[i])

                { max = array[i]; }
            }
            return max;
        }
        static void Print(Task<int> task)
        {
            int a = task.Result;
                Console.WriteLine(a); }
    }
}
