using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KursovayaWork
{
    /// <summary>
    /// Класс для управления данными (сохранение и загрузка)
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// Сохранение данных в текстовый файл
        /// </summary>
        /// <param name="products">Список продуктов</param>
        /// <param name="minCalories">Минимальная требуемая калорийность</param>
        /// <param name="filename">Имя файла</param>
        public static void SaveToTextFile(List<Product> products, double minCalories, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    writer.WriteLine(minCalories.ToString());
                    writer.WriteLine(products.Count.ToString());
                    foreach (var product in products)
                    {
                        writer.WriteLine($"{product.Weight};{product.Calories};{product.MaxQuantity}");
                    }
                }
                Console.WriteLine($"Данные успешно сохранены в файл: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в текстовый файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Загрузка данных из текстового файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="products">Список продуктов (выходной параметр)</param>
        /// <param name="minCalories">Минимальная требуемая калорийность (выходной параметр)</param>
        /// <returns>Успешность загрузки</returns>
        public static bool LoadFromTextFile(string filename, out List<Product> products, out double minCalories)
        {
            products = new List<Product>();
            minCalories = 0;

            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Файл {filename} не найден.");
                    return false;
                }

                using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                {
                    if (!double.TryParse(reader.ReadLine(), out minCalories))
                    {
                        Console.WriteLine("Ошибка чтения минимальной калорийности.");
                        return false;
                    }

                    if (!int.TryParse(reader.ReadLine(), out int count))
                    {
                        Console.WriteLine("Ошибка чтения количества продуктов.");
                        return false;
                    }

                    for (int i = 0; i < count; i++)
                    {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                        string[] parts = line.Split(';');
                        if (parts.Length >= 3)
                        {
                            if (double.TryParse(parts[0], out double weight) &&
                                double.TryParse(parts[1], out double calories) &&
                                int.TryParse(parts[2], out int maxQuantity))
                            {
                                products.Add(new Product(weight, calories, maxQuantity));
                            }
                        }
                    }
                }

                Console.WriteLine($"Данные успешно загружены из файла: {filename}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке из текстового файла: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Сохранение данных в двоичный файл (типизированный)
        /// </summary>
        /// <param name="products">Список продуктов</param>
        /// <param name="minCalories">Минимальная требуемая калорийность</param>
        /// <param name="filename">Имя файла</param>
        public static void SaveToBinaryFile(List<Product> products, double minCalories, string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
                {
                    writer.Write(minCalories);
                    writer.Write(products.Count);
                    foreach (var product in products)
                    {
                        writer.Write(product.Weight);
                        writer.Write(product.Calories);
                        writer.Write(product.MaxQuantity);
                    }
                }
                Console.WriteLine($"Данные успешно сохранены в двоичный файл: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в двоичный файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Загрузка данных из двоичного файла (типизированный)
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="products">Список продуктов (выходной параметр)</param>
        /// <param name="minCalories">Минимальная требуемая калорийность (выходной параметр)</param>
        /// <returns>Успешность загрузки</returns>
        public static bool LoadFromBinaryFile(string filename, out List<Product> products, out double minCalories)
        {
            products = new List<Product>();
            minCalories = 0;

            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Файл {filename} не найден.");
                    return false;
                }

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
                {
                    minCalories = reader.ReadDouble();
                    int count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        double weight = reader.ReadDouble();
                        double calories = reader.ReadDouble();
                        int maxQuantity = reader.ReadInt32();
                        products.Add(new Product(weight, calories, maxQuantity));
                    }
                }

                Console.WriteLine($"Данные успешно загружены из двоичного файла: {filename}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке из двоичного файла: {ex.Message}");
                return false;
            }
        }
    }
}

