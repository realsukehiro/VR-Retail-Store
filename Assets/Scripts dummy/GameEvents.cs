using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class GameEvents
{
    // Game state events
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    // Product events
    public static event Action<ProductData> OnProductSelected;
    public static event Action<ProductData> OnProductAddedToCart;

    // Cart events
    public static event Action<List<CartItemData>> OnCartUpdated;
    public static event Action<ProductData, int> OnCartItemQuantityChanged;
    public static event Action<ProductData> OnCartItemRemoved;
    public static event Action OnCheckoutCompleted;

    // UI events
    public static event Action OnProductUIOpened;
    public static event Action OnProductUIClosed;
    public static event Action OnCartUIToggled;

    // Methods to trigger events safely
    public static void PauseGame() => OnGamePaused?.Invoke();
    public static void ResumeGame() => OnGameResumed?.Invoke();
    public static void SelectProduct(ProductData product) => OnProductSelected?.Invoke(product);
    public static void AddProductToCart(ProductData product) => OnProductAddedToCart?.Invoke(product);
    public static void UpdateCart(List<CartItemData> items) => OnCartUpdated?.Invoke(items);
    public static void ChangeCartItemQuantity(ProductData product, int quantity) => OnCartItemQuantityChanged?.Invoke(product, quantity);
    public static void RemoveCartItem(ProductData product) => OnCartItemRemoved?.Invoke(product);
    public static void CompleteCheckout() => OnCheckoutCompleted?.Invoke();
    public static void OpenProductUI() => OnProductUIOpened?.Invoke();
    public static void CloseProductUI() => OnProductUIClosed?.Invoke();
    public static void ToggleCartUI() => OnCartUIToggled?.Invoke();
}
