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
            foreach (KeyValuePair<string, Tool> keyValue in TreeTop.TransformTools)
            {
                string uri = "C:/Users/90-STICK/Documents/Visual Studio 2015/Projects/Paint2/Paint2/bin/icons/" + keyValue.Key.ToString() + ".png";
                ImageBrush image = new ImageBrush();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(uri);
                bitmapImage.EndInit();
                image.ImageSource = bitmapImage;
                Button button = new Button
                {
                    Tag = keyValue.Key.ToString(),
                    Width = 30,
                    Height = 30,
                    Margin = new Thickness(4),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = image
                };
                button.Click += new RoutedEventHandler(Instance.ButtonChangeTool);
                Instance.toolbarPanel.Children.Add(button);
            }

            foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
            {
                Button button = new Button
                {
                    Tag = color.Key.ToString(),
                    Background = color.Value,
                    Height = 24,
                    Width = 24,
                    Margin = new Thickness(2.5)
                };
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
            Button button_Clean = new Button
            {
                Name = "Clean",
                Width = 30,
                Height = 30,
                Margin = new Thickness(4),
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = image_Clean
            };
            button_Clean.Click += new RoutedEventHandler(Instance.CleanMyCanvas);
            Instance.toolbarPanel.Children.Add(button_Clean);

            string uri_MinusZoom = "C:/Users/90-STICK/Documents/Visual Studio 2015/Projects/Paint2/Paint2/bin/icons/MinusZoom.png";
            ImageBrush image_MinusZoom = new ImageBrush();
            BitmapImage bitmapImage_MinusZoom = new BitmapImage();
            bitmapImage_MinusZoom.BeginInit();
            bitmapImage_MinusZoom.UriSource = new Uri(uri_MinusZoom);
            bitmapImage_MinusZoom.EndInit();
            image_MinusZoom.ImageSource = bitmapImage_MinusZoom;
            Button button_MinusZoom = new Button
            {
                Name = "MinusZoom",
                Width = 30,
                Height = 30,
                Margin = new Thickness(4),
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = image_MinusZoom
            };
            button_MinusZoom.Click += new RoutedEventHandler(Instance.MinusZoomMyCanvas);
            Instance.toolbarPanel.Children.Add(button_MinusZoom);
        }

        public static void PropertyButtonGeneration()
        {
            Label Changelinecolor = new Label
            {
                Content = "Change line color",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Instance.PropToolBarPanel.Children.Add(Changelinecolor);
            ToolBar prop1 = new ToolBar
            {
                Name = "PropertiesToolBar1",
                Margin = new Thickness(2)
            };

            foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
            {
                Button button = new Button
                {
                    Tag = color.Key.ToString(),
                    Background = color.Value,
                    Height = 18,
                    Width = 18,
                    Margin = new Thickness(2.5)
                };
                button.Click += new RoutedEventHandler(Instance.ChangeStrokeColor);
                prop1.Items.Add(button);
                
            }

            Instance.PropToolBarPanel.Children.Add(prop1);
            
            bool HaveLineorPolyline = false;

            foreach(Figure figure in TreeTop.Figures)
            {
                if(figure.Selected == true & (figure is Line || figure is Pencil))
                {
                    HaveLineorPolyline = true;
                    break;
                }
            }

            if (HaveLineorPolyline == false)
            {
                Label Changefillcolor = new Label
                {
                    Content = "Change fill color",
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Instance.PropToolBarPanel.Children.Add(Changefillcolor);
                ToolBar prop2 = new ToolBar
                {
                    Name = "PropertiesToolBar2",
                    Margin = new Thickness(2)
                };

                foreach (KeyValuePair<String, Brush> color in TreeTop.TransformColor)
                {
                    Button button = new Button
                    {
                        Tag = color.Key.ToString(),
                        Background = color.Value,
                        Height = 18,
                        Width = 18,
                        Margin = new Thickness(2.5)
                    };
                    button.Click += new RoutedEventHandler(Instance.ChangeBrushColor);
                    prop2.Items.Add(button);
                }

                Instance.PropToolBarPanel.Children.Add(prop2);
            }

            HaveLineorPolyline = false;

            Label Changedash = new Label
            {
                Content = "Change dash",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Instance.PropToolBarPanel.Children.Add(Changedash);

            foreach (KeyValuePair<String, DashStyle> dash in TreeTop.TransformDashProp)
            {
                Button button = new Button
                {
                    Height = 23,
                    Width = 60,
                    Content = dash.Key.ToString(),
                    Margin = new Thickness(2)
                };
                button.Click += new RoutedEventHandler(Instance.ChangeDash);
                Instance.PropToolBarPanel.Children.Add(button);
            }

            Label Removethefigures = new Label
            {
                Content = "Remove the figures",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Instance.PropToolBarPanel.Children.Add(Removethefigures);

            Button ClearSelectedFigure = new Button
            {
                Height = 23,
                Width = 60,
                Content = "Delet",
                Margin = new Thickness(2)
            };
            ClearSelectedFigure.Click += new RoutedEventHandler(Instance.ClearSelectedFigure);
            Instance.PropToolBarPanel.Children.Add(ClearSelectedFigure);

            Label Movethefigures = new Label
            {
                Content = "Move the figures",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Instance.PropToolBarPanel.Children.Add(Movethefigures);

            Button HandForSelectedFigure = new Button
            {
                Height = 23,
                Width = 60,
                Content = "Hand",
                Margin = new Thickness(2)
            };
            HandForSelectedFigure.Click += new RoutedEventHandler(Instance.HandForSelectedFigure);
            Instance.PropToolBarPanel.Children.Add(HandForSelectedFigure);

            bool HaveOnlyRoundRect = false;
            double RoundX = 0;
            double RoundY = 0;
            foreach (Figure figure in TreeTop.Figures)
            {
                if(figure is RoundRect & figure.Selected == true)
                {
                    RoundX = (figure as RoundRect).RoundX;
                    RoundY = (figure as RoundRect).RoundY;
                    break;
                }
            }

            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Selected)
                {
                    if (figure is RoundRect)
                    {
                        if (((figure as RoundRect).RoundX == RoundX & (figure as RoundRect).RoundY == RoundY) & figure.Selected == true)
                        {
                            HaveOnlyRoundRect = true;
                        }
                    }
                    else
                    {
                        HaveOnlyRoundRect = false;
                        break;
                    }
                }
            }

            if (HaveOnlyRoundRect)
            {
                Label ChengeRoundX = new Label
                {
                    Content = "Change RoundX",
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Instance.PropToolBarPanel.Children.Add(ChengeRoundX);
                Slider sldRoundX = new Slider
                {
                    Maximum = 40,
                    Minimum = 5,
                    Height = 26,
                    Width = 79,
                    Value = RoundX
                };
                sldRoundX.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.ChangeRoundX);
                sldRoundX.PreviewMouseUp += new MouseButtonEventHandler(Instance.SldMouseUp);
                Instance.PropToolBarPanel.Children.Add(sldRoundX);

                Label ChengeRoundY = new Label
                {
                    Content = "Change RoundY",
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Instance.PropToolBarPanel.Children.Add(ChengeRoundY);
                Slider sldRoundY = new Slider
                {
                    Maximum = 40,
                    Minimum = 5,
                    Height = 26,
                    Width = 79,
                    Value = RoundY
                };
                sldRoundY.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.ChangeRoundY);
                sldRoundY.PreviewMouseUp += new MouseButtonEventHandler(Instance.SldMouseUp);
                Instance.PropToolBarPanel.Children.Add(sldRoundY);
            }
            HaveOnlyRoundRect = false;
        }

        public static void RowThicknessButton(double i)
        {
            Label ChangeRowThikness = new Label
            {
                Content = "Change row thikness",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Instance.PropToolBarPanel.Children.Add(ChangeRowThikness);
            Slider sld = new Slider
            {
                Height = 26,
                Width = 79,
                Minimum = 1,
                Maximum = 20,
                Value = i
            };
            sld.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Instance.RowThicnessChange);
            sld.PreviewMouseUp += new MouseButtonEventHandler (Instance.SldMouseUp);
            Instance.PropToolBarPanel.Children.Add(sld);
        }
    }
}
