

namespace UploadAudioFromPowerAppsToAzure.Models
{
    public class UploadFileInfo
    {
        /// <summary>
        /// Name of the File
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// File extension
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// URL of the file
        /// </summary>
        public string FileURL { get; set; }
        /// <summary>
        /// Content Type of the uploaded file
        /// </summary>
        public string ContentType { get; set; }
    }
}