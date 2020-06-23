using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using Tdp.GeospatialConverter.Svc.Config;
using Tdp.GeospatialConverter.Svc.Handlers;

namespace Tdp.GeospatialConverter.Svc.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IGeoConvertingHandler _convertingHandler;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GeoServiceConfiguration _serviceConfiguration;

        public UploadController(IGeoConvertingHandler convertingHandler, GeoServiceConfiguration serviceConfiguration)
        {
            _convertingHandler = convertingHandler;
            _serviceConfiguration = serviceConfiguration;
        }

        [HttpPost]
        [Route("api/upload/complexpost")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            
            var root = _serviceConfiguration.SavedDataPath;
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                var uploadedFiles = new List<string>();
                // This illustrates how to get the file names.
                foreach (var file in provider.FileData)
                {
                    uploadedFiles.Add(file.LocalFileName);
                    _logger.Debug($"Upload file {file.LocalFileName}");
                }

                IDictionary<string, string> parameterDic = new Dictionary<string, string>();
                foreach (var key in provider.FormData.AllKeys)
                foreach (var val in provider.FormData.GetValues(key))
                {
                    var outputParameter = $"Form Data: {key} vs {val}";
                    parameterDic.Add(key, val);
                }

                var zipFile = _convertingHandler.ConvertingPreprocessor(uploadedFiles, parameterDic, _serviceConfiguration.LocalDataPath);

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(zipFile, FileMode.Open);
                response.Content = new StreamContent(stream);
                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "output.zip"
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}