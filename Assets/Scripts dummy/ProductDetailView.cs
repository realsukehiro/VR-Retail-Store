using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductDetailView : MonoBehaviour
{
    [Header("UI References")]
    public Image productImage;
    //public TextMeshProUGUI productNameText;
    public TextMeshProUGUI priceText;
    public Button addToCartButton;
    public Button closeButton;

    private ProductData _currentProduct;

    private void Awake()
    {
        // Setup button listeners
        if (addToCartButton != null)
            addToCartButton.onClick.AddListener(OnAddToCartClicked);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    public void ShowProductDetails(ProductData product)
    {
        _currentProduct = product;

        if (productImage != null && product.pamphletSprite != null)
            productImage.sprite = product.pamphletSprite;

        //if (productNameText != null)
        //    productNameText.text = product.productName;

        if (priceText != null)
            priceText.text = "Rs." + product.price.ToString("F2");
    }

    private void OnAddToCartClicked()
    {
        if (_currentProduct != null)
        {
            GameEvents.AddProductToCart(_currentProduct);
            GameEvents.CloseProductUI();
        }
    }

    private void OnCloseClicked()
    {
        GameEvents.CloseProductUI();
    }
}