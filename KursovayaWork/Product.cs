using System;

namespace KursovayaWork
{
    /// <summary>
    /// Класс для представления продукта
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Вес одной упаковки продукта (кг)
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Калорийность одной упаковки продукта (ккал)
        /// </summary>
        public double Calories { get; set; }

        /// <summary>
        /// Максимальное количество упаковок данного продукта
        /// </summary>
        public int MaxQuantity { get; set; }

        /// <summary>
        /// Удельная калорийность (калорийность на единицу веса)
        /// </summary>
        public double SpecificCalories => Weight > 0 ? Calories / Weight : 0;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Product()
        {
            Weight = 0;
            Calories = 0;
            MaxQuantity = 0;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="weight">Вес упаковки</param>
        /// <param name="calories">Калорийность упаковки</param>
        /// <param name="maxQuantity">Максимальное количество</param>
        public Product(double weight, double calories, int maxQuantity)
        {
            Weight = weight;
            Calories = calories;
            MaxQuantity = maxQuantity;
        }

        /// <summary>
        /// Создание копии продукта
        /// </summary>
        public Product Clone()
        {
            return new Product(Weight, Calories, MaxQuantity);
        }
    }
}

