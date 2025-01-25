using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;

namespace CSharp.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered,
    Cancelled
}

public class OrderCreateDto
{
    [Required]
    public required string CustomerId { get; set; }

    [Required]
    public required string ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
}

public class OrderUpdateDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int? Quantity { get; set; }

    public OrderStatus? Status { get; set; }
}

public class Order
{
    [Required]
    public required string Id { get; set; }

    [Required]
    public required string CustomerId { get; set; }

    [Required]
    public required string ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}