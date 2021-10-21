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

namespace inkCanvas
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
            rbuttBlack.IsChecked = true;
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

        }

        private void rbuttRed_Checked(object sender, RoutedEventArgs e)
        {
            
            solidcolorbrush.Color = Colors.Red;
            updatePen(); 
        }

        private void rbuttGreen_Checked(object sender, RoutedEventArgs e)
        {
            
            solidcolorbrush.Color = Colors.Green;
            updatePen();
        }

        private void rbuttBlue_Checked(object sender, RoutedEventArgs e)
        {
            
            solidcolorbrush.Color = Colors.Blue;
            updatePen();
        }

        private void rbuttBlack_Checked(object sender, RoutedEventArgs e)
        {
            
            solidcolorbrush.Color = Colors.Black;
            updatePen();
        }
        private void updatePen()
        {
            inkDrawingAttributes = new DrawingAttributes();
            inkDrawingAttributes.Color = solidcolorbrush.Color;
            theInkCanvas.DefaultDrawingAttributes = inkDrawingAttributes;
            //if (rbuttBlack.IsChecked == true)
            //{
            //    solidcolorbrush.Color = Colors.Black;
            //}
        }
    }
}
