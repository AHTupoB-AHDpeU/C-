using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    [Serializable]
    public class FileManager
    {
        public static string Name { get; private set; }
        public static event Action<string> FileNameSet;

        // Имя файла
        public static void SetFileName(string fileName)
        {
            Name = fileName;
            FileNameSet?.Invoke(Name);
        }

        // Метод сохранения данных о рисунке
        public static void SaveDataToFile(string fileName, List<(string, Size)> imageInfoList)
        {
            var dataList = new List<dynamic>();
            foreach (var imageInfo in imageInfoList)
            {
                var data = new { ImageName = imageInfo.Item1, FormSize = $"{imageInfo.Item2.Width},{imageInfo.Item2.Height}" };
                dataList.Add(data);
            }

            string jsonData = JsonConvert.SerializeObject(dataList);
            File.WriteAllText(fileName, jsonData);
        }

        // Метод чтения данных о рисунке
        public static List<(string, Size)> LoadDataFromFile(string fileName)
        {
            List<(string, Size)> imageInfoList = new List<(string, Size)>();

            if (!File.Exists(fileName))
                return imageInfoList;

            string jsonData = File.ReadAllText(fileName);
            var dataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            foreach (var data in dataList)
            {
                string imageName = data.ImageName;
                string[] formSizeArray = data.FormSize.ToString().Split(',');
                int width = int.Parse(formSizeArray[0]);
                int height = int.Parse(formSizeArray[1]);
                Size formSize = new Size(width, height);
                imageInfoList.Add((imageName, formSize));
            }

            return imageInfoList;
        }

        public static void SaveToFile(Form2 form, List<(string, Size)> imageInfoList)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.Filter = "Рисунки (*.png)|*.png";

            if (!string.IsNullOrEmpty(form.FileName)) // Проверяем, есть ли у формы имя файла
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(form.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, form.Figures);
                    formatter.Serialize(stream, form.Figures1);
                    formatter.Serialize(stream, form.Figures2);
                    formatter.Serialize(stream, form.Figures3);
                }
                form.Text = Path.GetFileName(form.FileName);
                SetFileName(form.Text);
                form.isChangesSaved = true; // Устанавливаем флаг изменений

                imageInfoList.Add((form.Text, form.Size));
                SaveDataToFile("data.txt", imageInfoList);
            }
            else
            {
                SaveToFileAs(form, imageInfoList);
            }
        }

        public static void SaveToFileAs(Form2 form, List<(string, Size)> imageInfoList)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.Filter = "Рисунки (*.png)|*.png";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, form.Figures);
                    formatter.Serialize(stream, form.Figures1);
                    formatter.Serialize(stream, form.Figures2);
                    formatter.Serialize(stream, form.Figures3);
                }
                form.FileName = saveFileDialog.FileName; // Сохраняем имя файла в форме
                form.Text = Path.GetFileName(saveFileDialog.FileName);
                SetFileName(form.Text);
                form.isChangesSaved = true; // Устанавливаем флаг изменений

                imageInfoList.Add((form.Text, form.Size));
                SaveDataToFile("data.txt", imageInfoList);
            }
        }

        public static void LoadFromFile(Form2 form)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Рисунки (*.png)|*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    List<Figure> figures = (List<Figure>)formatter.Deserialize(stream);
                    List<Figure> figures1 = (List<Figure>)formatter.Deserialize(stream);
                    List<Figure> figures2 = (List<Figure>)formatter.Deserialize(stream);
                    List<Figure> figures3 = (List<Figure>)formatter.Deserialize(stream);
                    form.SetFigures(figures);
                    form.SetFigures1(figures1);
                    form.SetFigures2(figures2);
                    form.SetFigures3(figures3);
                }
                form.FileName = openFileDialog.FileName; // Сохраняем имя файла в форме
                form.Text = Path.GetFileName(openFileDialog.FileName);
                SetFileName(form.Text);
                form.Invalidate();

                var imageInfoList = LoadDataFromFile("data.txt");
                foreach (var imageInfo in imageInfoList)
                {
                    if (imageInfo.Item1 == form.Text)
                    {
                        form.Size = imageInfo.Item2;
                        break;
                    }
                }
            }
        }
    }
}

