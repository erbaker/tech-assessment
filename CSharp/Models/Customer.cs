using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace CSharp.Models;

public class Address
{
    public required string StreetAddress { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
}

public class Customer
{
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required int Age { get; init; }
    public required Address Address { get; init; }
    public required string PhoneNumber { get; init; }
}