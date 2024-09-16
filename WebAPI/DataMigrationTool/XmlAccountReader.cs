using System.Xml.Serialization;
using WebAPI.Domain.Entities;

namespace DataMigrationTool
{
    public class XmlAccountReader
    {
        public async Task<List<Account>> ReadAccountsFromFile(string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            var serializer = new XmlSerializer(typeof(List<Account>), new XmlRootAttribute("Accounts"));
            return (List<Account>)serializer.Deserialize(fileStream);
        }
    }
}
