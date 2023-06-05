using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FourPhotosOneWordGame
{
    public partial class MainForm : Form
    {
        private List<string[]> imageAndAnswerList;
        private int currentIndex;

        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Инициализация списка перед загрузкой данных
            imageAndAnswerList = new List<string[]>();

            // Загрузка данных из файла
            string[] lines = File.ReadAllLines("answers.txt");

            // Разделение строк на изображение и ответ
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                string image = parts[0].Trim();
                string answer = parts[1].Trim();
                imageAndAnswerList.Add(new string[] { image, answer });
            }

            // Отображение первого изображения
            if (imageAndAnswerList.Count > 0)
            {
                currentIndex = 0;
                string[] firstImageAndAnswer = imageAndAnswerList[currentIndex];
                pictureBox1.Image = Image.FromFile(firstImageAndAnswer[0]);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string[] currentImageAndAnswer = imageAndAnswerList[currentIndex];
            string correctAnswer = currentImageAndAnswer[1];
            string userAnswer = textBox1.Text.Trim();

            if (userAnswer.Length == correctAnswer.Length)
            {
                if (userAnswer.ToLower() == correctAnswer.ToLower())
                {
                    MessageBox.Show("Правильный ответ!");
                    NextImage();
                }
                else
                {
                    MessageBox.Show("Неправильный ответ!");
                }

                textBox1.Clear();
                textBox1.Focus();
            }
        }


        private void NextImage()
        {
            currentIndex++;
            if (currentIndex < imageAndAnswerList.Count)
            {
                string[] nextImageAndAnswer = imageAndAnswerList[currentIndex];
                pictureBox1.Image = Image.FromFile(nextImageAndAnswer[0]);

            }
            else
            {
                MessageBox.Show("Вы прошли все уровни!");
                // Дополнительные действия при завершении игры
            }
        }

    }
}
