using System.Collections.Generic;
using UnityEngine;


public class CartManager : MonoBehaviour
{
    public static CartManager Instance;
    private Dictionary<Product, int> cartItems = new Dictionary<Product, int>();


    void Awake()
    {
        Instance = this;
    }

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            Debug.LogWarning("Tried to add null product to cart.");
            return;
        }

        if (cartItems.ContainsKey(product))
            cartItems[product]++;
        else
            cartItems[product] = 1;

        Debug.Log("Added to cart: " + product.productName + " (x" + cartItems[product] + ")");
    }

    public void UpdateProductQuantity(Product product, int quantity)
    {
        if (quantity <= 0)
            RemoveProduct(product);
        else
            cartItems[product] = quantity;
    }

    public void RemoveProduct(Product product)
    {
        cartItems.Remove(product);
    }

    public Dictionary<Product, int> GetCartItems()
    {
        return cartItems;
    }

    //public void ClearCart()
    //{
    //    cartItems.Clear();
    //}

}

