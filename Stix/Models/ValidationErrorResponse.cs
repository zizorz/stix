using System.Text.Json.Serialization;

namespace Stix;

public record ValidationErrorResponse(
    [property: JsonPropertyName("errors")] IEnumerable<string> Errors
);