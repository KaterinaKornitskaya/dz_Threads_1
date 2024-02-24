namespace dz_Threads_1
{
    //    Задание 1
    // Создайте консольное приложение, порождающее поток.
    // Этот поток должен отображать в консоль числа от 0 до 50.

    // Задание 2
    // Добавьте в первое задание возможность передачи
    // начала и конца диапазона чисел. Границы определяет пользователь.

    // Задание 3
    // Добавьте к первому заданию возможность определения
    // пользователем количества потоков.Границы диапазона
    // чисел также выбираются пользователем.
    internal class Program
    {
        static void Main(string[] args)
        {
            //Thread thCurr = Thread.CurrentThread;

            // Задание 1
            Thread th1 = new Thread(new ThreadStart(MyFun1));
            //th1.Start();

            // Задание 2
            //Console.WriteLine("Вывод диапазона чисел.");
            //Console.WriteLine("Введите начальное число:");
            //int start = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Введите конечное число:");
            //int end = Convert.ToInt32(Console.ReadLine());
            //Point p = new Point(start, end);

            //Thread th2 = new Thread(new ParameterizedThreadStart(MyFun2));
            //th2.Start(p);


            // Задание 3
            Console.WriteLine("Введите желаемое кол-во потоков.");
            int number = Convert.ToInt32(Console.ReadLine());
            while (number > 0)
            {
                Console.WriteLine("Введите начальное число:");
                int st = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите конечное число:");
                int en = Convert.ToInt32(Console.ReadLine());
                Point p1 = new Point(st, en);
                Thread thMany = new Thread(new ParameterizedThreadStart(MyFun2));
                thMany.Start(p1);
                Console.WriteLine("***********************************");
                number--;
            }



            Console.WriteLine("I am main.");
        }

        public static void MyFun1()
        {
            int start = 0;
            int end = 50;
            for(int i = start; i <= end; i++)
            {
                Console.WriteLine(i + "\t");
            }
        }

        struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static void MyFun2(object obj)
        {
            Point point = (Point)obj;

            int start = point.x;
            int end = point.y;
            for (int i = start; i <= end; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(i + "\t");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
