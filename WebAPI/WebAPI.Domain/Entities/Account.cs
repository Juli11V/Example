using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WebAPI.Domain.Entities;
[XmlRoot("Account")]
public class Account : BaseEntity
{
    [XmlElement("UserId")]
    public string UserId { get; set; }

    [XmlElement("Currency")]
    public string Currency { get; set; }

    [XmlElement("Balance")]
    public decimal Balance { get; set; }

    [XmlIgnore]
    public virtual ApplicationUser User { get; set; }

    [InverseProperty("FromAccount")]
    public virtual ICollection<TransferTransaction> OutgoingTransactions { get; set; } = new List<TransferTransaction>();

    [InverseProperty("ToAccount")]
    public virtual ICollection<TransferTransaction> IncomingTransferTransactions { get; set; } = new List<TransferTransaction>();
    [XmlArray("Transactions")]
    public List<Transaction> Transactions { get; set; }
}
