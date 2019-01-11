using System;
using System.Collections.Generic;
using System.Linq;
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
using Paint2.Paint;

namespace Paint2.Paint
{
    class ButtonGeneration : MainWindow
    {
        public static void Generation()
        {
            foreach (KeyValuePair<string, Tool> keyValue in TreeTop.Transform)
            {
                string uri = "C:/Users/90-STICK/Documents/Visual Studio 2015/Projects/Paint2/Paint2/bin/icons/" + keyValue.Key.ToString() + ".png";
                ImageBrush image = new ImageBrush();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(uri);
                bitmapImage.EndInit();
                image.ImageSource = bitmapImage;
                Button button = new Button();
                button.Tag = keyValue.Key.ToString();
                button.Width = 30;
                button.Height = 30;
                button.Margin = new Thickness(4);
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.Click += new RoutedEventHandler(Instance.ButtonChangeTool);
                button.Background = image;
                Instance.toolbarPanel.Children.Add(button);
            }

            foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
            {
                Button button = new Button();
                button.Tag = color.Key.ToString();
                button.Background = color.Value;
                button.Height = 24;
                button.Width = 24;
                button.Margin = new Thickness(2.5);
                button.Click += new RoutedEventHandler(Instance.ButtonChangeColor);
                Instance.colorbarPanel.Children.Add(button);
            }

            string uri_Clean = "C:/Users/90-STICK/Documents/Visual Studio 2015/Projects/Paint2/Paint2/bin/icons/Clean.png";
            ImageBrush image_Clean = new ImageBrush();
            BitmapImage bitmapImage_Clean = new BitmapImage();
            bitmapImage_Clean.BeginInit();
            bitmapImage_Clean.UriSource = new Uri(uri_Clean);
            bitmapImage_Clean.EndInit();
            image_Clean.ImageSource = bitmapImage_Clean;
            Button button_Clean = new Button();
            button_Clean.Name = "Clean";
            button_Clean.Width = 30;
            button_Clean.Height = 30;
            button_Clean.Margin = new Thickness(4);
            button_Clean.HorizontalAlignment = HorizontalAlignment.Left;
            button_Clean.Click += new RoutedEventHandler(Instance.CleanMyCanvas);
            button_Clean.Background = image_Clean;
            Instance.toolbarPanel.Children.Add(button_Clean);

            string uri_MinusZoom = "C:/Users/90-STICK/Documents/Visual Studio 2015/Projects/Paint2/Paint2/bin/icons/MinusZoom.png";
            ImageBrush image_MinusZoom = new ImageBrush();
            BitmapImage bitmapImage_MinusZoom = new BitmapImage();
            bitmapImage_MinusZoom.BeginInit();
            bitmapImage_MinusZoom.UriSource = new Uri(uri_MinusZoom);
            bitmapImage_MinusZoom.EndInit();
            image_MinusZoom.ImageSource = bitmapImage_MinusZoom;
            Button button_MinusZoom = new Button();
            button_MinusZoom.Name = "MinusZoom";
            button_MinusZoom.Width = 30;
            button_MinusZoom.Height = 30;
            button_MinusZoom.Margin = new Thickness(4);
            button_MinusZoom.HorizontalAlignment = HorizontalAlignment.Left;
            button_MinusZoom.Click += new RoutedEventHandler(Instance.MinusZoomMyCanvas);
            button_MinusZoom.Background = image_MinusZoom;
            Instance.toolbarPanel.Children.Add(button_MinusZoom);
        }

        public static void PropertyButtonGeneration()
        {
            ToolBar prop1 = new ToolBar();
            prop1.Name = "PropertiesToolBar1";
            prop1.Margin = new Thickness(2);
            
            foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
            {
                Button button = new Button();
                button.Tag = color.Key.ToString();
                button.Background = color.Value;
                button.Height = 18;
                button.Width = 18;
                button.Margin = new Thickness(2.5);
                button.Click += new RoutedEventHandler(Instance.ChangeStrokeColor);
                prop1.Items.Add(button);
                
            }

            Instance.PropToolBarPanel.Children.Add(prop1);

            ToolBar prop2 = new ToolBar();
            prop2.Name = "PropertiesToolBar2";
            prop2.Margin = new Thickness(2);

            bool HaveLineorPolyline = false;

            foreach(Figure figure in TreeTop.Figures)
            {
                if(figure.Select == true & (figure.Type == "Line" || figure.Type == "Pencil"))
                {
                    HaveLineorPolyline = true;
                }
            }

            if (HaveLineorPolyline == false)
            {

                foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
                {
                    Button button = new Button();
                    button.Tag = color.Key.ToString();
                    button.Background = color.Value;
                    button.Height = 18;
                    button.Width = 18;
                    button.Margin = new Thickness(2.5);
                    button.Click += new RoutedEventHandler(Instance.ChangeBrushColor);
                    prop2.Items.Add(button);
                }

                Instance.PropToolBarPanel.Children.Add(prop2);
            }

            HaveLineorPolyline = false;
            
            foreach (KeyValuePair<String, DashStyle> dash in TreeTop.TransformDashProp)
            {
                Button button = new Button();
                button.Height = 23;
                button.Width = 60;
                button.Content = dash.Key.ToString();
                button.Margin = new Thickness(2);
                button.Click += new RoutedEventHandler(Instance.ChangeDash);
                Instance.PropToolBarPanel.Children.Add(button);
            }

            Button ClearSelectedFigure = new Button();
            ClearSelectedFigure.Height = 23;
            ClearSelectedFigure.Width = 60;
            ClearSelectedFigure.Content = "Delet";
            ClearSelectedFigure.Click += new RoutedEventHandler(Instance.ClearSelectedFigure);
            ClearSelectedFigure.Margin = new Thickness(2);
            Instance.PropToolBarPanel.Children.Add(ClearSelectedFigure);

            Button HandForSelectedFigure = new Button();
            HandForSelectedFigure.Height = 23;
            HandForSelectedFigure.Width = 60;
            HandForSelectedFigure.Content = "Hand";
            HandForSelectedFigure.Click += new RoutedEventHandler(Instance.HandForSelectedFigure);
            HandForSelectedFigure.Margin = new Thickness(2);
            Instance.PropToolBarPanel.Children.Add(HandForSelectedFigure);

            bool HaveOnlyEllipse = true;
            double RoundX = 0;
            double RoundY = 0;
            foreach (Figure figure in TreeTop.Figures)
            {
                if(figure.Type == "RoundRect" & figure.Select == true)
                {
                    RoundX = figure.RoundX;
                    RoundY = figure.RoundY;
                    break;
                }
            }

            foreach (Figure figure in TreeTop.Figures)
            {
                if ((figure.Type != "RoundRect" & figure.Select) || ((figure.RoundX != RoundX || figure.RoundY != RoundY) & figure.Select == true))
                {
                    HaveOnlyEllipse = false;
                    break;
                }
            }

            if (HaveOnlyEllipse)
            {
                Label ChengeRoundX = new Label();
                ChengeRoundX.Content = "ChangeRoundX";
                Instance.PropToolBarPanel.Children.Add(ChengeRoundX);
                Slider sldRoundX = new Slider();
                sldRoundX.Maximum = 40;
                sldRoundX.Minimum = 5;
                sldRoundX.Height = 26;
                sldRoundX.Width = 79;
                sldRoundX.Value = RoundX;
                sldRoundX.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.changeRoundX);
                sldRoundX.PreviewMouseUp += new MouseButtonEventHandler(Instance.SldMouseUp);
                Instance.PropToolBarPanel.Children.Add(sldRoundX);
                Label ChengeRoundY = new Label();
                ChengeRoundY.Content = "ChangeRoundY";
                Instance.PropToolBarPanel.Children.Add(ChengeRoundY);
                Slider sldRoundY = new Slider();
                sldRoundY.Maximum = 40;
                sldRoundY.Minimum = 5;
                sldRoundY.Height = 26;
                sldRoundY.Width = 79;
                sldRoundY.Value = RoundY;
                sldRoundY.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.changeRoundY);
                sldRoundY.PreviewMouseUp += new MouseButtonEventHandler(Instance.SldMouseUp);
                Instance.PropToolBarPanel.Children.Add(sldRoundY);
            }
            HaveOnlyEllipse = true;
        }

        public static void RowThicknessButton(double i)
        {
            Label ChengeRoundX = new Label();
            ChengeRoundX.Content = "ChangeRowThikness";
            Instance.PropToolBarPanel.Children.Add(ChengeRoundX);
            Slider sld = new Slider();
            sld.Height = 26;
            sld.Width = 79;
            sld.Minimum = 1;
            sld.Maximum = 20;
            sld.Value = i;
            sld.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.RowThicnessChange);
            sld.PreviewMouseUp += new MouseButtonEventHandler (Instance.SldMouseUp);
            Instance.PropToolBarPanel.Children.Add(sld);
        }
    }
}
