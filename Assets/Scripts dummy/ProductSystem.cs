using System.Collections.Generic;
using UnityEngine;

public class ProductSystem : MonoBehaviour
{
    [SerializeField] private List<ProductData> _availableProducts = new List<ProductData>();

    public List<ProductData> GetAvailableProducts()
    {
        return _availableProducts;
    }

    public ProductData GetProductById(string id)
    {
        return _availableProducts.Find(p => p.id == id);
    }
}