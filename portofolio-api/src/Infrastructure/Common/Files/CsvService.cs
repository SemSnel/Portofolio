
using System.Dynamic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using SemSnel.Portofolio.Application.Common.Files;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Infrastructure.Common.Files;

public class CsvService : ICsvService
{
    public async Task<ErrorOr<FileDto>> Export<TData>(IEnumerable<TData> data
        , Dictionary<string, Func<TData, object>> mappers
        , string sheetName = "Sheet1")
    {
        var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            Encoding = Encoding.UTF8,
            
        };
        
        var records = new List<ExpandoObject>();
        
        // add the headers
        var headers = new List<string>();
        
        foreach (var mapper in mappers)
        {
            headers.Add(mapper.Key);
        }

        // add the data to the records
        foreach (var item in data)
        {
            // for each mapper, create a new record
            var record = new ExpandoObject() as IDictionary<string, object>;
            
            foreach (var mapper in mappers)
            {
                record.Add(mapper.Key, mapper.Value(item));
            }
            
            records.Add((ExpandoObject) record);
        }

        using var memoryStream = new MemoryStream();
        await using (var writer = new StreamWriter(memoryStream))
        await using (var csvWriter = new CsvWriter(writer, config))
        {
            
            // write the headers
            foreach (var header in headers)
            {
                csvWriter.WriteField(header);
            }
            
            await csvWriter.NextRecordAsync();
            
            
            foreach (var record in records)
            {
                csvWriter.WriteRecord(record);
                
                await csvWriter.NextRecordAsync();
            }
        }

        var content = memoryStream.ToArray();
        
        var base64 = Convert.ToBase64String(content);

        return new FileDto
        {
            Name = $"{sheetName}.csv",
            Path = "", // Fill in the path if you want to save the file to the disk
            ContentType = "text/csv",
            Content = content
        };
    }
}