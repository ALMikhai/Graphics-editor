﻿using System;
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

    class Line : Figure
    {
        public Line() { }

        public Line(Point point)
        {
            Coordinates = new List<Point> { point, point };
            Color = TreeTop.ColorNow;
            PenThikness = TreeTop.ThicnessNow;
            Dash = TreeTop.DashNow;
            DashString = TreeTop.DashStringhNow;
            Pen = new Pen(Color, PenThikness) { DashStyle = Dash };
            Selected = false;
            SelectedRect = null;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(Pen, Coordinates[0], Coordinates[1]);
        }

        public override void AddCord(Point point)
        {
            Coordinates[1] = point;
        }

        public override void Select()
        {
            if (Selected == false)
            {
                Point pForRect3 = new Point();
                pForRect3.X = Math.Min(Coordinates[0].X, Coordinates[1].X);
                pForRect3.Y = Math.Min(Coordinates[0].Y, Coordinates[1].Y);
                Point pForRect4 = new Point();
                pForRect4.X = Math.Max(Coordinates[0].X, Coordinates[1].X);
                pForRect4.Y = Math.Max(Coordinates[0].Y, Coordinates[1].Y);
                SelectedRect = new ZoomRect(new Point(pForRect3.X - 15, pForRect3.Y - 15), new Point(pForRect4.X + 15, pForRect4.Y + 15));
                var drawingVisual = new DrawingVisual();
                var drawingContext = drawingVisual.RenderOpen();
                SelectedRect.Draw(drawingContext);
                drawingContext.Close();
                Paint.TreeTop.FigureHost.Children.Add(drawingVisual);
                Selected = true;
            }
        }

        public override void UnSelect()
        {
            if (Selected == true)
            {
                Selected = false;
                SelectedRect = null;
            }
        }

        public override void ChangePen(Brush color)
        {
            Pen = new Pen(color, PenThikness) { DashStyle = Dash };
            Color = color;
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
            info.AddValue("Color", Color.ToString());
            info.AddValue("Dash", DashString);
        }

        public Line(SerializationInfo info, StreamingContext context)
        {
            Coordinates = (List<Point>)info.GetValue("Coordinates", typeof(List<Point>));
            PenThikness = (double)info.GetValue("PenThikness", typeof(double));
            DashString = (string)info.GetValue("Dash", typeof(string));
            Color = (SolidColorBrush)new BrushConverter().ConvertFromString((string)info.GetValue("Color", typeof(string)));
            Dash = TreeTop.TransformDashProp[DashString];
            Pen = new Pen(Color, PenThikness) { DashStyle = Dash };
        }

        public override Figure Clone()
        {
            return new Line
            {
                Color = this.Color,
                Coordinates = new List<Point>(Coordinates),
                Dash = this.Dash,
                DashString = this.DashString,
                Pen = this.Pen,
                PenThikness = this.PenThikness,
                Selected = this.Selected,
                SelectedRect = this.SelectedRect,
            };
        }
    }
}
