using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    public partial class MainForm : Form
    {
        private readonly GameManager _gameManager;
        private readonly Timer _gameTimer;
        private readonly int _currentLevel;

        public MainForm(int levelNumber)
        {
            InitializeComponent();

            try
            {
                this.Text = $"Уровень {levelNumber}";
                this.ClientSize = new Size(800, 600);

                _gameManager = new GameManager(this, levelNumber);
                _gameManager.LevelCompleted += OnLevelCompleted;

                _gameTimer = new Timer { Interval = 16 };
                _gameTimer.Tick += (s, e) => _gameManager.Update();
                _gameTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска уровня: {ex.Message}");
                this.Close();
            }

            // Таймер игры
            _gameTimer = new Timer { Interval = 16 };
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void OnLevelCompleted()
        {
            _gameTimer.Stop();
            MessageBox.Show($"Уровень {_currentLevel} пройден!");
            this.Close();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            _gameManager.Update();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _gameTimer.Stop();
                this.Close();
            }
            switch (e.KeyCode)
            {
                case Keys.Space:
                    _gameManager.Player.SwitchWorld();
                    break;
                case Keys.Left:
                    _gameManager.Player.Move(-1);
                    break;
                case Keys.Right:
                    _gameManager.Player.Move(1);
                    break;
                case Keys.Up:
                    _gameManager.Player.Jump();
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                _gameManager.Player.Move(0); // Останавливаем движение при отпускании клавиш
            }
        }
    }
}
