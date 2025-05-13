using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Portal
    {
        public PictureBox Sprite { get; private set; }

        public Portal(Form form, int x, int y)
        {
            Sprite = new PictureBox
            {
                Location = new Point(x, y),
                Width = 40,
                Height = 40,
                BackColor = Color.Purple,
                Tag = "portal"
            };
            form.Controls.Add(Sprite);
        }
    }
}