using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using v_conf_dn.Models;

namespace v_conf_dn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public EmailController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("sendMailWithAttachment")]
        public async Task<IActionResult> SendMailWithAttachment([FromBody] EmailDetails emailDetails)
        {
            if (emailDetails == null)
            {
                return BadRequest("Invalid email request");
            }

            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    // Add email details as individual StringContent parts
                    content.Add(new StringContent(emailDetails.Recipient), "recipient");
                    content.Add(new StringContent(emailDetails.Name), "subject");
                    content.Add(new StringContent(emailDetails.MsgBody), "msgBody");
                    content.Add(new StringContent(emailDetails.Useremail), "userEmail");

                    // Add the attachment if provided
                    if (!string.IsNullOrEmpty(emailDetails.Attachment) && System.IO.File.Exists(emailDetails.Attachment))
                    {
                        var fileStream = new FileStream(emailDetails.Attachment, FileMode.Open, FileAccess.Read);
                        var streamContent = new StreamContent(fileStream);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        content.Add(streamContent, "file", Path.GetFileName(emailDetails.Attachment));
                    }
                    else
                    {
                        return BadRequest("Attachment not found or invalid.");
                    }

                    // Forward the request to the Java microservice
                    var response = await _httpClient.PostAsync("http://localhost:8080/api/sendMailWithAttachment", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok("Email sent successfully.");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to send email via Java service.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
