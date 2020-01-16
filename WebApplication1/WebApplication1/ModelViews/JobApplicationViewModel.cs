using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ModelViews
{
    public class JobApplicationViewModel
    {
        private JobApplication _jobApp;

        public JobApplicationViewModel(JobApplication jo)
        {
            _jobApp = jo;
        }

        public JobApplication GetJobApplicationDatabase()
        {
            return _jobApp;
        }
        public int Id
        {
            get
            {
                return _jobApp.Id;
            }
            set
            {
                _jobApp.Id = value;
            }
        }

        public int OfferId
        {
            get
            {
                return _jobApp.OfferId;
            }
            set
            {
                _jobApp.OfferId = value;
            }
        }

        [Required(ErrorMessage = "Name is required")]
        public string FirstName
        {
            get
            {
                return _jobApp.FirstName;
            }
            set
            {
                _jobApp.FirstName = value;
            }
        }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName
        {
            get
            {
                return _jobApp.LastName;
            }
            set
            {
                _jobApp.LastName = value;
            }
        }

        [Required(ErrorMessage = "A phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid phone number (XXX-XXX-XXX)")]

        public string PhoneNumber
        {
            get
            {
                return _jobApp.PhoneNumber;
            }
            set
            {
                _jobApp.PhoneNumber = value;
            }
        }

        [Required(ErrorMessage = "An email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress
        {
            get
            {
                return _jobApp.EmailAddress;
            }
            set
            {
                _jobApp.EmailAddress = value;
            }
        }

        public bool ContactAgreement
        {
            get
            {
                return _jobApp.ContactAgreement;
            }
            set
            {
                _jobApp.ContactAgreement = value;
            }
        }

        public string CvUrl
        {
            get
            {
                return _jobApp.CvUrl;
            }
            set
            {
                _jobApp.CvUrl = value;
            }
        }
        public string CVName { get; set; }
        public string CVgiud { get; set; }
    }
}
