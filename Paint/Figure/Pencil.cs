using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint2.Paint
{
    class Pencil : Figure
    {
        public Pencil() { }

        public Pencil(Point point)
        {
            Coordinates = new List<Point> { point, point };
            Color = TreeTop.ColorNow;
            PenThikness = TreeTop.ThicnessNow;
            Dash = TreeTop.DashNow;
            Pen = new Pen(Color, PenThikness) { DashStyle = Dash };
            Select = false;
            SelectRect = null;
            Type = "Polyline";
        }

        public override void Draw(DrawingContext drawingContext)
        {
            for (int i = 0; i < Coordinates.Count - 1; i++)
                drawingContext.DrawLine( Pen, Coordinates[i], Coordinates[i+1]);
        }

        public override void AddCord(Point point)
        {
            Coordinates.Add(point);
        }

        public override void Selected()
        {
            if (Select == false)
            {
                Point pForRect3 = Coordinates[0];
                Point pForRect4 = new Point(0, 0);
                foreach (Point point in Coordinates)
                {
                    if(point.X < pForRect3.X)
                    {
                        pForRect3.X = point.X;
                    }

                    if(point.Y < pForRect3.Y)
                    {
                        pForRect3.Y = point.Y;
                    }

                    if (point.X > pForRect4.X)
                    {
                        pForRect4.X = point.X;
                    }

                    if (point.Y > pForRect4.Y)
                    {
                        pForRect4.Y = point.Y;
                    }
                }
                SelectRect = new ZoomRect(new Point(pForRect3.X - 7, pForRect3.Y - 7), new Point(pForRect4.X + 7, pForRect4.Y + 7));
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
        }

        public override void ChangePen(DashStyle dash, string str)
        {
            Pen = new Pen(Color, PenThikness) { DashStyle = dash };
            Dash = dash;
        }

        public override void ChangePen(double thikness)
        {
            Pen = new Pen(Color, thikness) { DashStyle = Dash };
            PenThikness = thikness;
        }
        public override Figure Clone()
        {
            return new Pencil
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
