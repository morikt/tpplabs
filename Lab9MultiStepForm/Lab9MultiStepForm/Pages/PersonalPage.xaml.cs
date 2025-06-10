using System.Windows;
using System.Windows.Controls;

namespace Lab9MultiStepForm.Pages
{
    public partial class PersonalPage : Page
    {
        private readonly MainWindow _main;

        public PersonalPage(MainWindow main)
        {
            InitializeComponent();
            _main = main;
            // если возвращаемся назад — подставляем уже введённые данные
            FirstNameBox.Text = _main.Data.FirstName;
            LastNameBox.Text = _main.Data.LastName;
            BirthDatePicker.SelectedDate = _main.Data.BirthDate;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text))
            {
                MessageBox.Show("Имя и фамилия обязательны");
                return;
            }

            _main.Data.FirstName = FirstNameBox.Text.Trim();
            _main.Data.LastName = LastNameBox.Text.Trim();
            _main.Data.BirthDate = BirthDatePicker.SelectedDate;

            _main.MainFrame.Navigate(new ContactPage(_main));
        }
    }
}
