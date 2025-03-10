﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication3.Comman.Models;

namespace WebApplication3.Entity.DataBase
{
    public class InvoiceHeader : CreatedUser
    {
        public int InvoiceHId { get; set; }
        public string? InvoiceName { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal? TotalAmount { get; set; }

        public int Status { get; set; } = 0;

        // Navigation property for related details

        public ICollection<InvoiceDetail>? InvoiceDetails { get; set; }
    }
}
