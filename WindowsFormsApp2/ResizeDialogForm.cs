using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ResizeDialogForm : Form
    {
        public Size SelectedSize { get; private set; }
        public CheckButtonMet checkbutton = new CheckButtonMet();

        public int width, height;

        public ResizeDialogForm()
        {
            InitializeComponent();
            checkbutton.CheckButton = checkbutton.LoadCheckButton();
        }

        private void ResizeDialogForm_Load(object sender, EventArgs e)
        {
            LoadCheckButton();
        }

        public void DefaultButton()
        {
            checkbutton.CheckButton = 1;
            checkbutton.SaveCheckButton();
        }
        
        // Флажок
        private void LoadCheckButton()
        {
            groupBoxFixedSizes.Enabled = !checkBoxManual.Checked;
            textBoxWidth.Enabled = textBoxHeight.Enabled = checkBoxManual.Checked;

            if (!checkBoxManual.Checked)
            {
                radioButton320x240.Checked = true;
            }

            switch (checkbutton.CheckButton)
            {
                case 1:
                    radioButton320x240.Checked = true;
                    break;
                case 2:
                    radioButton640x480.Checked = true;
                    break;
                case 3:
                    radioButton800x600.Checked = true;
                    break;
                case 4:
                    checkBoxManual.Checked = true;
                    break;
            }
        }

        private void checkBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxFixedSizes.Enabled = !checkBoxManual.Checked;
            textBoxWidth.Enabled = textBoxHeight.Enabled = checkBoxManual.Checked;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            bool manualChecked = checkBoxManual.Checked;
            if (!checkBoxManual.Checked)
            {
                if (radioButton320x240.Checked)
                {
                    SelectedSize = new Size(320, 240);
                    checkbutton.CheckButton = 1;
                }
                else if (radioButton640x480.Checked)
                {
                    SelectedSize = new Size(640, 480);
                    checkbutton.CheckButton = 2;
                }
                else if (radioButton800x600.Checked)
                {
                    SelectedSize = new Size(800, 600);
                    checkbutton.CheckButton = 3;
                }
            }
            else
            {
                if (int.TryParse(textBoxWidth.Text, out width) && int.TryParse(textBoxHeight.Text, out height))
                {
                    if (width <= 0 || height <= 0)
                    {
                        MessageBox.Show("Ширина и высота должны быть больше нуля.", "Ошибка");
                        return;
                    }
                    else if (width > 1556 || height > 884)
                    {
                        MessageBox.Show("Максимальное значение ширины - 1556, высоты - 884.", "Предупреждение");
                        return;
                    }
                    SelectedSize = new Size(width, height);
                    checkbutton.CheckButton = 4;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректные значения ширины и высоты.", "Ошибка");
                    return;
                }
            }

            checkbutton.SaveCheckButton();
            DialogResult = DialogResult.OK;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
