using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using MultiLangNotepad.Properties;

namespace MultiLangNotepad
{
    public partial class MainWindow : Window
    {
        private string _currentFilePath;

        public MainWindow()
        {
            InitializeComponent();

            // По умолчанию выбираем первый элемент в ComboBox и сразу применяем язык
            myCombobox.SelectedIndex = 0;
            if (myCombobox.SelectedItem is ComboBoxItem initItem)
                changeLanguage(initItem.Tag.ToString());

            // Привязки команд
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewCommand_Executed));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenCommand_Executed));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveCommand_Executed));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyCommand_Executed, CanExecuteCopyCut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteCommand_Executed, CanExecutePaste));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, CutCommand_Executed, CanExecuteCopyCut));
        }

        // Обработчик смены языка
        private void myCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myCombobox.SelectedItem is ComboBoxItem item)
                changeLanguage(item.Tag.ToString());
        }

        private void changeLanguage(string cultureCode)
        {
            // Устанавливаем культуру для ресурсного класса
            Strings.Culture = new CultureInfo(cultureCode);

            // Обновляем заголовки меню из ресурсов
            MainMenu.Header = Strings.File;
            NewFileBtn.Header = Strings.New;
            OpenFileBtn.Header = Strings.Open;
            SaveFileBtn.Header = Strings.Save;
            ExitBtn.Header = Strings.Exit;
            EditMenu.Header = Strings.Edit;
            CopyBtn.Header = Strings.Copy;
            PasteBtn.Header = Strings.Paste;
            CutBtn.Header = Strings.Cut;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextArea.Text))
            {
                var result = MessageBox.Show(
                    Strings.SaveBeforeNew,
                    Strings.File,
                    MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                    SaveFile();
                else if (result == MessageBoxResult.Cancel)
                    return;
            }

            TextArea.Clear();
            _currentFilePath = null;
            Title = Strings.File;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    TextArea.Text = File.ReadAllText(dialog.FileName);
                    _currentFilePath = dialog.FileName;
                    Title = $"{Strings.File} - {Path.GetFileName(_currentFilePath)}";
                }
                catch
                {
                    MessageBox.Show(Strings.ErrorOpen, Strings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e) => SaveFile();

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
                };

                if (dialog.ShowDialog() == true)
                    _currentFilePath = dialog.FileName;
                else
                    return;
            }

            try
            {
                File.WriteAllText(_currentFilePath, TextArea.Text);
                Title = $"{Strings.File} - {Path.GetFileName(_currentFilePath)}";
                MessageBox.Show(Strings.SavedOK, Strings.Save, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show(Strings.ErrorSave, Strings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextArea.Text))
            {
                var result = MessageBox.Show(
                    Strings.SaveBeforeExit,
                    Strings.File,
                    MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                    SaveFile();
                else if (result == MessageBoxResult.Cancel)
                    return;
            }

            Close();
        }

        // Команды Copy/Cut
        private void CanExecuteCopyCut(object sender, CanExecuteRoutedEventArgs e)
            => e.CanExecute = !string.IsNullOrEmpty(TextArea.SelectedText);

        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => TextArea.Copy();

        // Команда Paste
        private void CanExecutePaste(object sender, CanExecuteRoutedEventArgs e)
            => e.CanExecute = Clipboard.ContainsText();

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => TextArea.Paste();

        private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => TextArea.Cut();
    }
}
