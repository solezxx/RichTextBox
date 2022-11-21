using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mygrid.DataContext = Mcl;
           
        }
        public MyClass Mcl { get; set; } = new MyClass();
        private Random rm = new Random();
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            f();
        }

        private int x = 1;
        public async void f()
        {
            Task t1=Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    LdrLog("线程1");
                }
            });
            Task t2 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    LdrLog(x.ToString());
                    x++;
                }
            });
            await Task.WhenAll(t1, t2);
        }
        public string CurTime()
        {
            return DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString("D3");
        }
        
        public async void LdrLog(string strtoappend)
        {
            await Task.Run(() =>
            {
               Mcl.RichText=strtoappend;
            });
        }
    }

    public class MyClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
       object locklock=new object();
        private string richText;
        public string RichText
        {
            get { return richText; }
            set
            {
                lock (locklock)
                {
                    richText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RichText"));
                }
            }
        }
    }
}
