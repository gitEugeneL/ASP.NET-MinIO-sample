using Application.Files.Commands.DownloadFile;
using Application.Files.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/file")]
public class FileController(ISender mediator) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult> Upload(IFormFile file)
    {
        /*
         * [!]
         * You should add a file signature validator to your production API
         * https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-8.0#file-signature-validation
         * [!]
        */
        if (file.ContentType is not ("image/png" or "image/jpeg"))
            return BadRequest("png or jpg");
        
        if (file.Length > 524288000) // 50 mb.
            return BadRequest("50mb max.");
        
        var stream = file.OpenReadStream();
        var result = await mediator
            .Send(new UploadFileCommand(stream, file.FileName, file.ContentType, file.Length));
        return Ok(result);
    }

    [HttpGet("download")]
    public async Task<ActionResult> Download([FromQuery] DownloadFileCommand command)
    {
        var result = await mediator.Send(command);
        return File(result.MemoryStream, result.ContentType, result.FileName);
    }
    
    // todo get all names in bucket
}
