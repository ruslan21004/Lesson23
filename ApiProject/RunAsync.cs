using HttpClientSample;
using System.Net.Http.Headers;

static async Task RunAsync()
{
    // Update port # in the following line.
    client.BaseAddress = new Uri("http://localhost:64195/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    try
    {
        // Create a new product
        Product product = new Product
        {
            Name = "Gizmo",
            Price = 100,
            Category = "Widgets"
        };

        var url = await CreateProductAsync(product);
        Console.WriteLine($"Created at {url}");

        // Get the product
        product = await GetProductAsync(url.PathAndQuery);
        ShowProduct(product);

        // Update the product
        Console.WriteLine("Updating price...");
        product.Price = 80;
        await UpdateProductAsync(product);

        // Get the updated product
        product = await GetProductAsync(url.PathAndQuery);
        ShowProduct(product);

        // Delete the product
        var statusCode = await DeleteProductAsync(product.Id);
        Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    Console.ReadLine();
}