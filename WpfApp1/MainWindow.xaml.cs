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
            string x;
            x= rm.Next(0, 4).ToString();
            for (int i = 0; i < 3; i++)
            {
                x += x;
            }

            Mcl.RichText = x.ToString();
        }
    }
    public class MyClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand Command = new RoutedCommand();
        private int line = 0;
        private string richText;
        public string RichText
        {
            get { return richText; }
            set
            {
                line++;
                if (line>100)
                {
                    line=0;
                    richText = "";
                }
                richText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RichText"));
            }
        }
    }
}
