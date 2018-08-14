using MyDiary.WPF.Commands;
using MyDiary.WPF.Extensions;
using MyDiary.WPF.Models;

namespace MyDiary.WPF.ViewModels
{
    public class CreateNoteWindowViewModel : BaseViewModel
    {
        private readonly IRepository<Note> _noteRepository;
        private RepositoryCommand _getNotesCommand;
        private NoteViewModel _noteViewModel;

        public CreateNoteWindowViewModel()
        {
            _noteRepository = new NoteRepository(new ApplicationContext("DefaultConnection"));
        }

        public NoteViewModel NoteViewModel
        {
            get => _noteViewModel;
            set => SetValue(ref _noteViewModel, value, nameof(NoteViewModel));
        }

        public RepositoryCommand CreateNoteCommand
        {
            get
            {
                return _getNotesCommand ??
                       (_getNotesCommand = new RepositoryCommand(async obj =>
                       {
                           if (_noteViewModel == null)
                               return;
                            _noteRepository.Create(NoteViewModel.ToNoteModel());
                           await _noteRepository.SaveAsync();
                       }));
            }
        }
    }
}