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
        public int SwitchesLeft { get; private set; } = 50;
        public int Width { get; set; } = 30;
        public int Height { get; set; } = 30;
        private readonly Form _parentForm;
        private readonly int _levelWidth;
        private int _absoluteX; // Абсолютная позиция в уровне
        private bool _isGrounded;
        private float _jumpForce = 19f;
        private Task _colorChangeDelay;

        public Player(Form parentForm, int levelWidth)
        {
            _parentForm = parentForm ?? throw new ArgumentNullException(nameof(parentForm));
            _levelWidth = levelWidth;
            Sprite = new PictureBox
            {
                Width = Width,
                Height = Height,
                BackColor = Color.Blue,
                Tag = "player"
            };
            _parentForm.Controls.Add(Sprite);
        }

        public void UpdatePosition()
        {
            if (_parentForm?.IsDisposed != false || Sprite == null)
                return;

            // Обновляем абсолютную позицию
            _absoluteX += (int)VelocityX;
            _absoluteX = Math.Max(0, Math.Min(_absoluteX, _levelWidth - Width));

            int newY = Sprite.Top + (int)VelocityY;
            int maxY = _parentForm.ClientSize.Height - Height;
            newY = Math.Max(0, Math.Min(newY, maxY));

            Sprite.Top = newY;

            if (newY <= 0 || newY >= maxY)
            {
                VelocityY = 0;
                if (_colorChangeDelay == null)
                {
                    _colorChangeDelay = Task.Delay(200).ContinueWith(t =>
                    {
                        Sprite.BackColor = IsInBlueWorld ? Color.Blue : Color.Red;
                        _colorChangeDelay = null;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

        public int GetAbsoluteX() => _absoluteX;

        public void SwitchWorld()
        {
            if (SwitchesLeft <= 0) return;

            IsInBlueWorld = !IsInBlueWorld;
            SwitchesLeft--;

            Sprite.BackColor = IsInBlueWorld ? Color.Blue : Color.Red;
            VelocityY = IsInBlueWorld ? Math.Min(0, VelocityY) : Math.Max(0, VelocityY);
        }

        public void ApplyGravity(float gravityForce)
        {
            float gravity = IsInBlueWorld ? gravityForce : -gravityForce;
            VelocityY += gravity;

            float maxFallSpeed = 10f;
            if (IsInBlueWorld)
                VelocityY = Math.Min(VelocityY, maxFallSpeed);
            else
                VelocityY = Math.Max(VelocityY, -maxFallSpeed);
        }

        public void Move(int directionX) => VelocityX = directionX * 5;
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