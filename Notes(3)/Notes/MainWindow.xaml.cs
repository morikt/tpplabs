using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfNotesApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _allNotes = new ObservableCollection<string>();
        private CollectionViewSource _viewSource = new CollectionViewSource();
        private string _dataFile = "notes.txt";
        private const string Watermark = "Поиск...";

        public MainWindow()
        {
            InitializeComponent();
            _viewSource.Source = _allNotes;
            NotesList.ItemsSource = _viewSource.View;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка заметок из файла
            if (File.Exists(_dataFile))
            {
                foreach (var line in File.ReadAllLines(_dataFile))
                    _allNotes.Add(line);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Сохранение заметок в файл
            File.WriteAllLines(_dataFile, _allNotes);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NoteEditor.Text))
            {
                _allNotes.Add(NoteEditor.Text.Trim());
                NoteEditor.Clear();
                ClearSelection();
            }
        }

        private void NotesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (NotesList.SelectedIndex >= 0)
            {
                NoteEditor.Text = NotesList.SelectedItem as string;
                SaveButton.IsEnabled = DeleteButton.IsEnabled = true;
            }
            else
            {
                ClearSelection();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int idx = NotesList.SelectedIndex;
            if (idx >= 0)
            {
                _allNotes[idx] = NoteEditor.Text;
                ClearSelection();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int idx = NotesList.SelectedIndex;
            if (idx >= 0)
            {
                _allNotes.RemoveAt(idx);
                NoteEditor.Clear();
                ClearSelection();
            }
        }

        private void ClearSelection()
        {
            NotesList.SelectedIndex = -1;
            SaveButton.IsEnabled = DeleteButton.IsEnabled = false;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == Watermark)
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = Watermark;
                SearchBox.Foreground = Brushes.Gray;
                _viewSource.View.Filter = null; // сброс фильтра
            }
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string query = SearchBox.Text;
            if (query == Watermark) return;

            _viewSource.View.Filter = o =>
            {
                var note = (string)o;
                return note.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0;
            };
        }

        // Контекстное меню для NoteEditor
        private void MenuLight_Click(object sender, RoutedEventArgs e)
        {
            NoteEditor.Background = Brushes.White;
        }
        private void MenuDark_Click(object sender, RoutedEventArgs e)
        {
            NoteEditor.Background = Brushes.LightGray;
        }
        private void MenuWindowLight_Click(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.White;
        }
        private void MenuWindowDark_Click(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.LightSlateGray;
        }
    }
}