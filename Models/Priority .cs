using System.Collections.Generic;

namespace ToDoApp.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Level { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
    }
}
