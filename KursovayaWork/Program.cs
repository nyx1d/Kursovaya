using System;
using System.Collections.Generic;
using System.Linq;

namespace KursovayaWork
{
    /// <summary>
    /// Главный класс программы
    /// </summary>
    class Program
    {
        private static List<Product> products = new List<Product>();
        private static double minCalories = 0;

        /// <summary>
        /// Главная функция программы
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("   Система решения задачи оптимизации закупки продуктов");
            Console.WriteLine("   Вариант 6: План закупки продуктов для похода");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine();

            bool exit = false;
            while (!exit)
            {
                ShowMainMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        FormDataMenu();
                        break;
                    case "2":
                        DisplayData();
                        break;
                    case "3":
                        SolveProblemMenu();
                        break;
                    case "4":
                        SaveDataMenu();
                        break;
                    case "5":
                        LoadDataMenu();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Отображение главного меню
        /// </summary>
        static void ShowMainMenu()
        {
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                        ГЛАВНОЕ МЕНЮ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("1. Формирование исходных данных");
            Console.WriteLine("2. Вывод исходных данных");
            Console.WriteLine("3. Решение задачи");
            Console.WriteLine("4. Сохранение исходных данных");
            Console.WriteLine("5. Восстановление исходных данных");
            Console.WriteLine("0. Выход");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.Write("Выберите пункт меню: ");
        }

        /// <summary>
        /// Меню формирования исходных данных
        /// </summary>
        static void FormDataMenu()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("           ФОРМИРОВАНИЕ ИСХОДНЫХ ДАННЫХ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("1. Ручной ввод данных");
            Console.WriteLine("2. Генерация случайных данных");
            Console.WriteLine("0. Назад");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManualInput();
                    break;
                case "2":
                    RandomGeneration();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        /// <summary>
        /// Ручной ввод данных
        /// </summary>
        static void ManualInput()
        {
            products.Clear();

            Console.WriteLine("\n--- Ручной ввод данных ---");
            Console.Write("Введите минимальную требуемую калорийность (ккал): ");
            if (!double.TryParse(Console.ReadLine(), out minCalories) || minCalories <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число.");
                return;
            }

            Console.Write("Введите количество видов продуктов: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число.");
                return;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Продукт {i + 1} ---");
                Console.Write("Вес одной упаковки (кг): ");
                if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
                {
                    Console.WriteLine("Ошибка: введите положительное число.");
                    i--;
                    continue;
                }

                Console.Write("Калорийность одной упаковки (ккал): ");
                if (!double.TryParse(Console.ReadLine(), out double calories) || calories <= 0)
                {
                    Console.WriteLine("Ошибка: введите положительное число.");
                    i--;
                    continue;
                }

                Console.Write("Максимальное количество упаковок: ");
                if (!int.TryParse(Console.ReadLine(), out int maxQty) || maxQty <= 0)
                {
                    Console.WriteLine("Ошибка: введите положительное целое число.");
                    i--;
                    continue;
                }

                products.Add(new Product(weight, calories, maxQty));
            }

            Console.WriteLine("\nДанные успешно введены!");
        }

        /// <summary>
        /// Генерация случайных данных
        /// </summary>
        static void RandomGeneration()
        {
            products.Clear();
            Random random = new Random();

            Console.WriteLine("\n--- Генерация случайных данных ---");
            Console.Write("Введите минимальную требуемую калорийность (ккал): ");
            if (!double.TryParse(Console.ReadLine(), out minCalories) || minCalories <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число.");
                return;
            }

            Console.Write("Введите количество видов продуктов: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число.");
                return;
            }

            for (int i = 0; i < n; i++)
            {
                double weight = Math.Round(random.NextDouble() * 2 + 0.1, 2); // 0.1 - 2.1 кг
                double calories = Math.Round(random.NextDouble() * 500 + 100, 2); // 100 - 600 ккал
                int maxQty = random.Next(1, 11); // 1 - 10 упаковок

                products.Add(new Product(weight, calories, maxQty));
            }

            Console.WriteLine("\nСлучайные данные успешно сгенерированы!");
        }

        /// <summary>
        /// Вывод исходных данных в табличном виде
        /// </summary>
        static void DisplayData()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("\nИсходные данные не введены. Сначала сформируйте данные.");
                return;
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                              ИСХОДНЫЕ ДАННЫЕ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Минимальная требуемая калорийность: {minCalories:F2} ккал");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"№",-4} {"Вес (кг)",-12} {"Калорийность (ккал)",-22} {"Макс. кол-во",-15} {"Уд. калорийность",-18}");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                Console.WriteLine($"{i + 1,-4} {p.Weight,-12:F2} {p.Calories,-22:F2} {p.MaxQuantity,-15} {p.SpecificCalories,-18:F2}");
            }

            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
        }

        /// <summary>
        /// Меню решения задачи
        /// </summary>
        static void SolveProblemMenu()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("\nИсходные данные не введены. Сначала сформируйте данные.");
                return;
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("                    РЕШЕНИЕ ЗАДАЧИ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("1. Эвристический метод");
            Console.WriteLine("2. Полный перебор");
            Console.WriteLine("3. Оба метода (сравнение)");
            Console.WriteLine("0. Назад");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SolveHeuristic();
                    break;
                case "2":
                    SolveBruteForce();
                    break;
                case "3":
                    SolveBoth();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        /// <summary>
        /// Решение эвристическим методом
        /// </summary>
        static void SolveHeuristic()
        {
            Console.WriteLine("\n--- Эвристический метод решения ---");
            var plan = Solver.HeuristicSolve(products, minCalories, out long time);

            DisplaySolution(plan, "Эвристический метод", time);
        }

        /// <summary>
        /// Решение методом полного перебора
        /// </summary>
        static void SolveBruteForce()
        {
            Console.WriteLine("\n--- Метод полного перебора ---");
            Console.WriteLine("Вычисление... Это может занять некоторое время...");
            var plan = Solver.BruteForceSolve(products, minCalories, out long time);

            DisplaySolution(plan, "Полный перебор", time);
        }

        /// <summary>
        /// Решение обоими методами с сравнением
        /// </summary>
        static void SolveBoth()
        {
            Console.WriteLine("\n--- Сравнение методов решения ---\n");

            Console.WriteLine("1. Эвристический метод:");
            var heuristicPlan = Solver.HeuristicSolve(products, minCalories, out long heuristicTime);
            DisplaySolution(heuristicPlan, "Эвристический метод", heuristicTime);

            Console.WriteLine("\n2. Полный перебор:");
            Console.WriteLine("Вычисление... Это может занять некоторое время...");
            var bruteForcePlan = Solver.BruteForceSolve(products, minCalories, out long bruteForceTime);
            DisplaySolution(bruteForcePlan, "Полный перебор", bruteForceTime);

            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("                    СРАВНЕНИЕ РЕЗУЛЬТАТОВ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine($"Эвристический метод:");
            Console.WriteLine($"  Вес: {heuristicPlan.TotalWeight:F2} кг");
            Console.WriteLine($"  Калорийность: {heuristicPlan.TotalCalories:F2} ккал");
            Console.WriteLine($"  Время: {heuristicTime} мс");
            Console.WriteLine($"Полный перебор:");
            Console.WriteLine($"  Вес: {bruteForcePlan.TotalWeight:F2} кг");
            Console.WriteLine($"  Калорийность: {bruteForcePlan.TotalCalories:F2} ккал");
            Console.WriteLine($"  Время: {bruteForceTime} мс");
            Console.WriteLine($"Разница в весе: {Math.Abs(heuristicPlan.TotalWeight - bruteForcePlan.TotalWeight):F2} кг");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
        }

        /// <summary>
        /// Вывод результатов решения
        /// </summary>
        static void DisplaySolution(PurchasePlan plan, string methodName, long executionTime)
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"                    РЕЗУЛЬТАТ РЕШЕНИЯ: {methodName.ToUpper()}");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");

            Console.WriteLine($"\nПлан закупки продуктов:");
            Console.WriteLine($"{"№",-4} {"Вес (кг)",-12} {"Калорийность (ккал)",-22} {"Количество",-15} {"Общий вес",-15} {"Общая калорийность",-20}");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");

            for (int i = 0; i < products.Count; i++)
            {
                if (plan.Quantities[i] > 0)
                {
                    var p = products[i];
                    double totalWeight = p.Weight * plan.Quantities[i];
                    double totalCalories = p.Calories * plan.Quantities[i];
                    Console.WriteLine($"{i + 1,-4} {p.Weight,-12:F2} {p.Calories,-22:F2} {plan.Quantities[i],-15} {totalWeight,-15:F2} {totalCalories,-20:F2}");
                }
            }

            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Итого:");
            Console.WriteLine($"  Общий вес: {plan.TotalWeight:F2} кг");
            Console.WriteLine($"  Общая калорийность: {plan.TotalCalories:F2} ккал");
            Console.WriteLine($"  Требуемая калорийность: {minCalories:F2} ккал");
            Console.WriteLine($"  Выполнено требование: {(plan.TotalCalories >= minCalories ? "ДА" : "НЕТ")}");
            Console.WriteLine($"  Время выполнения алгоритма: {executionTime} мс");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
        }

        /// <summary>
        /// Меню сохранения данных
        /// </summary>
        static void SaveDataMenu()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("\nИсходные данные не введены. Сначала сформируйте данные.");
                return;
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("              СОХРАНЕНИЕ ИСХОДНЫХ ДАННЫХ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("1. Сохранить в текстовый файл");
            Console.WriteLine("2. Сохранить в двоичный файл");
            Console.WriteLine("0. Назад");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.Write("Выберите пункт меню: ");

            string? choice = Console.ReadLine();
            Console.Write("Введите имя файла: ");
            string? filename = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filename) || choice == null)
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                return;
            }

            switch (choice)
            {
                case "1":
                    if (!filename.EndsWith(".txt"))
                        filename += ".txt";
                    DataManager.SaveToTextFile(products, minCalories, filename);
                    break;
                case "2":
                    if (!filename.EndsWith(".dat"))
                        filename += ".dat";
                    DataManager.SaveToBinaryFile(products, minCalories, filename);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        /// <summary>
        /// Меню загрузки данных
        /// </summary>
        static void LoadDataMenu()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("           ВОССТАНОВЛЕНИЕ ИСХОДНЫХ ДАННЫХ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("1. Загрузить из текстового файла");
            Console.WriteLine("2. Загрузить из двоичного файла");
            Console.WriteLine("0. Назад");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();
            Console.Write("Введите имя файла: ");
            string? filename = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filename))
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                return;
            }

            bool success = false;
            switch (choice)
            {
                case "1":
                    if (!filename.EndsWith(".txt"))
                        filename += ".txt";
                    success = DataManager.LoadFromTextFile(filename, out products, out minCalories);
                    break;
                case "2":
                    if (!filename.EndsWith(".dat"))
                        filename += ".dat";
                    success = DataManager.LoadFromBinaryFile(filename, out products, out minCalories);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            if (success)
            {
                Console.WriteLine("Данные успешно загружены!");
            }
        }
    }
}

