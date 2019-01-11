using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace Paint2
{
    [Serializable]
    public class Figure : ISerializable
    {
        public List<Point> Coordinates { get; set; }
        
        public Brush Color { get; set; }

        public Brush BrushColor { get; set; }

        public string ColorString { get; set; }

        public string BrushColorString { get; set; }

        public Pen Pen { get; set; }
        
        public double PenThikness { get; set; }
        
        public bool Select { get; set; }
        
        public string Type { get; set; }
        
        public DashStyle Dash { get; set; }

        public string DashString { get; set; }

        public Figure SelectRect { get; set; }

        public double RoundX { get; set; }

        public double RoundY { get; set; }

        public virtual Figure Clone()
        {
            return new Figure();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        public Figure()
        {

        }

        public Figure(Point Point)
        {
            Coordinates.Add(Point);
        }

        public virtual void Draw(DrawingContext graphics)
        {

        }

        public virtual void AddCord(Point point)
        {
            Coordinates.Add(point);
        }

        public virtual void UnSelected()
        {

        }

        public virtual void Selected()
        {

        }

        public virtual void ChangePen(Brush color, string colorstring)
        {

        }

        public virtual void ChangePen(double thikness)
        {

        }

        public virtual void ChangePen(DashStyle dash, string dashstring)
        {

        }

        public virtual void ChangePen(Brush color, string brushstring, bool check)
        {

        }

        public virtual void ChangeRoundX(double newRoundX)
        {

        }

        public virtual void ChangeRoundY(double newRoundY)
        {

        }
    }
}
