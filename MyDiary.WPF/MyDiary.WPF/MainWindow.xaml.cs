using MyDiary.WPF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace MyDiary.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRepository<Note> _noteRepository;

        public MainWindow()
        {
            InitializeComponent();
            _noteRepository = new NoteRepository(new ApplicationContext("DefaultConnection"));
            NotesList.ItemsSource = _noteRepository.GetAll().ToList();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
