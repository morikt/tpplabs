using System.Windows;
using System.Windows.Controls;

namespace Lab9MultiStepForm.Pages
{
    public partial class AddressPage : Page
    {
        private readonly MainWindow _main;
        public AddressPage(MainWindow main)
        {
            InitializeComponent();
            _main = main;

            CityBox.Text = _main.Data.City;
            StreetBox.Text = _main.Data.Street;
            HouseBox.Text = _main.Data.HouseNumber;
            FlatBox.Text = _main.Data.FlatNumber;
        }

        private void Back_Click(object sender, RoutedEventArgs e) =>
            _main.MainFrame.GoBack();

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CityBox.Text) ||
                string.IsNullOrWhiteSpace(StreetBox.Text) ||
                string.IsNullOrWhiteSpace(HouseBox.Text))
            {
                MessageBox.Show("Город, улица и дом обязательны");
                return;
            }

            _main.Data.City = CityBox.Text.Trim();
            _main.Data.Street = StreetBox.Text.Trim();
            _main.Data.HouseNumber = HouseBox.Text.Trim();
            _main.Data.FlatNumber = FlatBox.Text.Trim();

            // итоговое сообщение
            var d = _main.Data;
            MessageBox.Show(
$@"Личные данные
--------------------
Имя: {d.FirstName}
Фамилия: {d.LastName}
Дата рождения: {d.BirthDate:d}

Контакт
--------------------
E-mail: {d.Email}
Телефон: {d.Phone}

Адрес
--------------------
{d.City}, {d.Street} {d.HouseNumber}{(string.IsNullOrEmpty(d.FlatNumber) ? "" : "/" + d.FlatNumber)}",
                "Отправка данных");
        }
    }
}
