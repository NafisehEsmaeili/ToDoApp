using System.Collections.Generic;
using System;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class TaskItemViewModel
    {

            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public string Category { get; set; }
            public string Priority { get; set; }
            

        public int CategoryId { get; set; } // Added for category selection
        public int PriorityId { get; set; } // Added for priority selection

        public List<Category> Categories { get; set; }
            public List<Priority> Priorities { get; set; }
        }
    }



