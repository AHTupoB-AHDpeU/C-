using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
enum aboba
{
    lew1,
    lew2,
    lew3,
    lew4
};

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public bool isDrawing, isErasing = false; // Флаги для определения режимов рисования и удаления
        private Point startPoint, customPoint;
        private int firstPointMouseX, firstPointMouseY, lastPointMouseX, lastPointMouseY; // Координаты начала и конца выделения для удаления
        public string FileName { get; set; }
        public bool isChangesSaved = true;

        public Coordinate CurrentMouseCoordinates { get; set; }
        public event EventHandler<MouseEventArgs> MouseCoordinatesChanged; // Передача координат и размера в статусБар

        private List<(string, Size)> imageInfoList = new List<(string, Size)>();

        aboba lew;

        public static Color colorW = Color.Black;
        public static Color color_backW = Color.Transparent;
        private static float thicknessW = 2;
        public static int abobaW = 1;

        private List<Figure> figures = new List<Figure>(); // Список фигур
        private List<Figure> figures1 = new List<Figure>();
        private List<Figure> figures2 = new List<Figure>();
        private List<Figure> figures3 = new List<Figure>();

        public int customX = 50;
        public int customY = 0;

        public List<Figure> Figures
        {
            get { return figures; }
        }
        public List<Figure> Figures1
        {
            get { return figures1; }
        }
        public List<Figure> Figures2
        {
            get { return figures2; }
        }
        public List<Figure> Figures3
        {
            get { return figures3; }
        }

        public void SetFigures(List<Figure> figures)
        {
            Figures.Clear();
            Figures.AddRange(figures);

            Invalidate();
        }
        public void SetFigures1(List<Figure> figures1)
        {
            Figures1.Clear();
            Figures1.AddRange(figures1);

            Invalidate();
        }
        public void SetFigures2(List<Figure> figures2)
        {
            Figures2.Clear();
            Figures2.AddRange(figures2);

            Invalidate();
        }
        public void SetFigures3(List<Figure> figures3)
        {
            Figures3.Clear();
            Figures3.AddRange(figures3);

            Invalidate();
        }

        public void AddFigure(Figure figure)
        {
            figures.Add(figure);
        }
        public void AddFigure1(Figure figure)
        {
            figures1.Add(figure);
        }
        public void AddFigure2(Figure figure)
        {
            figures2.Add(figure);
        }
        public void AddFigure3(Figure figure)
        {
            figures3.Add(figure);
        }

        public void Hide(int x1, int y1, int x2, int y2)
        {
            for (int i = figures.Count - 1; i >= 0; i--)
            {
                Figure figure = figures[i];
                if (figure is RectangleFigure rectangle && rectangle.Contains(x1, y1) && rectangle.Contains(x2, y2))
                {
                    figures.RemoveAt(i);
                }
            }
            for (int i = figures1.Count - 1; i >= 0; i--)
            {
                Figure figure = figures1[i];
                if (figure is RectangleFigure rectangle && rectangle.Contains(x1, y1) && rectangle.Contains(x2, y2))
                {
                    figures1.RemoveAt(i);
                }
            }
            for (int i = figures2.Count - 1; i >= 0; i--)
            {
                Figure figure = figures2[i];
                if (figure is RectangleFigure rectangle && rectangle.Contains(x1, y1) && rectangle.Contains(x2, y2))
                {
                    figures2.RemoveAt(i);
                }
            }
            for (int i = figures3.Count - 1; i >= 0; i--)
            {
                Figure figure = figures3[i];
                if (figure is RectangleFigure rectangle && rectangle.Contains(x1, y1) && rectangle.Contains(x2, y2))
                {
                    figures3.RemoveAt(i);
                }
            }
        }

        public void DrawAll(Graphics g)
        {
            foreach (var figure in figures2)
                figure.Draw(g);
            foreach (var figure in figures3)
                figure.DrawEllipse(g);
            foreach (var figure in figures)
                figure.DrawLine(g);
            foreach (var figure in figures1)
                figure.DrawCustomLine(g);
        }

        public void SetColor(Color color)
        {
            colorW = color;
        }

        public void SetColorBack(Color color_back)
        {
            color_backW = color_back;
        }

        public void SetThickness(float thickness)
        {
            thicknessW = thickness;
        }

        public Form2()
        {
            InitializeComponent();
            FormClosing += Form2_FormClosing;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateSaveButtonsInParentForm()
        {
            if (this.ParentForm != null && this.ParentForm is Form1)
            {
                Form1 parentForm = (Form1)this.ParentForm;
                parentForm.UpdateSaveButtonsAvailability();
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                startPoint = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isErasing = true;
                firstPointMouseX = e.X;
                firstPointMouseY = e.Y;
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                customPoint = new Point((startPoint.X + e.Location.X) / 2 + customX, (startPoint.Y + e.Location.Y) / 2 + customY);
                Invalidate();
            }
            else if (isErasing)
            {
                lastPointMouseX = e.X;
                lastPointMouseY = e.Y;
            }
            CurrentMouseCoordinates = new Coordinate { X = e.X, Y = e.Y };
            MouseCoordinatesChanged?.Invoke(this, e);
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false; // Сброс флага

                if (lew == aboba.lew1)
                {
                    RectangleFigure newRectangle = new RectangleFigure(startPoint, e.Location, colorW, color_backW, thicknessW, e.Location);
                    AddFigure(newRectangle); // Добавление в массив
                }
                else if (lew == aboba.lew2)
                {
                    RectangleFigure newRectangle = new RectangleFigure(startPoint, e.Location, colorW, color_backW, thicknessW, customPoint);
                    AddFigure1(newRectangle); // Добавление в массив
                }
                else if (lew == aboba.lew3)
                {
                    RectangleFigure newRectangle = new RectangleFigure(startPoint, e.Location, colorW, color_backW, thicknessW, e.Location);
                    AddFigure2(newRectangle); // Добавление в массив
                }
                else if (lew == aboba.lew4)
                {
                    RectangleFigure newRectangle = new RectangleFigure(startPoint, e.Location, colorW, color_backW, thicknessW, e.Location);
                    AddFigure3(newRectangle); // Добавление в массив
                }

                isChangesSaved = false; // Установка флага изменений
                Invalidate(); // Перерисовка формы
            }
            else if (isErasing)
            {
                isErasing = false;
                Hide(firstPointMouseX, firstPointMouseY, lastPointMouseX, lastPointMouseY); // Удаление
                isChangesSaved = false;
                Invalidate();
            }
        }

        public void Tfigure1()
        {
            lew = aboba.lew1;
        }
        public void Tfigure2()
        {
            lew = aboba.lew2;
        }
        public void Tfigure3()
        {
            lew = aboba.lew3;
        }
        public void Tfigure4()
        {
            lew = aboba.lew4;
        }
        public unsafe void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (lew == aboba.lew1)
            {
                DrawAll(e.Graphics);
                if (isDrawing)
                {
                    RectangleFigure tempRectangle = new RectangleFigure(startPoint, PointToClient(MousePosition), colorW, color_backW, thicknessW, PointToClient(MousePosition));
                    tempRectangle.DrawDashLine(e.Graphics);
                }
            }
            if (lew == aboba.lew2)
            {
                DrawAll(e.Graphics);
                RectangleFigure tempRectangle = new RectangleFigure(startPoint, PointToClient(MousePosition), colorW, color_backW, thicknessW, customPoint);
                tempRectangle.DrawDashCustomLine(e.Graphics);
            }
            if (lew == aboba.lew3)
            {
                DrawAll(e.Graphics);
                if (isDrawing)
                {
                    RectangleFigure tempRectangle = new RectangleFigure(startPoint, PointToClient(MousePosition), colorW, color_backW, thicknessW, PointToClient(MousePosition));
                    tempRectangle.DrawDash(e.Graphics);
                    Invalidate();
                }
            }
            if (lew == aboba.lew4)
            {
                DrawAll(e.Graphics);
                if (isDrawing)
                {
                    RectangleFigure tempRectangle = new RectangleFigure(startPoint, PointToClient(MousePosition), colorW, color_backW, thicknessW, PointToClient(MousePosition));
                    tempRectangle.DrawDashEllipse(e.Graphics);
                }
            }

            UpdateSaveButtonsInParentForm();
        }

        public void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isChangesSaved)
            {
                DialogResult result = MessageBox.Show("Хотите сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Отмена закрытия
                }
            }
        }

        public void SaveFile()
        {
            FileManager.SaveToFile(this, imageInfoList);
            isChangesSaved = true;
        }
    }
}