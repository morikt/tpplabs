using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AnimatedCalculator
{
    public partial class MainWindow : Window
    {
        private double _firstOperand = 0;
        private string _currentOperator = "";
        private bool _isSecondOperand = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик для цифр (0–9)
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string digit = (string)((Button)sender).Content;

            if (_isSecondOperand)
            {
                Display.Text = "0";
                _isSecondOperand = false;
            }

            if (Display.Text == "0")
                Display.Text = digit;
            else
                Display.Text += digit;
        }

        // Обработчик для кнопок-операций (+,−,×,÷)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string op = (string)((Button)sender).Content;

            if (double.TryParse(Display.Text, out double num))
            {
                _firstOperand = num;
                _currentOperator = op;
                _isSecondOperand = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Анимация затухания текста дисплея
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            fadeOut.Completed += (s, _) =>
            {
                Display.Text = "0";
                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
                Display.BeginAnimation(OpacityProperty, fadeIn);
            };
            Display.BeginAnimation(OpacityProperty, fadeOut);

            // Сбрасываем состояние калькулятора
            _firstOperand = 0;
            _currentOperator = "";
            _isSecondOperand = false;
        }

        // Обработчик кнопки "=" 
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double secondOperand) && !string.IsNullOrEmpty(_currentOperator))
            {
                double result = 0;
                bool validOperation = true;

                switch (_currentOperator)
                {
                    case "+":
                        result = _firstOperand + secondOperand;
                        break;
                    case "−":
                        result = _firstOperand - secondOperand;
                        break;
                    case "×":
                        result = _firstOperand * secondOperand;
                        break;
                    case "÷":
                        if (secondOperand == 0)
                        {
                            MessageBox.Show("На ноль делить нельзя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            validOperation = false;
                        }
                        else
                        {
                            result = _firstOperand / secondOperand;
                        }
                        break;
                    default:
                        validOperation = false;
                        break;
                }

                if (validOperation)
                {
                    // Анимация смены результатов: сначала делаем нуль прозрачным, потом плавно выводим новое значение
                    var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
                    fadeOut.Completed += (s, _) =>
                    {
                        Display.Text = result.ToString();
                        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(150));
                        Display.BeginAnimation(OpacityProperty, fadeIn);
                    };
                    Display.BeginAnimation(OpacityProperty, fadeOut);

                    _firstOperand = result;
                    _isSecondOperand = true;
                }
            }
        }
    }
}
