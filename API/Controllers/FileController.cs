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
        
        var stream = file.OpenReadStream();
        var length = stream.Length;
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (length > 5242880) // 5 mb.
            return BadRequest("5mb max.");
        
        var result =await mediator.Send(new UploadFileCommand(stream, ext, length));
        return Ok(result);
    }
    
    // get all names
    // get one file
}