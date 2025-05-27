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
            this.ClientSize = new Size(800, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(30, 30, 40);

            // Название игры (две строки)
            var titleLabel = new Label
            {
                Text = "Квантовый\nпереключатель",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 30)
            };

            // Кнопки уровней
            var level1Button = CreateLevelButton("Уровень 1", 1, 130);
            var level2Button = CreateLevelButton("Уровень 2", 2, 180);
            var level3Button = CreateLevelButton("Уровень 3", 3, 230);
            var level4Button = CreateLevelButton("Уровень 4", 4, 280);

            // Кнопка выхода
            var exitButton = new Button
            {
                Text = "Выход",
                Size = new Size(200, 40),
                Location = new Point(100, 340),
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            exitButton.Click += (s, e) => Application.Exit();

            // Описание управления и цели (справа)
            var instructionsLabel = new Label
            {
                Text = "Как играть:\n\n" +
                       "↑ Стрелка вверх — прыжок\n" +
                       "← и → — движение влево и вправо\n" +
                       "Пробел — переключение миров\n\n" +
                       "Цель игры:\n" +
                       "Доберитесь до фиолетового портала,\n" +
                       "избегая опасных красных шипов.",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = Color.White,
                Size = new Size(350, 250),
                Location = new Point(400, 100),
                BackColor = Color.FromArgb(40, 40, 50),
                Padding = new Padding(10)
            };

            // Добавляем элементы на форму
            this.Controls.AddRange(new Control[]
            {
                titleLabel,
                level1Button, level2Button, level3Button, level4Button,
                exitButton,
                instructionsLabel
            });
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
