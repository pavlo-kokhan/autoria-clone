using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Enums;
using AutoriaClone.Domain.Aggregates.Entities.Advertisement.Flags;

namespace AutoriaClone.Domain.Aggregates.Entities.Advertisement.ValueObjects;

public record VehicleDetailsValueObject(
    int CategoryId,
    int MakeId,
    int ModelId,
    int GenerationId,
    int Year,
    int RunAmount,
    string VinCode,
    TransmissionType Transmission,
    FuelType Fuel,
    PowerValueObject Power,
    FuelConsumptionValueObject FuelConsumption,
    float EngineVolume,
    EuroStandardType EuroStandard,
    VehicleDriveType VehicleDrive,
    int DoorsCount,
    ColorShadeType ColorShade,
    int PassengersCount,
    bool WasInAccident,
    PaintworkConditionType PaintworkCondition,
    ConditionType Condition,
    ComfortOptions ComfortOptions,
    SafetyOptions SafetyOptions,
    MultimediaOptions MultimediaOptions);