using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Media;
using Microsoft.Win32;

namespace Canvas_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;
        public GameState gameState = new GameState();
        private int timerDelay = 1500;
        private readonly Stopwatch timer = new Stopwatch();
        private readonly DateTime dateTime = DateTime.Now;

        private static MediaPlayer player = new MediaPlayer();



        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);

            timer.Start();
            SetTimeAsync();
            StartBackgroundMusic();

            /*
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if (openFile.ShowDialog() == true)      // This way of doing it will open a dialog box to choose the song to play
            {                                           // TODO - Select the teris theme song to always be played.
                // player.Open(new Uri(openFile.FileName));
               // player.Open(new Uri(@"C: \Users\lewis\OneDrive\Documents\Visual Studio 2022\VS Projects\Canvas WPF\Audio\Tetris"));
              //  player.Play();
            }
            */        
        }

        private static void StartBackgroundMusic()
        {
            player.Open(new Uri(@"C:\Users\lewis\OneDrive\Documents\Visual Studio 2022\VS Projects\Canvas WPF\Audio\Tetris.mp3"));
            player.MediaEnded += new EventHandler(BackgroundMusic);
            player.Play();
        }

        private static void BackgroundMusic(object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;
            player.Play();
            
        }

        private async void SetTimeAsync()
        {
            await SetTimer();
        }

        private async Task SetTimer()
        {
            int levelUp = 60;
            DateTime gameTimer;
            TimeSpan stopWatchSpan;

            while (true)
            {
                await Task.Delay(100);

                gameTimer = DateTime.Now;
                stopWatchSpan = dateTime - gameTimer;
                TimerText.Text = $"{Math.Abs(stopWatchSpan.Minutes),-1:00}:{Math.Abs(stopWatchSpan.Seconds),2:00}";
               
                if (Math.Abs(stopWatchSpan.TotalSeconds) > levelUp && timerDelay > 500)
                {
                    timerDelay -= 200;
                    levelUp += 60;
                }
            }

        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePosition())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image()
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;

                }
            }
            
            return imageControls;
        }

        public void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            
            ScoreText.Text = gameState.Score.ToString();
            int nextID = gameState.BlockQueue.NextBlock.Id;
            NextBlock.Source = blockImages[nextID];

            if (gameState.GameOver)
            {
                scoreFinal.Text = gameState.Score.ToString();
                GameOverMenu.Visibility = Visibility.Visible;
                // TODO - Create a method for saving scores to a text file.
            }
        }
        public async Task AwaitDraw(GameState gameState)
        {
            while (true)
            {
                await Task.Delay(timerDelay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }         
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {         
            await AwaitDraw(gameState);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {      
                if (e.Key == Key.Down)
                {
                    gameState.MoveBlockDown();
                    Draw(gameState);
                }
                if (e.Key == Key.Left)
                {
                    gameState.MoveBlockLeft();
                    Draw(gameState);
                }
                if (e.Key == Key.Right)
                {
                    gameState.MoveBlockRight();
                    Draw(gameState);

                }
                if (e.Key == Key.Up)
                {
                    gameState.RotateBlockCW();
                    Draw(gameState);
                }
        }
    }
}
