using System.Drawing;
using System;

namespace WindowsFormsApp2
{
    [Serializable]
    // Базовый класс для моделирования различных геометрических фигур
    public abstract class Figure
    {
        protected Point point1;
        protected Point point2;
        protected Color color;
        protected Color color_back;
        protected float thickness;

        public Figure(Point point1, Point point2, Color color, Color color_back, float thickness)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.color = color;
            this.color_back = color_back;
            this.thickness = thickness;
        }

        // Абстрактные методы для рисования, рисования пунктиром и стирания фигуры
        public abstract void Draw(Graphics g);
        public abstract void DrawDash(Graphics g);
        public abstract void DrawDashEllipse(Graphics g);
        public abstract void DrawDashLine(Graphics g);
        public abstract void Hide(Graphics g);

        public abstract void DrawLine(Graphics g);
        public abstract void DrawCustomLine(Graphics g, Pen pen, Point[] points);
        public abstract void DrawEllipse(Graphics g); 
    }
}
