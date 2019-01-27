using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint2.Paint
{
    class AllotmentTool : Tool
    {
        private void CheckIntersection(Point p1, Point p2, Point p3, Point p4, Figure figure, Point p1Min, Point p2Max)
        {
            var v1 = Point.Subtract(p1, p2);
            var v2 = Point.Subtract(p1, p3);
            var v3 = Point.Subtract(p1, p4);
            var v4 = Point.Subtract(p3, p4);
            var v5 = Point.Subtract(p3, p2);
            var v6 = Point.Subtract(p3, p1);
            var coord1 = v1.X * v2.Y - v1.Y * v2.X;
            var coord2 = v1.X * v3.Y - v1.Y * v3.X;
            var coord3 = v4.X * v5.Y - v4.Y * v5.X;
            var coord4 = v4.X * v6.Y - v4.Y * v6.X;
            if ((coord1 * coord2 < 0 & coord3 * coord4 < 0) ||
                ((p1Min.X < figure.Coordinates[0].X) &
                (figure.Coordinates[0].X < p2Max.X) &
                (p1Min.Y < figure.Coordinates[0].Y) &
                (figure.Coordinates[0].Y < p2Max.Y)) ||
                ((p1Min.X < figure.Coordinates[1].X) &
                (figure.Coordinates[1].X < p2Max.X) &
                (p1Min.Y < figure.Coordinates[1].Y) &
                (figure.Coordinates[1].Y < p2Max.Y)))
            {
                figure.Select();
            }
            else
            {
                figure.UnSelect();

            }
        }

        public override void MouseDown(Point point)
        {
            TreeTop.Figures.Add(new ZoomRect(point));
            foreach (var figure in TreeTop.Figures)
            {
                figure.UnSelect();
            }
            MainWindow.Instance.PropToolBarPanel.Children.Clear(); 
        }

        public override void MouseMove(Point point)
        {
            TreeTop.Figures[TreeTop.Figures.Count - 1].AddCord(point);
            Point p1 = new Point(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y);
            Point p2 = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1];
            Point p1_h = new Point(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y);
            Point p2_h  = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1];
            var p1Min = new Point(Math.Min(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X), Math.Min(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y));
            var p2Max = new Point(Math.Max(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X), Math.Max(TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y, TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y));

            foreach (Figure figure in TreeTop.Figures.ToArray())
            { 
                if (figure is Pencil)
                {
                    foreach (Point pnt in figure.Coordinates)
                    {
                        if (((p1Min.X < pnt.X) & (pnt.X < p2Max.X) & (p1Min.Y < pnt.Y) & (pnt.Y < p2Max.Y)))
                        {
                            figure.Select();
                            break;
                        }
                        else
                        {
                            figure.UnSelect();
                        }
                    }
                }
                else if(figure is Line)
                {
                    Point p3 = figure.Coordinates[0];
                    Point p4 = figure.Coordinates[1];
                    CheckIntersection(p1, p2, p3, p4, figure, p1Min, p2Max);
                    if (figure.Selected == true) { continue; }

                    p3 = figure.Coordinates[0];
                    p4 = figure.Coordinates[1];
                    CheckIntersection(p1_h, p2_h, p3, p4, figure, p1Min, p2Max);
                    if (figure.Selected == true) { continue; }
                }
                else
                {
                    Point p3 = figure.Coordinates[0];
                    Point p4 = new Point(figure.Coordinates[1].X, figure.Coordinates[0].Y);
                    CheckIntersection(p1, p2, p3, p4, figure, p1Min, p2Max);
                    if(figure.Selected == true) { continue;  }

                    p3 = new Point(figure.Coordinates[0].X, figure.Coordinates[1].Y);
                    p4 = figure.Coordinates[1];
                    CheckIntersection(p1, p2, p3, p4, figure, p1Min, p2Max);
                    if (figure.Selected == true) { continue; }

                    p3 = figure.Coordinates[0];
                    p4 = new Point(figure.Coordinates[0].X, figure.Coordinates[1].Y);
                    CheckIntersection(p1_h, p2_h, p3, p4, figure, p1Min, p2Max);
                    if (figure.Selected == true) { continue; }

                    p3 = new Point(figure.Coordinates[1].X, figure.Coordinates[0].Y);
                    p4 = figure.Coordinates[1];
                    CheckIntersection(p1_h, p2_h, p3, p4, figure, p1Min, p2Max);
                    if (figure.Selected == true) { continue; }
                }
            }
        }

        public override void MouseUp(Point point)
        {
            TreeTop.Figures.Remove(TreeTop.Figures[TreeTop.Figures.Count - 1]);

            double i = 0;

            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Selected)
                {
                    i = figure.PenThikness;
                    ButtonGeneration.PropertyButtonGeneration();
                    break;
                }
            }

            var CheckThikness = false;

            foreach (Figure figure in TreeTop.Figures)
            {
                if (figure.Selected)
                {
                    if(i == figure.PenThikness) { CheckThikness = true; }
                    else { CheckThikness = false; break;  }
                }
            }

            if (CheckThikness)
            {
                ButtonGeneration.RowThicknessButton(i);
            }
        }
    } 
}
