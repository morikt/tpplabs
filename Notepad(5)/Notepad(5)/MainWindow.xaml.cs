using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace WpfNotepad
{
    public partial class MainWindow : Window
    {
        private string _currentFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Команда "Новый" очищает редактор и сбрасывает путь
        private void New_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (Editor.Text.Length > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед созданием нового документа?",
                                             "Подтверждение",
                                             MessageBoxButton.YesNoCancel,
                                             MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                    return;
                if (result == MessageBoxResult.Yes)
                    SaveDocument();
            }

            Editor.Clear();
            _currentFilePath = null;
            Title = "Блокнот - Новый документ";
        }

        // Команда "Открыть"
        private void Open_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string text = File.ReadAllText(dlg.FileName);
                    Editor.Text = text;
                    _currentFilePath = dlg.FileName;
                    Title = "Блокнот - " + System.IO.Path.GetFileName(_currentFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла:\n{ex.Message}",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        // Команда "Сохранить"
        private void Save_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveDocument();
        }

        private void Save_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Общий метод сохранения
        private void SaveDocument()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                var dlg = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                    DefaultExt = ".txt"
                };

                if (dlg.ShowDialog() == true)
                    _currentFilePath = dlg.FileName;
                else
                    return;
            }

            try
            {
                File.WriteAllText(_currentFilePath, Editor.Text);
                Title = "Блокнот - " + System.IO.Path.GetFileName(_currentFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(Editor.Text))
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?",
                                             "Подтверждение",
                                             MessageBoxButton.YesNoCancel,
                                             MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (result == MessageBoxResult.Yes)
                    SaveDocument();
            }
            base.OnClosing(e);
        }
    }
}
