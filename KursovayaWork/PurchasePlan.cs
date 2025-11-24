using System;
using System.Collections.Generic;
using System.Linq;

namespace KursovayaWork
{
    /// <summary>
    /// Класс для представления плана закупки продуктов
    /// </summary>
    public class PurchasePlan
    {
        /// <summary>
        /// Количество упаковок каждого вида продукта
        /// </summary>
        public List<int> Quantities { get; set; }

        /// <summary>
        /// Список продуктов
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Общий вес закупки (кг)
        /// </summary>
        public double TotalWeight
        {
            get
            {
                double total = 0;
                for (int i = 0; i < Products.Count && i < Quantities.Count; i++)
                {
                    total += Products[i].Weight * Quantities[i];
                }
                return total;
            }
        }

        /// <summary>
        /// Общая калорийность закупки (ккал)
        /// </summary>
        public double TotalCalories
        {
            get
            {
                double total = 0;
                for (int i = 0; i < Products.Count && i < Quantities.Count; i++)
                {
                    total += Products[i].Calories * Quantities[i];
                }
                return total;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="products">Список продуктов</param>
        public PurchasePlan(List<Product> products)
        {
            Products = products ?? new List<Product>();
            Quantities = new List<int>(new int[Products.Count]);
        }

        /// <summary>
        /// Создание копии плана
        /// </summary>
        public PurchasePlan Clone()
        {
            var plan = new PurchasePlan(Products.Select(p => p.Clone()).ToList());
            plan.Quantities = new List<int>(Quantities);
            return plan;
        }
    }
}

