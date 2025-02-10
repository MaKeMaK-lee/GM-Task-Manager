
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GM_Task_Manager.Store.Core;

namespace GM_Task_Manager.Store.Entities.ToDoTask
{
    public enum TaskStatus
    {
        Created = 0,
        InProgress = 1,
        Completed = 2,
    }

    public class ToDoTask : ObservableObject
    {
        [Key]
        [Required]
        [Column("Id")]
        public required Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("Name")]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Column("Description")]
        public required string Description { get; set; }

        [NotMapped]
        public TaskStatus ChangableStatus
        {
            get => Status;
            set
            {
                Status = value;
                TimeStatusUpdated = DateTime.Now;
                OnPropertyChanged(nameof(TimeStatusUpdated));
                OnPropertyChanged(nameof(ChangableStatus));
            }
        }

        [Required]
        [Column("Status")]
        public required TaskStatus Status { get; set; }

        [Required]
        [Column("TimeDeadline")]
        public required DateTime TimeDeadline { get; set; }

        [Required]
        [Column("TimeCreated")]
        public required DateTime TimeCreated { get; set; }

        [Required]
        [Column("TimeStatusUpdated")]
        public required DateTime TimeStatusUpdated { get; set; }

        public ToDoTask()
        {

        }
    }
}
