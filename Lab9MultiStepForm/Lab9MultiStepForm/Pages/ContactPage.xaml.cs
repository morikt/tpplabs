using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Lab9MultiStepForm.Pages
{
    public partial class ContactPage : Page
    {
        private readonly MainWindow _main;

        private static readonly Regex EmailRegex =
            new Regex(@"^[\w\.-]+@[\w\.-]+\.\w+$");
        private static readonly Regex PhoneRegex =
            new Regex(@"^\+?\d{10,15}$");

        public ContactPage(MainWindow main)
        {
            InitializeComponent();
            _main = main;

            EmailBox.Text = _main.Data.Email;
            PhoneBox.Text = _main.Data.Phone;
        }

        private void Back_Click(object sender, RoutedEventArgs e) =>
            _main.MainFrame.GoBack();

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!EmailRegex.IsMatch(EmailBox.Text.Trim()))
            {
                MessageBox.Show("Некорректный e-mail");
                return;
            }
            if (!PhoneRegex.IsMatch(PhoneBox.Text.Trim()))
            {
                MessageBox.Show("Некорректный телефон. Формат: только цифры, 10-15 символов, опционально +");
                return;
            }

            _main.Data.Email = EmailBox.Text.Trim();
            _main.Data.Phone = PhoneBox.Text.Trim();

            _main.MainFrame.Navigate(new AddressPage(_main));
        }
    }
}
