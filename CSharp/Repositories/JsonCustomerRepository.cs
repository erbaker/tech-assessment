using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSharp.Models;

namespace CSharp.Repositories;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _customersFilePath;
    private readonly ILogger<JsonCustomerRepository> _logger;
    private List<Customer>? _customers;

    public JsonCustomerRepository(IHostEnvironment environment, ILogger<JsonCustomerRepository> logger)
    {
        var expectedPath = Path.Combine(environment.ContentRootPath, "Data", "customers.json");
        // Validate the path is exactly what we expect
        var normalizedPath = Path.GetFullPath(expectedPath);
        if (!normalizedPath.EndsWith(Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "customers.json"))
        {
            throw new InvalidOperationException("Invalid file path");
        }
        _customersFilePath = normalizedPath;
        _logger = logger;
    }

    private async Task LoadCustomersAsync()
    {
        try
        {
            var jsonString = await File.ReadAllTextAsync(_customersFilePath);
            _customers = JsonSerializer.Deserialize<List<Customer>>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading customers from JSON file");
            _customers = new List<Customer>();
        }
    }

    public async Task<Customer?> GetCustomerAsync(string customerId)
    {
        await LoadCustomersAsync();
        return _customers?.FirstOrDefault(c => c.Id == customerId);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        await LoadCustomersAsync();
        return _customers ?? Enumerable.Empty<Customer>();
    }
}