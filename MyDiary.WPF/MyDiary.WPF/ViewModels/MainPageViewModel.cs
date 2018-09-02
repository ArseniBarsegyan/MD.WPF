using System.Collections.ObjectModel;
using MyDiary.WPF.Commands;
using MyDiary.WPF.Extensions;

namespace MyDiary.WPF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private RestClientCommand _getNotesCommand;
        private ObservableCollection<NoteViewModel> _notes;

        public ObservableCollection<NoteViewModel> Notes
        {
            get => _notes;
            set => SetValue(ref _notes, value, nameof(Notes));
        }

        public RestClientCommand GetNotesCommand
        {
            get
            {
                return _getNotesCommand ??
                       (_getNotesCommand = new RestClientCommand(async obj =>
                       {
                           var allNotes = await ServiceClient.GetNotesAsync();
                           var viewModels = allNotes.ToNoteViewModels();
                           Notes = new ObservableCollection<NoteViewModel>(viewModels);
                       }));
            }
        }
    }
}