using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ModelViews
{
    public class JobOfferViewModel
    {
        private JobOffer _jobOffer;

        public JobOfferViewModel(JobOffer jo)
        {
            _jobOffer = jo;
        }

        public JobOffer GetJobOfferDatabase()
        {
            return _jobOffer;
        }

        public int Id {
            get
            {
                return _jobOffer.Id; 
            }
            set
            {
                _jobOffer.Id = value;
            }
        }

        [Display(Name = "Job title")]
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle
        {
            get
            {
                return _jobOffer.JobTitle;
            }
            set
            {
                _jobOffer.JobTitle = value;
            }
        }
        public int Salary {
            get
            {
                return _jobOffer.Salary;
            }
            set
            {
                _jobOffer.Salary = value;
            }
        }
        public Company Company {
            get
            {
                return _jobOffer.Company;
            }
            set
            {
                _jobOffer.Company = value;
            }
        }
        public string Location {
            get
            {
                return _jobOffer.Location;
            }
            set
            {
                _jobOffer.Location = value;
            }
        }
        public List<JobApplication> JobApplications
        {
            get
            {
                return _jobOffer.JobApplications;
            }
            set
            {
                _jobOffer.JobApplications = value;
            }
        }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(5)]
        public string Description {
            get
            {
                return _jobOffer.Description;
            }
            set
            {
                _jobOffer.Description = value;
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        [Display(Name = "Valid until")]
        public DateTime? ValidUntil
        {
            get
            {
                return _jobOffer.ValidUntil;
            }
            set
            {
                _jobOffer.ValidUntil = value;
            }
        }
    }
}
