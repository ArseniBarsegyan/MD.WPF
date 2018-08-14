using System.Collections.ObjectModel;
using System.Data.Entity;
using MyDiary.WPF.Commands;
using MyDiary.WPF.Extensions;
using MyDiary.WPF.Models;

namespace MyDiary.WPF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IRepository<Note> _noteRepository;
        private RepositoryCommand _getNotesCommand;
        private ObservableCollection<NoteViewModel> _notes;

        public MainPageViewModel()
        {
            _noteRepository = new NoteRepository(new ApplicationContext("DefaultConnection"));
        }

        public ObservableCollection<NoteViewModel> Notes
        {
            get => _notes;
            set => SetValue(ref _notes, value, nameof(Notes));
        }

        public RepositoryCommand GetNotesCommand
        {
            get
            {
                return _getNotesCommand ??
                       (_getNotesCommand = new RepositoryCommand(async obj =>
                       {
                           var allNotes = await _noteRepository.GetAll().ToListAsync();
                           var viewModels = allNotes.ToNoteViewModels();
                           Notes = new ObservableCollection<NoteViewModel>(viewModels);
                       }));
            }
        }
    }
}