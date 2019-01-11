using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint2.Paint
{
    class ZoomRect : Figure
    {
        public ZoomRect(Point point)
        {
            Coordinates = new List<Point> { point, point };
        }
        
        public ZoomRect(Point point, Point point2)
        {
            Coordinates = new List<Point> { point, point2 };
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var diagonal = Point.Subtract(Coordinates[0], Coordinates[1]);
            drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 2) { DashStyle = DashStyles.Dash }, new Rect(Coordinates[1], diagonal)); 
        }

        public override void AddCord(Point point)
        {
            Coordinates[1] = point;
        }
    }
}
