using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorage.Application.UseCases.Users.UploadProfilePhoto;

public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService;

    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService = storageService;
    }


    public void Execute(IFormFile file)
    {
        var fileStream = file.OpenReadStream();

        var isImage = fileStream.Is<JointPhotographicExpertsGroup>();

        if (!isImage)
            throw new Exception("The file is not an image.");

        var user = GetFromDatabase();

        _storageService.Upload(file, user);
    }

    private User GetFromDatabase()
    {
        return new User
        {
            Id = 1,
            Name = "Denis Amorim",
            Email = "denisamorim.teste@gmai.com",
            RefreshToken =
                "your_refresh_token",
            AccessToken =
                "your_access_token"
        };
    }
}
