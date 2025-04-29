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
        public bool IsGameRunning { get; private set; }

        public GameManager(Form form, int levelNumber)
        {
            GameForm = form ?? throw new ArgumentNullException(nameof(form));
            CurrentLevel = new Level(GameForm, levelNumber);  
            Player = new Player(GameForm)
            {
                Sprite = { Location = CurrentLevel.PlayerStartPosition }
            };

            if (Player == null) throw new Exception("Player не создан");
            if (Player.Sprite == null) throw new Exception("Sprite игрока не создан");
        }

        public void Update()
        {
            // Полная проверка всех объектов
            if (GameForm?.IsDisposed != false ||
                Player?.Sprite == null ||
                CurrentLevel?.Platforms == null)
                return;

            // Применяем гравитацию с силой 0.5f
            Player.ApplyGravity(0.5f);

            // Обновляем позицию
            Player.UpdatePosition();

            // Проверка достижения портала
            if (Player.Sprite.Bounds.IntersectsWith(CurrentLevel.ExitPortal.Sprite.Bounds))
            {
                LevelCompleted?.Invoke();
            }

            // Обновляем видимость всех платформ
            UpdatePlatformsVisibility();

            // Проверяем коллизии
            bool isGrounded = CheckCollisions();
            Player.SetGrounded(isGrounded);
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

            foreach (var platform in CurrentLevel.Platforms)
            {
                if (platform.Sprite.Visible &&
                    Player.Sprite.Bounds.IntersectsWith(platform.Sprite.Bounds))
                {
                    // Синий мир: столкновение сверху
                    if (Player.IsInBlueWorld && Player.VelocityY > 0)
                    {
                        Player.Sprite.Top = platform.Sprite.Top - Player.Sprite.Height;
                        isGrounded = true;
                    }
                    // Красный мир: столкновение снизу
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
    }
}