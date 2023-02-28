// See https://aka.ms/new-console-template for more information

using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

var schema =
    await JsonSchema.FromUrlAsync(
        "https://raw.githubusercontent.com/oasis-open/cti-stix2-json-schemas/master/schemas/sdos/vulnerability.json");
        
var generator = new CSharpGenerator(schema);
var code = generator.GenerateFile();

await File.WriteAllTextAsync("../../../Vulnerability.cs", code);