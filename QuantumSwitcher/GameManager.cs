using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class GameManager
    {
        private Camera _camera;
        public Player Player { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Form GameForm { get; private set; }
        public event Action LevelCompleted;
        public event Action LevelFailed;
        public bool IsGameRunning { get; private set; }
        private bool _levelCompleted = false;
        private readonly int _levelNumber;

        public GameManager(Form form, int levelNumber)
        {
            _levelNumber = levelNumber;
            GameForm = form ?? throw new ArgumentNullException(nameof(form));
            CurrentLevel = new Level(form, levelNumber);
            _camera = new Camera(form, form.ClientSize.Width, CurrentLevel.LevelWidth);

            Player = new Player(form, CurrentLevel.LevelWidth)
            {
                Sprite = { Location = CurrentLevel.PlayerStartPosition }
            };

            CurrentLevel.InitializeWorlds(Player.IsInBlueWorld);
        }

        public void Update()
        {
            if (_levelCompleted || GameForm?.IsDisposed != false || Player?.Sprite == null)
                return;

            Player.SetGrounded(CheckCollisions());
            Player.ApplyGravity(1.0f);
            Player.UpdatePosition();
            _camera.Update(Player.GetAbsoluteX());
            UpdateAllObjectsPosition();

            if (CheckSpikeCollision())
            {
                _levelCompleted = true;
                LevelFailed?.Invoke();
                return;
            }

            if (Player.Sprite.Bounds.IntersectsWith(CurrentLevel.ExitPortal.Sprite.Bounds))
            {
                _levelCompleted = true;
                LevelCompleted?.Invoke();
            }

            UpdatePlatformsVisibility();
            
        }

        private void UpdateAllObjectsPosition()
        {
            Player.Sprite.Left = Player.GetAbsoluteX() - _camera.X;

            foreach (var platform in CurrentLevel.Platforms)
            {
                platform.Sprite.Left = platform.OriginalX - _camera.X;
            }

            foreach (var spike in CurrentLevel.Spikes)
            {
                spike.Sprite.Left = spike.OriginalX - _camera.X;
            }

            CurrentLevel.ExitPortal.Sprite.Left = CurrentLevel.ExitPortal.OriginalX - _camera.X;
        }

        private void UpdatePlatformsVisibility()
        {
            foreach (var platform in CurrentLevel.Platforms)
            {
                platform.UpdateVisibility(Player.IsInBlueWorld);
            }

            foreach (var spike in CurrentLevel.Spikes)
            {
                spike.UpdateVisibility(Player.IsInBlueWorld);
            }
        }

        public void ResetLevel()
        {
            foreach (Control control in GameForm.Controls.OfType<PictureBox>().ToList())
            {
                if (control.Tag != null)
                {
                    GameForm.Controls.Remove(control);
                    control.Dispose();
                }
            }

            CurrentLevel = new Level(GameForm, _levelNumber);
            Player = new Player(GameForm, CurrentLevel.LevelWidth)
            {
                Sprite = { Location = CurrentLevel.PlayerStartPosition }
            };

            _levelCompleted = false;
        }

        private bool CheckCollisions()
        {
            bool isGrounded = false;

            foreach (var platform in CurrentLevel.Platforms.Where(p => p.Sprite.Visible))
            {
                if (Player.Sprite.Bounds.IntersectsWith(platform.Sprite.Bounds))
                {
                    // Проверяем направление движения игрока
                    if (Player.IsInBlueWorld)
                    {
                        // Если игрок движется ВНИЗ (падение) и находится над платформой
                        if (Player.VelocityY > 0 &&
                            Player.Sprite.Bottom >= platform.Sprite.Top &&
                            Player.Sprite.Bottom <= platform.Sprite.Top + 10) // Небольшой допуск
                        {
                            Player.Sprite.Top = platform.Sprite.Top - Player.Sprite.Height;
                            isGrounded = true;
                        }
                        // Если игрок движется ВВЕРХ (прыжок) и находится под платформой
                        else if (Player.VelocityY < 0 &&
                                 Player.Sprite.Top <= platform.Sprite.Bottom &&
                                 Player.Sprite.Top >= platform.Sprite.Bottom - 10)
                        {
                            Player.VelocityY = 0; // Останавливаем прыжок
                        }
                    }
                    else // Красный мир (гравитация вверх)
                    {
                        // Если игрок движется ВВЕРХ (падение) и находится под платформой
                        if (Player.VelocityY < 0 &&
                            Player.Sprite.Top <= platform.Sprite.Bottom &&
                            Player.Sprite.Top >= platform.Sprite.Bottom - 10)
                        {
                            Player.Sprite.Top = platform.Sprite.Bottom;
                            isGrounded = true;
                        }
                        // Если игрок движется ВНИЗ (прыжок) и находится над платформой
                        else if (Player.VelocityY > 0 &&
                                 Player.Sprite.Bottom >= platform.Sprite.Top &&
                                 Player.Sprite.Bottom <= platform.Sprite.Top + 10)
                        {
                            Player.VelocityY = 0; // Останавливаем прыжок
                        }
                    }

                    if (isGrounded)
                    {
                        Player.VelocityY = 0;
                    }
                }
            }
            return isGrounded;
        }

        private bool CheckSpikeCollision()
        {
            foreach (var spike in CurrentLevel.Spikes)
            {
                bool spikeActive = (Player.IsInBlueWorld && spike.ExistsInBlueWorld) ||
                                 (!Player.IsInBlueWorld && spike.ExistsInRedWorld);

                if (spikeActive && spike.Sprite.Visible && Player.Sprite.Bounds.IntersectsWith(spike.Sprite.Bounds))
                {
                    Debug.WriteLine("Обнаружено столкновение с шипами!");
                    return true;
                }
            }
            return false;
        }
    }
}