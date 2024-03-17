namespace Workshop.Api.ViewModels;

public record GoodViewModel(
    string Name,
    int Id,
    int Height,
    int Length,
    int Width,
    int Weight,
    int Count,
    double Price
);