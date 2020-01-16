using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class JobOfferApiController : ControllerBase
    {
        private readonly WebApplication1Context _context;

        public JobOfferApiController(WebApplication1Context context)
        {
            _context = context;
        }


        [Route("getoffers/search={searchString}&pageNo={pageNo}&pageSize={pageSize}")]
        [HttpGet]
        public JobOfferPagingModel GetOffers(string searchString, int pageNo, int pageSize)
        {
            int totalRecord = 0;
            List<JobOffer> record;
            if (string.IsNullOrEmpty(searchString) || searchString.Equals("_"))
            {
                record = (from o in _context.JobOffer
                          orderby o.Id
                          select (o)).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                totalRecord = _context.JobOffer.Count();
            }
            else
            {
                string searchToLower = searchString.ToLower();
                record = (from o in _context.JobOffer
                          where o.JobTitle.ToLower().Contains(searchToLower)
                          orderby o.Id
                          select (o)).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                totalRecord = _context.JobOffer.Where(o => o.JobTitle.ToLower().Contains(searchToLower)).Count();

            }

            JobOfferPagingModel data = new JobOfferPagingModel
            {
                Offers = record,
                TotalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0),
                TotalRecord = totalRecord
            };
            return data;
        }

        [Route("getoffers/name={nameString}")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany(string nameString)
        {
            //TODO Create
            var com = new Company { Name = nameString};
            if (ModelState.IsValid)
            {
                _context.Add(com);
                await _context.SaveChangesAsync();
                return Ok("OK");
            }
            return BadRequest("You made bad xD");
        }
    }
}