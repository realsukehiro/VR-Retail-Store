using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI countText;
    public Button increaseButton;
    public Button decreaseButton;
    public Button deleteButton;

    private Product product;
    private int quantity;

    public void Setup(Product product, int quantity)
    {
        this.product = product;
        this.quantity = quantity;

        if (product.iconImage != null)
        {
            iconImage.sprite = product.iconImage;
        }
        else
        {
            Debug.LogError("iconImage not assigned in CartItemUI prefab.");
        }
        if (productNameText == null || priceText == null || countText == null)
        {
            Debug.LogError("UI Text components not assigned in CartItemUI prefab.");
            return;
        }
        if (product == null)
        {
            Debug.LogError("Product is null in CartItemUI.");
            return;
        }
        
        productNameText.text = product.productName;
        priceText.text = "Rs." + product.price.ToString("F2");
        UpdateCountText();

        increaseButton.onClick.AddListener(() => ChangeQuantity(1));
        decreaseButton.onClick.AddListener(() => ChangeQuantity(-1));
        deleteButton.onClick.AddListener(() => RemoveItem());
    }

    private void ChangeQuantity(int amount)
    {
        quantity += amount;
        quantity = Mathf.Max(1, quantity);
        CartManager.Instance.UpdateProductQuantity(product, quantity);
        UpdateCountText();
        CartUIManager.instance.UpdateTotalCost();
    }

    private void UpdateCountText()
    {
        countText.text = "x" + quantity;
    }

    private void RemoveItem()
    {
        CartManager.Instance.RemoveProduct(product);
        CartUIManager.instance.UpdateTotalCost();
        Destroy(gameObject);
    }
}
