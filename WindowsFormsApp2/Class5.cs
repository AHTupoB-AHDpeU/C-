using System;
using System.IO;

namespace WindowsFormsApp2
{
    public class CheckButtonMet
    {
        private string fileName = "checkButton.txt";
        public int CheckButton;

        public CheckButtonMet()
        {
            CheckButton = LoadCheckButton();
        }

        public int LoadCheckButton()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string value = File.ReadAllText(fileName);
                    return int.Parse(value);
                }
                catch (Exception)
                {
                    return 1; // по умолчанию
                }
            }
            else
            {
                return 1;
            }
        }

        public void SaveCheckButton()
        {
            File.WriteAllText(fileName, CheckButton.ToString());
        }
    }
}
