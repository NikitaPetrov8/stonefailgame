using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace stonefail
{
    public partial class Form1 : Form
    {
        private List<PictureBox> stoneList;
        private Random random;
        private double gameTime;
        private double stoneSpeed;
        private double timeAccumulator;
        private PictureBox player;
        private int playerSpeed = 5;
        private bool gameOver = false;

        public Form1()
        {
            InitializeComponent();
            SetupGame();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.MouseMove += Form1_MouseMove; // Подписка на событие движения мыши
        }

        private void SetupGame()
        {
            stoneList = new List<PictureBox>();
            random = new Random();
            gameTime = 0;
            stoneSpeed = 2;
            timeAccumulator = 0;

            // Создаём персонажа-игрока.
            player = new PictureBox();
            player.Size = new Size(40, 40);
            player.BackColor = Color.Black;
            // Располагаем игрока по центру нижней части контейнера Game.
            player.Location = new Point(Game.Width / 2 - player.Width / 2, Game.Height - player.Height - 10);
            Game.Controls.Add(player);

            timer1.Interval = 20;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (gameOver)
                return;

            stoneSpeed += 0.001;

            if (random.NextDouble() < 0.05)
                CreateNewStone();

            for (int i = stoneList.Count - 1; i >= 0; i--)
            {
                PictureBox stone = stoneList[i];
                stone.Top += (int)stoneSpeed;

                if (stone.Bounds.IntersectsWith(player.Bounds))
                {
                    EndGame();
                    return;
                }

                if (stone.Top > Game.Height)
                {
                    Game.Controls.Remove(stone);
                    stoneList.RemoveAt(i);
                }
            }

            timeAccumulator += timer1.Interval / 1000.0;
            if (timeAccumulator >= 1)
            {
                gameTime++;
                Timegame.Text = "Time: " + gameTime.ToString() + " s";
                timeAccumulator = 0;
            }
        }

        private void EndGame()
        {
            gameOver = true;
            timer1.Stop();

            // Воспроизведение звука окончания игры.
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer("Recourses/wav1.wav");
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения звука: " + ex.Message);
            }

            MessageBox.Show("Game Over!\nYour time: " + gameTime + " s", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
                return;

            if (e.KeyCode == Keys.W && player.Top - playerSpeed >= 0)
                player.Top -= playerSpeed;
            else if (e.KeyCode == Keys.S && player.Bottom + playerSpeed <= Game.Height)
                player.Top += playerSpeed;
            else if (e.KeyCode == Keys.A && player.Left - playerSpeed >= 0)
                player.Left -= playerSpeed;
            else if (e.KeyCode == Keys.D && player.Right + playerSpeed <= Game.Width)
                player.Left += playerSpeed;
        }

        // Обработчик движения мыши использует абсолютные координаты внутри формы.
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameOver)
                return;

            // Получаем позицию курсора относительно формы
            Point mousePos = this.PointToClient(Cursor.Position);

            int newX = mousePos.X - player.Width / 2;
            int newY = mousePos.Y - player.Height / 2;

            // Ограничиваем перемещение, чтобы игрок не выходил за границы контейнера Game.
            if (newX < 0)
                newX = 0;
            else if (newX > Game.Width - player.Width)
                newX = Game.Width - player.Width;

            if (newY < 0)
                newY = 0;
            else if (newY > Game.Height - player.Height)
                newY = Game.Height - player.Height;

            player.Location = new Point(newX, newY);
        }

        private void CreateNewStone()
        {
            Image stoneImage;
            try
            {
                stoneImage = Image.FromFile("Recourses/stone.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения камня: " + ex.Message);
                return;
            }

            PictureBox stone = new PictureBox();
            int size = random.Next(20, 61);
            stone.Size = new Size(size, size);
            stone.Image = stoneImage;
            stone.SizeMode = PictureBoxSizeMode.StretchImage;
            stone.BackColor = Color.Transparent;

            // Определяем позицию, чтобы камень не пересекался с уже созданными камнями.
            int maxAttempts = 20;
            int attempts = 0;
            int xPosition;
            Rectangle newStoneRect;

            do
            {
                xPosition = random.Next(0, Game.Width - stone.Width);
                newStoneRect = new Rectangle(new Point(xPosition, -stone.Height), stone.Size);
                attempts++;
            }
            while (attempts < maxAttempts && IsIntersectingWithExistingStones(newStoneRect));

            if (attempts >= maxAttempts && IsIntersectingWithExistingStones(newStoneRect))
            {
                // Если после нескольких попыток не удалось получить свободную позицию, не создаём камень.
                return;
            }

            stone.Location = new Point(xPosition, -stone.Height);
            Game.Controls.Add(stone);
            stoneList.Add(stone);
        }

        // Метод проверяет, пересекается ли заданный прямоугольник с уже созданными камнями.
        private bool IsIntersectingWithExistingStones(Rectangle newRect)
        {
            foreach (PictureBox existingStone in stoneList)
            {
                if (newRect.IntersectsWith(existingStone.Bounds))
                    return true;
            }
            return false;
        }
    }
}