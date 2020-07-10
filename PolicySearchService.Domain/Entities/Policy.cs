namespace PolicySearchService.Domain.Entities
{
    using System;

    public class Policy
    {
        public Guid PolicyNumber { get; set; }

        public DateTimeOffset PolicyStartDate { get; set; }

        public DateTimeOffset PolicyEndDate { get; set; }

        public string ProductCode { get; set; }

        public string PolicyHolder { get; set; }

        public decimal PremiumAmount { get; set; }
    }
}
