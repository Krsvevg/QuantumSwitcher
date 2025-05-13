using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Level
    {
        public List<Platform> Platforms { get; private set; }
        public List<Spike> Spikes { get; private set; }
        public Portal ExitPortal { get; private set; }
        public Point PlayerStartPosition { get; private set; }

        public Level(Form form, int levelNumber)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            try
            {
                switch (levelNumber)
                {
                    case 1:
                        LoadLevel1(form);
                        break;
                    case 2:
                        LoadLevel2(form);
                        break;
                    case 3:
                        LoadLevel3(form);
                        break;
                    default:
                        LoadLevel1(form);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки уровня: {ex.Message}");
                throw;
            }

        }
        private void LoadLevel1(Form form)
        {
            Platforms = new List<Platform>(); // Инициализация перед добавлением
            Spikes = new List<Spike>(); // Инициализация перед добавлением

            PlayerStartPosition = new Point(100, 100);

            // Основной пол (виден только в синем мире)
            Platforms.Add(new Platform(form, 0, form.ClientSize.Height - 50, form.ClientSize.Width, 50, true, false));
            // Потолок (виден только в красном мире)
            Platforms.Add(new Platform(form, 0, 0, form.ClientSize.Width, 20, false, true));
            // Промежуточная платформа (видна только в красном мире)
            Platforms.Add(new Platform(form, 300, 300, 200, 20, false, true));
            //Видна в синем мире
            Platforms.Add(new Platform(form, 300, 450, 200, 20, true, false));

            // Шипы
            Spikes.Add(new Spike(form, 200, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 400, 380, 50, 20, false, true));

            ExitPortal = new Portal(form, 700, 350);
        }

        private void LoadLevel2(Form form)
        {
            PlayerStartPosition = new Point(50, 100);
        }

        private void LoadLevel3(Form form)
        {
            PlayerStartPosition = new Point(50, 50);
        }
        public void InitializeWorlds(bool isBlueWorld)
        {
            foreach (var platform in Platforms)
            {
                platform.UpdateVisibility(isBlueWorld);
            }

            foreach (var spike in Spikes)
            {
                spike.UpdateVisibility(isBlueWorld);
            }
        }

    }
}