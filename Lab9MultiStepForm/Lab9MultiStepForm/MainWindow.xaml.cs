using System.Windows;
using System.Windows.Controls;
using Lab9MultiStepForm.Models;
using Lab9MultiStepForm.Pages;

namespace Lab9MultiStepForm
{
    public partial class MainWindow : Window
    {
        public FormData Data { get; } = new FormData();
        public Frame NavFrame => MainFrame;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PersonalPage(this));   // стартуем с первой страницы
        }
    }
}
