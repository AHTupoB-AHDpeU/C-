using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Dictionary<string, ToolStripMenuItem> menuItems = new Dictionary<string, ToolStripMenuItem>();
        private Dictionary<string, ToolStripMenuItem> scoreMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private Dictionary<string, ToolStripMenuItem> backMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private Dictionary<string, ToolStripMenuItem> figureMenuItems = new Dictionary<string, ToolStripMenuItem>();

        public ResizeDialogForm defolt = new ResizeDialogForm();

        public Form2 form2 = new Form2();

        private static Color color = Color.Black;
        private static Color color_back = Color.Transparent;
        private static float thickness = 1;

        private List<(string, Size)> imageInfoList = new List<(string, Size)>();

        public Form1()
        {
            InitializeComponent();
            form2.Owner = this;

            FileManager.FileNameSet += UpdateFormTitle;
        }

        private void UpdateFormTitle(string fileName)
        {
            this.Text = "MyPaint - " + fileName; // Обновляем заголовок формы
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            form2.Owner = this;
            form2.MdiParent = this;
            form2.Text = "Рисунок";
            form2.Show();

            this.Text = "MyPaint - " + "Рисунок"; //this.MdiChildren.Length.ToString()
            form2.MouseCoordinatesChanged += Form2_MouseCoordinatesChanged;
            UseForm2MouseCoordinates(); // Вызываем для начального обновления координат

            form2.Size = new Size(320, 240); // Размер
            defolt.DefaultButton();

            toolStripButton5.Checked = true;

            сохранитьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem.Enabled = false;
            цветФонаToolStripMenuItem.Enabled = заливкавклотклToolStripMenuItem.Checked;

            menuItems.Add("чёрныйToolStripMenuItem", чёрныйToolStripMenuItem);
            menuItems.Add("красныйToolStripMenuItem", красныйToolStripMenuItem);
            menuItems.Add("синийToolStripMenuItem", синийToolStripMenuItem);
            menuItems.Add("зелёныйToolStripMenuItem", зелёныйToolStripMenuItem);
            menuItems.Add("жёлтыйToolStripMenuItem", жёлтыйToolStripMenuItem);
            menuItems.Add("оранжевыйToolStripMenuItem", оранжевыйToolStripMenuItem);
            menuItems.Add("фиолетовыйToolStripMenuItem", фиолетовыйToolStripMenuItem);
            menuItems.Add("розовыйToolStripMenuItem", розовыйToolStripMenuItem);
            menuItems.Add("голубойToolStripMenuItem", голубойToolStripMenuItem);
            menuItems.Add("белыйToolStripMenuItem", белыйToolStripMenuItem);

            scoreMenuItems.Add("1птToolStripMenuItem", птToolStripMenuItem);
            scoreMenuItems.Add("2птToolStripMenuItem", птToolStripMenuItem1);
            scoreMenuItems.Add("5птToolStripMenuItem", птToolStripMenuItem2);
            scoreMenuItems.Add("8птToolStripMenuItem", птToolStripMenuItem3);
            scoreMenuItems.Add("10птToolStripMenuItem", птToolStripMenuItem4);
            scoreMenuItems.Add("12птToolStripMenuItem", птToolStripMenuItem5);
            scoreMenuItems.Add("15птToolStripMenuItem", птToolStripMenuItem6);

            backMenuItems.Add("чёрныйToolStripMenuItem1", чёрныйToolStripMenuItem1);
            backMenuItems.Add("красныйToolStripMenuItem1", красныйToolStripMenuItem1);
            backMenuItems.Add("синийToolStripMenuItem1", синийToolStripMenuItem1);
            backMenuItems.Add("зелёныйToolStripMenuItem", зелёныйToolStripMenuItem1);
            backMenuItems.Add("жёлтыйToolStripMenuItem1", жёлтыйToolStripMenuItem1);
            backMenuItems.Add("оранжевыйToolStripMenuItem1", оранжевыйToolStripMenuItem1);
            backMenuItems.Add("фиолетовыйToolStripMenuItem1", фиолетовыйToolStripMenuItem1);
            backMenuItems.Add("розовыйToolStripMenuItem1", розовыйToolStripMenuItem1);
            backMenuItems.Add("голубойToolStripMenuItem1", голубойToolStripMenuItem1);
            backMenuItems.Add("белыйToolStripMenuItem1", белыйToolStripMenuItem1);

            figureMenuItems.Add("прямоугольникToolStripMenuItem", прямоугольникToolStripMenuItem);
            figureMenuItems.Add("эллипсToolStripMenuItem", эллипсToolStripMenuItem);
            figureMenuItems.Add("прямаяЛинияToolStripMenuItem", прямаяЛинияToolStripMenuItem);
            figureMenuItems.Add("произвольнаяЛинияToolStripMenuItem", произвольнаяЛинияToolStripMenuItem);

            foreach (var item in menuItems.Values)
            {
                item.Click += Button_Click;
            }
            foreach (var item in scoreMenuItems.Values)
            {
                item.Click += ScoreButton_Click;
            }
            foreach (var item in backMenuItems.Values)
            {
                item.Click += BackButton_Click;
            }
            foreach (var item in figureMenuItems.Values)
            {
                item.Click += FigureButton_Click;
            }

            чёрныйToolStripMenuItem.Checked = true;
            птToolStripMenuItem.Checked = true;
            прямаяЛинияToolStripMenuItem.Checked = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (var item in menuItems.Values)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
        }
        private void ScoreButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (var item in scoreMenuItems.Values)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (var item in backMenuItems.Values)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
        }
        private void FigureButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (var item in figureMenuItems.Values)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
            if (прямаяЛинияToolStripMenuItem.Checked)
            {
                toolStripButton5.Checked = true;
                toolStripButton6.Checked = false;
                toolStripButton7.Checked = false;
                toolStripButton8.Checked = false;
            }
            else if (произвольнаяЛинияToolStripMenuItem.Checked)
            {
                toolStripButton5.Checked = false;
                toolStripButton6.Checked = true;
                toolStripButton7.Checked = false;
                toolStripButton8.Checked = false;
            }
            else if (прямоугольникToolStripMenuItem.Checked)
            {
                toolStripButton5.Checked = false;
                toolStripButton6.Checked = false;
                toolStripButton7.Checked = true;
                toolStripButton8.Checked = false;
            }
            else if (эллипсToolStripMenuItem.Checked)
            {
                toolStripButton5.Checked = false;
                toolStripButton6.Checked = false;
                toolStripButton7.Checked = false;
                toolStripButton8.Checked = true;
            }

        }

        private void newToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (form2 != null)
            {
                form2.Close();
            }

            form2 = new Form2();
            form2.MdiParent = this;
            this.Text = "MyPaint - " + "Новый рисунок";
            form2.Text = "Новый рисунок ";
            form2.Show();

            form2.Size = new Size(320, 240);
            statusBar1.Invalidate();

            form2.MouseCoordinatesChanged += Form2_MouseCoordinatesChanged;
            UseForm2MouseCoordinates(); // Вызываем для начального обновления координат

            defolt.DefaultButton();

            Black_col();
            Transparent_col_Back();
            One_pt();
            thickness = 1;
            form2.Tfigure1();
            statusBar1.Invalidate();

            toolStripButton5.Checked = true;
            сохранитьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem.Enabled = false;
            цветФонаToolStripMenuItem.Enabled = заливкавклотклToolStripMenuItem.Checked;
            чёрныйToolStripMenuItem.Checked = true;
            птToolStripMenuItem.Checked = true;
            прямаяЛинияToolStripMenuItem.Checked = true;

            toolStripButton9.Checked = false;
            произвольнаяЛинияToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = true;
            эллипсToolStripMenuItem.Checked = false;

            птToolStripMenuItem1.Checked = false;
            птToolStripMenuItem2.Checked = false;
            птToolStripMenuItem3.Checked = false;
            птToolStripMenuItem4.Checked = false;
            птToolStripMenuItem5.Checked = false;
            птToolStripMenuItem6.Checked = false;

            красныйToolStripMenuItem.Checked = false;
            синийToolStripMenuItem.Checked = false;
            зелёныйToolStripMenuItem.Checked = false;
            жёлтыйToolStripMenuItem.Checked = false;
            оранжевыйToolStripMenuItem.Checked = false;
            фиолетовыйToolStripMenuItem.Checked = false;
            розовыйToolStripMenuItem.Checked = false;
            голубойToolStripMenuItem.Checked = false;
            белыйToolStripMenuItem.Checked = false;

            чёрныйToolStripMenuItem1.Checked = false;
            красныйToolStripMenuItem1.Checked = false;
            синийToolStripMenuItem1.Checked = false;
            зелёныйToolStripMenuItem1.Checked = false;
            жёлтыйToolStripMenuItem1.Checked = false;
            оранжевыйToolStripMenuItem1.Checked = false;
            фиолетовыйToolStripMenuItem1.Checked = false;
            розовыйToolStripMenuItem1.Checked = false;
            голубойToolStripMenuItem1.Checked = false;

            цветФонаToolStripMenuItem.Enabled = заливкавклотклToolStripMenuItem.Checked;
            цветФонаToolStripMenuItem.Enabled = toolStripButton9.Checked;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
            toolStripButton8.Checked = false;

            прямоугольникToolStripMenuItem.Checked = false;
            эллипсToolStripMenuItem.Checked = false;
            произвольнаяЛинияToolStripMenuItem.Checked = false;
            заливкавклотклToolStripMenuItem.Checked = false;

        }

        // Обновление состояний кнопок сохранений
        public void UpdateSaveButtonsAvailability()
        {
            if (this.ActiveMdiChild != null && this.ActiveMdiChild is Form2)
            {
                Form2 activeChild = (Form2)this.ActiveMdiChild;
                List<Figure> figures = activeChild.Figures;
                List<Figure> figures1 = activeChild.Figures1;
                List<Figure> figures2 = activeChild.Figures2;
                List<Figure> figures3 = activeChild.Figures3;
                bool hasDrawing = figures != null && figures.Count > 0;
                сохранитьToolStripMenuItem.Enabled = hasDrawing;
                сохранитьКакToolStripMenuItem.Enabled = hasDrawing;
            }
            else
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null && this.ActiveMdiChild is Form2)
            {
                Form2 activeChild = (Form2)this.ActiveMdiChild;
                FileManager.LoadFromFile(activeChild);
                windowToolStripMenuItem.Invalidate();
            }
            else
            {
                MessageBox.Show("Нет открытых окон для загрузки изображения.", "Ошибка");
            }
            statusBar1.Invalidate();
            UpdateSaveButtonsAvailability();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null && this.ActiveMdiChild is Form2)
            {
                Form2 activeChild = (Form2)this.ActiveMdiChild;
                FileManager.SaveToFile(activeChild, imageInfoList);
            }
            else
            {
                MessageBox.Show("Нет открытых окон для сохранения изображения.", "Ошибка");
            }
            UpdateSaveButtonsAvailability();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null && this.ActiveMdiChild is Form2)
            {
                Form2 activeChild = (Form2)this.ActiveMdiChild;
                FileManager.SaveToFileAs(activeChild, imageInfoList);
            }
            else
            {
                MessageBox.Show("Нет открытых окон для сохранения изображения.", "Ошибка");
            }
            UpdateSaveButtonsAvailability();
        }

        private void чёрныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Black_col();
            statusBar1.Invalidate();
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Red_col();
            statusBar1.Invalidate();
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Blue_col();
            statusBar1.Invalidate();
        }

        private void зелёныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Green_col();
            statusBar1.Invalidate();
        }

        private void жёлтыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yellow_col();
            statusBar1.Invalidate();
        }

        private void оранжевыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orange_col();
            statusBar1.Invalidate();
        }

        private void фиолетовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Violet_col();
            statusBar1.Invalidate();
        }

        private void розовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pink_col();
            statusBar1.Invalidate();
        }

        private void голубойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cyan_col();
            statusBar1.Invalidate();
        }
        private void белыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            White_col();
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem_Click(object sender, EventArgs e)
        {
            One_pt();
            thickness = 1;
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Two_pt();
            thickness = 2;
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Five_pt();
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Eight_pt();
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Ten_pt();
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Twelve_pt();
            statusBar1.Invalidate();
        }

        private void птToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Fifteen_pt();
            statusBar1.Invalidate();
        }

        // Фоны
        private void чёрныйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Black_col_Back();
            statusBar1.Invalidate();
        }

        private void красныйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Red_col_Back();
            statusBar1.Invalidate();
        }

        private void синийToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Blue_col_Back();
            statusBar1.Invalidate();
        }

        private void зелёныйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Green_col_Back();
            statusBar1.Invalidate();
        }

        private void жёлтыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Yellow_col_Back();
            statusBar1.Invalidate();
        }

        private void оранжевыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Orange_col_Back();
            statusBar1.Invalidate();
        }

        private void фиолетовыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Violet_col_Back();
            statusBar1.Invalidate();
        }

        private void розовыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pink_col_Back();
            statusBar1.Invalidate();
        }

        private void голубойToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cyan_col_Back();
            statusBar1.Invalidate();
        }

        private void белыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            White_col_Back();
            statusBar1.Invalidate();
        }

        private void размерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var resizeDialog = new ResizeDialogForm())
            {
                if (resizeDialog.ShowDialog() == DialogResult.OK)
                {
                    Size newSize = resizeDialog.SelectedSize;
                    // Применяем новый размер к Form2
                    if (this.ActiveMdiChild != null && this.ActiveMdiChild is Form2)
                    {
                        Form2 activeChild = (Form2)this.ActiveMdiChild;
                        activeChild.ClientSize = newSize;
                    }
                }
                statusBar1.Invalidate();
            }
            statusBar1.Invalidate();
        }

        public void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Tfigure3();
        }

        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Tfigure4();
        }

        private void прямаяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Tfigure1();
        }

        private void произвольнаяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Tfigure2();
        }

        private void заливкавклотклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            цветФонаToolStripMenuItem.Enabled = заливкавклотклToolStripMenuItem.Checked;
            if (заливкавклотклToolStripMenuItem.Checked)
            {
                White_col_Back();
                белыйToolStripMenuItem1.Checked = true;
                toolStripButton9.Checked = true;
            }
            else
            {
                Transparent_col_Back();
                белыйToolStripMenuItem1.Checked = false;
                toolStripButton9.Checked = false;

                чёрныйToolStripMenuItem1.Checked = false;
                красныйToolStripMenuItem1.Checked = false;
                синийToolStripMenuItem1.Checked = false;
                зелёныйToolStripMenuItem1.Checked = false;
                жёлтыйToolStripMenuItem1.Checked = false;
                оранжевыйToolStripMenuItem1.Checked = false;
                фиолетовыйToolStripMenuItem1.Checked = false;
                розовыйToolStripMenuItem1.Checked = false;
                голубойToolStripMenuItem1.Checked = false;
            }
            statusBar1.Invalidate();
        }

        public void SetColor(Color c)
        {
            color = c;
        }
        public void SetColor_back(Color b)
        {
            color_back = b;
        }
        public void SetThickness(float t)
        {
            thickness = t;
        }
        public Color GetColor()
        {
            return color;
        }
        public Color GetColor_back()
        {
            return color_back;
        }
        public float GetThickness()
        {
            return thickness;
        }

        private void Form2_MouseCoordinatesChanged(object sender, MouseEventArgs e)
        {
            UseForm2MouseCoordinates();
        }

        private void UseForm2MouseCoordinates()
        {
            if (form2.CurrentMouseCoordinates != null)
            {
                string dynamicText1 = "X=" + form2.CurrentMouseCoordinates.X + ", Y=" + form2.CurrentMouseCoordinates.Y;
                statusBarPanel6.Text = dynamicText1;
            }
            else
            {
                statusBarPanel6.Text = "Неопределенны";
            }
        }

        private void statusBar1_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        {
            Brush myBrush = new SolidBrush(color);
            Brush myBrush_back = new SolidBrush(color_back);

            string dynamicText = "Толщина " + thickness + " пт";
            statusBarPanel1.Text = dynamicText;

            string dynamicText2 = "Размер: " + form2.Width + "*" + form2.Height;
            statusBarPanel7.Text = dynamicText2;

            if (sbdevent.Index == 0)
            {
                // Уже есть код
            }
            if (sbdevent.Index == 1)
            {
                // Уже есть текст
            }
            if (sbdevent.Index == 2)
            {
                sbdevent.Graphics.FillRectangle(myBrush, sbdevent.Bounds);
            }
            if (sbdevent.Index == 3)
            {
                // Уже есть текст
            }
            if (sbdevent.Index == 4)
            {
                sbdevent.Graphics.FillRectangle(myBrush_back, sbdevent.Bounds);
            }
            if (sbdevent.Index == 5)
            {
                // Уже есть код
            }
            if (sbdevent.Index == 6)
            {
                // Уже есть код
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_MouseDown(sender, null);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem_Click(sender, null);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, null);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            сохранитьКакToolStripMenuItem_Click(sender, null);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            заливкавклотклToolStripMenuItem.Checked = true;
            цветФонаToolStripMenuItem.Enabled = заливкавклотклToolStripMenuItem.Checked;
            цветФонаToolStripMenuItem.Enabled = toolStripButton9.Checked;
            if (toolStripButton9.Checked)
            {
                White_col_Back();
                белыйToolStripMenuItem1.Checked = true;
                заливкавклотклToolStripMenuItem.Checked = true;
                toolStripButton9.Checked = true;
            }
            else
            {
                Transparent_col_Back();
                белыйToolStripMenuItem1.Checked = false;
                заливкавклотклToolStripMenuItem.Checked = false;
                toolStripButton9.Checked = false;

                чёрныйToolStripMenuItem1.Checked = false;
                красныйToolStripMenuItem1.Checked = false;
                синийToolStripMenuItem1.Checked = false;
                зелёныйToolStripMenuItem1.Checked = false;
                жёлтыйToolStripMenuItem1.Checked = false;
                оранжевыйToolStripMenuItem1.Checked = false;
                фиолетовыйToolStripMenuItem1.Checked = false;
                розовыйToolStripMenuItem1.Checked = false;
                голубойToolStripMenuItem1.Checked = false;
            }
            statusBar1.Invalidate();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = true;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
            toolStripButton8.Checked = false;
            form2.Tfigure1();
            прямаяЛинияToolStripMenuItem.Checked = true;
            произвольнаяЛинияToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = false;
            эллипсToolStripMenuItem.Checked = false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = true;
            toolStripButton7.Checked = false;
            toolStripButton8.Checked = false;
            form2.Tfigure2();
            прямаяЛинияToolStripMenuItem.Checked = false;
            произвольнаяЛинияToolStripMenuItem.Checked = true;
            прямоугольникToolStripMenuItem.Checked = false;
            эллипсToolStripMenuItem.Checked = false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = true;
            toolStripButton8.Checked = false;
            form2.Tfigure3();
            прямаяЛинияToolStripMenuItem.Checked = false;
            произвольнаяЛинияToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = true;
            эллипсToolStripMenuItem.Checked = false;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
            toolStripButton8.Checked = true;
            form2.Tfigure4();
            прямаяЛинияToolStripMenuItem.Checked = false;
            произвольнаяЛинияToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = false;
            эллипсToolStripMenuItem.Checked = true;
        }

        public static Color Black()
        {
            return Color.Black;
        }
        public static Color Red()
        {
            return Color.Red;
        }
        public static Color Blue()
        {
            return Color.Blue;
        }
        public static Color Green()
        {
            return Color.Green;
        }
        public static Color Yellow()
        {
            return Color.Yellow;
        }
        public static Color Orange()
        {
            return Color.Orange;
        }
        public static Color Violet()
        {
            return Color.Violet;
        }
        public static Color Pink()
        {
            return Color.Pink;
        }
        public static Color Cyan()
        {
            return Color.Cyan;
        }
        public static Color White()
        {
            return Color.White;
        }

        public void Black_col()
        {
            Color color = Black();
            SetColor(color); //для статусБара
            form2.SetColor(color);
        }
        public void Red_col()
        {
            Color color = Red();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Blue_col()
        {
            Color color = Blue();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Green_col()
        {
            Color color = Green();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Yellow_col()
        {
            Color color = Yellow();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Orange_col()
        {
            Color color = Orange();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Violet_col()
        {
            Color color = Violet();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Pink_col()
        {
            Color color = Pink();
            SetColor(color);
            form2.SetColor(color);
        }
        public void Cyan_col()
        {
            Color color = Cyan();
            SetColor(color);
            form2.SetColor(color);
        }
        public void White_col()
        {
            Color color = White();
            SetColor(color);
            form2.SetColor(color);
        }

        public void Black_col_Back()
        {
            Color color_back = Black();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Red_col_Back()
        {
            Color color_back = Red();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Blue_col_Back()
        {
            Color color_back = Blue();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Green_col_Back()
        {
            Color color_back = Green();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Yellow_col_Back()
        {
            Color color_back = Yellow();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Orange_col_Back()
        {
            Color color_back = Orange();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Violet_col_Back()
        {
            Color color_back = Violet();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Pink_col_Back()
        {
            Color color_back = Pink();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Cyan_col_Back()
        {
            Color color_back = Cyan();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void White_col_Back()
        {
            Color color_back = White();
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }
        public void Transparent_col_Back()
        {
            Color color_back = Color.Transparent;
            SetColor_back(color_back);
            form2.SetColorBack(color_back);
        }

        public static float One()
        {
            return 2;
        }
        public static float Two()
        {
            return 3;
        }
        public static float Five()
        {
            return 5;
        }
        public static float Eight()
        {
            return 8;
        }
        public static float Ten()
        {
            return 10;
        }
        public static float Twelve()
        {
            return 12;
        }
        public static float Fifteen()
        {
            return 15;
        }

        public void One_pt()
        {
            float thickness = One();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Two_pt()
        {
            float thickness = Two();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Five_pt()
        {
            float thickness = Five();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Eight_pt()
        {
            float thickness = Eight();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Ten_pt()
        {
            float thickness = Ten();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Twelve_pt()
        {
            float thickness = Twelve();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }
        public void Fifteen_pt()
        {
            float thickness = Fifteen();
            SetThickness(thickness);
            form2.SetThickness(thickness);
        }

    }
}
