using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Text { get; set; }
  
        public bool Completed { get; set; }

    }
}
