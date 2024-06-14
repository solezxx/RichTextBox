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
            richTextBox.Document.Blocks.Clear();
            XLog._richTextBox= richTextBox;
        }
        XColorLog.XLog XLog=new XColorLog.XLog();
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 模拟日志添加
            //AddLog(richTextBox,"This is a normal log.");
            //AddLog(richTextBox,"This is an alarm log!", LogLevel.Alarm);
            XLog.AddLog("This is a normal log.",XColorLog.LogLevel.Information);
        }

        public void AddLog(RichTextBox _richTextBox ,string logMessage, LogLevel logLevel = LogLevel.Normal)
        {
            // 创建新的段落
            Paragraph paragraph = new Paragraph();

            // 设置文本内容和颜色
            Run run = new Run(logMessage);
            if (logLevel == LogLevel.Alarm)
            {
                run.Foreground = Brushes.Red;
            }
            else
            {
                run.Foreground = Brushes.Black; // 默认颜色
            }

            // 将Run添加到段落中
            paragraph.Inlines.Add(run);
            paragraph.LineHeight = 1;

            // 将段落添加到RichTextBox的文档中
            _richTextBox.Document.Blocks.Add(paragraph);
            if (_richTextBox.Document.Blocks.Count > 10)
            {
                _richTextBox.Document.Blocks.Remove(richTextBox.Document.Blocks.FirstBlock);
            }

            // 滚动到文档末尾
            _richTextBox.ScrollToEnd();
        }
        public string CurTime()
        {
            return DateTime.Now + "." + DateTime.Now.Millisecond.ToString("D3");
        }
    }
    public enum LogLevel
    {
        Normal,
        Alarm
    }
}
