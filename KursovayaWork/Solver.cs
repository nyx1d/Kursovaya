using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KursovayaWork
{
    /// <summary>
    /// Класс для решения задачи оптимизации закупки продуктов
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// Эвристический метод решения (жадный алгоритм)
        /// Сортирует продукты по удельной калорийности и выбирает лучшие
        /// </summary>
        /// <param name="products">Список продуктов</param>
        /// <param name="minCalories">Минимальная требуемая калорийность</param>
        /// <param name="executionTime">Время выполнения (выходной параметр)</param>
        /// <returns>План закупки</returns>
        public static PurchasePlan HeuristicSolve(List<Product> products, double minCalories, out long executionTime)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var plan = new PurchasePlan(products);
            double currentCalories = 0;

            // Создаем список индексов продуктов, отсортированных по убыванию удельной калорийности
            var sortedIndices = Enumerable.Range(0, products.Count)
                .OrderByDescending(i => products[i].SpecificCalories)
                .ToList();

            // Жадный выбор: берем продукты с наибольшей удельной калорийностью
            foreach (int idx in sortedIndices)
            {
                var product = products[idx];
                int quantity = 0;

                // Пытаемся взять максимально возможное количество
                while (quantity < product.MaxQuantity && currentCalories < minCalories)
                {
                    quantity++;
                    currentCalories += product.Calories;
                }

                plan.Quantities[idx] = quantity;

                // Если достигли требуемой калорийности, можно остановиться
                if (currentCalories >= minCalories)
                {
                    break;
                }
            }

            // Если не достигли требуемой калорийности, пытаемся добавить еще продукты
            if (currentCalories < minCalories)
            {
                foreach (int idx in sortedIndices)
                {
                    var product = products[idx];
                    int currentQty = plan.Quantities[idx];

                    while (currentQty < product.MaxQuantity && currentCalories < minCalories)
                    {
                        currentQty++;
                        currentCalories += product.Calories;
                    }

                    plan.Quantities[idx] = currentQty;
                }
            }

            sw.Stop();
            executionTime = sw.ElapsedMilliseconds;

            return plan;
        }

        /// <summary>
        /// Метод полного перебора всех возможных комбинаций
        /// </summary>
        /// <param name="products">Список продуктов</param>
        /// <param name="minCalories">Минимальная требуемая калорийность</param>
        /// <param name="executionTime">Время выполнения (выходной параметр)</param>
        /// <returns>Оптимальный план закупки</returns>
        public static PurchasePlan BruteForceSolve(List<Product> products, double minCalories, out long executionTime)
        {
            Stopwatch sw = Stopwatch.StartNew();

            PurchasePlan? bestPlan = null;
            double bestWeight = double.MaxValue;

            // Вычисляем общее количество комбинаций
            long totalCombinations = 1;
            foreach (var product in products)
            {
                totalCombinations *= (product.MaxQuantity + 1);
                if (totalCombinations > 10000000) // Ограничение для предотвращения переполнения
                {
                    Console.WriteLine("Внимание: Слишком много комбинаций для полного перебора!");
                    Console.WriteLine("Используется ограниченный перебор.");
                    break;
                }
            }

            // Рекурсивный перебор всех комбинаций
            var currentPlan = new PurchasePlan(products);
            BruteForceRecursive(products, minCalories, 0, currentPlan, ref bestPlan, ref bestWeight);

            sw.Stop();
            executionTime = sw.ElapsedMilliseconds;

            if (bestPlan == null)
            {
                // Если не найдено решение, возвращаем пустой план
                return new PurchasePlan(products);
            }

            return bestPlan;
        }

        /// <summary>
        /// Рекурсивная функция для полного перебора
        /// </summary>
        private static void BruteForceRecursive(
            List<Product> products,
            double minCalories,
            int productIndex,
            PurchasePlan currentPlan,
            ref PurchasePlan? bestPlan,
            ref double bestWeight)
        {
            // Если обработали все продукты
            if (productIndex >= products.Count)
            {
                // Проверяем, удовлетворяет ли план требованиям
                if (currentPlan.TotalCalories >= minCalories)
                {
                    // Если это лучший план (минимальный вес)
                    if (currentPlan.TotalWeight < bestWeight)
                    {
                        bestWeight = currentPlan.TotalWeight;
                        bestPlan = currentPlan.Clone();
                    }
                }
                return;
            }

            // Перебираем все возможные количества текущего продукта
            var product = products[productIndex];
            for (int qty = 0; qty <= product.MaxQuantity; qty++)
            {
                currentPlan.Quantities[productIndex] = qty;
                BruteForceRecursive(products, minCalories, productIndex + 1, currentPlan, ref bestPlan, ref bestWeight);
            }
        }
    }
}

