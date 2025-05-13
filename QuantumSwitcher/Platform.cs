using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Platform
    {
        public PictureBox Sprite { get; private set; }
        public bool ExistsInBlueWorld { get; set; }
        public bool ExistsInRedWorld { get; set; }

        public Platform(Form form, int x, int y, int width, int height, bool blueWorld, bool redWorld)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));

            ExistsInBlueWorld = blueWorld;
            ExistsInRedWorld = redWorld;

            Sprite = new PictureBox
            {
                Location = new Point(x, y),
                Width = width,
                Height = height,
                BackColor = Color.Gray,
                Tag = "platform"
            };
            form.Controls.Add(Sprite);
            UpdateVisibility(true); // По умолчанию начинаем в синем мире
        }

        public void UpdateVisibility(bool isBlueWorld)
        {
            // Важно использовать Invoke для потокобезопасности
            if (Sprite.InvokeRequired)
            {
                Sprite.Invoke(new Action(() => UpdateVisibility(isBlueWorld)));
                return;
            }

            Sprite.Visible = (isBlueWorld && ExistsInBlueWorld) || (!isBlueWorld && ExistsInRedWorld);
            Sprite.Enabled = Sprite.Visible; 
            Sprite.BackColor = (isBlueWorld && ExistsInBlueWorld) ? Color.LightBlue :
                      (!isBlueWorld && ExistsInRedWorld) ? Color.Pink : Color.Gray;
        }
    }
}