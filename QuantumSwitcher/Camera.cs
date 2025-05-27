using System;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Camera
    {
        public int X { get; private set; }
        private readonly int _screenWidth;
        private readonly int _levelWidth;
        private readonly Form _form;

        public Camera(Form form, int screenWidth, int levelWidth)
        {
            _form = form;
            _screenWidth = screenWidth;
            _levelWidth = levelWidth;
        }

        public void Update(int playerX)
        {
            // Камера следует за игроком в обоих мирах
            X = Math.Max(0, playerX - _screenWidth / 2);
            X = Math.Min(X, _levelWidth - _screenWidth);
        }
    }
}