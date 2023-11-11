namespace Explorer.API.Services;

public class ImageService
{
    private const string ImageStoragePath = "wwwroot/images";

    public ImageService()
    {
        InitializeStorageDirectory();
    }

    public List<string> UploadImages(List<IFormFile> images)
    {
        var uploadedImageNames = new List<string>();

        foreach (var image in images)
        {
            try
            {
                var imageName = SaveImage(image);
                uploadedImageNames.Add(imageName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
            }
        }

        return uploadedImageNames;
    }

    private string SaveImage(IFormFile image)
    {
        using var stream = new MemoryStream();
        image.CopyTo(stream);

        var imageName = GenerateUniqueImageName();
        var imagePath = Path.Combine(ImageStoragePath, imageName);
        File.WriteAllBytes(imagePath, stream.ToArray());

        return imageName;
    }


    private void InitializeStorageDirectory()
    {
        if (!Directory.Exists(ImageStoragePath))
        {
            Directory.CreateDirectory(ImageStoragePath);
        }
    }

    private string GenerateUniqueImageName()
    {
        return $"{Guid.NewGuid().ToString()}.jpg";
    }
}
