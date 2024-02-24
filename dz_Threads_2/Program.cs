using System.Text;

namespace dz_Threads_2
{
    // Задание 4
    // Консольное приложение генерирует набор чисел, 
    // состоящий из 10000 элементов.С помощью механизма
    // потоков нужно найти максимум, минимум, среднее в этом
    // наборе.
    // Для каждой из задач выделите поток.

    // Задание 5
    // К четвертому заданию добавьте поток, выводящий
    // набор чисел и результаты вычислений в файл.
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            int[] arr = new int[100];
            
            // первый поток - формируем массив
            Thread thArr = new Thread(() => {
               
                for (int i = 0; i < arr.Length; i++)
                {
                    Random rand = new Random();
                    arr[i] = rand.Next(1, 1000);
                    Console.Write(arr[i] + "\n");
                }
                Console.WriteLine("\n***********************");
            });
            thArr.Start();

            // здесь ждем, пока сформируется массив в первом потоке,
            // и уже после этого будут выполнятся следующие потоки с мин, макс
            thArr.Join();
            
            // новый поток, выводит максимальное значение
            Thread thMax = new Thread(()=>{
                Console.WriteLine("max = " + arr.Max());
            }); 
            thMax.Start();

            // новый поток, выводит минимальное значение
            Thread thMin = new Thread(() =>
            {
                Console.WriteLine("min = " + arr.Min());
            });
            thMin.Start();

            // новый поток, записывает в файл
            Thread thAvg = new Thread(() =>
            {
                Console.WriteLine("avg = " + arr.Average());
            });
            thAvg.Start();

            Thread thToFile = new Thread(() =>
            {
                using(FileStream file = new FileStream("myFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            writer.Write(arr[i] + "\t");
                        }
                        writer.WriteLine();
                        writer.WriteLine("max = " + arr.Max());
                        writer.WriteLine("min = " + arr.Min());
                        writer.WriteLine("avg = " + arr.Average());
                    }
                }
            });
            thToFile.Start();
        }
    }
}
