using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class GameManager
    {
        public Player Player { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Form GameForm { get; private set; }
        public event Action LevelCompleted;
        public event Action LevelFailed; // Добавлено отсутствующее событие
        public bool IsGameRunning { get; private set; }
        private bool _levelCompleted = false;
        private readonly int _levelNumber; // Добавлено поле для хранения номера уровня

        public GameManager(Form form, int levelNumber)
        {
            _levelNumber = levelNumber;
            GameForm = form ?? throw new ArgumentNullException(nameof(form));
            CurrentLevel = new Level(GameForm, levelNumber);
            Player = new Player(GameForm)
            {
                Sprite = { Location = CurrentLevel.PlayerStartPosition }
            };

            // Инициализация видимости объектов в текущем мире
            CurrentLevel.InitializeWorlds(Player.IsInBlueWorld);

            if (Player == null) throw new Exception("Player не создан");
            if (Player.Sprite == null) throw new Exception("Sprite игрока не создан");

        }

        public void Update()
        {
            if (_levelCompleted) return;

            if (GameForm?.IsDisposed != false ||
                Player?.Sprite == null ||
                CurrentLevel?.Platforms == null)
                return;

            Player.ApplyGravity(0.5f);
            Player.UpdatePosition();

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
            bool isGrounded = CheckCollisions();
            Player.SetGrounded(isGrounded);
        }

        public void ResetLevel()
        {
            // Очищаем элементы управления
            foreach (Control control in GameForm.Controls.OfType<PictureBox>().ToList())
            {
                if (control.Tag != null)
                {
                    GameForm.Controls.Remove(control);
                    control.Dispose();
                }
            }

            // Пересоздаем уровень
            CurrentLevel = new Level(GameForm, _levelNumber);
            Player = new Player(GameForm)
            {
                Sprite = { Location = CurrentLevel.PlayerStartPosition }
            };

            _levelCompleted = false;
        }

        private void UpdatePlatformsVisibility()
        {
            foreach (var platform in CurrentLevel.Platforms)
            {
                platform.UpdateVisibility(Player.IsInBlueWorld);
            }
        }

        private bool CheckCollisions()
        {
            bool isGrounded = false;

            foreach (var platform in CurrentLevel.Platforms.Where(p => p.Sprite.Visible))
            {
                if (Player.Sprite.Bounds.IntersectsWith(platform.Sprite.Bounds))
                {
                    if (Player.IsInBlueWorld && Player.VelocityY > 0)
                    {
                        Player.Sprite.Top = platform.Sprite.Top - Player.Sprite.Height;
                        isGrounded = true;
                    }
                    else if (!Player.IsInBlueWorld && Player.VelocityY < 0)
                    {
                        Player.Sprite.Top = platform.Sprite.Bottom;
                        isGrounded = true;
                    }

                    Player.VelocityY = 0;
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