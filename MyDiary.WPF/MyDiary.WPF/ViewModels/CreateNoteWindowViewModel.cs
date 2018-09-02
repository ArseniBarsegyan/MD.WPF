using MyDiary.WPF.Commands;
using MyDiary.WPF.Extensions;

namespace MyDiary.WPF.ViewModels
{
    public class CreateNoteWindowViewModel : BaseViewModel
    {
        private RestClientCommand _createNoteCommand;
        private NoteViewModel _noteViewModel;

        public NoteViewModel NoteViewModel
        {
            get => _noteViewModel;
            set => SetValue(ref _noteViewModel, value, nameof(NoteViewModel));
        }

        public RestClientCommand CreateNoteCommand
        {
            get
            {
                return _createNoteCommand ??
                       (_createNoteCommand = new RestClientCommand(async obj =>
                       {
                           if (_noteViewModel == null)
                               return;
                           await ServiceClient.CreateNote(NoteViewModel.ToNoteModel());
                       }));
            }
        }
    }
}