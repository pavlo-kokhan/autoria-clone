using AutoriaClone.Domain.Aggregates.Abstract;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;
using AutoriaClone.Domain.Aggregates.Validation;
using AutoriaClone.Domain.Results.Generic;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.Root;

public class AdvertisementEntity : PersistenceEntity, IUserRelatedEntity, ITimeRelatedEntity
{
    private static readonly AdvertisementEntityValidator Validator = new();

    private List<int> _imageIds;
    
    private AdvertisementEntity(
        int userId,
        DateTime createdAt,
        DateTime updatedAt,
        string title,
        string description,
        string? youtubeVideoUrl,
        IReadOnlySet<int> imageIds,
        AdvertisementStatusType status,
        PriceValueObject lastPrice,
        string regionRef,
        string cityRef,
        bool isBargainAvailable,
        bool isExchangeAvailable,
        bool isPartsPaymentAvailable,
        bool isBusinessCallAvailable,
        VehicleDetailsValueObject vehicleDetails)
    {
        UserId = userId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Title = title;
        Description = description;
        YoutubeVideoUrl = youtubeVideoUrl;
        _imageIds = imageIds.ToList();
        Status = status;
        LastPrice = lastPrice;
        RegionRef = regionRef;
        CityRef = cityRef;
        IsBargainAvailable = isBargainAvailable;
        IsExchangeAvailable = isExchangeAvailable;
        IsPartsPaymentAvailable = isPartsPaymentAvailable;
        IsBusinessCallAvailable = isBusinessCallAvailable;
        VehicleDetails = vehicleDetails;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public AdvertisementEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    { }

    // user related
    public int UserId { get; private set; }

    // time related
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }

    // properties
    public string Title { get; private set; }
    
    public string Description { get; private set; }
    
    public string? YoutubeVideoUrl { get; private set; }

    public IReadOnlyCollection<int> ImageIds 
        => _imageIds;
    
    public AdvertisementStatusType Status { get; private set; }
    
    public PriceValueObject LastPrice { get; private set; }
    
    public string RegionRef { get; private set; }
    
    public string CityRef { get; private set; }
    
    public bool IsBargainAvailable { get; private set; }
    
    public bool IsExchangeAvailable { get; private set; }
    
    public bool IsPartsPaymentAvailable { get; private set; }
    
    public bool IsBusinessCallAvailable { get; private set; }

    // details value object
    public VehicleDetailsValueObject VehicleDetails { get; private set; }
    
    public static Result<AdvertisementEntity> Create(
        int userId,
        string title,
        string description,
        string? youtubeVideoUrl,
        IReadOnlySet<int> imageIds,
        AdvertisementStatusType status,
        PriceValueObject lastPrice,
        string regionRef,
        string cityRef,
        bool isBargainAvailable,
        bool isExchangeAvailable,
        bool isPartsPaymentAvailable,
        bool isBusinessCallAvailable,
        VehicleDetailsValueObject vehicleDetails)
    {
        var entity = new AdvertisementEntity(
            userId,
            DateTime.UtcNow, 
            DateTime.UtcNow, 
            title,
            description,
            youtubeVideoUrl,
            imageIds,
            status,
            lastPrice,
            regionRef,
            cityRef,
            isBargainAvailable,
            isExchangeAvailable,
            isPartsPaymentAvailable,
            isBusinessCallAvailable,
            vehicleDetails);

        return Validator.ToResult(entity);
    }
}