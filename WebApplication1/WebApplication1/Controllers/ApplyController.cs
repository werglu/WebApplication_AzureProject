using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SendGrid;
using SendGrid.Helpers.Mail;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    public class ApplyController : Controller
    {
        private readonly WebApplication1Context _context;
        public int? JobOfferID;

        public ApplyController(WebApplication1Context context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
                return NotFound();

            JobOfferID = id;
            ViewBag.JobName = _context.JobOffer.Where(j => j.Id == id).FirstOrDefault().JobTitle;
            JobApplication jobApplication = new JobApplication();
            return View(new JobApplicationViewModel(jobApplication));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, [Bind("FirstName,LastName,EmailAddress,PhoneNumber,ContactAgreement")] JobApplication jobApplication, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                    try
                    {
                        string connectionString = "DefaultEndpointsProtocol=https;AccountName=jobwebappstorage;AccountKey=bElRFx0QFTcwSxUqEfVII97eUcaOjaSq1pw8Ja3nJjs3XAzLFuTuAVHjkUgFTo7j9QQjOq6pMu5CQr4wTfFoeQ==;EndpointSuffix=core.windows.net";
                        var containerName = "applications";

                        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                        if (await cloudBlobContainer.CreateIfNotExistsAsync())
                        {
                            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                        }

                        string blobFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobFileName);
                        cloudBlockBlob.Properties.ContentType = file.ContentType;
                        using (var stream = file.OpenReadStream())
                        {
                            await cloudBlockBlob.UploadFromStreamAsync(stream);
                        }

                        jobApplication.CVName = file.FileName;
                        jobApplication.CVgiud = blobFileName;
                        jobApplication.CvUrl = cloudBlockBlob.Uri.ToString();

                        ViewBag.Message = "File uploaded successfully";

                        //
                        await cloudBlockBlob.FetchAttributesAsync();
                        long fileByteLength = cloudBlockBlob.Properties.Length;
                        byte[] fileContent = new byte[fileByteLength];
                        for (int i = 0; i < fileByteLength; i++)
                        {
                            fileContent[i] = 0x20;
                        }
                        await cloudBlockBlob.DownloadToByteArrayAsync(fileContent, 0);
                        //
                        var client = new SendGridClient("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");
                        var msg = new SendGridMessage();

                        msg.SetFrom(new EmailAddress("webapplication@gmail.com", "WebJobApp"));

                        var recipients = new List<EmailAddress>
                        {
                            new EmailAddress("weronika.gluszczak@wp.pl"),
                        };
                  
                        msg.AddTos(recipients);

                        var cv = new Attachment()
                        {
                            Type = "application/pdf",
                            Filename = file.FileName,
                            Content = Convert.ToBase64String(fileContent),
                            ContentId = "contentId"
                        };
                        msg.AddAttachment(cv);

                        msg.SetSubject("New Application");

                        msg.AddContent(MimeType.Text, "New Application with attached CV file.");
                        msg.AddContent(MimeType.Html, "<p>New Application with attached CV file.</p><br></br><a href=\""+ cloudBlockBlob.Uri.ToString()+ "\">Link</a>");

                        var response = await client.SendEmailAsync(msg);


                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }

                if (_context.JobOffer.Where(i => i.Id == id).FirstOrDefault().JobApplications == null)
                {
                    _context.JobOffer.Where(i => i.Id == id).FirstOrDefault().JobApplications = new List<JobApplication>();
                }
                _context.JobOffer.Where(i => i.Id == id).FirstOrDefault().JobApplications.Add(jobApplication);
                var x = _context.JobOffer.Where(i => i.Id == id).FirstOrDefault();
                _context.JobApplications.Add(jobApplication);
                await _context.SaveChangesAsync();
                return this.RedirectToAction("Index", "JobOffers");
            }
            return View(new JobApplicationViewModel(jobApplication));
        }

        public IActionResult Details(int? aa)
        {
            if (aa == null)
                return NotFound();
            JobApplication r = _context.JobApplications.Where(j => j.Id == aa).FirstOrDefault();
            return View(new JobApplicationViewModel(r));
        }

        public async Task<IActionResult> Download(int id, int offerId)
        {
           
            var jobapplication = _context.JobApplications.Where(i => i.Id == id).FirstOrDefault();

            var containerName = "applications";
            string storageConnection = "DefaultEndpointsProtocol=https;AccountName=jobwebappstorage;AccountKey=bElRFx0QFTcwSxUqEfVII97eUcaOjaSq1pw8Ja3nJjs3XAzLFuTuAVHjkUgFTo7j9QQjOq6pMu5CQr4wTfFoeQ==;EndpointSuffix=core.windows.net";
            string fileName = jobapplication.CVgiud;
            if (fileName != null)
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                byte[] fileBytes;
                using (MemoryStream memStream = new MemoryStream())
                {
                    await blockBlob.DownloadToStreamAsync(memStream);
                    fileBytes = memStream.ToArray();
                }

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, jobapplication.CVName);
            }
            return this.RedirectToAction("Details", "JobOffers", new { Id = offerId });
        }
    }
}