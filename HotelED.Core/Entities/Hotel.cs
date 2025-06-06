﻿using HotelED.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(60)]
    public required string Location { get; set; }
    [MaxLength(300)]
    public required string Description { get; set; }
    [MaxLength(20)]
    public required string Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Range(0, double.MaxValue)]
    public decimal PricePerDay { get; set; }
    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;

    public ICollection<Image> Images { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}