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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace karimov_eyes
{
    /// <summary>
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public AgentPage()
        {
            InitializeComponent();
            var currentServices = karimov_eyesEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentServices;
            ComboType.SelectedIndex = 0;
            ComboType1.SelectedIndex = 0;
            UpdateServices();
        }
        private void UpdateServices() {

            var currentServices = karimov_eyesEntities.GetContext().Agent.ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID ==p.AgentTypeID)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 1)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 2)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 3)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 4)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 5)).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 6)).ToList();
            }

            if (ComboType1.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.Title ==p.Title)).ToList();
            }
            if (ComboType1.SelectedIndex == 1)
            {
                currentServices = currentServices.OrderBy(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 2)
            {
                currentServices = currentServices.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 3)
            {
                
            }
            if (ComboType1.SelectedIndex == 4)
            {
                
            }
            if (ComboType1.SelectedIndex == 5)
            {
                currentServices = currentServices.OrderBy(p => p.Priority).ToList();
            }
            if (ComboType1.SelectedIndex == 6)
            {
                currentServices = currentServices.OrderByDescending(p => p.Priority).ToList();
            }
            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())
            || p.Phone.Replace("+7", "8").Replace(")", "").Replace("(", "").Replace("-", "").Replace(" ", "").Contains(TBoxSearch.Text.Replace("+7", "8").Replace(")", "").Replace("(", "").Replace("-", "").Replace(" ", ""))
            || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            AgentListView.ItemsSource = currentServices;
            TableList = currentServices;
            ChangePage(0, 0);
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }


        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else { CountPage = CountRecords / 10; }
            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) -1);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;

            var currentClientServices = karimov_eyesEntities.GetContext().Agent.ToList();
            currentClientServices = currentClientServices.Where(p => p.AgentTypeID == currentAgent.ID).ToList();

            if (currentClientServices.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существуют записи на эту услугу");
            else
            {

                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        karimov_eyesEntities.GetContext().Agent.Remove(currentAgent);
                        karimov_eyesEntities.GetContext().SaveChanges();
                        AgentListView.ItemsSource = karimov_eyesEntities.GetContext().Agent.ToList();
                        UpdateServices();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                karimov_eyesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = karimov_eyesEntities.GetContext().Agent.ToList();
            }
            UpdateServices();
        }

        private void ChangePriorityButton_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach (Agent agent in AgentListView.SelectedItems)
            {
                if (agent.Priority >= max)
                {
                    max = agent.Priority;
                }
            }
            Prioritypage window = new Prioritypage(max);
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PriorityTB.Text)) return;
            MessageBox.Show(window.PriorityTB.Text);

            foreach (Agent agent in AgentListView.SelectedItems)
            {

                agent.Priority = Convert.ToInt32(window.PriorityTB.Text);
            }
            try
            {
                karimov_eyesEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateServices();
        }

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentListView.SelectedItems.Count > 1)
            {
                ChangePriorityButton.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
