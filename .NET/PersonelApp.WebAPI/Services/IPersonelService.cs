using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Models;

namespace PersonelApp.WebAPI.Services;

public interface IPersonelService
{
    List<Personel> GetAll();
    bool Create(CreatePersonelDto request);
}
