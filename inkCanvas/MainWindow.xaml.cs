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
        int stylusStyleFlag = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            theInkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            circleBrush.IsChecked = true;
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
            inkDrawingAttributes = new DrawingAttributes();                     //Create new instance of drawing attributes

            byte red = Convert.ToByte(scrollBarRed.Value);                      //Convert all scroll bar values into bytes
            byte green = Convert.ToByte(scrollBarGreen.Value);
            byte blue = Convert.ToByte(scrollBarBlue.Value);
            byte value = Convert.ToByte(scrollBarSize.Value);

            txtRed.Text = Convert.ToString(red);                                //Set the text on the scroll bars to the byte variables
            txtGreen.Text = Convert.ToString(green);
            txtBlue.Text = Convert.ToString(blue);

            solidcolorbrush.Color = Color.FromArgb(255, red, green, blue);

            buttColorSelect.Background = solidcolorbrush;
            inkDrawingAttributes.Color = solidcolorbrush.Color;

            inkDrawingAttributes.Width = value;
            inkDrawingAttributes.Height = value;

            if (stylusStyleFlag == 0)
            {
                inkDrawingAttributes.StylusTip = StylusTip.Ellipse;
            }
            if (stylusStyleFlag == 1)
            {
                inkDrawingAttributes.StylusTip = StylusTip.Rectangle;
            }
           if (stylusStyleFlag == 2)
            {
                inkDrawingAttributes.StylusTip = StylusTip.Ellipse;
                inkDrawingAttributes.Width = scrollBarSize.Value * 4;
            }
            theInkCanvas.DefaultDrawingAttributes = inkDrawingAttributes;
        }

        private void scrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updatePen();
        }

        private void circleBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 0;
            updatePen();
            
        }

        private void squareBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 1;
            updatePen();
        }

        private void ovalBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 2;
            updatePen();
        }
    }
}
