using System.Collections.Generic;
using UnityEngine;

public class CartSystem : MonoBehaviour
{
    private List<CartItemData> _cartItems = new List<CartItemData>();

    private void OnEnable()
    {
        // Subscribe to events
        GameEvents.OnProductAddedToCart += AddProductToCart;
        GameEvents.OnCartItemQuantityChanged += UpdateCartItemQuantity;
        GameEvents.OnCartItemRemoved += RemoveCartItem;
        GameEvents.OnCheckoutCompleted += ClearCart;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEvents.OnProductAddedToCart -= AddProductToCart;
        GameEvents.OnCartItemQuantityChanged -= UpdateCartItemQuantity;
        GameEvents.OnCartItemRemoved -= RemoveCartItem;
        GameEvents.OnCheckoutCompleted -= ClearCart;
    }

    private void AddProductToCart(ProductData product)
    {
        if (product == null) return;

        // Check if product already exists in cart
        CartItemData existingItem = _cartItems.Find(item => item.product.id == product.id);

        if (existingItem != null)
        {
            // Increment quantity
            existingItem.quantity++;
        }
        else
        {
            // Add new item
            _cartItems.Add(new CartItemData(product));
        }

        // Notify listeners that cart was updated
        GameEvents.UpdateCart(_cartItems);

        Debug.Log("Added to cart: " + product.productName);
    }

    private void UpdateCartItemQuantity(ProductData product, int quantity)
    {
        if (product == null) return;

        if (quantity <= 0)
        {
            RemoveCartItem(product);
            return;
        }

        CartItemData cartItem = _cartItems.Find(item => item.product.id == product.id);
        if (cartItem != null)
        {
            cartItem.quantity = quantity;
            GameEvents.UpdateCart(_cartItems);
        }
    }

    private void RemoveCartItem(ProductData product)
    {
        if (product == null) return;

        _cartItems.RemoveAll(item => item.product.id == product.id);
        GameEvents.UpdateCart(_cartItems);
    }

    private void ClearCart()
    {
        _cartItems.Clear();
        GameEvents.UpdateCart(_cartItems);
    }

    public List<CartItemData> GetCartItems()
    {
        return _cartItems;
    }

    public float CalculateCartTotal()
    {
        float total = 0f;
        foreach (CartItemData item in _cartItems)
        {
            total += item.product.price * item.quantity;
        }
        return total;
    }
}