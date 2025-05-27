using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Level
    {
        public Point PlayerStartPosition { get; set; }
        public int LevelWidth { get; private set; } = 2000;
        public List<Platform> Platforms { get; private set; }
        public List<Spike> Spikes { get; private set; }
        public Portal ExitPortal { get; private set; }

        public Level(Form form, int levelNumber)
        {
            Platforms = new List<Platform>();
            Spikes = new List<Spike>();

            // Добавляем пол и потолок для всех уровней
            AddFloorAndCeiling(form);

            switch (levelNumber)
            {
                case 1: LoadLevel1(form); break;
                case 2: LoadLevel2(form); break;
                case 3: LoadLevel3(form); break;
                default: LoadLevel1(form); break;
            }
        }

        private void AddFloorAndCeiling(Form form)
        {
            // Пол (виден только в синем мире)
            Platforms.Add(new Platform(form, 0, form.ClientSize.Height - 50,
                LevelWidth, 50, true, false));

            // Потолок (виден только в красном мире)
            Platforms.Add(new Platform(form, 0, 0,
                LevelWidth, 20, false, true));
        }

        private void LoadLevel1(Form form)
        {
            PlayerStartPosition = new Point(100, 100);
            LevelWidth = 2000;

            // Добавляем платформы уровня 1
            Platforms.Add(new Platform(form, 300, 300, 200, 20, false, true));
            Platforms.Add(new Platform(form, 300, 450, 200, 20, true, false));

            // Добавляем шипы уровня 1
            Spikes.Add(new Spike(form, 200, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 400, 380, 50, 20, false, true));

            ExitPortal = new Portal(form, 700, 350);
        }

        private void LoadLevel2(Form form)
        {
            PlayerStartPosition = new Point(50, 100);
            LevelWidth = 2500;

            // Добавляем платформы уровня 2
            Platforms.Add(new Platform(form, 400, 350, 200, 20, true, false));
            Platforms.Add(new Platform(form, 600, 200, 200, 20, false, true));

            // Добавляем шипы уровня 2
            Spikes.Add(new Spike(form, 500, 530, 50, 20, true, false));

            ExitPortal = new Portal(form, 1000, 350);
        }

        private void LoadLevel3(Form form)
        {
            PlayerStartPosition = new Point(50, 50);
            LevelWidth = 3000;

            // Добавляем платформы уровня 3
            Platforms.Add(new Platform(form, 500, 400, 200, 20, true, true)); // Видна в обоих мирах
            Platforms.Add(new Platform(form, 800, 250, 200, 20, false, true));

            // Добавляем шипы уровня 3
            Spikes.Add(new Spike(form, 700, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 900, 180, 50, 20, false, true));

            ExitPortal = new Portal(form, 1500, 350);
        }

        public void InitializeWorlds(bool isBlueWorld)
        {
            foreach (var platform in Platforms)
                platform.UpdateVisibility(isBlueWorld);

            foreach (var spike in Spikes)
                spike.UpdateVisibility(isBlueWorld);
        }
    }
}