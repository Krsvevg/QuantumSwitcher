using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Player
    {
        public PictureBox Sprite { get; private set; }
        public bool IsInBlueWorld { get; private set; } = true;
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public int SwitchesLeft { get; private set; } = 5;
        public int Width { get; set; } = 30;
        public int Height { get; set; } = 30;
        private readonly Form _parentForm;
        private bool _isGrounded;
        private float _jumpForce = 15f;
        private float _gravity = 0.5f;
        private float _maxFallSpeed = 10f;

        public Player(Form parentForm)
        {
            if (parentForm == null)
                throw new ArgumentNullException(nameof(parentForm));
            _parentForm = parentForm;
            Sprite = new PictureBox
            {
                Width = Width,
                Height = Height,
                BackColor = Color.Blue,
                Tag = "player"
            };
            _parentForm.Controls.Add(Sprite);
        }

            public void SwitchWorld()
        {
            if (SwitchesLeft <= 0) return;

            IsInBlueWorld = !IsInBlueWorld;
            SwitchesLeft--;

            // Меняем цвет игрока
            Sprite.BackColor = IsInBlueWorld ? Color.Blue : Color.Red;

            // Меняем направление гравитации
            VelocityY = IsInBlueWorld ? Math.Min(0, VelocityY) : Math.Max(0, VelocityY);
        }

        public void ApplyGravity(float gravityForce)
        {
            // Применяем гравитацию с учетом текущего мира
            float gravity = IsInBlueWorld ? gravityForce : -gravityForce;
            VelocityY += gravity;

            // Ограничиваем максимальную скорость падения/подъема
            float maxFallSpeed = 10f;
            if (IsInBlueWorld)
                VelocityY = Math.Min(VelocityY, maxFallSpeed);
            else
                VelocityY = Math.Max(VelocityY, -maxFallSpeed);
        }

        public void UpdatePosition()
        {
            if (_parentForm == null || _parentForm.IsDisposed || Sprite == null)
                return;

            // Движение по X с границами
            int newX = Sprite.Left + (int)VelocityX;
            newX = Math.Max(0, Math.Min(newX, _parentForm.ClientSize.Width - Sprite.Width));
            Sprite.Left = newX;

            // Движение по Y с границами для обоих миров
            int newY = Sprite.Top + (int)VelocityY;

            // Верхняя граница (для красного мира)
            int minY = 0;

            // Нижняя граница (для синего мира)
            int maxY = _parentForm.ClientSize.Height - Sprite.Height;

            newY = Math.Max(minY, Math.Min(newY, maxY));
            Sprite.Top = newY;

            // Если достигли границы - останавливаем движение
            if (newY <= minY || newY >= maxY)
            {
                VelocityY = 0;
                Sprite.BackColor = IsInBlueWorld ? Color.LightBlue : Color.Pink;
                Task.Delay(200).ContinueWith(t =>
                {
                    Sprite.BackColor = IsInBlueWorld ? Color.Blue : Color.Red;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        public void Move(int directionX)
        {
            // directionX: -1 (влево), 0 (стоять), 1 (вправо)
            VelocityX = directionX * 5; // 5 - скорость движения
        }
        public void ResetSwitches(int switchesCount) => SwitchesLeft = switchesCount;
        public void Jump()
        {
            Debug.WriteLine($"Прыжок. grounded={_isGrounded}, world={(IsInBlueWorld ? "blue" : "red")}");
            if (_isGrounded)
            {
                VelocityY = IsInBlueWorld ? -_jumpForce : _jumpForce;
                _isGrounded = false;
                Debug.WriteLine($"Применена сила прыжка: {VelocityY}");
            }
        }

        public void SetGrounded(bool grounded) => _isGrounded = grounded;
    }
}