using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SeaBattle_game.GameForm;

namespace SeaBattle_game
{
    public partial class GameForm : Form

    {
        private void GameForm_Load(object sender, EventArgs e)
        {

        }
        // Добавляем статистику игр
        private static int cnt1 = 0;
        private static int cnt2 = 0;
        private Game game;
        private IPlayer player1;
        private IPlayer player2;
        private int currentPlayer;
        public bool[,] lastPlants = new bool[10, 10];
        public bool[,] lastPlants2 = new bool[10, 10];
        private bool gamePrc = false;
        private bool isBotVsBot;
        private bool isPlayerVsBot;
        private int currentUserState=0;
        private bool isPlayerVsPlayer;
        private System.Windows.Forms.Timer gameTimer;
        private Button[,] buttons1;
        private Button[,] buttons2;
        private int currentGame;
        private void InitializeGame()
        {
            game = new Game();
            if (isPlayerVsBot)
            {
                player1 = new PlayerReal(textBox_p1name.Text);
            }
            if (isPlayerVsPlayer)
            {
                player1 = new PlayerReal(textBox_p1name.Text);
                player2 = new PlayerReal(textBox_p2name.Text);
                button_switchUser.Visible = true;
            }
            player1 = new SmartBot(textBox_p1name.Text);
            player2 = new SmartBot(textBox_p2name.Text);

            // Инициализация таймера для режима bot vs bot
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // 500ms между ходами
            gameTimer.Tick += GameTimer_Tick;

            // Создание игровых полей
            CreateGameFields();

            // Инициализация счета
            label_score = new Label
            {
                Text = "Счёт: 0 - 0",
                Location = new Point(400, 460),
                Size = new Size(100, 30)
            };
            this.Controls.Add(label_score);

            // Инициализация индикатора текущего хода
            label_turn = new Label
            {
                Text = "Ход: матч не начат",
                Location = new Point(400, 430),
                Size = new Size(150, 30)
            };
            this.Controls.Add(label_turn);

            //RestartGame();
        }

        private void CreateGameFields()
        {
            buttons1 = new Button[10, 10];
            buttons2 = new Button[10, 10];

            // Создание кнопок для первого поля (левое поле)
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    buttons1[i, j] = CreateButton(i, j, 50);  // левое поле
                    buttons2[i, j] = CreateButton(i, j, 450); // правое поле (увеличен отступ)
                }
            }
        }
        private void UpdateGameField()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    char state1 = game.GetCellState(0, i, j);
                    char state2 = game.GetCellState(1, i, j);

                    // Всегда показываем корабли в режиме botVsbot
                    if (isBotVsBot)
                    {
                        if (player1.Report(i, j))
                        {
                            buttons1[j, i].BackColor = state1 == 'X' ? Color.Red : Color.Green;
                            
                        }
                        else if (state1 == '-')
                        {
                            buttons1[j, i].BackColor = Color.Blue;
                        }
                        else
                        {
                            buttons1[j, i].BackColor = Color.Gray;
                        }
                        if (player2.Report(i, j))
                        {
                            buttons2[j, i].BackColor = state2 == 'X' ? Color.Red : Color.Green;
                            //if (state2 == 'X')
                            //{
                            //    buttons2[j, i].BackColor = Color.Red;
                            //}
                            //else if (isBotVsBot)
                            //{
                            //    buttons2[j, i].BackColor = Color.Green;
                            //}
                        }
                        else if (state2 == '-')
                        {
                            buttons2[j, i].BackColor = Color.Blue;
                        }
                        else
                        {
                            buttons2[j, i].BackColor = Color.Gray;
                        }
                        
                    }
                    else if(isPlayerVsBot)
                    {
                        if (player1.Report(i, j))
                        {
                            state1 = state1 == 'X' ? 'X' : 'S';

                        }
                        UpdateButton(buttons1[i, j], state1);
                        UpdateButton(buttons2[i, j], state2);
                    }
                    else
                    {
                        UpdateButton(buttons1[i, j], state1);
                        UpdateButton(buttons2[i, j], state2);
                    }
                    
                }
               this.Refresh();
            }
            
            UpdateScoreLabel();
            
            this.Refresh();
        }
        private void ClearPlane()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    
                    
                    UpdateButton(buttons1[i, j], 'q');
                    UpdateButton(buttons2[i, j], 'q');
                    

                }
                this.Refresh();
            }

            this.Refresh();
        }

        private void UpdateButton(Button button, char state)
        {
            
            switch (state)
            {
                case 'X': // Попадание
                    button.BackColor = Color.Red;
                    break;
                case '-': // Промах
                    button.BackColor = Color.Blue;
                    break;
                case 'S': // Корабль (только для режима botVsbot)
                    button.BackColor = Color.Green;
                    break;
                default:
                    button.BackColor = SystemColors.Control;
                    break;
            }
        }

        private void UpdateScoreLabel()
        {
            label_score.Text = $"Счёт: {cnt1} - {cnt2}";
        }

        private void UpdateTurnLabel()
        {
            label_turn.Text = $"Ход: {(currentPlayer == 0 ? player1.GetName() : player2.GetName())}";
        }
        private Button CreateButton(int x, int y, int offsetX)
        {
            Button button = new Button
            {
                Location = new Point(offsetX + x * 35, 50 + y * 35), // Увеличен интервал между кнопками
                Size = new Size(30, 30),
                Tag = new Point(x, y),
                Margin = new Padding(1)
            };
            button.Click += Button_Click;
            button.MouseDown += Button_MouseDown;   
            this.Controls.Add(button);
            return button;
        }
        private void CheckGameEnd()
        {
            int hits1 = CountHits(0);
            int hits2 = CountHits(1);
            gameTimer.Stop();
            if (hits1 >= 20 || hits2 >= 20)
            {
                if (hits1 >= 20)
                {
                    cnt1++;
                    if (!isBotVsBot)
                        MessageBox.Show($"{player2.GetName()} победил!", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cnt2++;
                    if (!isBotVsBot)
                        MessageBox.Show($"{player1.GetName()} победил!", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                gamePrc = false;
                if (isBotVsBot)
                {
                    currentGame++;
                    if (currentGame >= Convert.ToInt32(textBox_countR.Text))
                    {
                        gameTimer.Stop();
                        string winner = "";
                        if (cnt1 > cnt2)
                        {
                            winner = "Победил " + player1.GetName();
                        }
                        else if (cnt2 > cnt1)
                        {
                            winner = "Победил " + player2.GetName();
                        }
                        else
                        {
                            winner = "Ничья!";
                        }
                        MessageBox.Show($"Финальный счёт: {cnt1} - {cnt2}. Итог: {winner}", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        gamePrc = false;
                    }
                    else
                    {
                        game.Reset();
                        player1.Init();
                        player2.Init();
                        gamePrc = true;
                        if (!game.ValidateShipPlacement(0, player1))
                        {
                            cnt2++;
                        }

                        if (!game.ValidateShipPlacement(1, player2))
                        {
                            cnt2++;
                        }
                        UpdateGameField();

                    }
                }
                else
                {

                    //RestartGame();
                }
                ClearPlane();
            }
            else
            {
                gameTimer.Start();
            }
            //gameTimer.Start();
        }

        private int CountHits(int field)
        {
            int hits = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (game.GetCellState(field, i, j) == 'X')
                        hits++;
            return hits;
        }
        private void editSqr(int currentPlayer, int x, int y, char st)
        {
            char state = game.GetCellState(currentPlayer, y, x);
            state = st;
            if (currentPlayer == 1)
            {
                if (player1.Report(x, y))
                {
                    buttons1[y, x].BackColor = state == 'X' ? Color.Red : Color.Green;
                    if (state == 'X')
                    {
                        buttons1[y, x].BackColor = Color.Red;
                    }

                }
                else if (state == '-')
                {
                    buttons1[y, x].BackColor = Color.Blue;
                }
                else
                {
                    buttons1[y, x].BackColor = Color.Gray;
                }
            }
            else
            {
                if (player2.Report(x, y))
                {
                    buttons2[y, x].BackColor = state == 'X' ? Color.Red : Color.Green;
                }
                else if (state == '-')
                {
                    buttons2[y, x].BackColor = Color.Blue;
                }
                else
                {
                    buttons2[y, x].BackColor = Color.Gray;
                }
            }
        }
        private void GameProcessor()
        {
            UpdateTurnLabel();
            while (gamePrc)
            {
                if (isBotVsBot)
                {
                    gameTimer.Stop();
                    var (x, y) = currentPlayer == 0 ? player1.Shot() : player2.Shot();
                    bool hit = currentPlayer == 0 ? player2.Report(x, y) : player1.Report(x, y);

                    game.SetCellState(currentPlayer, x, y, hit ? 'X' : '-');

                    if (currentPlayer == 0)
                        player1.Check(x, y, hit);
                    else
                        player2.Check(x, y, hit);
                    editSqr(currentPlayer, x, y, hit ? 'X' : '-');
                    //UpdateGameField();
                    this.Refresh();
                    //Thread.Sleep(500);
                    if (!hit)
                    {
                        currentPlayer = 1 - currentPlayer;
                        UpdateTurnLabel();
                    }

                    CheckGameEnd();
                    gameTimer.Start();
                }
            }
        } 
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameTimer.Stop();
            if(currentPlayer == 0)
            {

                cnt2++;
                if ((!isBotVsBot))
                {


                    gamePrc = false;

                    MessageBox.Show($"{player2.GetName()} победил!", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                cnt1++;
                if ((!isBotVsBot))
                {
                    gamePrc = false;
                    MessageBox.Show($"{player1.GetName()} победил!", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            /*if (isBotVsBot)
            {
                var (x, y) = currentPlayer == 0 ? player1.Shot() : player2.Shot();
                bool hit = currentPlayer == 0 ? player2.Report(x, y) : player1.Report(x, y);

                game.SetCellState(currentPlayer, x, y, hit ? 'X' : '-');

                if (currentPlayer == 0)
                    player1.Check(x, y, hit);
                else
                    player2.Check(x, y, hit);

                UpdateGameField();

                if (!hit)
                {
                    currentPlayer = 1 - currentPlayer;
                    UpdateTurnLabel();
                }

                CheckGameEnd();
            }*/

        }
        

        // Добавляем класс случайного игрока (бота)
        

        // Добавляем метод для перезапуска игры
        public void RestartGame()
        {
            //InitializeGame()
            gameTimer.Stop();
            player1 = new SmartBot(textBox_p1name.Text);
            if (isPlayerVsBot||isPlayerVsPlayer)
            {
                player1 = new PlayerReal(textBox_p1name.Text);
            }
            gameTimer.Interval = Convert.ToInt32(textBox_wait.Text)*1000;
            player2 = new SmartBot(textBox_p2name.Text);
            if (isPlayerVsPlayer)
            {
                player2 = new PlayerReal(textBox_p2name.Text);
            }
            if (isPlayerVsBot)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (lastPlants[j,i])
                        {
                            player1.ShipEdit(j, i);
                        }
                        
                    }
                }
            }
            if (isPlayerVsPlayer)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (lastPlants[j, i])
                        {
                            player1.ShipEdit(j, i);
                        }

                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (lastPlants2[j, i])
                        {
                            player2.ShipEdit(j, i);
                        }

                    }
                }
            }
            game.Reset();
            currentGame = 0;
            cnt1 = 0;
            cnt2 = 0;

            player1.Init();
            player2.Init();

            // Проверка корректности расстановки
            if (!game.ValidateShipPlacement(0, player1))
            {
                MessageBox.Show($"Некорректная расстановка игрока {player1.GetName()}! Перезапуск...", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //RestartGame();
                return;
            }

            if (!game.ValidateShipPlacement(1, player2))
            {
                MessageBox.Show($"Некорректная расстановка игрока {player2.GetName()}! Перезапуск...", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //RestartGame();
                return;
            }
            
            UpdateGameField();
            gamePrc = true;
            lastPlants = new bool[10, 10];
            lastPlants2 = new bool[10, 10];
            gameTimer.Start();
            if (isBotVsBot)
            {
                GameProcessor();
            }

        }

        // Добавляем конструктор формы с дополнительными элементами управления
        public GameForm()
        {
            InitializeComponent();
            this.Size = new Size(850, 550); // Увеличим размер формы
            // Добавляем кнопку перезапуска
            Button restartButton = new Button
            {
                Text = "Перезапуск",
                Location = new Point(50, 450), // Измените положение
                Size = new Size(100, 30)
            };

            
            
            restartButton.Click += (s, e) => RestartGame();
            this.Controls.Add(restartButton);

            // Добавляем выбор режима игры
            ComboBox gameMode = new ComboBox
            {
                Location = new Point(200, 450),
                Size = new Size(150, 30)
            };
            gameMode.Items.AddRange(new string[] { "Bot vs Bot", "Player vs Bot", "Player vs Player" });
            gameMode.SelectedIndex = 0;
            gameMode.SelectedIndexChanged += (s, e) =>
            {
                isBotVsBot = gameMode.SelectedIndex == 0;
                isPlayerVsBot = gameMode.SelectedIndex == 1;
                isPlayerVsPlayer = gameMode.SelectedIndex == 2;
                RestartGame();
            };
            this.Controls.Add(gameMode);

            InitializeGame();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currentUserState == 0)
                {
                    Button clickButton = (Button)sender;
                    Point Posp = (Point)clickButton.Tag;
                    lastPlants[Posp.X, Posp.Y] = false;
                    player1.ShipEdit(Posp.X, Posp.Y);
                    clickButton.BackColor = SystemColors.Control;
                }
                else
                {
                    Button clickButton = (Button)sender;
                    Point Posp = (Point)clickButton.Tag;
                    lastPlants2[Posp.X, Posp.Y] = false;
                    player2.ShipEdit(Posp.X, Posp.Y);
                    clickButton.BackColor = SystemColors.Control;
                }
            }
        }
        // Улучшаем обработку кликов по кнопкам
        private void Button_Click(object sender, EventArgs e)
        {
            if (isBotVsBot) return;
            if (!gamePrc)
            {
                if (currentUserState == 0)
                {
                    Button clickButton = (Button)sender;
                    Point Posp = (Point)clickButton.Tag;
                    if (true)
                    {
                        lastPlants[Posp.X, Posp.Y] = true;
                        player1.ShipEdit(Posp.X, Posp.Y);
                        clickButton.BackColor = Color.Green;
                    }
                    return;
                }
                else
                {
                    Button clickButton = (Button)sender;
                    Point Posp = (Point)clickButton.Tag;
                    if (true)
                    {
                        lastPlants2[Posp.X, Posp.Y] = true;
                        player2.ShipEdit(Posp.X, Posp.Y);
                        clickButton.BackColor = Color.Green;
                    }
                    return;
                }
            }
            gameTimer.Stop();
            Button clickedButton = (Button)sender;
            Point position = (Point)clickedButton.Tag;
            if (isPlayerVsPlayer)
            {
                if (currentPlayer == 0)
                {
                    // Ход игрока p1
                    bool hit = player2.Report(position.X, position.Y);
                    game.SetCellState(1, position.X, position.Y, hit ? 'X' : '-');
                    UpdateGameField();
                    if (!hit) currentPlayer = 1;
                    UpdateTurnLabel();
                    CheckGameEnd();
                    return;


                }
                if (currentPlayer == 1)
                {
                    // Ход игрока p2
                    bool hit = player1.Report(position.X, position.Y);
                    game.SetCellState(0, position.X, position.Y, hit ? 'X' : '-');
                    UpdateGameField();
                    if (!hit) currentPlayer = 0;
                    UpdateTurnLabel();
                    CheckGameEnd();
                    return;


                }
            }
            if (isPlayerVsBot)
            {
                if (currentPlayer == 0)
                {
                    // Ход игрока p1
                    bool hit = player2.Report(position.X, position.Y);
                    game.SetCellState(1, position.X, position.Y, hit ? 'X' : '-');
                    UpdateGameField();
                    if (!hit) currentPlayer = 1;
                    UpdateTurnLabel();
                    CheckGameEnd();


                }
                
                while (currentPlayer != 0)
                {
                    gameTimer.Start();
                    if (currentPlayer == 1)
                    {
                        // Ход бота p2
                        var (x, y) = player2.Shot();
                        bool hit = player1.Report(x, y);
                        game.SetCellState(0, x, y, hit ? 'X' : '-');
                        UpdateGameField();
                        if (!hit) currentPlayer = 0;
                        UpdateTurnLabel();

                    }
                    gameTimer.Stop();
                    CheckGameEnd();
                }
            }
        }

        private void button_switchUser_Click(object sender, EventArgs e)
        {
            currentUserState= currentUserState==0 ? 1 : 0;
            label_stTurn.Text = $"Ставит игрок №{currentUserState+1}";
            ClearPlane();
        }
    }
    public class Game
    {
        private char[,,] field = new char[2, 10, 10];
        public bool ValidateShipPlacement(int playerIndex, IPlayer player) //проверка
        {
            int[] expectedShips = { 4, 3, 2, 1 }; // Количество кораблей каждого размера
            int[] foundShips = new int[5]; // Для подсчета найденных кораблей
            bool[,] visited = new bool[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!visited[i, j] && player.Report(i, j))
                    {
                        // Найден новый корабль
                        int size = GetShipSize(i, j, player, visited);
                        if (size > 4) return false; // Корабль слишком большой
                        foundShips[size]++;
                    }
                }
            }

            // Проверка количества кораблей каждого размера
            for (int i = 1; i <= 4; i++)
            {
                if (foundShips[i] != expectedShips[i - 1])
                    return false;
            }

            return true;
        }

        private int GetShipSize(int x, int y, IPlayer player, bool[,] visited) //проверка
        {
            int size = 0;
            bool isHorizontal = false;
            bool isVertical = false;

            // Определяем направление корабля
            if (x + 1 < 10 && player.Report(x + 1, y)) isHorizontal = true;
            if (y + 1 < 10 && player.Report(x, y + 1)) isVertical = true;

            if (isHorizontal && isVertical) return 999; // Некорректная форма корабля

            if (isHorizontal)
            {
                while (x < 10 && player.Report(x, y))
                {
                    visited[x, y] = true;
                    size++;
                    x++;
                }
            }
            else if (isVertical)
            {
                while (y < 10 && player.Report(x, y))
                {
                    visited[x, y] = true;
                    size++;
                    y++;
                }
            }
            else
            {
                visited[x, y] = true;
                size = 1;
            }

            return size;
        }
        public char GetCellState(int field, int x, int y)
        {
            return this.field[field, x, y];
        }

        public void SetCellState(int field, int x, int y, char state)
        {
            this.field[field, x, y] = state;
        }

        private bool CheckEnd(int pn) //не используется
        {
            int hits = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (field[pn, i, j] == 'X')
                        hits++;
            return hits >= 20; // Для победы нужно потопить все корабли (20 клеток)
        }

        public void Reset()
        {
            for (int f = 0; f < 2; f++)
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                        field[f, i, j] = '\0';
        }
    }

    // Добавляем интерфейс игрока
    public interface IPlayer
    {
        void Init();
        string GetName();
        bool Report(int x, int y);
        (int, int) Shot();
        void ShipEdit(int x, int y);
        void Check(int x, int y, bool hit);
    }
    public class PlayerRandom : IPlayer
    {
        private readonly string name;
        private bool[,] ships = new bool[10, 10]  {   { false, false, false, false, false, false, true, false, false, true },
                                                           { false, true, true, true, true, false, true, false, false, true},
                                                           { false, false, false, false, false, false, false, false, false, true},
                                                           { false, false, true, false, false, false, true, false, false, false},
                                                           { false, false, false, false, true, false, false, false, false, false},
                                                           { false, false, false, false, false, false, false, false, false, false},
                                                           { true, false, false, false, false, false, true, false, false, true},
                                                           { true, false, false, false, false, false, false, false, false, true},
                                                           { true, false, false, false, false, false, false, false, false, false},
                                                           { false, false, false, false, false, true, true, false, false, false}

                };
        private readonly Random random = new Random();

        public PlayerRandom(string name)
        {
            this.name = name;
        }
        public void ShipEdit(int x, int y)
        {
            //ИСПОЛЬЗОВАНИЕ ЗАПРЕЩЕНО!
        }
        public void Init()
        {


            // Простая случайная расстановка кораблей
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 10; j++)
            //        ships[i, j] = random.Next(100) < 20; // 20% вероятность корабля
        }

        public string GetName() => name;

        public bool Report(int x, int y)
        {
            if (x < 0 || x >= 10 || y < 0 || y >= 10) return false;
            return ships[x, y];
        }

        public (int, int) Shot()
        {
            return (random.Next(10), random.Next(10));
        }

        public void Check(int x, int y, bool hit) { }
    }
    
    public class PlayerReal : IPlayer
    {
        //GameForm gameform = new GameForm();
        
        private readonly string name;
        private bool[,] ships = new bool[10, 10];
        public bool[,] lss = new bool[10, 10];
        private readonly Random random = new Random();

        public PlayerReal(string name)
        {
            this.name = name;
        }
        public void ShipEdit(int x, int y)
        {
            ships[x, y] = true;
           // lss[x,y] = true;
        }
        public void Init()
        {
            //ships = lss;

            // Простая случайная расстановка кораблей
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 10; j++)
            //        ships[i, j] = random.Next(100) < 20; // 20% вероятность корабля
        }

        public string GetName() => name;

        public bool Report(int x, int y)
        {
            if (x < 0 || x >= 10 || y < 0 || y >= 10) return false;
            return ships[x, y];
        }

        public (int, int) Shot()
        {
            return (random.Next(10), random.Next(10));
        }

        public void Check(int x, int y, bool hit) { }
    }
    public class SmartBot : IPlayer
    {
        private readonly string name;
        private readonly bool[,] ships = new bool[10, 10];
        private readonly bool[,] shotMap = new bool[10, 10];
        private readonly List<Point> hits = new List<Point>();
        private readonly Random random = new Random();
        
        public SmartBot(string name)
        {
            this.name = name;
        }

        public string GetName() => name;

        public void Init()
        {
            // Очистка предыдущих данных
            Array.Clear(ships, 0, ships.Length);
            Array.Clear(shotMap, 0, shotMap.Length);
            hits.Clear();

            // Расстановка кораблей
            PlaceShips();
        }
        public void ShipEdit(int x, int y)
        {
            //ИСПОЛЬЗОВАНИЕ ЗАПРЕЩЕНО!
        }
        private void PlaceShips()
        {
            int maxAttempts = 1000;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Array.Clear(ships, 0, ships.Length);

                if (PlaceShip(4, 1) &&
                    PlaceShip(3, 2) &&
                    PlaceShip(2, 3) &&
                    PlaceShip(1, 4))
                {
                    return; // Успешная расстановка
                }

                attempts++;
            }

            // Если не удалось расставить корабли
            throw new Exception("Нет вариантов расстановки");
        }

        private bool PlaceShip(int size, int count)
        {
            for (int i = 0; i < count; i++)
            {
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < 100)
                {
                    int x = random.Next(10);
                    int y = random.Next(10);
                    bool horizontal = random.Next(2) == 0;

                    if (CanPlaceShip(x, y, size, horizontal))
                    {
                        PlaceShipOnField(x, y, size, horizontal);
                        placed = true;
                    }
                    attempts++;
                }

                if (!placed) return false;
            }
            return true;
        }

        private bool CanPlaceShip(int x, int y, int size, bool horizontal)
        {
            // Проверка границ
            if (horizontal && x + size > 10) return false;
            if (!horizontal && y + size > 10) return false;

            // Проверка окружения
            for (int i = Math.Max(0, x - 1); i <= Math.Min(9, x + (horizontal ? size : 1)); i++)
                for (int j = Math.Max(0, y - 1); j <= Math.Min(9, y + (horizontal ? 1 : size)); j++)
                    if (ships[i, j]) return false;

            return true;
        }

        private void PlaceShipOnField(int x, int y, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                if (horizontal)
                    ships[x + i, y] = true;
                else
                    ships[x, y + i] = true;
            }
        }

        public bool Report(int x, int y)
        {
            if (x < 0 || x >= 10 || y < 0 || y >= 10) return false;
            return ships[x, y];
        }

        public (int, int) Shot()
        {
            // Если есть попадания, стреляем вокруг них
            if (hits.Count > 0)
            {
                foreach (var hit in hits.ToList()) // Используем ToList() для избежания изменения коллекции при переборе
                {
                    int[] dx = { -1, 1, 0, 0 };
                    int[] dy = { 0, 0, -1, 1 };

                    for (int i = 0; i < 4; i++)
                    {
                        int newX = hit.X + dx[i];
                        int newY = hit.Y + dy[i];

                        if (IsValidCell(newX, newY) && !shotMap[newX, newY])
                        {
                            shotMap[newX, newY] = true;
                            return (newX, newY);
                        }
                    }
                    // Если все соседние клетки проверены, удаляем это попадание из списка
                    hits.Remove(hit);
                }
            }

            // Если нет подсказок, стреляем по диагонали
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if ((i + j) % 2 == 0 && !shotMap[i, j])
                    {
                        shotMap[i, j] = true;
                        return (i, j);
                    }

            // Если все диагональные клетки проверены, стреляем случайно
            int x, y;
            do
            {
                x = random.Next(10);
                y = random.Next(10);
            } while (shotMap[x, y]);

            shotMap[x, y] = true;
            return (x, y);
        }

        public void Check(int x, int y, bool hit)
        {
            if (hit)
                hits.Add(new Point(x, y));
        }

        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && x < 10 && y >= 0 && y < 10;
        }
    }
}


