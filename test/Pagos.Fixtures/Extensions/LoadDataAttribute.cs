using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using Xunit.Sdk;

namespace Pagos.Fixtures.Extensions;

public class LoadDataAttribute : DataAttribute
{
    private readonly string _fileName;
    private readonly string _section;

    public LoadDataAttribute(string fileName, string section)
    {
        this._fileName = $"./Data/{fileName}.json";
        this._section = section;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null)
            throw new ArgumentNullException(nameof(testMethod), "Parameter MethodInfo required");

        var path = Path.IsPathRooted(_fileName)
            ? _fileName
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), _fileName);

        if (!File.Exists(path))
            throw new ArgumentException($"File not found: {_fileName}");

        var fileData = File.ReadAllText(_fileName);
        if (string.IsNullOrEmpty(_section))
            return JsonConvert.DeserializeObject<List<object[]>>(fileData);

        var allData = JObject.Parse(fileData);
        var data = allData[_section];

        return new List<object[]>
            {
                new []
                {
                    data.ToObject(testMethod.GetParameters().First().ParameterType)
                }
            };
    }
}

