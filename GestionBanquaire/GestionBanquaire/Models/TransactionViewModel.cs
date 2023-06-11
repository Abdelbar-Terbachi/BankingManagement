namespace GestionBanquaire.Models
{
    public class TransactionViewModel
    {
        public string ReceiverNumber { get; set; }
        public  User User { get; set; }
        public InvoiceViewModel Invoice { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsSuccessful { get; set; }
        public int? RecipientId { get; set; }
    }
}
