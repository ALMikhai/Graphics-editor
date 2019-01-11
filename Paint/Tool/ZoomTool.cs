using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Paint2.Paint;

namespace Paint2.Paint
{
    class ZoomTool : Tool
    {
        public override void MouseDown(Point point)
        {
            TreeTop.Figures.Add(new ZoomRect(point));
        }

        public override void MouseMove(Point point)
        {
            TreeTop.Figures[TreeTop.Figures.Count - 1].AddCord(point);
        }

        public override void MouseUp(Point point)
        {
            if (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X > TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X)
            {
                TreeTop.ScaleRateX = TreeTop.CanvasWidth / (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X);
            }
            else
            {
                TreeTop.ScaleRateX = TreeTop.CanvasWidth / (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X);
            }

            if (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y > TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y)
            {
                TreeTop.ScaleRateY = TreeTop.CanvasHeigth / (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y);
            }
            else
            {
                TreeTop.ScaleRateY = TreeTop.CanvasHeigth / (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y);
            }

            if (TreeTop.ScaleRateX > TreeTop.ScaleRateY)
            {
                TreeTop.ScaleRateY = TreeTop.ScaleRateX;
            }
            else
            {
                TreeTop.ScaleRateX = TreeTop.ScaleRateY;
            }

            if (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X > TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X)
            {
                TreeTop.DistanceToPointX = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X;
            }
            else
            {
                TreeTop.DistanceToPointX = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X;
            }

            if (TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y > TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y)
            {
                TreeTop.DistanceToPointY = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y;
            }
            else
            {
                TreeTop.DistanceToPointY = TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y;
            }
            TreeTop.Figures.Remove(TreeTop.Figures[TreeTop.Figures.Count-1]);
        }
    }
}
