using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public class Level
    {
        public Point PlayerStartPosition { get; set; }
        public int LevelWidth { get; private set; }
        public List<Platform> Platforms { get; private set; }
        public List<Spike> Spikes { get; private set; }
        public Portal ExitPortal { get; private set; }

        public Level(Form form, int levelNumber)
        {
            Platforms = new List<Platform>();
            Spikes = new List<Spike>();

            switch (levelNumber)
            {
                case 1: LoadLevel1(form); break;
                case 2: LoadLevel2(form); break;
                case 3: LoadLevel3(form); break;
                case 4: LoadLevel4(form); break;
                default: LoadLevel1(form); break;
            }

            // Добавляем правую границу уровня (невидимую платформу)
            AddRightBoundary(form);
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

        private void AddRightBoundary(Form form)
        {
            // Невидимая платформа как правая граница уровня
            Platforms.Add(new Platform(form, LevelWidth - 10, 0,
                10, form.ClientSize.Height, false, false)
            {
                Sprite = { Visible = false }
            });
        }

        private void LoadLevel1(Form form)
        {
            LevelWidth = 2200; // увеличена ширина уровня
            PlayerStartPosition = new Point(100, 100);

            AddFloorAndCeiling(form);

            // Добавлены дополнительные платформы
            Platforms.Add(new Platform(form, 250, 450, 150, 20, true, false));  // синяя платформа ближе к старту
            Platforms.Add(new Platform(form, 450, 420, 150, 20, false, true));  // красная платформа
            Platforms.Add(new Platform(form, 700, 380, 200, 20, true, false));   // синяя платформа дальше

            // Усложнённый пандус для прыжков с разной высотой
            Platforms.Add(new Platform(form, 1000, 400, 100, 20, true, false));
            Platforms.Add(new Platform(form, 1100, 350, 100, 20, false, true)); // теперь красная
            Platforms.Add(new Platform(form, 1200, 300, 100, 20, true, false));

            // Добавлены дополнительные шипы
            Spikes.Add(new Spike(form, 1350, 530, 50, 20, true, false));  // на земле
            Spikes.Add(new Spike(form, 1400, 180, 50, 20, false, true));  // потолочные шипы

            // Выход сдвинут дальше
            ExitPortal = new Portal(form, 2100, 250);
        }

        private void LoadLevel2(Form form)
        {
            LevelWidth = 2600; // увеличена ширина уровня
            PlayerStartPosition = new Point(50, 100);

            AddFloorAndCeiling(form);

            // Усложнённые платформы с чередованием
            Platforms.Add(new Platform(form, 250, 480, 150, 20, true, false));
            Platforms.Add(new Platform(form, 450, 400, 150, 20, false, true));
            Platforms.Add(new Platform(form, 650, 350, 150, 20, true, true)); // в обоих мирах
            Platforms.Add(new Platform(form, 900, 300, 150, 20, false, true));
            Platforms.Add(new Platform(form, 1100, 250, 150, 20, true, false));

            // Прыжок в красный мир
            Platforms.Add(new Platform(form, 1300, 200, 120, 20, false, true));
            Spikes.Add(new Spike(form, 1350, 180, 50, 20, false, true)); // потолочные шипы

            // Добавлены шипы и платформы в средней зоне
            Spikes.Add(new Spike(form, 1500, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 1550, 530, 50, 20, true, false));
            Platforms.Add(new Platform(form, 1600, 400, 150, 20, true, false));

            // Узкая платформа с шипами снизу
            Platforms.Add(new Platform(form, 1850, 350, 100, 20, false, true));
            Spikes.Add(new Spike(form, 1870, 180, 50, 20, false, true)); // потолочные шипы

            ExitPortal = new Portal(form, 2500, 300);
        }

        private void LoadLevel3(Form form)
        {
            LevelWidth = 3500; // увеличена ширина уровня
            PlayerStartPosition = new Point(50, 50);

            AddFloorAndCeiling(form);

            // Более длинная и сложная лестница
            Platforms.Add(new Platform(form, 300, 470, 100, 20, true, false));
            Platforms.Add(new Platform(form, 450, 430, 100, 20, false, true));
            Platforms.Add(new Platform(form, 600, 390, 100, 20, true, false));
            Platforms.Add(new Platform(form, 750, 350, 100, 20, false, true));
            Platforms.Add(new Platform(form, 900, 310, 100, 20, true, false));
            Platforms.Add(new Platform(form, 1050, 270, 100, 20, false, true));

            // Платформы с чередованием видимости
            Platforms.Add(new Platform(form, 1200, 230, 150, 20, true, true));
            Platforms.Add(new Platform(form, 1400, 270, 150, 20, true, false));
            Platforms.Add(new Platform(form, 1600, 310, 150, 20, false, true));

            // Увеличенное количество шипов
            Spikes.Add(new Spike(form, 1700, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 1750, 530, 50, 20, true, false));
            Spikes.Add(new Spike(form, 1800, 180, 50, 20, false, true));
            Spikes.Add(new Spike(form, 1850, 180, 50, 20, false, true));

            // Более длинный финальный рывок
            Platforms.Add(new Platform(form, 1900, 300, 250, 20, true, false));
            Platforms.Add(new Platform(form, 2200, 250, 150, 20, false, true));
            Platforms.Add(new Platform(form, 2500, 200, 150, 20, true, false));

            ExitPortal = new Portal(form, 3300, 180);
        }
        private void LoadLevel4(Form form)
        {
            LevelWidth = 4000;
            PlayerStartPosition = new Point(50, 50);

            AddFloorAndCeiling(form);

            // Стартовая зона: чередование платформ
            Platforms.Add(new Platform(form, 300, 450, 120, 20, true, false));
            Platforms.Add(new Platform(form, 450, 400, 120, 20, false, true));
            Platforms.Add(new Platform(form, 600, 350, 120, 20, true, false));
            Platforms.Add(new Platform(form, 750, 300, 120, 20, false, true));
            Platforms.Add(new Platform(form, 900, 250, 120, 20, true, false));

            Spikes.Add(new Spike(form, 960, 230, 50, 20, true, false)); // над платформой
            Spikes.Add(new Spike(form, 1000, 10, 50, 20, false, true));
            Spikes.Add(new Spike(form, 1800, 10, 50, 20, false, true));
            Spikes.Add(new Spike(form, 2600, 10, 50, 20, false, true));
            Spikes.Add(new Spike(form, 3400, 10, 50, 20, false, true));

            // Средняя зона: лабиринт платформ
            for (int i = 0; i < 5; i++)
            {
                int x = 1100 + i * 200;
                Platforms.Add(new Platform(form, x, 450, 100, 20, true, i % 2 == 0));
                Platforms.Add(new Platform(form, x, 300, 100, 20, false, i % 2 == 1));
                if (i % 2 == 0)
                {
                    Spikes.Add(new Spike(form, x + 30, 530, 50, 20, true, false)); // снизу
                }
                else
                {
                    Spikes.Add(new Spike(form, x + 30, 180, 50, 20, false, true)); // сверху
                }
            }

            // Зона с ложными маршрутами
            Platforms.Add(new Platform(form, 2200, 250, 100, 20, true, true)); // общая
            Platforms.Add(new Platform(form, 2350, 200, 100, 20, false, true)); // тупик
            Spikes.Add(new Spike(form, 2400, 180, 50, 20, false, true)); // наказание

            // Точное переключение в прыжке
            Platforms.Add(new Platform(form, 2600, 300, 150, 20, true, false));
            Spikes.Add(new Spike(form, 2700, 280, 50, 20, true, false));
            Platforms.Add(new Platform(form, 2800, 200, 150, 20, false, true));
            Spikes.Add(new Spike(form, 2850, 180, 50, 20, false, true));

            // Тонкая платформа над пропастью
            Platforms.Add(new Platform(form, 3100, 400, 80, 20, true, false));
            Platforms.Add(new Platform(form, 3200, 350, 80, 20, false, true));
            Platforms.Add(new Platform(form, 3300, 300, 80, 20, true, false));
            Spikes.Add(new Spike(form, 3180, 530, 50, 20, true, false)); // снизу
            Spikes.Add(new Spike(form, 3320, 180, 50, 20, false, true)); // сверху

            // Финальный спуск
            Platforms.Add(new Platform(form, 3500, 250, 150, 20, true, true));
            Platforms.Add(new Platform(form, 3700, 400, 150, 20, true, false));

            // Последняя ловушка перед порталом
            Spikes.Add(new Spike(form, 3800, 530, 50, 20, true, false));
            Platforms.Add(new Platform(form, 3900, 300, 100, 20, true, false));

            ExitPortal = new Portal(form, 3950, 250);
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