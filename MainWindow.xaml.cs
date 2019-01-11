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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Paint2
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        bool ClikOnCanvas = false;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            MyCanvas.Children.Add(Paint.TreeTop.FigureHost);
            ButtonGeneration.Generation();
            TreeTop.AddCondition();
        }

        private void Invalidate()
        {
            Paint.TreeTop.FigureHost.Children.Clear();
            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();
            foreach (var figure in Paint.TreeTop.Figures)
            {
                figure.Draw(drawingContext);
                if(figure.SelectRect != null)
                {
                    figure.SelectRect.Draw(drawingContext);
                }
            }

            drawingContext.Close();
            Paint.TreeTop.FigureHost.Children.Add(drawingVisual);
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            TreeTop.ToolNow.MouseDown(e.GetPosition(MyCanvas));
            ClikOnCanvas = true;
            Invalidate();
        }

        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ClikOnCanvas)
            {
                TreeTop.ToolNow.MouseMove(e.GetPosition(MyCanvas));
                if (TreeTop.ToolNow == TreeTop.Transform["Hand"])
                {
                    ScrollViewerCanvas.ScrollToVerticalOffset(TreeTop.HandScrollY);
                    ScrollViewerCanvas.ScrollToHorizontalOffset(TreeTop.HandScrollX);
                }
                Invalidate();
            }   
        }
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ClikOnCanvas)
            {
                TreeTop.ToolNow.MouseUp(e.GetPosition(MyCanvas));

                if (TreeTop.ToolNow != TreeTop.Transform["Allotment"] & TreeTop.ToolNow != TreeTop.Transform["ZoomRect"] & TreeTop.ToolNow != TreeTop.Transform["Hand"] & TreeTop.ToolNow != TreeTop.HandTool)
                {
                    TreeTop.AddCondition();
                    gotoPastCondition.IsEnabled = true;
                    gotoSecondCondition.IsEnabled = false;
                }
                if (TreeTop.ToolNow == TreeTop.Transform["ZoomRect"])
                {
                    MyCanvas.LayoutTransform = new ScaleTransform(TreeTop.ScaleRateX, TreeTop.ScaleRateY);
                    ScrollViewerCanvas.ScrollToVerticalOffset(TreeTop.DistanceToPointY * TreeTop.ScaleRateY);
                    ScrollViewerCanvas.ScrollToHorizontalOffset(TreeTop.DistanceToPointX * TreeTop.ScaleRateX);
                }
                ClikOnCanvas = false;
                Invalidate();
            }
        }

        private void MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ClikOnCanvas)
            {
                TreeTop.ToolNow.MouseUp(e.GetPosition(MyCanvas));

                if (TreeTop.ToolNow != TreeTop.Transform["Allotment"] & TreeTop.ToolNow != TreeTop.Transform["ZoomRect"] & TreeTop.ToolNow != TreeTop.Transform["Hand"] & TreeTop.ToolNow != TreeTop.HandTool)
                {
                    TreeTop.AddCondition();
                    gotoPastCondition.IsEnabled = true;
                    gotoSecondCondition.IsEnabled = false;
                }
                if (TreeTop.ToolNow == TreeTop.Transform["ZoomRect"])
                {
                    MyCanvas.LayoutTransform = new ScaleTransform(TreeTop.ScaleRateX, TreeTop.ScaleRateY);
                    ScrollViewerCanvas.ScrollToVerticalOffset(TreeTop.DistanceToPointY * TreeTop.ScaleRateY);
                    ScrollViewerCanvas.ScrollToHorizontalOffset(TreeTop.DistanceToPointX * TreeTop.ScaleRateX);
                }
                ClikOnCanvas = false;
                Invalidate();
            }
        }

        public void ButtonChangeTool(object sender, RoutedEventArgs e)
        {
            TreeTop.ToolNow = TreeTop.Transform[(sender as System.Windows.Controls.Button).Tag.ToString()];
            if((sender as System.Windows.Controls.Button).Tag.ToString() == "RoundRect")
            {
                textBoxRoundRectX.IsEnabled = true;
                textBoxRoundRectY.IsEnabled = true;
            }
            else
            {
                textBoxRoundRectX.IsEnabled = false;
                textBoxRoundRectY.IsEnabled = false;
            }
            foreach (Figure figure in TreeTop.Figures)
            {
                figure.UnSelected();
            }
            Invalidate();
            PropToolBarPanel.Children.Clear();
        }

        public void ButtonChangeColor(object sender, RoutedEventArgs e)
        {
            if (TreeTop.FirstPress== true){
                TreeTop.ColorNow = TreeTop.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()];
                TreeTop.ColorStringNow = (sender as System.Windows.Controls.Button).Tag.ToString();
                button_firstColor.Background = (sender as System.Windows.Controls.Button).Background;
            }
            else
            {
                TreeTop.BrushNow = TreeTop.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()];
                TreeTop.BrushStringNow = (sender as System.Windows.Controls.Button).Tag.ToString();
                button_secondColor.Background = (sender as System.Windows.Controls.Button).Background;
            }
        }

        public void ChangeStrokeColor(object sender, RoutedEventArgs e)
        {
            foreach(Figure figure in TreeTop.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(TreeTop.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()], (sender as System.Windows.Controls.Button).Tag.ToString());
                }
            }
            TreeTop.AddCondition();
            Invalidate();
        }

        public void ChangeBrushColor(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangeBrush(TreeTop.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()], (sender as System.Windows.Controls.Button).Tag.ToString());
                }
            }
            TreeTop.AddCondition();
            Invalidate();
        }

        private void ThiknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TreeTop.ThicnessNow = ThiknessSlider.Value;
        }

        public void RowThicnessChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(e.NewValue);
                }
            }
            Invalidate();
        }

        public void RowThicknessSldMouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeTop.AddCondition();
        }

        private void FirstColor(object sender, RoutedEventArgs e)
        {
            TreeTop.FirstPress = true;
            TreeTop.SecondPress = false;
            button_firstColor.BorderThickness = new Thickness(5);
            button_secondColor.BorderThickness = new Thickness(0);
        }

        private void SecondColor(object sender, RoutedEventArgs e)
        {
            TreeTop.FirstPress = false;
            TreeTop.SecondPress = true;
            button_secondColor.BorderThickness = new Thickness(5);
            button_firstColor.BorderThickness = new Thickness(0);
        }

        private void MyCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            TreeTop.CanvasHeigth = MyCanvas.Height;
            TreeTop.CanvasWidth = MyCanvas.Width;
        }

        private void MyCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TreeTop.CanvasHeigth = MyCanvas.Height;
            TreeTop.CanvasWidth = MyCanvas.Width;
        }

        public void CleanMyCanvas(object sender, RoutedEventArgs e)
        {
            TreeTop.FigureHost.Children.Clear();
            TreeTop.Figures.Clear();
            TreeTop.ConditionNumber = 0;
            TreeTop.ConditionsCanvas.Clear();
            TreeTop.AddCondition();
            gotoPastCondition.IsEnabled = false;
            gotoSecondCondition.IsEnabled = false;
        }

        public void MinusZoomMyCanvas(object sender, RoutedEventArgs e)
        {
            MyCanvas.LayoutTransform = new ScaleTransform(1, 1);
            ScrollViewerCanvas.ScrollToVerticalOffset(0);
            ScrollViewerCanvas.ScrollToHorizontalOffset(0);
        }

        bool PressHand = false;

        public void HandForSelectedFigure(object sender, RoutedEventArgs e)
        {
            if (PressHand == false)
            {
                TreeTop.ToolNow = TreeTop.HandTool;
                PressHand = true;
            }
            else
            {
                TreeTop.ToolNow = TreeTop.Transform["Allotment"];
                PressHand = false;
            }
        }

        public void ClearSelectedFigure(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in TreeTop.Figures.ToArray())
            {
                if(figure.Select == true)
                {
                    TreeTop.Figures.Remove(figure);
                }
            }
            TreeTop.AddCondition();
            Invalidate();
        }

        public void ChangeDash(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(TreeTop.TransformDashProp[(sender as System.Windows.Controls.Button).Content.ToString()], (sender as System.Windows.Controls.Button).Content.ToString());
                }
            }
            TreeTop.AddCondition();
            Invalidate();
        }

        private void ChangeSelectionDash(object sender, SelectionChangedEventArgs e)
        {
            TreeTop.DashNow = TreeTop.TransformDash[comboBoxDash.SelectedIndex.ToString()];
            if (comboBoxDash.SelectedIndex.ToString() == "0")
            {
                TreeTop.DashStringhNow = "―――――";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "1")
            {
                TreeTop.DashStringhNow = "— — — — — —";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "2")
            {
                TreeTop.DashStringhNow = "— ∙ — ∙ — ∙ — ∙ —";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "3")
            {
                TreeTop.DashStringhNow = "— ∙ ∙ — ∙ ∙ — ∙ ∙ — ";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "4")
            {
                TreeTop.DashStringhNow = "∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙";
            }
        }

        private void textBoxRoundRectX_TextChanged(object sender, TextChangedEventArgs e)
        {
            TreeTop.RoundXNow = Convert.ToDouble(textBoxRoundRectX.Text);
        }

        private void textBoxRoundRectY_TextChanged(object sender, TextChangedEventArgs e)
        {
            TreeTop.RoundYNow = Convert.ToDouble(textBoxRoundRectY.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TreeTop.Figures.Count != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить как";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Files(*.bin)|*.bin";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    FileStream file = (FileStream)sfd.OpenFile();
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(file, TreeTop.Figures);
                    file.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Нарисуйте что-нибудь...");
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (TreeTop.Figures.Count != 0)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Сохранить текущее изображение?", "", MessageBoxButtons.YesNo);
                if(dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Сохранить как";
                    sfd.OverwritePrompt = true;
                    sfd.CheckPathExists = true;
                    sfd.Filter = "Files(*.bin)|*.bin";
                    sfd.ShowDialog();
                    if (sfd.FileName != "")
                    {
                        FileStream file = (FileStream)sfd.OpenFile();
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(file, TreeTop.Figures);
                        file.Close();
                    }
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Files(*.bin)|*.bin";
            ofd.Title = "Открыть";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                Stream file = (FileStream)ofd.OpenFile();
                BinaryFormatter deserializer = new BinaryFormatter();
                TreeTop.Figures = (List<Figure>)deserializer.Deserialize(file);
                file.Close();
                Invalidate();
            }
            TreeTop.ConditionsCanvas.Clear();
            TreeTop.ConditionNumber = 0;
            TreeTop.AddCondition();
        }

        private void gotoPastCondition_Click(object sender, RoutedEventArgs e)
        {
            TreeTop.gotoPastCondition();
            if(TreeTop.ConditionNumber == 1)
            {
                gotoPastCondition.IsEnabled = false;
            }
            gotoSecondCondition.IsEnabled = true;
            Invalidate();
        }

        private void gotoSecondCondition_Click(object sender, RoutedEventArgs e)
        {
            TreeTop.gotoSecondCondition();
            if(TreeTop.ConditionNumber == TreeTop.ConditionsCanvas.Count)
            { 
                gotoSecondCondition.IsEnabled = false;
            }
            gotoPastCondition.IsEnabled = true;
            Invalidate();
        }

        public void changeRoundX (object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
