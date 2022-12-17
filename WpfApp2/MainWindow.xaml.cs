using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Threading;


namespace TypingTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public Random random = new Random();
        public string[] words = new string[] { "params", "equals", "unshort", "global", "with", "unsafe", "unmanaged", "readonly", "fixed", "extern", "nuint", "uint", "async", "sync", "set", "get", "nameof", "abstract", "override", "internal", "as", "return", "select", "file", "not", "string", "and", "or", "function", "else", "if", "foreach", "int", "bool", "var", "console", "new", "string", "char", "float", "byte", "double", "void", "const", "unsigned", "to", "do", "using", "virtual", "private", "protected", "typedef", "struct", "signed", "noexcept", "except", "public", "static", "sealed", "operator", "extern"};
        string myText;
        int sec = 0;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            textBox.Focus();
            name1_Copy.Visibility = Visibility.Hidden;
            int n = 15;
            string[] str = new string[n];
            for (int i = 0; i < n; i++)
            {
                str[i] = words[random.Next(words.Length)];
            }
            myText = string.Join(" ", str);
            PreviewKeyDown += new KeyEventHandler(OnPreviewKeyDown);
            PreviewKeyUp += new KeyEventHandler(OnPreviewKeyUp);
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(myText));
            document.Blocks.Add(paragraph);
            textBox.Document = document;
            textBox.CaretPosition = textBox.Document.ContentStart;
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        private void timerTick(object sender, EventArgs e)
        {
            sec += 1;
            name1_Copy.Visibility = Visibility.Visible;
            name1_Copy.Text = sec.ToString();
        }
        private MediaPlayer player = new MediaPlayer();
        private static TextPointer GetPositionAtCharOffset(TextPointer start, int numbertOfChars)
        {
            var offset = start;
            int i = 0;
            string stringSoFar = "";
            while (stringSoFar.Length < numbertOfChars)
            {
                i++;
                TextPointer offsetCandidate = start.GetPositionAtOffset(
                        i, LogicalDirection.Forward);

                if (offsetCandidate == null)
                    return offset;

                offset = offsetCandidate;
                stringSoFar = new TextRange(start, offset).Text;
            }
            return offset;
        }
        private static TextRange GetTextRange(TextPointer start, int startIndex, int length)
        {
            var rangeStart = GetPositionAtCharOffset(start, startIndex);
            var rangeEnd = GetPositionAtCharOffset(rangeStart, length);
            return new TextRange(rangeStart, rangeEnd);
        }
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            timer.Start();
            if (GetCareteIndex() == myText.Length)
            {
                timer.Stop();
                MessageBox.Show($"WPF: {((float)myText.Length / 5) / sec * 50}", "Скорость печати");
                System.Windows.Application.Current.MainWindow.Close();
                return;
            }
            if (e.IsDown && !e.IsRepeat)
            {
                player.Open(new Uri("D:/123.wav", UriKind.Relative));
                player.Play();
            }
            byte R = (byte)random.Next(110, 255);
            byte G = (byte)random.Next(110, 255);
            byte B = (byte)random.Next(110, 255);
            DropShadowEffect ef = new DropShadowEffect { Color = Color.FromArgb(255, R, G, B), Direction = 0, ShadowDepth = 0, BlurRadius = 50 };
            SolidColorBrush br = new SolidColorBrush(Color.FromArgb(150, R, G, B));
            string textBoxText = new TextRange(textBox.Document.ContentStart, textBox.CaretPosition).Text + (e.Key.ToString().ToLower() == "space" ? " " : e.Key.ToString().ToLower());
            string mText = myText.Substring(0, GetCareteIndex() + 1);
            if (textBoxText == mText && e.Key.ToString() != "Back")
            {
                var range = GetTextRange(textBox.Document.ContentStart, 0, GetCareteIndex());
                range.ClearAllProperties();
                textBox.CaretPosition.DeleteTextInRun(1);
                range = GetTextRange(textBox.Document.ContentStart, 0, GetCareteIndex());
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.White));
                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                var range = GetTextRange(textBox.Document.ContentStart, 0, GetCareteIndex());
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Red));
                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
            }
            if (e.Key == Key.Escape && !e.IsRepeat)
            {
                esc.Fill = br;
                esc.Effect = ef;
            }
            if (e.Key == Key.F1 && !e.IsRepeat)
            {
                f1.Fill = br;
                f1.Effect = ef;
            }
            if (e.Key == Key.F2 && !e.IsRepeat)
            {
                f2.Fill = br;
                f2.Effect = ef;
            }
            if (e.Key == Key.F3 && !e.IsRepeat)
            {
                f3.Fill = br;
                f3.Effect = ef;
            }
            if (e.Key == Key.F4 && !e.IsRepeat)
            {
                f4.Fill = br;
                f4.Effect = ef;
            }
            if (e.Key == Key.F5 && !e.IsRepeat)
            {
                f5.Fill = br;
                f5.Effect = ef;
            }
            if (e.Key == Key.F6 && !e.IsRepeat)
            {
                f6.Fill = br;
                f6.Effect = ef;
            }
            if (e.Key == Key.F7 && !e.IsRepeat)
            {
                f7.Fill = br;
                f7.Effect = ef;
            }
            if (e.Key == Key.OemTilde && !e.IsRepeat)
            {
                tilda.Fill = br;
                tilda.Effect = ef;
            }
            if (e.Key == Key.D1 && !e.IsRepeat)
            {
                _1.Fill = br;
                _1.Effect = ef;
            }
            if (e.Key == Key.D2 && !e.IsRepeat)
            {
                _2.Fill = br;
                _2.Effect = ef;
            }
            if (e.Key == Key.D3 && !e.IsRepeat)
            {
                _3.Fill = br;
                _3.Effect = ef;
            }
            if (e.Key == Key.D4 && !e.IsRepeat)
            {
                _4.Fill = br;
                _4.Effect = ef;
            }
            if (e.Key == Key.D5 && !e.IsRepeat)
            {
                _5.Fill = br;
                _5.Effect = ef;
            }
            if (e.Key == Key.D6 && !e.IsRepeat)
            {
                _6.Fill = br;
                _6.Effect = ef;
            }
            if (e.Key == Key.D7 && !e.IsRepeat)
            {
                _7.Fill = br;
                _7.Effect = ef;
            }
            if (e.Key == Key.D8 && !e.IsRepeat)
            {
                _8.Fill = br;
                _8.Effect = ef;
            }
            if (e.Key == Key.D9 && !e.IsRepeat)
            {
                _9.Fill = br;
                _9.Effect = ef;
            }
            if (e.Key == Key.D0 && !e.IsRepeat)
            {
                _0.Fill = br;
                _0.Effect = ef;
            }
            if (e.Key == Key.OemMinus && !e.IsRepeat)
            {
                min.Fill = br;
                min.Effect = ef;
            }
            if (e.Key == Key.OemPlus && !e.IsRepeat)
            {
                plus.Fill = br;
                plus.Effect = ef;
            }
            if (e.Key == Key.Back && !e.IsRepeat)
            {
                backSpace.Fill = br;
                backSpace.Effect = ef;
            }
            if (e.Key == Key.OemPipe && !e.IsRepeat)
            {
                backSlash.Fill = br;
                backSlash.Effect = ef;
            }
            if (e.Key == Key.P && !e.IsRepeat)
            {
                p.Fill = br;
                p.Effect = ef;
            }
            if (e.Key == Key.O && !e.IsRepeat)
            {
                o.Fill = br;
                o.Effect = ef;
            }
            if (e.Key == Key.I && !e.IsRepeat)
            {
                i.Fill = br;
                i.Effect = ef;
            }
            if (e.Key == Key.U && !e.IsRepeat)
            {
                u.Fill = br;
                u.Effect = ef;
            }
            if (e.Key == Key.Y && !e.IsRepeat)
            {
                y.Fill = br;
                y.Effect = ef;
            }
            if (e.Key == Key.T && !e.IsRepeat)
            {
                t.Fill = br;
                t.Effect = ef;
            }
            if (e.Key == Key.R && !e.IsRepeat)
            {
                r.Fill = br;
                r.Effect = ef;
            }
            if (e.Key == Key.E && !e.IsRepeat)
            {
                _e.Fill = br;
                _e.Effect = ef;
            }
            if (e.Key == Key.W && !e.IsRepeat)
            {
                w.Fill = br;
                w.Effect = ef;
            }
            if (e.Key == Key.Q && !e.IsRepeat)
            {
                q.Fill = br;
                q.Effect = ef;
            }
            if (e.Key == Key.Tab && !e.IsRepeat)
            {
                tab.Fill = br;
                tab.Effect = ef;
            }
            if (e.Key == Key.CapsLock && !e.IsRepeat)
            {
                capsLock.Fill = br;
                capsLock.Effect = ef;
            }
            if (e.Key == Key.A && !e.IsRepeat)
            {
                a.Fill = br;
                a.Effect = ef;
            }
            if (e.Key == Key.S && !e.IsRepeat)
            {
                s.Fill = br;
                s.Effect = ef;
            }
            if (e.Key == Key.D && !e.IsRepeat)
            {
                d.Fill = br;
                d.Effect = ef;
            }
            if (e.Key == Key.F && !e.IsRepeat)
            {
                f.Fill = br;
                f.Effect = ef;
            }
            if (e.Key == Key.G && !e.IsRepeat)
            {
                g.Fill = br;
                g.Effect = ef;
            }
            if (e.Key == Key.H && !e.IsRepeat)
            {
                h.Fill = br;
                h.Effect = ef;
            }
            if (e.Key == Key.J && !e.IsRepeat)
            {
                j.Fill = br;
                j.Effect = ef;
            }
            if (e.Key == Key.K && !e.IsRepeat)
            {
                k.Fill = br;
                k.Effect = ef;
            }
            if (e.Key == Key.L && !e.IsRepeat)
            {
                l.Fill = br;
                l.Effect = ef;
            }
            if (e.Key == Key.Enter && !e.IsRepeat)
            {
                enter.Fill = br;
                enter.Effect = ef;
            }
            if (e.Key == Key.RightShift && !e.IsRepeat)
            {
                rShift.Fill = br;
                rShift.Effect = ef;
            }
            if (e.Key == Key.OemQuestion && !e.IsRepeat)
            {
                slash.Fill = br;
                slash.Effect = ef;
            }
            if (e.Key == Key.OemPeriod && !e.IsRepeat)
            {
                dot.Fill = br;
                dot.Effect = ef;
            }
            if (e.Key == Key.OemComma && !e.IsRepeat)
            {
                zpt.Fill = br;
                zpt.Effect = ef;
            }
            if (e.Key == Key.M && !e.IsRepeat)
            {
                m.Fill = br;
                m.Effect = ef;
            }
            if (e.Key == Key.N && !e.IsRepeat)
            {
                n.Fill = br;
                n.Effect = ef;
            }
            if (e.Key == Key.B && !e.IsRepeat)
            {
                b.Fill = br;
                b.Effect = ef;
            }
            if (e.Key == Key.V && !e.IsRepeat)
            {
                v.Fill = br;
                v.Effect = ef;
            }
            if (e.Key == Key.C && !e.IsRepeat)
            {
                c.Fill = br;
                c.Effect = ef;
            }
            if (e.Key == Key.X && !e.IsRepeat)
            {
                x.Fill = br;
                x.Effect = ef;
            }
            if (e.Key == Key.Z && !e.IsRepeat)
            {
                z.Fill = br;
                z.Effect = ef;
            }
            if (e.Key == Key.LeftShift && !e.IsRepeat)
            {
                lShift.Fill = br;
                lShift.Effect = ef;
            }
            if (e.Key == Key.LeftCtrl && !e.IsRepeat)
            {
                lCtrl.Fill = br;
                lCtrl.Effect = ef;
            }
            if (e.Key == Key.LWin && !e.IsRepeat)
            {
                lWin.Fill = br;
                lWin.Effect = ef;
            }
            if (e.Key == Key.LeftAlt && !e.IsRepeat)
            {
                lAlt.Fill = br;
                lAlt.Effect = ef;
            }
            if (e.Key == Key.Space && !e.IsRepeat)
            {
                space.Fill = br;
                space.Effect = ef;
            }
            if (e.Key == Key.RightAlt && !e.IsRepeat)
            {
                rAlt.Fill = br;
                rAlt.Effect = ef;
            }
            if (e.Key == Key.RWin && !e.IsRepeat)
            {
                rWin.Fill = br;
                rWin.Effect = ef;
            }
            if (e.Key == Key.RightCtrl && !e.IsRepeat)
            {
                rCtrl.Fill = br;
                rCtrl.Effect = ef;
            }
        }
        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                esc.Fill = Brushes.Transparent;
                esc.Effect = null;
            }
            if (e.Key == Key.F1 && !e.IsRepeat)
            {
                f1.Fill = Brushes.Transparent;
                f1.Effect = null;
            }
            if (e.Key == Key.F2 && !e.IsRepeat)
            {
                f2.Fill = Brushes.Transparent;
                f2.Effect = null;
            }
            if (e.Key == Key.F3 && !e.IsRepeat)
            {
                f3.Fill = Brushes.Transparent;
                f3.Effect = null;
            }
            if (e.Key == Key.F4 && !e.IsRepeat)
            {
                f4.Fill = Brushes.Transparent;
                f4.Effect = null;
            }
            if (e.Key == Key.F5 && !e.IsRepeat)
            {
                f5.Fill = Brushes.Transparent;
                f5.Effect = null;
            }
            if (e.Key == Key.F6 && !e.IsRepeat)
            {
                f6.Fill = Brushes.Transparent;
                f6.Effect = null;
            }
            if (e.Key == Key.F7 && !e.IsRepeat)
            {
                f7.Fill = Brushes.Transparent;
                f7.Effect = null;
            }
            if (e.Key == Key.OemTilde && !e.IsRepeat)
            {
                tilda.Fill = Brushes.Transparent;
                tilda.Effect = null;
            }
            if (e.Key == Key.D1 && !e.IsRepeat)
            {
                _1.Fill = Brushes.Transparent;
                _1.Effect = null;
            }
            if (e.Key == Key.D2 && !e.IsRepeat)
            {
                _2.Fill = Brushes.Transparent;
                _2.Effect = null;
            }
            if (e.Key == Key.D3 && !e.IsRepeat)
            {
                _3.Fill = Brushes.Transparent;
                _3.Effect = null;
            }
            if (e.Key == Key.D4 && !e.IsRepeat)
            {
                _4.Fill = Brushes.Transparent;
                _4.Effect = null;
            }
            if (e.Key == Key.D5 && !e.IsRepeat)
            {
                _5.Fill = Brushes.Transparent;
                _5.Effect = null;
            }
            if (e.Key == Key.D6 && !e.IsRepeat)
            {
                _6.Fill = Brushes.Transparent;
                _6.Effect = null;
            }
            if (e.Key == Key.D7 && !e.IsRepeat)
            {
                _7.Fill = Brushes.Transparent;
                _7.Effect = null;
            }
            if (e.Key == Key.D8 && !e.IsRepeat)
            {
                _8.Fill = Brushes.Transparent;
                _8.Effect = null;
            }
            if (e.Key == Key.D9 && !e.IsRepeat)
            {
                _9.Fill = Brushes.Transparent;
                _9.Effect = null;
            }
            if (e.Key == Key.D0 && !e.IsRepeat)
            {
                _0.Fill = Brushes.Transparent;
                _0.Effect = null;
            }
            if (e.Key == Key.OemMinus && !e.IsRepeat)
            {
                min.Fill = Brushes.Transparent;
                min.Effect = null;
            }
            if (e.Key == Key.OemPlus && !e.IsRepeat)
            {
                plus.Fill = Brushes.Transparent;
                plus.Effect = null;
            }
            if (e.Key == Key.Back && !e.IsRepeat)
            {
                backSpace.Fill = Brushes.Transparent;
                backSpace.Effect = null;
            }
            if (e.Key == Key.OemPipe && !e.IsRepeat)
            {
                backSlash.Fill = Brushes.Transparent;
                backSlash.Effect = null;
            }
            if (e.Key == Key.P && !e.IsRepeat)
            {
                p.Fill = Brushes.Transparent;
                p.Effect = null;
            }
            if (e.Key == Key.O && !e.IsRepeat)
            {
                o.Fill = Brushes.Transparent;
                o.Effect = null;
            }
            if (e.Key == Key.I && !e.IsRepeat)
            {
                i.Fill = Brushes.Transparent;
                i.Effect = null;
            }
            if (e.Key == Key.U && !e.IsRepeat)
            {
                u.Fill = Brushes.Transparent;
                u.Effect = null;
            }
            if (e.Key == Key.Y && !e.IsRepeat)
            {
                y.Fill = Brushes.Transparent;
                y.Effect = null;
            }
            if (e.Key == Key.T && !e.IsRepeat)
            {
                t.Fill = Brushes.Transparent;
                t.Effect = null;
            }
            if (e.Key == Key.R && !e.IsRepeat)
            {
                r.Fill = Brushes.Transparent;
                r.Effect = null;
            }
            if (e.Key == Key.E && !e.IsRepeat)
            {
                _e.Fill = Brushes.Transparent;
                _e.Effect = null;
            }
            if (e.Key == Key.W && !e.IsRepeat)
            {
                w.Fill = Brushes.Transparent;
                w.Effect = null;
            }
            if (e.Key == Key.Q && !e.IsRepeat)
            {
                q.Fill = Brushes.Transparent;
                q.Effect = null;
            }
            if (e.Key == Key.Tab && !e.IsRepeat)
            {
                tab.Fill = Brushes.Transparent;
                tab.Effect = null;
            }
            if (e.Key == Key.CapsLock && !e.IsRepeat)
            {
                capsLock.Fill = Brushes.Transparent;
                capsLock.Effect = null;
            }
            if (e.Key == Key.A && !e.IsRepeat)
            {
                a.Fill = Brushes.Transparent;
                a.Effect = null;
            }
            if (e.Key == Key.S && !e.IsRepeat)
            {
                s.Fill = Brushes.Transparent;
                s.Effect = null;
            }
            if (e.Key == Key.D && !e.IsRepeat)
            {
                d.Fill = Brushes.Transparent;
                d.Effect = null;
            }
            if (e.Key == Key.F && !e.IsRepeat)
            {
                f.Fill = Brushes.Transparent;
                f.Effect = null;
            }
            if (e.Key == Key.G && !e.IsRepeat)
            {
                g.Fill = Brushes.Transparent;
                g.Effect = null;
            }
            if (e.Key == Key.H && !e.IsRepeat)
            {
                h.Fill = Brushes.Transparent;
                h.Effect = null;
            }
            if (e.Key == Key.J && !e.IsRepeat)
            {
                j.Fill = Brushes.Transparent;
                j.Effect = null;
            }
            if (e.Key == Key.K && !e.IsRepeat)
            {
                k.Fill = Brushes.Transparent;
                k.Effect = null;
            }
            if (e.Key == Key.L && !e.IsRepeat)
            {
                l.Fill = Brushes.Transparent;
                l.Effect = null;
            }
            if (e.Key == Key.Enter && !e.IsRepeat)
            {
                enter.Fill = Brushes.Transparent;
                enter.Effect = null;
            }
            if (e.Key == Key.RightShift && !e.IsRepeat)
            {
                rShift.Fill = Brushes.Transparent;
                rShift.Effect = null;
            }
            if (e.Key == Key.OemQuestion && !e.IsRepeat)
            {
                slash.Fill = Brushes.Transparent;
                slash.Effect = null;
            }
            if (e.Key == Key.OemPeriod && !e.IsRepeat)
            {
                dot.Fill = Brushes.Transparent;
                dot.Effect = null;
            }
            if (e.Key == Key.OemComma && !e.IsRepeat)
            {
                zpt.Fill = Brushes.Transparent;
                zpt.Effect = null;
            }
            if (e.Key == Key.M && !e.IsRepeat)
            {
                m.Fill = Brushes.Transparent;
                m.Effect = null;
            }
            if (e.Key == Key.N && !e.IsRepeat)
            {
                n.Fill = Brushes.Transparent;
                n.Effect = null;
            }
            if (e.Key == Key.B && !e.IsRepeat)
            {
                b.Fill = Brushes.Transparent;
                b.Effect = null;
            }
            if (e.Key == Key.V && !e.IsRepeat)
            {
                v.Fill = Brushes.Transparent;
                v.Effect = null;
            }
            if (e.Key == Key.C && !e.IsRepeat)
            {
                c.Fill = Brushes.Transparent;
                c.Effect = null;
            }
            if (e.Key == Key.X && !e.IsRepeat)
            {
                x.Fill = Brushes.Transparent;
                x.Effect = null;
            }
            if (e.Key == Key.Z && !e.IsRepeat)
            {
                z.Fill = Brushes.Transparent;
                z.Effect = null;
            }
            if (e.Key == Key.LeftShift && !e.IsRepeat)
            {
                lShift.Fill = Brushes.Transparent;
                lShift.Effect = null;
            }
            if (e.Key == Key.LeftCtrl && !e.IsRepeat)
            {
                lCtrl.Fill = Brushes.Transparent;
                lCtrl.Effect = null;
            }
            if (e.Key == Key.LWin && !e.IsRepeat)
            {
                lWin.Fill = Brushes.Transparent;
                lWin.Effect = null;
            }
            if (e.Key == Key.LeftAlt && !e.IsRepeat)
            {
                lAlt.Fill = Brushes.Transparent;
                lAlt.Effect = null;
            }
            if (e.Key == Key.Space && !e.IsRepeat)
            {
                space.Fill = Brushes.Transparent;
                space.Effect = null;
            }
            if (e.Key == Key.RightAlt && !e.IsRepeat)
            {
                rAlt.Fill = Brushes.Transparent;
                rAlt.Effect = null;
            }
            if (e.Key == Key.RWin && !e.IsRepeat)
            {
                rWin.Fill = Brushes.Transparent;
                rWin.Effect = null;
            }
            if (e.Key == Key.RightCtrl && !e.IsRepeat)
            {
                rCtrl.Fill = Brushes.Transparent;
                rCtrl.Effect = null;
            }
            string bT = new TextRange(textBox.Document.ContentStart, textBox.CaretPosition).Text;
            string nT = myText.Substring(0, GetCareteIndex());
            // name1.Text = bT;
            // name2.Text = nT;
            // name3.Text = GetCareteIndex().ToString();
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private int GetCareteIndex()
        {
            TextPointer start = textBox.Document.ContentStart;
            TextPointer caret = textBox.CaretPosition;
            TextRange range = new TextRange(start, caret);
            return range.Text.Length;
        }
        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
        }
    }
}
