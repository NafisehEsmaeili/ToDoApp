using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        

        public DateTime DueDate { get; set; }

        // Foreign key for the category
        public int CategoryId { get; set; }
        public Category Category{ get; set; }

        // Foreign key for the priority
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        // Foreign key for the user
        public string UserId { get; set; }  // Store the user ID
        
    }
}
