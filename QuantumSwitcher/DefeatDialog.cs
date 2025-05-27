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
            this.Size = new Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label = new Label
            {
                Text = "Вы проиграли!",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 20)
            };

            var restartBtn = new Button
            {
                Text = "Заново",
                DialogResult = DialogResult.Retry,
                Size = new Size(100, 30),
                Location = new Point(30, 60),
                BackColor = Color.LightGreen
            };

            var menuBtn = new Button
            {
                Text = "В меню",
                DialogResult = DialogResult.Abort, 
                Size = new Size(100, 30),
                Location = new Point(170, 60),
                BackColor = Color.LightCoral
            };

            this.Controls.Add(label);
            this.Controls.Add(restartBtn);
            this.Controls.Add(menuBtn);

            this.AcceptButton = restartBtn;
            this.CancelButton = menuBtn;
        }
    }
}