
namespace WindowsFormsApp2
{
    partial class ResizeDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.radioButton320x240 = new System.Windows.Forms.RadioButton();
            this.radioButton640x480 = new System.Windows.Forms.RadioButton();
            this.radioButton800x600 = new System.Windows.Forms.RadioButton();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.groupBoxFixedSizes = new System.Windows.Forms.GroupBox();
            this.buttonOKClick = new System.Windows.Forms.Button();
            this.buttonCancelClick = new System.Windows.Forms.Button();
            this.groupBoxFixedSizes.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxManual
            // 
            this.checkBoxManual.AutoSize = true;
            this.checkBoxManual.Location = new System.Drawing.Point(302, 38);
            this.checkBoxManual.Name = "checkBoxManual";
            this.checkBoxManual.Size = new System.Drawing.Size(179, 21);
            this.checkBoxManual.TabIndex = 0;
            this.checkBoxManual.Text = "Произвольный размер";
            this.checkBoxManual.UseVisualStyleBackColor = true;
            this.checkBoxManual.CheckedChanged += new System.EventHandler(this.checkBoxManual_CheckedChanged);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(302, 65);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(75, 22);
            this.textBoxWidth.TabIndex = 1;
            // 
            // radioButton320x240
            // 
            this.radioButton320x240.AutoSize = true;
            this.radioButton320x240.Location = new System.Drawing.Point(6, 21);
            this.radioButton320x240.Name = "radioButton320x240";
            this.radioButton320x240.Size = new System.Drawing.Size(192, 21);
            this.radioButton320x240.TabIndex = 2;
            this.radioButton320x240.TabStop = true;
            this.radioButton320x240.Text = "Размер малый (320*240)";
            this.radioButton320x240.UseVisualStyleBackColor = true;
            // 
            // radioButton640x480
            // 
            this.radioButton640x480.AutoSize = true;
            this.radioButton640x480.Location = new System.Drawing.Point(6, 48);
            this.radioButton640x480.Name = "radioButton640x480";
            this.radioButton640x480.Size = new System.Drawing.Size(204, 21);
            this.radioButton640x480.TabIndex = 3;
            this.radioButton640x480.TabStop = true;
            this.radioButton640x480.Text = "Размер средний (640*480)";
            this.radioButton640x480.UseVisualStyleBackColor = true;
            // 
            // radioButton800x600
            // 
            this.radioButton800x600.AutoSize = true;
            this.radioButton800x600.Location = new System.Drawing.Point(6, 75);
            this.radioButton800x600.Name = "radioButton800x600";
            this.radioButton800x600.Size = new System.Drawing.Size(207, 21);
            this.radioButton800x600.TabIndex = 4;
            this.radioButton800x600.TabStop = true;
            this.radioButton800x600.Text = "Размер большой (800*600)";
            this.radioButton800x600.UseVisualStyleBackColor = true;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(383, 65);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(75, 22);
            this.textBoxHeight.TabIndex = 5;
            // 
            // groupBoxFixedSizes
            // 
            this.groupBoxFixedSizes.Controls.Add(this.radioButton320x240);
            this.groupBoxFixedSizes.Controls.Add(this.radioButton640x480);
            this.groupBoxFixedSizes.Controls.Add(this.radioButton800x600);
            this.groupBoxFixedSizes.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFixedSizes.Name = "groupBoxFixedSizes";
            this.groupBoxFixedSizes.Size = new System.Drawing.Size(239, 100);
            this.groupBoxFixedSizes.TabIndex = 6;
            this.groupBoxFixedSizes.TabStop = false;
            this.groupBoxFixedSizes.Text = "Фиксированные размеры";
            // 
            // buttonOKClick
            // 
            this.buttonOKClick.Location = new System.Drawing.Point(12, 141);
            this.buttonOKClick.Name = "buttonOKClick";
            this.buttonOKClick.Size = new System.Drawing.Size(117, 29);
            this.buttonOKClick.TabIndex = 7;
            this.buttonOKClick.Text = "Принять";
            this.buttonOKClick.UseVisualStyleBackColor = true;
            this.buttonOKClick.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancelClick
            // 
            this.buttonCancelClick.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelClick.Location = new System.Drawing.Point(377, 141);
            this.buttonCancelClick.Name = "buttonCancelClick";
            this.buttonCancelClick.Size = new System.Drawing.Size(117, 29);
            this.buttonCancelClick.TabIndex = 8;
            this.buttonCancelClick.Text = "Выйти";
            this.buttonCancelClick.UseVisualStyleBackColor = true;
            this.buttonCancelClick.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ResizeDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 182);
            this.Controls.Add(this.buttonCancelClick);
            this.Controls.Add(this.buttonOKClick);
            this.Controls.Add(this.groupBoxFixedSizes);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.checkBoxManual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResizeDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Изменить размер";
            this.Load += new System.EventHandler(this.ResizeDialogForm_Load);
            this.groupBoxFixedSizes.ResumeLayout(false);
            this.groupBoxFixedSizes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxManual;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.RadioButton radioButton320x240;
        private System.Windows.Forms.RadioButton radioButton640x480;
        private System.Windows.Forms.RadioButton radioButton800x600;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.GroupBox groupBoxFixedSizes;
        private System.Windows.Forms.Button buttonOKClick;
        private System.Windows.Forms.Button buttonCancelClick;
    }
}