using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace UploadAudioFromPowerAppsToAzure.Controllers
{
    public class AddFileParamTypes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId == "UploadAudio")  // SwaggerOperation
            {
                operation.consumes.Add("multipart/form-data");
                operation.parameters = new List<Parameter>
                {
                    new Parameter
                    {
                        name = "file",
                        required = true,
                        type = "file",
                        @in = "formData",
                        vendorExtensions = new Dictionary<string, object> { {"x-ms-media-kind", "audio" } }
                    }
                };
                operation.parameters.Add(new Parameter()
                {
                    name = "fileName",
                    @in = "query",
                    required = false,
                    type = "string"
                });
                operation.parameters.Add(new Parameter()
                {
                    name = "NoteGuid",
                    @in = "query",
                    required = false,
                    type = "string"
                });
                operation.parameters.Add(new Parameter()
                {
                    name = "AudioType",
                    @in = "query",
                    required = false,
                    type = "string"
                });
                operation.parameters.Add(new Parameter()
                {
                    name = "CreatedBy",
                    @in = "query",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}