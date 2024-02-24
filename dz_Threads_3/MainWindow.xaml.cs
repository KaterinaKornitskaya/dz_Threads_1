using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dz_Threads_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Задание 6
        // Переведите первое задание со всеми изменениями в оконный интерфейс.
        public MainWindow()
        {
            InitializeComponent();
            
            List<int> list = new List<int>();
            Thread th = new Thread(new ParameterizedThreadStart(MyFun1));
            th.Start(list);
            listBoxTask1.ItemsSource = list;

        }

        public static void MyFun1(object obj)
        {
            List<int> list = (List<int>)obj;
            int start = 0;
            int end = 50;
            for (int i = start; i <= end; i++)
            {
                list.Add(i);
            }
        }

        struct Point
        {
            public int x;
            public int y;
            public List<int> list;

            public Point(int x, int y, List<int> list)
            {
                this.x = x;
                this.y = y;
                this.list = list;
            }
        }

        public static void MyFun2(object obj)
        {
            Point p = (Point)obj;
            List<int> list = p.list;
            int start = p.x;
            int end = p.y;
            for (int i = start; i <= end; i++)
            {
                list.Add(i);
            }
        }

        private void btnTask2_Click(object sender, RoutedEventArgs e)
        {
            listBoxTask2.ItemsSource = null;
            List<int> list2 = new List<int>();
            int a = Convert.ToInt32(txtBoxTask2_1.Text);
            int b = Convert.ToInt32(txtBoxTask2_2.Text);
            Point point = new Point(a, b, list2);
            Thread th2 = new Thread(new ParameterizedThreadStart(MyFun2));
            th2.Start(point);
            listBoxTask2.ItemsSource = point.list;
        }

       
        private void btnTask3_Click(object sender, RoutedEventArgs e)
        {
            gridForTask3.Children.Clear();
            int threadsNum = Convert.ToInt32(txtBoxTask3_threadsNumber.Text);
            
            while (threadsNum > 0)
            {
                List<int> list3 = new List<int>();
                ListBox listBox = new ListBox();
                listBox.Margin = new Thickness(0,0,10,0);
                listBox.Width = 50;
                Random rand = new Random();
                Point point = new Point(rand.Next(1,10), rand.Next(10,20), list3);
                Thread th = new Thread(new ParameterizedThreadStart(MyFun2));
                th.Start(point);
                gridForTask3.Children.Add(listBox);
                listBox.ItemsSource = point.list;
                threadsNum--;
            }
        }
    }
}