using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ModelViews
{
    public class CompanyViewModel
    {
        private Company _company;

        public CompanyViewModel(Company jo)
        {
            _company = jo;
        }

        public Company GetJobOfferDatabase()
        {
            return _company;
        }

        public int Id { 
            get 
            {
                return _company.Id;
            } set
            {
                _company.Id = value;
            }
        }
        public string Name
        {
            get
            {
                return _company.Name;
            }
            set
            {
                _company.Name = value;
            }
        }
    }
}
