using System.Text.Json.Serialization;

namespace Stix.Models;

public record ValidationErrorDetails(
    [property: JsonPropertyName("errors")] IEnumerable<string> Errors
);