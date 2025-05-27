using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public partial class DefeatDialog : Form
    {
        public DefeatDialog()
        {
            this.Text = "Поражение";
            this.Size = new Size(350, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(40, 40, 60);

            var label = new Label
            {
                Text = "Вы проиграли!",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 25)
            };

            var restartBtn = new Button
            {
                Text = "🔁 Заново",
                DialogResult = DialogResult.Retry,
                Size = new Size(120, 40),
                Location = new Point(40, 90),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            restartBtn.FlatAppearance.BorderSize = 0;

            var menuBtn = new Button
            {
                Text = "🏠 В меню",
                DialogResult = DialogResult.Abort,
                Size = new Size(120, 40),
                Location = new Point(180, 90),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            menuBtn.FlatAppearance.BorderSize = 0;

            this.Controls.Add(label);
            this.Controls.Add(restartBtn);
            this.Controls.Add(menuBtn);

            this.AcceptButton = restartBtn;
            this.CancelButton = menuBtn;
        }
    }
}