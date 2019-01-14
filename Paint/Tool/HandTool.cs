using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint2.Paint
{
    class HandTool : Tool
    {

        public override void MouseDown(Point point)
        {
            TreeTop.Figures.Add(new HandLine(point));
            TreeTop.HandScrollX = point.X;
            TreeTop.HandScrollY = point.Y;
        }

        public override void MouseMove(Point point)
        {
            TreeTop.HandScrollX += TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].X - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].X;
            TreeTop.HandScrollY += TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[0].Y - TreeTop.Figures[TreeTop.Figures.Count - 1].Coordinates[1].Y;
            TreeTop.Figures[TreeTop.Figures.Count - 1].AddCord(point);
        }

        public override void MouseUp(Point point)
        {
            TreeTop.Figures.Remove(TreeTop.Figures[TreeTop.Figures.Count - 1]);
        }
    }
}
