using Xunit;
using OptimalWeightApp;

namespace OptimalWeightApp.Tests
{
    /// <summary>
    /// Автоматизированные тесты для класса WeightCalculator.
    /// Вариант №8 — Вычисление оптимального веса человека.
    /// </summary>
    public class WeightCalculatorTests
    {
        // ============================================================
        // Тесты CalculateOptimalWeight — расчёт оптимального веса
        // ============================================================

        /// <summary>
        /// TC_AUTO_1: Расчёт оптимального веса для мужчины (рост 180 см).
        /// Ожидаемый результат: (180 – 100) × 0.87 = 69.6 кг.
        /// </summary>
        [Fact]
        public void CalculateOptimalWeight_Male_180cm_Returns69Point6()
        {
            double result = WeightCalculator.CalculateOptimalWeight(180, isMale: true);
            Assert.Equal(69.6, result, precision: 1);
        }

        /// <summary>
        /// TC_AUTO_2: Расчёт оптимального веса для женщины (рост 165 см).
        /// Ожидаемый результат: (165 – 100) × 0.90 = 58.5 кг.
        /// </summary>
        [Fact]
        public void CalculateOptimalWeight_Female_165cm_Returns58Point5()
        {
            double result = WeightCalculator.CalculateOptimalWeight(165, isMale: false);
            Assert.Equal(58.5, result, precision: 1);
        }

        /// <summary>
        /// TC_AUTO_3: Граничное значение роста — минимум 130 см, мужчина.
        /// Ожидаемый результат: (130 – 100) × 0.87 = 26.1 кг.
        /// </summary>
        [Fact]
        public void CalculateOptimalWeight_Male_MinHeight130_Returns26Point1()
        {
            double result = WeightCalculator.CalculateOptimalWeight(130, isMale: true);
            Assert.Equal(26.1, result, precision: 1);
        }

        /// <summary>
        /// TC_AUTO_4: Граничное значение роста — максимум 220 см, женщина.
        /// Ожидаемый результат: (220 – 100) × 0.90 = 108.0 кг.
        /// </summary>
        [Fact]
        public void CalculateOptimalWeight_Female_MaxHeight220_Returns108()
        {
            double result = WeightCalculator.CalculateOptimalWeight(220, isMale: false);
            Assert.Equal(108.0, result, precision: 1);
        }

        /// <summary>
        /// TC_AUTO_5: Мужчина и женщина с одинаковым ростом дают разные оптимальные веса.
        /// </summary>
        [Fact]
        public void CalculateOptimalWeight_MaleVsFemale_DifferentResults()
        {
            double male = WeightCalculator.CalculateOptimalWeight(170, isMale: true);
            double female = WeightCalculator.CalculateOptimalWeight(170, isMale: false);
            Assert.NotEqual(male, female);
            Assert.True(male < female); // мужчина имеет меньший оптимальный вес
        }

        // ============================================================
        // Тесты GetCharacteristic — сравнительная характеристика
        // ============================================================

        /// <summary>
        /// TC_AUTO_6: Вес точно равен оптимальному → «Норма».
        /// </summary>
        [Fact]
        public void GetCharacteristic_ExactOptimal_ReturnsNorm()
        {
            string result = WeightCalculator.GetCharacteristic(70.0, 70.0);
            Assert.Equal("Норма", result);
        }

        /// <summary>
        /// TC_AUTO_7: Вес на 3 кг выше оптимального (граница нормы) → «Норма».
        /// </summary>
        [Fact]
        public void GetCharacteristic_3kgAboveOptimal_ReturnsNorm()
        {
            string result = WeightCalculator.GetCharacteristic(73.0, 70.0);
            Assert.Equal("Норма", result);
        }

        /// <summary>
        /// TC_AUTO_8: Вес на 3 кг ниже оптимального (граница нормы) → «Норма».
        /// </summary>
        [Fact]
        public void GetCharacteristic_3kgBelowOptimal_ReturnsNorm()
        {
            string result = WeightCalculator.GetCharacteristic(67.0, 70.0);
            Assert.Equal("Норма", result);
        }

        /// <summary>
        /// TC_AUTO_9: Вес более чем на 3 кг выше оптимального → «Выше нормы».
        /// </summary>
        [Fact]
        public void GetCharacteristic_MoreThan3kgAbove_ReturnsAboveNorm()
        {
            string result = WeightCalculator.GetCharacteristic(74.0, 70.0);
            Assert.Equal("Выше нормы", result);
        }

        /// <summary>
        /// TC_AUTO_10: Вес более чем на 3 кг ниже оптимального → «Ниже нормы».
        /// </summary>
        [Fact]
        public void GetCharacteristic_MoreThan3kgBelow_ReturnsBelowNorm()
        {
            string result = WeightCalculator.GetCharacteristic(66.0, 70.0);
            Assert.Equal("Ниже нормы", result);
        }

        /// <summary>
        /// TC_AUTO_11: Значительно выше нормы → «Выше нормы».
        /// </summary>
        [Fact]
        public void GetCharacteristic_SignificantlyAbove_ReturnsAboveNorm()
        {
            string result = WeightCalculator.GetCharacteristic(100.0, 60.0);
            Assert.Equal("Выше нормы", result);
        }

        /// <summary>
        /// TC_AUTO_12: Значительно ниже нормы → «Ниже нормы».
        /// </summary>
        [Fact]
        public void GetCharacteristic_SignificantlyBelow_ReturnsBelowNorm()
        {
            string result = WeightCalculator.GetCharacteristic(40.0, 70.0);
            Assert.Equal("Ниже нормы", result);
        }

        // ============================================================
        // Тесты IsHeightValid — валидация роста
        // ============================================================

        /// <summary>
        /// TC_AUTO_13: Рост 175 см — в допустимом диапазоне → true.
        /// </summary>
        [Fact]
        public void IsHeightValid_ValidHeight175_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsHeightValid(175));
        }

        /// <summary>
        /// TC_AUTO_14: Рост 130 см — граничное минимальное значение → true.
        /// </summary>
        [Fact]
        public void IsHeightValid_MinBoundary130_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsHeightValid(130));
        }

        /// <summary>
        /// TC_AUTO_15: Рост 220 см — граничное максимальное значение → true.
        /// </summary>
        [Fact]
        public void IsHeightValid_MaxBoundary220_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsHeightValid(220));
        }

        /// <summary>
        /// TC_AUTO_16: Рост 129 см — ниже минимума → false.
        /// </summary>
        [Fact]
        public void IsHeightValid_BelowMin129_ReturnsFalse()
        {
            Assert.False(WeightCalculator.IsHeightValid(129));
        }

        /// <summary>
        /// TC_AUTO_17: Рост 221 см — выше максимума → false.
        /// </summary>
        [Fact]
        public void IsHeightValid_AboveMax221_ReturnsFalse()
        {
            Assert.False(WeightCalculator.IsHeightValid(221));
        }

        /// <summary>
        /// TC_AUTO_18: Рост 0 — недопустимое значение → false.
        /// </summary>
        [Fact]
        public void IsHeightValid_Zero_ReturnsFalse()
        {
            Assert.False(WeightCalculator.IsHeightValid(0));
        }

        // ============================================================
        // Тесты IsWeightValid — валидация веса
        // ============================================================

        /// <summary>
        /// TC_AUTO_19: Вес 70 кг — в допустимом диапазоне → true.
        /// </summary>
        [Fact]
        public void IsWeightValid_ValidWeight70_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsWeightValid(70));
        }

        /// <summary>
        /// TC_AUTO_20: Вес 40 кг — граничное минимальное значение → true.
        /// </summary>
        [Fact]
        public void IsWeightValid_MinBoundary40_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsWeightValid(40));
        }

        /// <summary>
        /// TC_AUTO_21: Вес 170 кг — граничное максимальное значение → true.
        /// </summary>
        [Fact]
        public void IsWeightValid_MaxBoundary170_ReturnsTrue()
        {
            Assert.True(WeightCalculator.IsWeightValid(170));
        }

        /// <summary>
        /// TC_AUTO_22: Вес 39 кг — ниже минимума → false.
        /// </summary>
        [Fact]
        public void IsWeightValid_BelowMin39_ReturnsFalse()
        {
            Assert.False(WeightCalculator.IsWeightValid(39));
        }

        /// <summary>
        /// TC_AUTO_23: Вес 171 кг — выше максимума → false.
        /// </summary>
        [Fact]
        public void IsWeightValid_AboveMax171_ReturnsFalse()
        {
            Assert.False(WeightCalculator.IsWeightValid(171));
        }

        // ============================================================
        // Параметризованные тесты (Theory)
        // ============================================================

        /// <summary>
        /// TC_AUTO_24-28: Параметризованные тесты характеристики веса.
        /// </summary>
        [Theory]
        [InlineData(70.0, 70.0, "Норма")]
        [InlineData(73.0, 70.0, "Норма")]
        [InlineData(67.0, 70.0, "Норма")]
        [InlineData(74.0, 70.0, "Выше нормы")]
        [InlineData(66.0, 70.0, "Ниже нормы")]
        public void GetCharacteristic_Theory_ReturnsExpected(
            double actual, double optimal, string expected)
        {
            string result = WeightCalculator.GetCharacteristic(actual, optimal);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// TC_AUTO_29-32: Параметризованные тесты формулы оптимального веса для мужчин.
        /// </summary>
        [Theory]
        [InlineData(130, true, 26.1)]
        [InlineData(170, true, 60.9)]
        [InlineData(180, true, 69.6)]
        [InlineData(220, true, 104.4)]
        public void CalculateOptimalWeight_Male_Theory(
            double height, bool isMale, double expected)
        {
            double result = WeightCalculator.CalculateOptimalWeight(height, isMale);
            Assert.Equal(expected, result, precision: 1);
        }

        /// <summary>
        /// TC_AUTO_33-36: Параметризованные тесты формулы для женщин.
        /// </summary>
        [Theory]
        [InlineData(130, false, 27.0)]
        [InlineData(165, false, 58.5)]
        [InlineData(170, false, 63.0)]
        [InlineData(220, false, 108.0)]
        public void CalculateOptimalWeight_Female_Theory(
            double height, bool isMale, double expected)
        {
            double result = WeightCalculator.CalculateOptimalWeight(height, isMale);
            Assert.Equal(expected, result, precision: 1);
        }
    }
}
