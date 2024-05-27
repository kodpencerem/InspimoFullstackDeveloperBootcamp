﻿using Microsoft.Extensions.Caching.Memory;
using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Models;
using PersonelApp.WebAPI.Repositories;
using PersonelApp.WebAPI.Utilities;

namespace PersonelApp.WebAPI.Services;

public sealed class PersonelService(
    IPersonelRepository personelRepository,
    IMemoryCache memoryCache) : IPersonelService
{
    public bool Create(CreatePersonelDto request)
    {
        bool isPersonelExists = personelRepository.IsPersonelExists(request.FirstName, request.LastName);

        if (isPersonelExists)
        {
            //throw new ArgumentException(JsonSerializer.Serialize(new ErrorResult("Personel already exists")));
            //throw new ArgumentException(new ErrorResult("Personel already exists").ToString());
            //throw new ArgumentException(ErrorResult.Failure("Personel already exists"));
            string errorMessage = "Personel already exists";
            throw new ArgumentException(errorMessage.ToErrorResult());
        }

        Personel personel = new Personel()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            StartingDate = request.StartingDate,
        };

        var result = personelRepository.Create(personel);

        memoryCache.Remove("personels");

        return result;
    }

    public List<Personel> GetAll()
    {
        memoryCache.TryGetValue("personels", out List<Personel>? personels);
        if (personels is null)
        {
            personels = personelRepository.GetAll();
            memoryCache.Set("personels", personels);
        }

        return personels;
    }
}
