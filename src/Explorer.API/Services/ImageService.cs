namespace Explorer.API.Services;

public class ImageService
{
    private readonly string _imageStoragePath = "wwwroot/images";

    public ImageService()
    {
        InitializeStorageDirectory();
    }

    public List<string> UploadImages(List<IFormFile> images)
    {
        var imageBytes = new List<byte[]>();

        foreach (var image in images)
        {
            using var stream = new MemoryStream();
            image.CopyTo(stream);
            imageBytes.Add(stream.ToArray());
        }

        List<string> uploadedImagePaths = new List<string>();
        
        foreach (var imageData in imageBytes)
        {
            string imageName = GenerateUniqueImageName();
            string imagePath = Path.Combine(_imageStoragePath, imageName);

            try
            {
                File.WriteAllBytes(imagePath, imageData);
                uploadedImagePaths.Add(imageName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
            }
        }

        return uploadedImagePaths;
    }

    private void InitializeStorageDirectory()
    {
        if (!Directory.Exists(_imageStoragePath))
        {
            Directory.CreateDirectory(_imageStoragePath);
        }
    }

    private string GenerateUniqueImageName()
    {
        return $"{Guid.NewGuid().ToString()}.jpg";
    }
}
