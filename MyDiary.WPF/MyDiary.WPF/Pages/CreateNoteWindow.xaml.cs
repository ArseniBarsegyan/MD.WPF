using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Win32;
using MyDiary.WPF.ViewModels;

namespace MyDiary.WPF.Pages
{
    /// <summary>
    /// Interaction logic for CreateNoteWindow.xaml
    /// </summary>
    public partial class CreateNoteWindow : Window
    {
        private string _image;
        private string _fileName;

        public CreateNoteWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PickPhotoButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                _fileName = System.IO.Path.GetFileName(filePath);
                PickPhotoButton.Content = _fileName;
                byte[] array = File.ReadAllBytes(filePath);
                _image = Convert.ToBase64String(array);
            }
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var photoModel = new PhotoViewModel
            {
                Name = _fileName,
                Image = _image
            };

            ViewModel.NoteViewModel = new NoteViewModel
            {
                Date = DateTime.Now,
                Description = DescriptionTextBox.Text,
                Photos = new ObservableCollection<PhotoViewModel> { photoModel }
            };

            ViewModel.CreateNoteCommand.Execute(null);
        }
    }
}
