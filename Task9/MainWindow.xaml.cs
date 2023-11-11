using System.Windows;
using Task9.Model.DataAccess;
using Task9.ViewModel;

namespace Task9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            ConstructDataBase constructDataBase = new ConstructDataBase();
            constructDataBase.CreateDataBase();
        }
    }
}
