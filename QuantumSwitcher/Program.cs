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
            while (true)
            {
                // Показываем главное меню
                using (var menu = new MainMenuForm())
                {
                    if (menu.ShowDialog() != DialogResult.OK)
                        break; // Выход из игры

                    // Запускаем уровень
                    using (var gameForm = new MainForm(menu.SelectedLevel))
                    {
                        var result = gameForm.ShowDialog();

                        // Проверяем, не был ли выбран возврат в меню
                        if (result == DialogResult.Cancel)
                            continue; // Возврат в главное меню

                        // Выход из игры
                        if (result != DialogResult.OK)
                            break;
                    }
                }
            }
        }
    }
}
