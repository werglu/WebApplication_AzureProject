using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class JobOfferPagingModel
    {
        public IEnumerable<JobOffer> Offers { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }

    }
}
