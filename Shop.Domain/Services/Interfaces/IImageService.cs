using Microsoft.AspNetCore.Http;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image> UpLoadImageAsync(IFormCollection files);
        Task<string> RetriveImageAsync(int TargetId, int TargetType);
        Task<bool> DeleteImageAsync(int TargetId, int TargetType);
    }
}
