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
            this.Text = "Квантовый Переключатель - Главное меню";
            this.ClientSize = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(30, 30, 45);

            var titleLabel = new Label
            {
                Text = "КВАНТОВЫЙ\nПЕРЕКЛЮЧАТЕЛЬ",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.DeepSkyBlue,
                AutoSize = true,
                Location = new Point(50, 30)
            };

            var buttonsY = new[] { 150, 200, 250, 300 };
            var buttons = new[]
            {
        CreateLevelButton("▶ Уровень 1", 1, buttonsY[0]),
        CreateLevelButton("▶ Уровень 2", 2, buttonsY[1]),
        CreateLevelButton("▶ Уровень 3", 3, buttonsY[2]),
        CreateLevelButton("▶ Уровень 4", 4, buttonsY[3]),
    };

            var exitButton = new Button
            {
                Text = "❌ Выход",
                Size = new Size(220, 45),
                Location = new Point(80, 370),
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += (s, e) => Application.Exit();

            var instructionsLabel = new Label
            {
                Text = "📖 Как играть:\n\n" +
                       "↑ — прыжок\n←/→ — движение\nПробел — переключение миров\n\n🎯 Цель:\nДостигните фиолетового портала,\nизбегая красных шипов.",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Size = new Size(400, 240),
                Location = new Point(430, 120),
                BackColor = Color.FromArgb(45, 45, 60),
                Padding = new Padding(10)
            };

            this.Controls.Add(titleLabel);
            this.Controls.AddRange(buttons);
            this.Controls.Add(exitButton);
            this.Controls.Add(instructionsLabel);
        }

        private Button CreateLevelButton(string text, int level, int yPos)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(220, 45),
                Location = new Point(80, yPos),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Tag = level
            };
            button.FlatAppearance.BorderSize = 0;

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
