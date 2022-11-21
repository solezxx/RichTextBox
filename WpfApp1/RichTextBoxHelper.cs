using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfApp1
{
    public class RichTextBoxHelper : DependencyObject
    {

        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RichTextBoxHelper), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                PropertyChangedCallback = (obj, e) =>
                {
                    var richTextBox = (RichTextBox)obj;
                    var text = GetRichText(richTextBox);
                    Run run = new Run();
                    Paragraph paragraph = new Paragraph();
                    if (text.Contains("2"))
                    {
                        run.Foreground=Brushes.Red;
                        paragraph.FontWeight = FontWeights.DemiBold;
                    }
                    else if (text.Contains("1"))
                    {
                        run.Foreground = Brushes.Blue;
                    }
                    else if (text.Contains("3"))
                    {
                        run.Foreground = Brushes.Black;
                    }
                    else
                    {
                        run.Foreground = Brushes.DarkGray;
                    }
                    if (text.Contains("clear"))
                    {
                        richTextBox.Document.Blocks.Clear();
                        text = text.Split(',')[0];
                    }
                    run.Text = text;
                    paragraph.Inlines.Add(run);
                    paragraph.LineHeight = 1;
                    richTextBox.Document.Blocks.Add(paragraph);
                    richTextBox.ScrollToEnd();
                }
            });
    }
}
