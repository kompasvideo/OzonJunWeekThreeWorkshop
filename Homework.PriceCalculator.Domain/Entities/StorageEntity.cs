using System;

namespace Homework.PriceCalculator.Domain.Entities;

public record StorageEntity(
    double Volume,
    double Price,
    DateTime At,
    double Weight);