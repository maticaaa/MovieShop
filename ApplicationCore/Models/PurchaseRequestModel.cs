using System;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public PurchaseRequestModel()
        {
            PurchaseDateTime = DateTime.Now;
            PurchaseNumber = Guid.NewGuid();
        }

        public Guid? PurchaseNumber { get; }
        public DateTime? PurchaseDateTime { get; }
        public int MovieId { get; set; }
    }
}