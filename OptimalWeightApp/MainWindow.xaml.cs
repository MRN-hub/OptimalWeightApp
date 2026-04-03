using System.Windows;
using System.Windows.Media;

namespace OptimalWeightApp
{
    /// <summary>
    /// Главное окно приложения «Вычисление оптимального веса человека».
    /// Вариант №8.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Вычислить».
        /// Считывает введённые данные, валидирует их и вычисляет результат.
        /// </summary>
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрыть предыдущие результаты и сообщения об ошибках
            ResultPanel.Visibility = Visibility.Collapsed;
            ErrorText.Visibility = Visibility.Collapsed;

            // --- Валидация поля «Рост» ---
            if (string.IsNullOrWhiteSpace(HeightTextBox.Text))
            {
                ShowError("Заполните все поля. Поле «Рост» не должно быть пустым.");
                return;
            }

            if (!double.TryParse(HeightTextBox.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double height))
            {
                ShowError("Введите корректное числовое значение для роста.");
                return;
            }

            // --- Валидация поля «Вес» ---
            if (string.IsNullOrWhiteSpace(WeightTextBox.Text))
            {
                ShowError("Заполните все поля. Поле «Вес» не должно быть пустым.");
                return;
            }

            if (!double.TryParse(WeightTextBox.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double weight))
            {
                ShowError("Введите корректное числовое значение для веса.");
                return;
            }

            // --- Проверка диапазонов ---
            // Диапазон роста: 130–220 см
            if (height < 130 || height > 220)
            {
                ShowError("Рост должен быть в диапазоне 130–220 см.");
                return;
            }

            // Диапазон веса: 40–170 кг
            if (weight < 40 || weight > 170)
            {
                ShowError("Вес должен быть в диапазоне 40–170 кг.");
                return;
            }

            // --- Определение пола ---
            bool isMale = MaleRadioButton.IsChecked == true;

            // --- Расчёт оптимального веса ---
            // Мужчины: [рост] – 100 – 13% = (рост – 100) × 0.87
            // Женщины: [рост] – 100 – 10% = (рост – 100) × 0.90
            double optimalWeight = WeightCalculator.CalculateOptimalWeight(height, isMale);

            // --- Определение сравнительной характеристики ---
            // Норма: ±3 кг от оптимального
            string characteristic = WeightCalculator.GetCharacteristic(weight, optimalWeight);

            // --- Вывод результата ---
            OptimalWeightText.Text = $"{optimalWeight:F1} кг";
            CharacteristicText.Text = characteristic;

            // Цвет характеристики для наглядности
            CharacteristicText.Foreground = characteristic switch
            {
                "Норма" => new SolidColorBrush(Color.FromRgb(0x1E, 0x8B, 0x4C)),    // зелёный
                "Ниже нормы" => new SolidColorBrush(Color.FromRgb(0xE6, 0x7E, 0x22)), // оранжевый
                _ => new SolidColorBrush(Color.FromRgb(0xCB, 0x4C, 0x35))             // красный
            };

            ResultPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Отображает сообщение об ошибке в поле ErrorText.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
