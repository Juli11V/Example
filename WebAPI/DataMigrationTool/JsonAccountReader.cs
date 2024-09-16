using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Domain.Entities;

namespace DataMigrationTool
{
    public class JsonAccountReader
    {
        public async Task<List<Account>> ReadAccountsFromFile(string filePath)
        {
            var jsonContent = await File.ReadAllTextAsync(filePath);
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Account>>(jsonContent, jsonOptions);
        }
    }
}
