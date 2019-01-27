using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Paint2.Paint
{
    [Serializable]
    public class TreeTop
    {
        public static List<Figure> Figures = new List<Figure>();
        
        public static FigureHost FigureHost = new FigureHost();

        public static Tool ToolNow = new PenTool();

        public static List<List<Figure>> StatesCanvas = new List<List<Figure>>();

        public static int ConditionNumber = 0;

        public static Tool HandTool = new HandForFigureTool();

        public static void AddState()
        {
            List<Figure> figuresNow = new List<Figure>();

            foreach(Figure figure in Figures)
            {
                figuresNow.Add(figure.Clone());
            }

            StatesCanvas.Add(figuresNow);
            ConditionNumber++;

            if (ConditionNumber != StatesCanvas.Count)
            {
                StatesCanvas.RemoveRange(ConditionNumber - 1, StatesCanvas.Count - ConditionNumber);
            }

            Figures.Clear();

            foreach(Figure figure in figuresNow)
            {
                Figures.Add(figure.Clone());
            }

            foreach(Figure figure in StatesCanvas[ConditionNumber - 1])
            {
                figure.Selected = false;
                figure.SelectedRect = null;
            }

            if (StatesCanvas.Count > 1)
            {
                foreach (Figure figure in StatesCanvas[ConditionNumber - 2])
                {
                    figure.Selected = false;
                    figure.SelectedRect = null;
                }
            }
        }

        public static void GotoPastState()
        {
            if (ConditionNumber != 1)
            {
                ConditionNumber--;
                Figures.Clear();
                foreach(Figure figure in StatesCanvas[ConditionNumber - 1])
                {
                    Figures.Add(figure.Clone());
                }
            }
        }

        public static void GotoSecondState()
        {
            if(ConditionNumber != StatesCanvas.Count)
            {
                ConditionNumber++;
                Figures.Clear();
                foreach (Figure figure in StatesCanvas[ConditionNumber - 1])
                {
                    Figures.Add(figure.Clone());
                }
            }
        }

        public static Brush BrushNow = Brushes.Transparent;
        public static Brush tempBrush = null;
        public static Brush ColorNow = Brushes.Black;
        public static double ThicnessNow = 4;
        public static DashStyle DashNow = DashStyles.Solid;
        public static string DashStringhNow = "―――――";
        public static double RoundXNow = 0;
        public static double RoundYNow = 0;


        public static double ScaleRateX = 1;
        public static double ScaleRateY = 1;
        public static double DistanceToPointX;
        public static double DistanceToPointY;
        public static double HandScrollX;
        public static double HandScrollY;
        public static bool FirstPress = true;
        public static bool SecondPress = false;
        public static double CanvasWidth;
        public static double CanvasHeigth;
        
        public static readonly Dictionary<string, Tool> TransformTools = new Dictionary<string, Tool>()
        {
            { "Line", new LineTool() },
            { "Rectangle", new RectanglTool() },
            { "Ellipse", new EllipseTool() },
            { "RoundRect", new RoundRectTool() },
            { "Pen", new PenTool() },
            { "Hand", new HandTool() },
            { "ZoomRect", new ZoomTool() },
            { "Allotment", new AllotmentTool() },
        };
        
        public static readonly Dictionary<string, DashStyle> TransformDash = new Dictionary<string, DashStyle>()
        {
            { "0", DashStyles.Solid },
            { "1", DashStyles.Dash },
            { "2", DashStyles.DashDot },
            { "3", DashStyles.DashDotDot },
            { "4", DashStyles.Dot },

        };
        
        public static readonly Dictionary<string, DashStyle> TransformDashProp = new Dictionary<string, DashStyle>()
        {
            { "―――――", DashStyles.Solid },
            { "— — — — — —", DashStyles.Dash },
            { "— ∙ — ∙ — ∙ — ∙ —", DashStyles.DashDot },
            { "— ∙ ∙ — ∙ ∙ — ∙ ∙ — ", DashStyles.DashDotDot },
            { "∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙", DashStyles.Dot },

        };
        
        public static readonly Dictionary<string, Brush> TransformColor = new Dictionary<string, Brush>()
        {
            { "Black", Brushes.Black },
            { "Red", Brushes.Red },
            { "Gray", Brushes.Gray },
            { "Orange", Brushes.Orange },
            { "Yellow", Brushes.Yellow },
            { "Blue", Brushes.Blue },
            { "Purple", Brushes.Purple },
            { "Coral", Brushes.Coral },
            { "White", Brushes.White },
            { "null", Brushes.Transparent }
        };
    }
}
