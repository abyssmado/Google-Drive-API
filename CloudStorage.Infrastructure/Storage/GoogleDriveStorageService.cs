using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using File = Google.Apis.Drive.v3.Data.File;

namespace CloudStorage.Infrastructure.Storage;

public class GoogleDriveStorageService : IStorageService
{
    private readonly GoogleAuthorizationCodeFlow _authorization;

    public GoogleDriveStorageService(GoogleAuthorizationCodeFlow authorization)
    {
        _authorization = authorization;
    }

    public string Upload(IFormFile file, User user)
    {
        var credential = new UserCredential(_authorization, user.Email, new TokenResponse
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken
        });
        var service = new DriveService(new BaseClientService.Initializer
        {
            ApplicationName = "My First Project",
            HttpClientInitializer = credential
        });
        var driveFile = new File
        {
            Name = file.Name,
            MimeType = file.ContentType
        };

        var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
        command.Fields = "Id";

        var response = command.Upload();

        if (response.Status is not UploadStatus.Completed)
            throw response.Exception;

        return command.ResponseBody.Id;
    }
}