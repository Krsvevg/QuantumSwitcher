using System.Drawing;
using System.Windows.Forms;

public class Spike
{
    public PictureBox Sprite { get; }
    public Rectangle Bounds => Sprite.Bounds;
    public bool ExistsInBlueWorld { get; set; }
    public bool ExistsInRedWorld { get; set; }
    public int OriginalX { get; private set; }
    public Spike(Form form, int x, int y, int width, int height,
                bool existsInBlueWorld, bool existsInRedWorld)
    {
        OriginalX = x;
        ExistsInBlueWorld = existsInBlueWorld;
        ExistsInRedWorld = existsInRedWorld;

        Sprite = new PictureBox
        {
            Location = new Point(x, y),
            Size = new Size(width, height),
            BackColor = Color.Red,
            Tag = "spike",
            Visible = existsInBlueWorld // Начальная видимость
        };

        form.Controls.Add(Sprite);
    }

    public void UpdateVisibility(bool isBlueWorld)
    {
        Sprite.Visible = (isBlueWorld && ExistsInBlueWorld) ||
                        (!isBlueWorld && ExistsInRedWorld);
    }
}