using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace theInkCanvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush solidcolorbrush = new SolidColorBrush();
        DrawingAttributes inkDrawingAttributes = new DrawingAttributes();


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            theInkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
        }

        private void buttSave_Click(object sender, RoutedEventArgs e)
        {
            //Code from: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/storing-ink?view=netframeworkdesktop-4.8
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "isf files (*.isf)|*.isf";

            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName,
                                               FileMode.Create);
                theInkCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void buttOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "isf files (*.isf)|*.isf";

            if (openFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName,
                                               FileMode.Open);
                theInkCanvas.Strokes = new StrokeCollection(fs);
                fs.Close();
            }
        }

        private void buttClear_Click(object sender, RoutedEventArgs e)
        {
            this.theInkCanvas.Strokes.Clear();
        }
        private void updatePen()
        {
            inkDrawingAttributes = new DrawingAttributes();
            byte red = Convert.ToByte(scrollBarRed.Value);
            byte green = Convert.ToByte(scrollBarGreen.Value);
            byte blue = Convert.ToByte(scrollBarBlue.Value);
            txtRed.Text = Convert.ToString(red);
            txtGreen.Text = Convert.ToString(green);
            txtBlue.Text = Convert.ToString(blue);
            solidcolorbrush.Color = Color.FromArgb(255, red, green, blue);
            buttColorSelect.Background = solidcolorbrush;
            inkDrawingAttributes.Color = solidcolorbrush.Color;
            theInkCanvas.DefaultDrawingAttributes = inkDrawingAttributes;
        }

        private void scrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updatePen();
        }
    }
}
