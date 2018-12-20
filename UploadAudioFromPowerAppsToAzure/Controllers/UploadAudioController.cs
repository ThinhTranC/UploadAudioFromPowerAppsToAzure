using Swashbuckle.Swagger.Annotations;
using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UploadAudioFromPowerAppsToAzure.Models;

namespace UploadAudioFromPowerAppsToAzure.Controllers
{

    public class UploadAudioController : ApiController
    {
        [HttpPost]
        [SwaggerResponse(
            HttpStatusCode.OK,
            Description = "Saved successfully",
            Type = typeof(UploadFileInfo))]
        [SwaggerResponse(
            HttpStatusCode.BadRequest,
            Description = "Could not find file to upload")]
        [SwaggerOperation("UploadAudio")]
        public async Task<IHttpActionResult> UploadAudio(string NoteGuid = "", string AudioType ="", string CreatedBy = "", string RecordedTime = "", string fileName = "")
        {
          
            //Use a GUID in case the fileName is not specified
            if (fileName == "")
            {
                fileName = Guid.NewGuid().ToString();
            }
            //Check if submitted content is of MIME Multi Part Content with Form-data in it?
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Could not find file to upload");
            }

            //Read the content in a InMemory Muli-Part Form Data format
            var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

            //Get the first file
            var files = provider.Files;
            var uploadedFile = files[0];

            //Extract the file extention
            var extension = ExtractExtension(uploadedFile);
            //Get the file's content type
            var contentType = uploadedFile.Headers.ContentType.ToString();

            //create the full name of the audio with the fileName and extension
            var audioName = string.Concat(fileName, extension);

           
               string tempPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/AudioStoreHere/");
           
            string filePath = tempPath + "\\" + audioName;

            using (var fileStream = await uploadedFile.ReadAsStreamAsync()) //as Stream is IDisposable
            {


                using (FileStream file = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, (int)fileStream.Length);
                    file.Write(bytes, 0, bytes.Length);
                    fileStream.Close();
                }
            }
            return Ok("ok");
        }

        /// <summary>
        /// Extract the file extension for the file passed
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Extension of the file</returns>
        public static string ExtractExtension(HttpContent file)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var fileStreamName = file.Headers.ContentDisposition.FileName;
            var fileName = new string(fileStreamName.Where(x => !invalidChars.Contains(x)).ToArray());
            var extension = Path.GetExtension(fileName);
            return extension;
        }
    }
}
