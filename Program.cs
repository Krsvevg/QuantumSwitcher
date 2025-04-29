using System;
using System.Windows.Forms;

namespace QuantumSwitcher
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Показываем главное меню
            using (var menu = new MainMenuForm())
            {
                if (menu.ShowDialog() == DialogResult.OK)
                {
                    // Запускаем игру с выбранным уровнем
                    Application.Run(new MainForm(menu.SelectedLevel));
                }
            }
        }
    }
}