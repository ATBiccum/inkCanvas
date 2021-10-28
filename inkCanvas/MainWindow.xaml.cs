/*inkCanvas Project
 * 
 * This project is for a whiteboard that includes various features.
 * You can draw with a pen in the window with various sizes and colours by using the sliders
 * You can change the shape of the pen by using the three buttons
 * And you can save, open and clear the space with the buttons on the top
 * 
 * 
 * Tony Biccum
 * Project created for ECET 230 
 * October 28th, 2021
 * Camosun College
 */

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
    public partial class MainWindow : Window
    {
        SolidColorBrush solidcolorbrush = new SolidColorBrush();
        DrawingAttributes inkDrawingAttributes = new DrawingAttributes();
        int stylusStyleFlag = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;                                            //Run MainWindow_Loaded before program loads
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            theInkCanvas.DefaultDrawingAttributes.Color = Colors.Black;             //Set default color to black
            scrollBarSize.Value = 1;
            circleBrush.IsChecked = true;                                           //Set default brush to circle
            
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
            //Code from: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/storing-ink?view=netframeworkdesktop-4.8
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
            this.theInkCanvas.Strokes.Clear();                                  //Clear the ink canvas of all strokes if clear button pushed
        }
        private void updatePen()
        {
            inkDrawingAttributes = new DrawingAttributes();                     //Create new instance of drawing attributes

            double value = Convert.ToDouble(scrollBarSize.Value);
            byte red = Convert.ToByte(scrollBarRed.Value);                      //Convert all scroll bar values into bytes
            byte green = Convert.ToByte(scrollBarGreen.Value);
            byte blue = Convert.ToByte(scrollBarBlue.Value);

            txtSize.Text = value.ToString("f1");
            txtRed.Text = Convert.ToString(red);                                //Set the text on the scroll bars to the byte variables
            txtGreen.Text = Convert.ToString(green);
            txtBlue.Text = Convert.ToString(blue);

            solidcolorbrush.Color = Color.FromArgb(255, red, green, blue);      //Save the colour to 3 bytes r g b

            buttColorSelect.Background = solidcolorbrush;                       //Set the color taster to selected color
            inkDrawingAttributes.Color = solidcolorbrush.Color;                 //Set the stylus color to the rgb color

            inkDrawingAttributes.Width = value;                                 //Set the width of the stylus to the size value
            inkDrawingAttributes.Height = value;                                //Set the height of the stylus to the size value
           
            if (stylusStyleFlag == 0)                                           //If circle is checked will set flag to 0
            {
                inkDrawingAttributes.StylusTip = StylusTip.Ellipse;             //Sets the stylus to a circle
            }
            else if (stylusStyleFlag == 1)                                           //If rectangle is checked will set flag to 1
            {
                inkDrawingAttributes.StylusTip = StylusTip.Rectangle;           //Sets the stylus to a rectangle
            }
           else if (stylusStyleFlag == 2)                                            //If the oval is checked will set flag to 2
            {
                inkDrawingAttributes.StylusTip = StylusTip.Ellipse;             //Change to a circle with width x 4
                inkDrawingAttributes.Width = scrollBarSize.Value * 4;
            }
            theInkCanvas.DefaultDrawingAttributes = inkDrawingAttributes;       //Set all attributes to the stylus we just created
        }

        private void scrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updatePen();    //Update pen function if a scroll bar is changed
            
        }

        private void circleBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 0;    //Set flag to 0 and update pen if circle is checked
            updatePen();
            
        }

        private void squareBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 1;    //Set flag to 1 and update pen if rectangle is checked
            updatePen();
        }

        private void ovalBrush_Checked(object sender, RoutedEventArgs e)
        {
            stylusStyleFlag = 2;    //Set flag to 2 and update pen if oval is checked
            updatePen();
        }
    }
}
