using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CartView : MonoBehaviour
{
    [Header("UI References")]
    public Transform cartItemsContainer;
    public GameObject cartItemPrefab;
    public TextMeshProUGUI totalPriceText;
    public Button checkoutButton;
    public Button closeButton;

    private void Awake()
    {
        // Setup button listeners
        if (checkoutButton != null)
            checkoutButton.onClick.AddListener(OnCheckoutClicked);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnEnable()
    {
        // Subscribe to events
        GameEvents.OnCartUpdated += UpdateCartDisplay;

        // Refresh display when shown
        RefreshCartItems();
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEvents.OnCartUpdated -= UpdateCartDisplay;
    }

    public void RefreshCartItems()
    {
        CartSystem cartSystem = FindObjectOfType<CartSystem>();
        if (cartSystem != null)
        {
            UpdateCartDisplay(cartSystem.GetCartItems());
        }
    }

    private void UpdateCartDisplay(List<CartItemData> cartItems)
    {
        // Clear current items
        foreach (Transform child in cartItemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new item views
        foreach (CartItemData item in cartItems)
        {
            GameObject itemGO = Instantiate(cartItemPrefab, cartItemsContainer);
            CartItemView itemView = itemGO.GetComponent<CartItemView>();
            if (itemView != null)
            {
                itemView.Initialize(item);
            }
        }

        // Update total price
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        CartSystem cartSystem = FindObjectOfType<CartSystem>();
        if (cartSystem != null && totalPriceText != null)
        {
            float total = cartSystem.CalculateCartTotal();
            totalPriceText.text = "Total: Rs." + total.ToString("F2");
        }
    }

    private void OnCheckoutClicked()
    {
        GameEvents.CompleteCheckout();
    }

    private void OnCloseClicked()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}