﻿using Beadando1.BELÉPÉS;
using Beadando1.MUSIC;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Beadando1.Game1;

namespace Beadando1
{
    /// <summary>
    /// Interaction logic for GameSelect.xaml
    /// </summary>
    public partial class GameSelect : Window
    {
        public MainWindow _mainWindow;
        public GameSelect(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            RegisterButton.Content = UserSession.Username;

            this.Left = _mainWindow.Left;
            this.Top = _mainWindow.Top;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

           
            if (MusicState.isMuted)
            {
                ToggleMuteButton.Content = "🔇";  
            }
            else
            {
                ToggleMuteButton.Content = "🔊";  
            }
        }

      
        private void ToggleMuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MusicState.isMuted)
            {
                MusicState.mediaPlayer.Play();
                MusicState.isMuted = false;
                ToggleMuteButton.Content = "🔊";
                Log.Information($"Felhasználó lenémította a hangot!");
            }
            else
            {
                MusicState.mediaPlayer.Pause();
                MusicState.isMuted = true;
                ToggleMuteButton.Content = "🔇";
                Log.Information($"Felhasználó feloldotta a némítást!");
            }
        }

       
        private void G1_Click(object sender, RoutedEventArgs e)
        {
            MusicState.mediaPlayer.Stop();

            // Új zene betöltése
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string newFullPath = System.IO.Path.Combine(projectRoot, "Sound", "game.mp3");

            MusicState.mediaPlayer.Open(new Uri(newFullPath, UriKind.Absolute));
            MusicState.mediaPlayer.MediaEnded += (s, e2) => MusicState.mediaPlayer.Position = TimeSpan.Zero;
            if (!MusicState.isMuted)
                MusicState.mediaPlayer.Play();

            Game1 win1 = new Game1(this);
            win1.Show();
            Log.Information($"Felhasználó megnyitotta a black jacket, és nem tudta hogy mennyit fog bukni");
            this.Close();

        }

        private void G2_Click(object sender, RoutedEventArgs e)
        {
            Game2 win2 = new Game2(this);
            win2.Show();
            Log.Information($"Felhasználü megnyitotta a félkarú rablót! Kár volt.");
            this.Close();
        }

        private void G3_Click(object sender, RoutedEventArgs e)
        {
            Game3 win3 = new Game3(this);
            win3.Show();
            Log.Information($"Felhasználó rossz döntést hozott, roulettezni kezd.");
            this.Close();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow
            {
                Left = this.Left,
                Top = this.Top,
                WindowStartupLocation = WindowStartupLocation.Manual

            };

            if (UserSession.IsLoggedIn)
                main.SetLoggedInUser(UserSession.Username);

            main.Show();
            Log.Information($"Felhasználó visszalépett.");
            this.Close();
        }
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new ProfileWindow(UserSession.Username);
            profileWindow.ShowDialog();
        }
    }
}
