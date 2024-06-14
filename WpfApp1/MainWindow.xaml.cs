using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
            viewModel = (LogViewModel)DataContext;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        private LogViewModel viewModel;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 模拟日志添加
            viewModel.AddLog("This is a normal log.");
            viewModel.AddLog("This is an alarm log!");
        }
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logs")
            {
                UpdateRichTextBox();
            }
        }
        private void UpdateRichTextBox()
        {
            richTextBox.Document.Blocks.Clear();
            foreach (var log in viewModel.Logs)
            {
                var paragraph = new Paragraph();
                var run = new Run(log);

                // 简单判断日志内容，实际应用中可以更复杂
                if (log.Contains("alarm"))
                {
                    run.Foreground = Brushes.Red;
                }
                else
                {
                    run.Foreground = Brushes.Black;
                }

                paragraph.Inlines.Add(run);
                richTextBox.Document.Blocks.Add(paragraph);
            }

            richTextBox.ScrollToEnd();
        }
      
        public string CurTime()
        {
            return DateTime.Now + "." + DateTime.Now.Millisecond.ToString("D3");
        }
        object loglock=new object();
        public async void LdrLog(string logMessage, bool isAlarm = false)
        {
            await Task.Run(() =>
            {
                lock (loglock)
                {
                    // 创建新的段落
                    Paragraph paragraph = new Paragraph();

                    // 设置文本内容和颜色
                    Run run = new Run(logMessage);
                    if (isAlarm)
                    {
                        run.Foreground = Brushes.Red;
                    }
                    else
                    {
                        run.Foreground = Brushes.Black; // 默认颜色
                    }

                    // 将Run添加到段落中
                    paragraph.Inlines.Add(run);

                    // 将段落添加到RichTextBox的文档中
                    richTextBox.Document.Blocks.Add(paragraph);
                }
            });
        }
    }

    public class LogViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _logs;
        public ObservableCollection<string> Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                OnPropertyChanged("Logs");
            }
        }

        public LogViewModel()
        {
            Logs = new ObservableCollection<string>();
        }

        public void AddLog(string logMessage)
        {
            Logs.Add(logMessage);
            Logs = Logs;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
