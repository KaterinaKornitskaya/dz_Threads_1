using System.IO;
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

namespace dz_Threads_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Задание 7
        // Переведите четвертое задание со всеми изменениями в оконный интерфейс.
       // List<int> list;
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void btnTask4_Click(object sender, RoutedEventArgs e)
        {
            List<int> list = new List<int>();

            Thread thArr = new Thread(() =>
            {

                for (int i = 0; i < 100; i++)
                {
                    Random rand = new Random();
                    list.Add(rand.Next(1, 10000));
                    //Thread.Sleep(100);
                }
            });
            thArr.Start();
            listBoxTask4.ItemsSource = list;

            // здесь ждем, пока сформируется массив в первом потоке,
            // и уже после этого будут выполнятся следующие потоки с мин, макс
            thArr.Join();

            int max = 0, min = 0;
            double avg = 0;
            // новый поток, выводит максимальное значение
            Thread thMax = new Thread(() => {
                max = list.Max();
                //txtBoxForMax.Text = max.ToString();
            });
            thMax.Start();
            thMax.Join();
            txtBoxForMax.Text = max.ToString();

            //новый поток, выводит минимальное значение
            Thread thMin = new Thread(() =>
            {
                min = list.Min();
            });
            thMin.Start();
            thMin.Join();
            txtBoxForMin.Text = min.ToString();

            // новый поток, записывает в файл
            Thread thAvg = new Thread(() =>
            {
                avg = list.Average();
            });
            thAvg.Start();
            thAvg.Join();
            txtBoxForAvg.Text = avg.ToString();

            Thread thToFile = new Thread(() =>
            {
                using (FileStream file = new FileStream("myFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                    {
                        foreach (var item in list)
                        {
                            writer.Write(item.ToString() + "\t");
                        }
                       
                        writer.WriteLine();
                        writer.WriteLine("max = " + list.Max());
                        writer.WriteLine("min = " + list.Min());
                        writer.WriteLine("avg = " + list.Average());
                    }
                }
            });
            thToFile.Start();
        }
    }
}