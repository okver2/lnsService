using lns.services.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace lns.api.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [Route("api/inventory")]
        [HttpGet]
        [ResponseType(typeof(IInventory))]
        public async Task<IHttpActionResult> GetInventory()
        {
            var result = await _service.GetInventoryAsync();
            return Ok(result);
        }

        [Route("api/inventory/{id}")]
        [HttpGet]
        [ResponseType(typeof(IInventory))]
        public async Task<IHttpActionResult> GetInventoryById(int id)
        {
            var result = await _service.GetInventoryItemAsync(id);
            return Ok(result);
        }

        [Route("api/inventory/create")]
        [HttpPost]
        [ResponseType(typeof(IInventory))]
        public async Task<IHttpActionResult> CreateInventoryItem(Inventory item)
        {
            var result = await _service.CreateInventoryItemAsync(item);
            return Ok(result);
        }

        [Route("api/inventory/delete/{id}")]
        [HttpGet]
        [ResponseType(typeof(IInventory))]
        public async Task<IHttpActionResult> DeleteInventoryItem(int id)
        {
            var result = await _service.DeleteInventoryItemAsync(id);
            return Ok(result);
        }

        /*
        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var savePath = Path.Combine(_appEnvironment.WebRootPath, "uploads", file.FileName);

                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return Created(savePath, file)
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, ex.Message);
            }
        }
        */

        //        [Route("api/inventory/upload/{id}")]
        [HttpPost()]
        public string UploadFiles(int id)
        {
            string step = "0";

            try
            {
                int iUploadedCnt = 0;

                step = "1";
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                //string sPath = "";
                //                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/../images/inventory/") + id.ToString() + "/";
                var sPath = HttpContext.Current.Request.MapPath("~/../images/inventory/" + id.ToString() + "/");

                step = "2";

                if (!Directory.Exists(sPath))
                    Directory.CreateDirectory(sPath);

                step = "3";

                // CHECK THE FILE COUNT.
                for (int i = 0; i < hfc.Count; i++)
                {
                    step = "4";

                    System.Web.HttpPostedFile hpf = hfc[i];

                    step = "5";

                    if (hpf.ContentLength > 0)
                    {

                        step = "6";

                        // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            step = "7";

                            // SAVE THE FILES IN THE FOLDER.
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }

                step = "8";

                // RETURN A MESSAGE (OPTIONAL).
                if (iUploadedCnt > 0)
                {
                    return iUploadedCnt + " Files Uploaded Successfully";
                }
                else
                {
                    return "Upload Failed";
                }
            }
            catch (Exception e)
            {
                return step + " " + e.Message;
            }
        }

        [Route("api/inventory/images/{id}")]
        [HttpGet()]
        public HttpResponseMessage GetFilesFrom(int id)
        {

            var path = HttpContext.Current.Request.MapPath("~/../images/inventory/" + id.ToString() + "/");

            try
            {
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };

                List<String> filesFound = new List<String>();
                List<String> files = new List<String>();
                var searchOption = false ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                if (File.Exists(path))
                {

                    //var path = HttpContext.Current.Request.MapPath("~/../images/inventory/" + id.ToString() + "/");
                    filesFound.AddRange(Directory.GetFiles(path, String.Format("*.{0}", filters), searchOption));

                    foreach (string file in filesFound)
                        files.Add(Path.GetFileName(file));

                }

                return Request.CreateResponse(HttpStatusCode.OK, files.ToArray());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }


        [Route("api/inventory/upload/{id}")]
        [HttpPost] 
        public async Task<List<string>> PostAsync()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/uploads");

                MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                List<string> messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                }

                return messages;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
        }

        public class MyStreamProvider : MultipartFormDataStreamProvider
        {
            public MyStreamProvider(string uploadPath)
                : base(uploadPath)
            {

            }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                string fileName = headers.ContentDisposition.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = Guid.NewGuid().ToString() + ".data";
                }
                return fileName.Replace("\"", string.Empty);
            }
        }


    }

}
