using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public partial class LevelCompletedDialog : Form
    {
        public LevelCompletedDialog(int levelNumber)
        {
            this.Text = "Уровень пройден!";
            this.Size = new Size(500, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(35, 45, 65);

            var iconLabel = new Label
            {
                Text = "🎉",
                Font = new Font("Segoe UI", 40),
                AutoSize = true,
                Location = new Point(30, 30)
            };

            var messageLabel = new Label
            {
                Text = $"Уровень {levelNumber} пройден!",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(150, 45)
            };

            var menuBtn = new Button
            {
                Text = "🏠 В меню",
                DialogResult = DialogResult.OK,
                Size = new Size(160, 45),
                Location = new Point((this.ClientSize.Width - 160) / 2, 110),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            menuBtn.FlatAppearance.BorderSize = 0;

            this.Controls.Add(iconLabel);
            this.Controls.Add(messageLabel);
            this.Controls.Add(menuBtn);

            this.AcceptButton = menuBtn;
            this.CancelButton = menuBtn;
        }
    }
}
