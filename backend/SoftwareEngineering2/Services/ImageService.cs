using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Services;

public class ImageService : IImageService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageRepository _imageRepository;
    private IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly IMapper _mapper;

    public ImageService(
        IUnitOfWork unitOfWork,
        IImageRepository imageRepository,
        string bucketName,
        RegionEndpoint regionEndpoint,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _imageRepository = imageRepository;
        _bucketName = bucketName;
        _s3Client = new AmazonS3Client(regionEndpoint);
        _mapper = mapper;
    }

    public async Task<ImageDTO> UploadImageAsync(NewImageDTO image) {
        var uploadRequest = new PutObjectRequest() {
            BucketName = _bucketName,
            Key = Guid.NewGuid().ToString(),
            ContentBody = image.base64Image
        };
        
        var response = await _s3Client.PutObjectAsync(uploadRequest);
        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK) {
            throw new Exception("Failed to upload image");
        }

        var model = new ImageModel() {
            ImageUri = new Uri($"https://{_bucketName}.s3.amazonaws.com/{uploadRequest.Key}")
        };
        
        await _imageRepository.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ImageDTO>(model);
    }
    
    public async Task<ImageDTO?> GetImageByIdAsync(int imageId) {
        var result = await _imageRepository.GetByIdAsync(imageId);
        return result != null ? 
            _mapper.Map<ImageDTO>(result) : 
            null;
    }

    public async Task DeleteImageAsync(int imageId) {
        var image = await _imageRepository.GetByIdAsync(imageId) ?? throw new KeyNotFoundException("Image not found");
        var objectName = image.ImageUri.Segments.Last();
        
        var request = new DeleteObjectRequest {
            BucketName = _bucketName,
            Key = objectName
        };

        var response = await _s3Client.DeleteObjectAsync(request);
        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK) {
            throw new Exception("Failed to delete image");
        }
        
        _imageRepository.Delete(image);
        await _unitOfWork.SaveChangesAsync();
    }
}