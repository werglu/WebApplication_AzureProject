
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;  
using System.Collections.Generic;  
using System.IO;  
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Controllers
{  
    public class UploadController : Controller
{
    // GET: Upload  
    public ActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public ActionResult UploadFile()
    { 
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
            if (file != null )
                try
                {
                    string connectionString = Environment.GetEnvironmentVariable("CONNECT_STR");
                    //CreateWebHostBuilder()

                    // Create a BlobServiceClient object which will be used to create a container client
                    //    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    var ff = Request.Form.Files[0];
                    var directoryName = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles");
                    var pathToSave = Path.Combine(directoryName, file.FileName);
                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("applications");
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(file.FileName); 
                    cloudBlockBlob.Properties.ContentType = file.ContentType;
                    using (var filestream = System.IO.File.OpenRead(@pathToSave))
                    {

                       await cloudBlockBlob.UploadFromStreamAsync(filestream);

                    }
                    /* string path = Path.Combine(Server.MapPath("~/Images"),
                                                Path.GetFileName(file.FileName));
                     file.SaveAs(path);*/
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();

        }
}  
}  