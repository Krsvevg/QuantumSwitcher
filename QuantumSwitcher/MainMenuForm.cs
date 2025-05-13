using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public partial class MainMenuForm : Form
    {
        public int SelectedLevel { get; private set; } = 1;

        public MainMenuForm()
        {
            // Настройки формы
            this.Text = "Квантовый Переключатель - Главное меню";
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(30, 30, 40);

            // Название игры
            var titleLabel = new Label
            {
                Text = "Название игры",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 30)
            };

            // Кнопки уровней
            var level1Button = CreateLevelButton("Уровень 1", 1, 100);
            var level2Button = CreateLevelButton("Уровень 2", 2, 150);
            var level3Button = CreateLevelButton("Уровень 3", 3, 200);

            // Кнопка выхода
            var exitButton = new Button
            {
                Text = "Выход",
                Size = new Size(120, 40),
                Location = new Point(140, 250),
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            exitButton.Click += (s, e) => Application.Exit();

            // Добавляем элементы на форму
            this.Controls.AddRange(new Control[] { titleLabel, level1Button, level2Button, level3Button, exitButton });
        }

        private Button CreateLevelButton(string text, int level, int yPos)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(200, 40),
                Location = new Point(100, yPos),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Tag = level
            };

            button.Click += (s, e) =>
            {
                SelectedLevel = (int)((Button)s).Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            return button;
        }
    }
}