using OrchardCore.Commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Abstractions;

/// <summary>
/// Service for working with <see cref="ProductPart"/>.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Returns the products that have the provided SKUs.
    /// </summary>
    Task<IEnumerable<ProductPart>> GetProductsAsync(IEnumerable<string> skus);

    /// <summary>
    /// Returns the key the product variant is identified by.
    /// </summary>
    string GetVariantKey(string sku);

    /// <summary>
    /// Returns the exact variant of a product, as well as its identifying key, associated with the provided SKU.
    /// </summary>
    Task<(PriceVariantsPart Part, string VariantKey)> GetExactVariantAsync(string sku);
}
