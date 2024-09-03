using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mixture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NFCController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NFCController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("Read")]
        public async Task<IActionResult> ReadNFC([FromBody] NFCRequest nfcRequest)
        {
            if (nfcRequest == null)
                return BadRequest("Invalid NFC request data.");

            if (nfcRequest.Value == 1)
            {
                // 1. التقاط الصورة من الكاميرا
                var imageBytes = CaptureImageFromCamera();

                if (imageBytes == null)
                    return StatusCode(500, "Failed to capture image from camera.");

                // 2. إرسال الصورة إلى موديل الـ AI والحصول على الوزن
                var weight = await SendImageToAIModel(imageBytes);
               // var weight = 69;

                if (weight == null)
                    return StatusCode(500, "Failed to get weight from AI model.");

                // 3. حساب الوزن الفعلي (يمكن إضافة أي منطق إضافي هنا)

                // 4. إعادة النتيجة
                return Ok(new { ActualWeight = weight });
            }
            else
            {
                // قيمة الـ NFC هي 0، يمكن تخزينها أو تجاهلها حسب الحاجة
                return Ok("NFC value is 0. No action taken.");
            }
        }

        private byte[] CaptureImageFromCamera()
        {
            try
            {
                // تغيير المسار ليشير لصورة ثابتة موجودة على جهازك
                string imagePath = "E:\\img.JPG";
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return imageBytes;
            }
            catch
            {
                Console.WriteLine("Failed to capture image from camera.");
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
                    // معالجة الأخطاء هنا
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class NFCRequest
    {
        public int Value { get; set; }
    }

    public class AIResponse
    {
        public double Weight { get; set; }
    }
}

