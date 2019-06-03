using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tanki_3._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level CurrentLevel;
        static TextBlock lives;
        public MainWindow()
        {
            InitializeComponent();
            lives = Lives;
            StartGame();
        }

        void StartGame ()
        {
            CurrentLevel = new Level(0, GameCanvas);

            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Tick += new EventHandler(GameCycle);
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            gameTimer.Start();
        }
        private void GameCycle(object sender, EventArgs e)
        {
            if (CurrentLevel.Game)
            {
                CurrentLevel.UpdateField();
                CurrentLevel.PrintLevel();
            }
            else {
                CurrentLevel = new Level(0, GameCanvas);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentLevel.ChangeKeyState(e.Key);
        }

        static void ShowGameOverMessage(string message) {
            MessageBox.Show(message);
        }

        
    }
}
