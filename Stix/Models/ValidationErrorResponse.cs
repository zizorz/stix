using System.Text.Json.Serialization;

namespace Stix.Models;

public record ValidationErrorResponse(
    [property: JsonPropertyName("errors")] IEnumerable<string> Errors
);