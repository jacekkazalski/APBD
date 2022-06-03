using System;

namespace LinqTutorials
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var res = LinqTasks.Task1();

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            int[] arr = { 1, 1, 1, 1, 9, 1, 1 };
            var result = LinqTasks.Task13(arr);
        }
    }
}
