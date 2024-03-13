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
                "1//04S6CnwzcrJYbCgYIARAAGAQSNgF-L9Ir6hTCRCjU45xg9Nt7qx51EvgzrxgegwiMRMmiqPFsb30q2tJPuXPHn80F1N766U5WoQ",
            AccessToken =
                "ya29.a0Ad52N3-fUrpZUcK7QFUII3RyQcqoeg_2X07DXmC-B92z09LqbxTSnze3oJLJ5QHpilU5BLU5dTlPVjHXucFnvI021tfFbmMYN0_6QsKU6YGGUPEFsVwz803UHgEY3k0U_BqYOknSiH-TbyTY8JO_1UNDY0W2m5NnqMdsaCgYKAZ8SARASFQHGX2MivslhbYnnVifkQ4BN6dlKnQ0171"
        };
    }
}