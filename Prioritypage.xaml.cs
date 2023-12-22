using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace karimov_eyes
{
    /// <summary>
    /// Логика взаимодействия для Prioritypage.xaml
    /// </summary>
    public partial class Prioritypage : Window
    {
        public Prioritypage(int max)
        {
            InitializeComponent();
            PriorityTB.Text = max.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriorityTB.Text)) Close();
            else MessageBox.Show("Введите новый приоритет", "Ошибка!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            PriorityTB.Text = "";
            Close();
        }
    }
}
