using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class JobOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Job title")]
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public Company Company { get; set; }
        public string Location { get; set; }
        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
        [Required(ErrorMessage = "Description is required")]
        [MinLength(5)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        [Display(Name = "Valid until")]
        public DateTime? ValidUntil { get; set; } = DateTime.Now;


        public JobOffer()
        {
            JobTitle = "";
            Location = "";          
        }
    }


}
