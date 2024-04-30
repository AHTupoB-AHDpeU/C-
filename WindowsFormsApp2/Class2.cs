using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    [Serializable]
    public class RectangleFigure : Figure
    {
        private Color backgroundColor;

        public RectangleFigure(Point point1, Point point2, Color color, Color color_back, float thickness) : base(point1, point2, color, color_back, thickness)
        {
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(color, thickness), GetRectangle());
            g.FillRectangle(new SolidBrush(color_back), GetRectangle());
        }

        public override void DrawDash(Graphics g)
        {
            Pen dashedPen = new Pen(Color.Blue, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(dashedPen, GetRectangle());
            dashedPen.Dispose();
        }
        public override void DrawDashEllipse(Graphics g)
        {
            Pen dashedPen = new Pen(Color.Blue, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawEllipse(dashedPen, GetRectangle());
            dashedPen.Dispose();
        }
        public override void DrawDashLine(Graphics g)
        {
            Pen dashedPen = new Pen(Color.Blue, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(dashedPen, point1, point2);
            dashedPen.Dispose();
        }

        public override void Hide(Graphics g)
        {
            g.DrawRectangle(new Pen(backgroundColor, 1), GetRectangle());
        }

        public bool Contains(int x, int y)
        {
            Rectangle rect = GetRectangle();
            return rect.Contains(x, y);
        }

        // Получение прямоугольника на основе его координат
        private Rectangle GetRectangle()
        {
            return new Rectangle(Math.Min(point1.X, point2.X),
                                 Math.Min(point1.Y, point2.Y),
                                 Math.Abs(point1.X - point2.X),
                                 Math.Abs(point1.Y - point2.Y));
        }

        // Методы для рисования эллипса, прямой линии и произвольной линии
        public override void DrawEllipse(Graphics g)
        {
            g.DrawEllipse(new Pen(color, thickness), GetRectangle());
            g.FillEllipse(new SolidBrush(color_back), GetRectangle());
        }

        public override void DrawLine(Graphics g)
        {
            g.DrawLine(new Pen(color, thickness), point1, point2);
        }

        // Произвольная линия
        public override void DrawCustomLine(Graphics g, Pen pen, Point[] points)
        {
            pen.Color = color;
            pen.Width = thickness;

            if (points != null && points.Length >= 2)
            {
                if (points.Length == 2)
                {
                    g.DrawLine(pen, points[0], points[1]);
                }
                else
                {
                    g.DrawCurve(pen, points);
                }
            }
        }

    }
}
