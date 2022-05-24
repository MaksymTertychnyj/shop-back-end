using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageRepository;

        public ImageService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task<bool> DeleteImageAsync(int TargetId, int TargetType)
        {
            var image = await imageRepository
                .Query()
                .FirstOrDefaultAsync(
                    i => 
                    i.TargetId == TargetId &&
                    i.TargetType == TargetType
                );

            if (image != null)
            {
                imageRepository.Delete(image);
                await imageRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<string> RetriveImageAsync(int TargetId, int TargetType)
        {
            var image = await imageRepository
                .Query()
                .FirstOrDefaultAsync(
                    i => 
                    i.TargetId == TargetId && 
                    i.TargetType == TargetType
                    );

            if (image != null)
            {
                var base64 = Convert.ToBase64String(image.ImageData!);
                return String.Format("data: image/jpg;base64,{0}", base64);
            }

            return null!;
        }

        public async Task<Image> UpLoadImageAsync(IFormCollection form)
        {
            var image = new Image { 
                TargetId = int.Parse(form["targetId"]), 
                TargetType = int.Parse(form["targetType"]) 
            };

            var imgObj = await imageRepository
                .Query()
                .FirstOrDefaultAsync(
                    i =>
                    i.TargetId == image.TargetId &&
                    i.TargetType == image.TargetType
                    );

            if (imgObj == null)
            {
                if (form.Files.Count() > 0)
                {
                    byte[] imgData;

                    using (var filestream = form.Files[0].OpenReadStream())
                    {
                        using (var memorystream = new MemoryStream())
                        {
                            filestream.CopyTo(memorystream);
                            imgData = memorystream.ToArray();
                        }
                    }

                    if (imgData != null)
                    {
                        image.ImageData = imgData;

                        await imageRepository.AddAsync(image);
                        await imageRepository.SaveChangesAsync();

                        return image;
                    }
                }
            }

            return null!;
        }
    }
}
