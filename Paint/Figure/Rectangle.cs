using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace Paint2.Paint
{
    [Serializable]

    class Rectangle : Figure
    {
        public Rectangle() { }

        public Rectangle(Point point)
        {
            Coordinates = new List<Point> { point, point };
            Color = TreeTop.ColorNow;
            ColorString = TreeTop.ColorStringNow;
            BrushColor = TreeTop.BrushNow;
            BrushColorString = TreeTop.BrushStringNow;
            PenThikness = TreeTop.ThicnessNow;
            Dash = TreeTop.DashNow;
            DashString = TreeTop.DashStringhNow;
            Pen = new Pen(Color, PenThikness) { DashStyle = Dash };
            Select = false;
            SelectRect = null;
            Type = "Rectangle";
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var diagonal = Point.Subtract(Coordinates[0], Coordinates[1]);
            drawingContext.DrawRectangle(BrushColor, Pen, new Rect(Coordinates[1], diagonal));
        }

        public override void AddCord(Point point)
        {
            Coordinates[1] = point;
        }

        public override void Selected()
        {
            if (Select == false)
            {
                Point pForRect3 = new Point();
                pForRect3.X = Math.Min(Coordinates[0].X, Coordinates[1].X);
                pForRect3.Y = Math.Min(Coordinates[0].Y, Coordinates[1].Y);
                Point pForRect4 = new Point();
                pForRect4.X = Math.Max(Coordinates[0].X, Coordinates[1].X);
                pForRect4.Y = Math.Max(Coordinates[0].Y, Coordinates[1].Y);
                SelectRect = new ZoomRect(new Point(pForRect3.X - 15, pForRect3.Y - 15), new Point(pForRect4.X + 15, pForRect4.Y + 15));
                var drawingVisual = new DrawingVisual();
                var drawingContext = drawingVisual.RenderOpen();
                SelectRect.Draw(drawingContext);
                drawingContext.Close();
                Paint.TreeTop.FigureHost.Children.Add(drawingVisual);
                Select = true;
            }
        }

        public override void UnSelected()
        {
            if (Select == true)
            {
                Select = false;
                SelectRect = null;
            }
        }

        public override void ChangePen(Brush color, string str)
        {
            Pen = new Pen(color, PenThikness) { DashStyle = Dash };
            Color = color;
            ColorString = str;
        }

        public override void ChangePen(Brush color, string str, bool check)
        {
            BrushColor = color;
            BrushColorString = str;
        }

        public override void ChangePen(DashStyle dash, string str)
        {
            Pen = new Pen(Color, PenThikness) { DashStyle = dash };
            Dash = dash;
            DashString = str;
        }

        public override void ChangePen(double thikness)
        {
            Pen = new Pen(Color, thikness) { DashStyle = Dash };
            PenThikness = thikness;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Coordinates", Coordinates);
            info.AddValue("PenThikness", PenThikness);
            info.AddValue("Color", ColorString);
            info.AddValue("BrushColor", BrushColorString);
            info.AddValue("Dash", DashString);
            info.AddValue("Type", Type);
        }

        public Rectangle(SerializationInfo info, StreamingContext context)
        {
            Coordinates = (List<Point>)info.GetValue("Coordinates", typeof(List<Point>));
            PenThikness = (double)info.GetValue("PenThikness", typeof(double));
            ColorString = (string)info.GetValue("Color", typeof(string));
            BrushColorString = (string)info.GetValue("BrushColor", typeof(string));
            DashString = (string)info.GetValue("Dash", typeof(string));
            Type = (string)info.GetValue("Type", typeof(string));
            Color = TreeTop.TransformColor[ColorString];
            BrushColor = TreeTop.TransformColor[BrushColorString];
            Dash = TreeTop.TransformDashProp[DashString];
            Pen = new Pen(Color, PenThikness) { DashStyle = Dash };
        }

        public override Figure Clone()
        {
            return new Rectangle
            {
                BrushColor = this.BrushColor,
                BrushColorString = this.BrushColorString,
                Color = this.Color,
                ColorString = this.ColorString,
                Coordinates = new List<Point>(Coordinates),
                Dash = this.Dash,
                DashString = this.DashString,
                Pen = this.Pen,
                PenThikness = this.PenThikness,
                RoundX = this.RoundX,
                RoundY = this.RoundY,
                Select = this.Select,
                SelectRect = this.SelectRect,
                Type = this.Type
            };
        }
    }
}
