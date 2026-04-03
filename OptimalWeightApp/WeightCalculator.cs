namespace OptimalWeightApp
{
    /// <summary>
    /// Класс бизнес-логики для вычисления оптимального веса человека.
    /// Вынесен в отдельный класс для удобства модульного тестирования.
    /// </summary>
    public static class WeightCalculator
    {
        // Константы допустимых диапазонов ввода
        public const double MinHeight = 130.0;
        public const double MaxHeight = 220.0;
        public const double MinWeight = 40.0;
        public const double MaxWeight = 170.0;

        // Коэффициент «нормы» (±3 кг)
        public const double NormTolerance = 3.0;

        /// <summary>
        /// Вычисляет оптимальный вес по формуле:
        /// Мужчины: (рост – 100) × 0.87
        /// Женщины: (рост – 100) × 0.90
        /// </summary>
        /// <param name="height">Рост в сантиметрах (130–220).</param>
        /// <param name="isMale">true — мужчина, false — женщина.</param>
        /// <returns>Оптимальный вес в килограммах.</returns>
        public static double CalculateOptimalWeight(double height, bool isMale)
        {
            // Базовое значение (рост – 100)
            double baseWeight = height - 100;

            // Поправочный коэффициент в зависимости от пола
            double coefficient = isMale ? 0.87 : 0.90;

            return baseWeight * coefficient;
        }

        /// <summary>
        /// Определяет сравнительную характеристику веса пользователя
        /// относительно оптимального значения.
        /// Норма: actualWeight в диапазоне [optimalWeight – 3; optimalWeight + 3].
        /// </summary>
        /// <param name="actualWeight">Фактический вес пользователя (кг).</param>
        /// <param name="optimalWeight">Рассчитанный оптимальный вес (кг).</param>
        /// <returns>Строка «Норма», «Ниже нормы» или «Выше нормы».</returns>
        public static string GetCharacteristic(double actualWeight, double optimalWeight)
        {
            if (actualWeight < optimalWeight - NormTolerance)
                return "Ниже нормы";

            if (actualWeight > optimalWeight + NormTolerance)
                return "Выше нормы";

            return "Норма";
        }

        /// <summary>
        /// Проверяет, находится ли значение роста в допустимом диапазоне (130–220 см).
        /// </summary>
        public static bool IsHeightValid(double height)
            => height >= MinHeight && height <= MaxHeight;

        /// <summary>
        /// Проверяет, находится ли значение веса в допустимом диапазоне (40–170 кг).
        /// </summary>
        public static bool IsWeightValid(double weight)
            => weight >= MinWeight && weight <= MaxWeight;
    }
}
