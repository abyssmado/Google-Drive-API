using CloudStorage.Application.UseCases.Users.UploadProfilePhoto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Controller;

[Route("api/[controller]")]
[ApiController]
public class StorageController : ControllerBase
{
    [HttpPost]
    public IActionResult UploadImage([FromServices] IUploadProfilePhotoUseCase useCase, IFormFile file)
    {
        useCase.Execute(file);

        return Created();
    }
}