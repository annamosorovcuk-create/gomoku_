using System;

namespace GomokuGame
{
    class Program
    {
        private const int BOARD_SIZE = 15; // Размер поля.

        enum CellState // Состояние клетки на поле.
        {
            Empty, // Пустая клетка.
            X, // Игрок 1.
            O // Игрок 2.
        }
        private static CellState[,] board = new CellState[BOARD_SIZE, BOARD_SIZE]; // Размер поля 15 на 15.

        private static CellState currentPlayer = CellState.X; // Игрок с X ходит 1-ым.
        
        private static int moveCount = 0;  // Счетчик ходов.

        // Имена игроков.
        private static string playerXName = "Игрок 1 (X)";

        private static string playerOName = "Игрок 2 (O)";

        // Цвета.
        private static ConsoleColor defaultColor = ConsoleColor.White;
        private static ConsoleColor playerXColor = ConsoleColor.Red;
        private static ConsoleColor playerOColor = ConsoleColor.Blue;
        private static ConsoleColor menuColor = ConsoleColor.Yellow;
        private static ConsoleColor infoColor = ConsoleColor.Cyan;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false; // Скрывает мигающий курсор в консоли.

            int menuIndex = 0; // Хранит номер выбранного пункта меню.
            string[] menuItems = { " Начать игру", "Ввести имена игрoков", "Правила", "Выход" };
            bool menuRunning = true;
            
            while (menuRunning)
            {
                Console.Clear(); // Очиста консоли.
                Console.ForegroundColor = menuColor; // Установка цвета.
                Console.WriteLine("=================================");
                Console.WriteLine(" ГОМОКУ (5 В РЯД)");
                Console.WriteLine("=================================");
                Console.ResetColor(); // Сброс цвета.

                Console.WriteLine("Главное меню:");

                for (int i = 0; i < menuItems.Length; i++) // menuItems.Length = 4.
                {
                    if (i == menuIndex) // Проверяем, является ли текущий путь выбранным.
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Меняет цвет текста.
                        Console.WriteLine(">" + menuItems[i]); // Выводится пункт со стрелочкой. [i] - доступ к элементу массива.
                        Console.ResetColor(); // Вернет обычный цвет.
                    } 
                    else
                    {
                        Console.WriteLine(menuItems[i]);
                    }
                }
                Console.WriteLine("Используйте стрелки ↑↓ для выбора, Enter для подтверждения");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Программа ждет действий пользователя.

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuIndex = (menuIndex - 1 + menuItems.Length) % menuItems.Length;  // Перемещение выбора на 1 пункт ВВЕРХ. % menuItems.Length - зацикливание (если ушли за 0, переходим в конец)
                        break;

                    case ConsoleKey.DownArrow:
                        menuIndex = (menuIndex + 1) % menuItems.Length; // Стрелка ВНИЗ.
                        break;

                    case ConsoleKey.Enter:
                        ExecuteMenuItem (menuIndex, ref menuRunning); // Вызов метода. ref menuRunning -  ссылка на переменную, управляющую циклом меню.
                        break;
                    case ConsoleKey.Escape: // завершение цикла. Выход из меню.
                        menuRunning = false;
                        break;
                }
            }
        }
        static void ExecuteMenuItem(int index, ref bool menuRunning)
        {
            Console.Clear();

            switch (index)
            {
                case 0: // Начать игру.
                    Console.WriteLine("Начать игру..");
                    Console.WriteLine("Идет запуск.....");
                    Console.ReadKey(); // Остановка программы. Программа ждет нажатия любой клавиши пользователем.
                    StartNewGame (ref menuRunning); // Запускает игровой цикл.
                    break;

                case 1: // Вывод имен игроков.
                    EnterPlayerNames();
                    break;

                case 2: // Правила.
                    ShowRules();
                    break;

                case 3: // Выход.
                    Console.WriteLine("Выход из программы..");
                    Console.ReadKey ();
                    menuRunning = false;
                    Environment.Exit (0);
                    break;
            }
            if (index != 3)
            {
                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }
        }
        static void EnterPlayerNames() // Вывод имен игроков.
        {
            Console.Clear ();
            Console.ForegroundColor = infoColor;
            Console.WriteLine(" ВВОД ИМЕН ИГРОКОВ");
            Console.ResetColor();

            Console.Write("Введите имя для Игрока 1 (Х):");
            string name1 = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(name1))
                playerXName = name1;

            Console.Write("Введите имя для Игрока 2 (O):");
            string name2 = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(name2))
                playerOName = name2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Имена установлены: {playerXName} (Х) и {playerOName} (O)");

            Console.ResetColor();
        }
        static void ShowRules() // Правила игры.
        {
            Console.Clear();
            Console.ForegroundColor = infoColor;
            Console.WriteLine("ПРАВИЛА ИГРЫ ГОМОКУ");
            Console.ResetColor ();

            Console.WriteLine("Правила игры:");
            Console.WriteLine("1. Игра ведется на поле 15×15 клеток.");
            Console.WriteLine("2. Игроки по очереди ставят свои фишки на пустые клетки.");
            Console.WriteLine("3. Игрок 1 использует X (красный цвет).");
            Console.WriteLine("4. Игрок 2 использует O (синий цвет).");
            Console.WriteLine("5. Цель игры - первым построить непрерывный ряд.");
            Console.WriteLine("   из 5 своих фишек по горизонтали, вертикали.");
            Console.WriteLine("   или диагонали.");
            Console.WriteLine("6. Если все клетки заполнены, а ряд из 5 фишек.");
            Console.WriteLine("   не построен - объявляется ничья.");
            Console.WriteLine("Управление в игре:");
            Console.WriteLine("- Вводите координаты в формате: строка столбец.");
            Console.WriteLine("  (например: '8 8')");
            Console.WriteLine("- Для возврата в меню введите: меню");
            Console.WriteLine("- Для выхода из игры введите: выход");
        }
        // Начать новую игру.
        static void StartNewGame(ref bool menuRunning)
        {
            Console.CursorVisible = true;
            InitializeBoard(); // Очищение поля.

            bool gameOver = false;
            currentPlayer = CellState.X;
            bool returnToMenu = false;

            // Игровой цикл.
            while (!gameOver && !returnToMenu) // Игровой цикл выполняется, пока: 1. Игра НЕ закончена (gameOver = false) 2. И НЕ запрошен возврат в меню 
            {
                PrintBoard();

                bool validMove = MakeMove(currentPlayer, ref returnToMenu); // Запрос у текущего игрока координаты хода.

                if (validMove && !returnToMenu)
                {
                    if (CheckForWin(currentPlayer)) // Проверяет, есть ли 5 фишек подряд у текущего игрока.
                    {
                        PrintBoard(); //  Показывает поле с ПОСЛЕДНИМ победным ходом.

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n" + new string('=', 40)); // Создает строку из 40 символов '='.
                        Console.WriteLine($" ПОЗДРАВЛЯЕМ! {GetPlayerName(currentPlayer)} ПОБЕДИЛ(А)!");
                        Console.WriteLine(new string('=', 40));
                        Console.ResetColor();

                        gameOver = true;
                    }
                    else if (moveCount == BOARD_SIZE * BOARD_SIZE)
                    {
                        PrintBoard();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n" + new string('=', 40));
                        Console.WriteLine("            НИЧЬЯ!");
                        Console.WriteLine("   Все клетки поля заполнены.");
                        Console.WriteLine(new string('=', 40));
                        Console.ResetColor();

                        gameOver = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == CellState.X) ? CellState.O : CellState.X; //  оператор для смены игрока.
                    }
                }
                if (gameOver)
                {
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                    Console.ReadKey();
                    returnToMenu = true;
                }
            }
            Console.CursorVisible = false;
        }
        // Инициализация игрового поля
        static void InitializeBoard()
            { 
                for (int row = 0; row < BOARD_SIZE; row++)
                {
                    for (int col = 0; col < BOARD_SIZE; col++)
                    {
                        board[row, col] = CellState.Empty;
                    }
                }
                moveCount = 0;
            }
        static void PrintBoard() // Отображение игрового поля с цветами.
        {
            Console.Clear();

            Console.ForegroundColor = menuColor;
            Console.WriteLine("    ГОМОКУ (5 В РЯД)    ");
            Console.ResetColor();

            Console.WriteLine($"Играют: {playerXName} (X) vs {playerOName} (O)");

            // Отображение заголовка с номерами столбцов.
            Console.Write("    ");
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                Console.Write($"{col + 1,2} "); // преобразует индекс (0-14) в номер (1-15) и резервирует 2 символа.
            }
            Console.WriteLine();

            Console.Write("    ");
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                Console.Write("---"); // Рисует горизонтальную линию из тире под номерами столбцов.
            }
            Console.WriteLine();

            // Отображение строк поля с номерами строк
            for (int row = 0; row <  BOARD_SIZE; row++)
            {
                Console.Write($"{row + 1,2} |");
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    switch (board[row, col])
                    {
                        case CellState.Empty:
                            Console.Write(" · ");
                            break;
                        case CellState.X:
                            Console.ForegroundColor = playerXColor;
                            Console.Write(" X ");
                            Console.ResetColor();
                            break;
                        case CellState.O:
                            Console.ForegroundColor = playerOColor;
                            Console.Write(" O ");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = infoColor;
            Console.WriteLine($"Ход игрока: {GetPlayerName(currentPlayer)} ({GetPlayerSymbol(currentPlayer)})");
            Console.WriteLine();

            Console.ForegroundColor = defaultColor;
            Console.WriteLine("Формат ввода: строка столбец (например: '8 8')");
            Console.WriteLine("Для возврата в меню введите: меню");
            Console.WriteLine("Для выхода из игры введите: выход");
            Console.ResetColor();
        } 
        // Выполнение хода.
        static bool MakeMove (CellState player, ref bool returnToMenu)
        {
            while (true)
            {
                Console.Write($" {GetPlayerName(player)}, ваш ход: ");
                string input = Console.ReadLine()!;

                // Проверка специальных команд.
                if (input.ToLower() == "меню" || input.ToLower() == "menu")
                {
                    returnToMenu = true;
                    return false;
                }
                if (input.ToLower() == "выход" || input.ToLower() == "exit")
                {
                    Console.WriteLine("Выход из игры...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return false;
                }
                string[] coordinates = input.Split(' ', StringSplitOptions.RemoveEmptyEntries); // Деление строки на части и опция, которая удаляет пустые элементы из результата.

                if (coordinates.Length != 2) // Проверка корректности ввода.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка! Введите два числа через пробел (строка и столбец).");
                    Console.ResetColor();
                    continue;
                }
                // Пытается преобразовать две координаты в целые числа row и col.
                if (!int.TryParse(coordinates[0], out int row) ||
                        !int.TryParse(coordinates[1], out int col))
                        {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка! Введите числа, а не текст.");
                    Console.ResetColor();
                    continue;
                }
               
                // Проверка границ поля.
                if (row < 1 || row > BOARD_SIZE || col < 1 || col > BOARD_SIZE)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка! Координаты должны быть от 1 до {BOARD_SIZE}.");
                    Console.ResetColor();
                    continue;
                }
                // Корректировка индексов.
                row--;
                col--;
                // Проверка, что клетка свободна.
                if (board[row, col] != CellState.Empty)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Ошибка! Эта клетка уже занята. Выберите другую.");
                    Console.ResetColor();
                    continue;
                }
                // Выполнение хода.
                board[row, col] = player;
                moveCount++;

                return true;
            }
        }
        static bool CheckForWin(CellState player)
        {
            // Проходим по всем клеткам поля.
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for(int col = 0; col < BOARD_SIZE; col++)
                {
                    // Проверяем только клетки с фишкой текущего игрока.
                    if (board[row, col] == player) // Если в этой клетке фишка игрока.
                    {
                        // Проверяем все 4 направления.
                        if (CheckDirection(row,  col, 1, 0, player) ||  // Горизонталь.
                            CheckDirection(row, col, 0, 1, player) ||  // Вертикаль.
                            CheckDirection(row, col, 1, 1, player) || // Диагональ ↘
                            CheckDirection(row, col, 1, -1, player)) // Диагональ ↗
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        // Проверка линии в заданном направлении.
        static bool CheckDirection(int startRow, int startCol, int deltaRow, int  deltaCol, CellState player)
        {
            int count = 0;
            // Проверяем 5 позиций в заданном направлении.
            for (int i =0; i < 5; i++)
            {
                int row = startRow + i * deltaRow;
                int col = startCol + i * deltaCol;

                // Проверка границы поля.
                if (row < 0  || row >= BOARD_SIZE || col < 0 || col >= BOARD_SIZE)
                {
                    return false;
                }

                // Проверка того, что клетка содержит фишку нужного игрока.
                if (board[row, col] == player)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count == 5;
        }
        // Получение имени игрока.
        static string GetPlayerName(CellState player)
        {
            switch (player)
            {
                case CellState.X:
                    return playerXName;
                case CellState.O: 
                    return playerOName;
                default:
                    return "Неизвестный игрок";
            }
        }
        // Получение символа игрока.
        static string GetPlayerSymbol(CellState player)
        {
            switch (player)
            {
                case CellState.X:
                    return "X";
                case CellState.O:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}