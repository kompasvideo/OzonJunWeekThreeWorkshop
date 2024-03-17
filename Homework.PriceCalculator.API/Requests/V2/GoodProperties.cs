using System.ComponentModel.DataAnnotations;

namespace Workshop.Api.Requests.V2;

public record GoodProperties
(
  [Range(1, Int32.MaxValue)]
  int length,
  [Range(1, Int32.MaxValue)]
  int width,
  [Range(1, Int32.MaxValue)]
  int height,
  [Range(1, double.MaxValue)]
  double weight 
);