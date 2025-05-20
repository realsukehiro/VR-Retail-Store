using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartUIManager : MonoBehaviour
{
    public GameObject cartItemPrefab; // Assign your CartItemUI prefab
    public Transform cartItemParent;  // This is the 'Content' GameObject in the ScrollView
    public static CartUIManager instance; // Singleton instance
    public TMPro.TextMeshProUGUI totalAmountText; // Assign your TextMeshProUGUI for total amount

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForCartManager());
    }

    IEnumerator WaitForCartManager()
    {
        while (CartManager.Instance == null)
            yield return null;

        RefreshCartUI();
    }

    public void RefreshCartUI()
    {
        if (CartManager.Instance == null)
        {
            Debug.LogWarning("CartManager.Instance is null — delaying cart UI update.");
            return;
        }

        // Clear old entries
        foreach (Transform child in cartItemParent)
            Destroy(child.gameObject);


        // Loop through each unique product in cart
        Dictionary<Product, int> cartItems = CartManager.Instance.GetCartItems();
        Debug.Log($"Refreshing UI. Cart contains {cartItems.Count} items.");
        
        foreach(var kvp in cartItems)
        {
            Debug.Log($"Creating UI for: {kvp.Key.productName}, quantity: {kvp.Value}");

            GameObject newItem = Instantiate(cartItemPrefab, cartItemParent);
            if (newItem == null)
            {
                Debug.LogError("Failed to instantiate cartItemPrefab!");
                continue;
            }

            CartItemUI itemUI = newItem.GetComponent<CartItemUI>();
            if (itemUI == null)
            {
                Debug.LogError("CartItemUI component is missing on the prefab!");
                continue;
            }

            itemUI.Setup(kvp.Key, kvp.Value);
        }
        UpdateTotalCost();
    }

    public void UpdateTotalCost()
    {
        if (CartUIManager.instance == null) return;

        float total = 0f;

        Dictionary<Product, int> cartItems = CartManager.Instance.GetCartItems();

        foreach (var kvp in cartItems)
        {
            total += kvp.Key.price * kvp.Value;
        }

        totalAmountText.text = $"Total: Rs.{total:F2}";

    }
}
