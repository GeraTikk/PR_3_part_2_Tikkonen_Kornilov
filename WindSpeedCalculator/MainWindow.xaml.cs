using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WindSpeedCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события для проверки ввода только натуральных чисел
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, что вводится только число
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Обработчик события для кнопки "Вычислить"
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что поля ввода не пустые
            if (string.IsNullOrEmpty(Distance.Text) || string.IsNullOrEmpty(Time.Text))
            {
                Result.Text = "Введите расстояние и время!";
                return;
            }

            // Преобразование введенных данных в числа
            if (!double.TryParse(Distance.Text, out double distance) || !double.TryParse(Time.Text, out double time))
            {
                Result.Text = "Неверное значение. Введите натуральные числа!";
                return;
            }

            // Проверка, что время не равно нулю (чтобы избежать деления на ноль)
            if (time == 0)
            {
                Result.Text = "Время не может быть равно нулю!";
                return;
            }

            // Проверка, что выбрана одна из единиц измерения
            if (ChooseMS.IsChecked == false && ChooseKMH.IsChecked == false)
            {
                Result.Text = "Выберите единицы измерения!";
                return;
            }

            // Вычисление скорости в м/с
            double speedMps = distance / time;

            // Вывод результата в зависимости от выбранной единицы измерения
            if (ChooseMS.IsChecked == true)
            {
                Result.Text = $"{speedMps:F2} м/с";
            }
            else if (ChooseKMH.IsChecked == true)
            {
                double speedKmph = speedMps * 3.6; // Преобразование м/с в км/ч
                Result.Text = $"{speedKmph:F2} км/ч";
            }
        }
    }
}