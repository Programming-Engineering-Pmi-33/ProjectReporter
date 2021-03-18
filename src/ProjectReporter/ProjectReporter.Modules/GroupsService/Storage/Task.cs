using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class Task
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public DateTime StartDateTime { get; set; }
        [Required] public DateTime EndDateTime { get; set; }
        [Required] public int Points { get; set; }
        [Required] public int GroupId { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }//
        [Required] public DateTime DateTimeModified { get; set; }//

        public virtual Group Group { get; set; }//
        public List<Report> Reports { get; set; }
    }
}