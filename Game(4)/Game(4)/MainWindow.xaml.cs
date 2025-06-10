using System;
using System.ComponentModel;
using System.Windows;

namespace WpfGuessNumber
{
    public partial class MainWindow : Window
    {
        private GameModel _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = DataContext as GameModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Запуск новой игры при загрузке окна
            _model.StartNewGame();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            _model.ProcessGuess();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            _model.StartNewGame();
        }
    }

    public class GameModel : INotifyPropertyChanged
    {
        private int _targetNumber;
        private int _attempts;
        private string _hintText;
        private string _attemptsText;
        private string _playerInput;

        public string HintText
        {
            get => _hintText;
            set
            {
                if (_hintText != value)
                {
                    _hintText = value;
                    OnPropertyChanged("HintText");
                }
            }
        }

        public string AttemptsText
        {
            get => _attemptsText;
            set
            {
                if (_attemptsText != value)
                {
                    _attemptsText = value;
                    OnPropertyChanged("AttemptsText");
                }
            }
        }

        public string PlayerInput
        {
            get => _playerInput;
            set
            {
                if (_playerInput != value)
                {
                    _playerInput = value;
                    OnPropertyChanged("PlayerInput");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void StartNewGame()
        {
            var rnd = new Random();
            _targetNumber = rnd.Next(1, 101); // число от 1 до 100
            _attempts = 0;
            HintText = "Угадайте число от 1 до 100";
            AttemptsText = "Попыток: 0";
            PlayerInput = string.Empty;
        }

        public void ProcessGuess()
        {
            if (!int.TryParse(PlayerInput, out int guessed))
            {
                HintText = "Некорректный ввод! Введите целое число.";
                return;
            }

            if (guessed < 1 || guessed > 100)
            {
                HintText = "Число должно быть от 1 до 100.";
                return;
            }

            _attempts++;
            AttemptsText = $"Попыток: {_attempts}";

            if (guessed < _targetNumber)
            {
                HintText = "Слишком маленькое";
            }
            else if (guessed > _targetNumber)
            {
                HintText = "Слишком большое";
            }
            else
            {
                HintText = $"Поздравляем! Вы угадали за {_attempts} попыток.";
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
