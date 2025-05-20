using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItemView : MonoBehaviour
{
    [Header("UI References")]
    public Image productIcon;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI quantityText;
    public Button increaseButton;
    public Button decreaseButton;
    public Button removeButton;

    private ProductData _product;
    private int _quantity;

    private void Awake()
    {
        // Setup button listeners
        if (increaseButton != null)
            increaseButton.onClick.AddListener(() => ChangeQuantity(1));

        if (decreaseButton != null)
            decreaseButton.onClick.AddListener(() => ChangeQuantity(-1));

        if (removeButton != null)
            removeButton.onClick.AddListener(RemoveItem);
    }

    public void Initialize(CartItemData cartItem)
    {
        _product = cartItem.product;
        _quantity = cartItem.quantity;

        if (productIcon != null && _product.iconImage != null)
            productIcon.sprite = _product.iconImage;

        if (productNameText != null)
            productNameText.text = _product.productName;

        if (priceText != null)
            priceText.text = "Rs." + _product.price.ToString("F2");

        UpdateQuantityText();
    }

    private void ChangeQuantity(int amount)
    {
        int newQuantity = _quantity + amount;
        newQuantity = Mathf.Max(1, newQuantity); // Ensure quantity is at least 1

        if (newQuantity != _quantity)
        {
            _quantity = newQuantity;
            UpdateQuantityText();
            GameEvents.ChangeCartItemQuantity(_product, _quantity);
        }
    }

    private void UpdateQuantityText()
    {
        if (quantityText != null)
            quantityText.text = "x" + _quantity;
    }

    private void RemoveItem()
    {
        GameEvents.RemoveCartItem(_product);
        Destroy(gameObject);
    }
}