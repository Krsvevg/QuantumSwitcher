﻿using System;
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

            this.ClientSize = new Size(800, 600);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);

            // Вот тут добавляем темно-синий фон:
            this.BackColor = Color.FromArgb(15, 20, 35);

            try
            {
                this.Text = $"Уровень {levelNumber}";
                this.ClientSize = new Size(800, 600);
                _currentLevel = levelNumber;
                _gameManager = new GameManager(this, levelNumber);
                _gameManager.LevelCompleted += OnLevelCompleted;
                _gameManager.LevelFailed += OnLevelFailed;
                _gameTimer = new Timer { Interval = 16 };
                _gameTimer.Tick += (s, e) => _gameManager.Update();
                _gameTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска уровня: {ex.Message}");
                this.Close();
            }
        }

        private void OnLevelFailed()
        {
            _gameTimer.Stop();

            using (var defeatDialog = new DefeatDialog())
            {
                var result = defeatDialog.ShowDialog();

                if (result == DialogResult.Retry)
                {
                    // Перезапуск уровня
                    _gameManager.ResetLevel();
                    _gameTimer.Start();
                }
                else if (result == DialogResult.Abort)
                {
                    // Возврат в главное меню
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                {
                    // По умолчанию просто закрыть игру
                    Close();
                }
            }
        }

        private void OnLevelCompleted()
        {
            _gameTimer.Stop();

            using (var dialog = new LevelCompletedDialog(_currentLevel))
            {
                dialog.ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
                    _gameManager.Player.Move(-2);
                    break;
                case Keys.Right:
                    _gameManager.Player.Move(2);
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
                _gameManager.Player.Move(0);
            }
        }
    }
}
