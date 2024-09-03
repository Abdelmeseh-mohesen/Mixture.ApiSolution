using Mixture.Core.Entity;
using Mixture.Core.Interface;
using Mixture.Core.Repositery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Servise.Implmention
{
    public class NFCService : INFCService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGenericRepository<WeightRecord> weightRepository;
 

        public NFCService(IHttpClientFactory httpClientFactory, IGenericRepository<WeightRecord> WeightRepository)
        {
            _httpClientFactory = httpClientFactory;
            weightRepository = WeightRepository;

        }

        public async Task<double?> ProcessNFCRequestAsync(NFCRequest nfcRequest)
        {
            if (nfcRequest == null)
                return null;

            if (nfcRequest.Value == 1)
            {
                var imageBytes = CaptureImageFromCamera();

                if (imageBytes == null)
                    return null;

                var weight = await SendImageToAIModel(imageBytes);

                if (weight.HasValue)
                {
                    // تخزين الوزن في قاعدة البيانات
                    var weightRecord = new WeightRecord
                    {
                        Weight = weight.Value,
                        Timestamp = DateTime.UtcNow
                    };

                    await weightRepository.AddAsync(weightRecord);
                }

                return weight;
            }

            return null;
        }

        private byte[] CaptureImageFromCamera()
        {
            try
            {
                string imagePath = "path/to/your/image.jpg";
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return imageBytes;
            }
            catch
            {
                return null;
            }
        }

        private async Task<double?> SendImageToAIModel(byte[] imageBytes)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var requestContent = new MultipartFormDataContent();
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                requestContent.Add(imageContent, "image", "mixer.jpg");

                var response = await client.PostAsync("https://your-ai-model-endpoint/api/predict", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AIResponse>();
                    return result.Weight;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
