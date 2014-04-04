using System.Windows;

namespace CsvMapper.UI
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private MainViewModel viewModel;

        public MainView()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void ButtonFindFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FindFile();
        }

        private void ButtonCreateOutput_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DoExport();
            this.TextBoxResultsView.CaretIndex = this.TextBoxResultsView.Text.Length;
            this.TextBoxResultsView.ScrollToEnd();
        }
    }
}
