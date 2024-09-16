using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebAPI.Domain.Entities
{
    public class TransactionExternal
    {
        [XmlElement("UserId")]
        public string UserId { get; set; }

        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        [XmlElement("TransactionType")]
        public string TransactionType { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Timestamp { get; set; }
        public string ExternalTransactionId { get; set; }
    }
}
