namespace MyDiary.WPF.Models
{
    /// <summary>
    /// Photos table in database.
    /// </summary>
    public class Photo : Entity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public int NoteId { get; set; }
        public virtual Note Note { get; set; }
    }
}
