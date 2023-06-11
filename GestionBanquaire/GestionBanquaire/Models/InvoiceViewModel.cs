using System;
namespace GestionBanquaire.Models
{
    public class InvoiceViewModel
    {
        public string UserName { get; set; }
        public string TransactionType { get; set; }
        public string Receiver { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal TransactionAmount { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime DateTime { get; set; }
    }

}

