using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject productDetailPanel;
    public GameObject cartPanel;
    public GameObject checkoutConfirmationPanel;

    private void Awake()
    {
        // Initially hide all panels
        if (productDetailPanel != null) productDetailPanel.SetActive(false);
        if (cartPanel != null) cartPanel.SetActive(false);
        if (checkoutConfirmationPanel != null) checkoutConfirmationPanel.SetActive(false);
    }

    private void OnEnable()
    {
        // Subscribe to events
        GameEvents.OnProductSelected += ShowProductDetail;
        GameEvents.OnProductUIClosed += HideProductDetail;
        GameEvents.OnCartUIToggled += ToggleCartPanel;
        GameEvents.OnCheckoutCompleted += ShowCheckoutConfirmation;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEvents.OnProductSelected -= ShowProductDetail;
        GameEvents.OnProductUIClosed -= HideProductDetail;
        GameEvents.OnCartUIToggled -= ToggleCartPanel;
        GameEvents.OnCheckoutCompleted -= ShowCheckoutConfirmation;
    }

    private void ShowProductDetail(ProductData product)
    {
        if (productDetailPanel != null)
        {
            productDetailPanel.SetActive(true);
            ProductDetailView productDetailView = productDetailPanel.GetComponent<ProductDetailView>();
            if (productDetailView != null)
                productDetailView.ShowProductDetails(product);

            GameEvents.OpenProductUI();
        }
    }

    private void HideProductDetail()
    {
        if (productDetailPanel != null)
            productDetailPanel.SetActive(false);
    }

    private void ToggleCartPanel()
    {
        if (cartPanel != null)
        {
            bool newState = !cartPanel.activeSelf;
            cartPanel.SetActive(newState);

            if (newState)
            {
                // Refresh cart UI
                CartView cartView = cartPanel.GetComponent<CartView>();
                if (cartView != null)
                    cartView.RefreshCartItems();
            }
        }
    }

    private void ShowCheckoutConfirmation()
    {
        if (cartPanel != null)
            cartPanel.SetActive(false);

        if (checkoutConfirmationPanel != null)
            checkoutConfirmationPanel.SetActive(true);
    }

    public void CloseCheckoutConfirmation()
    {
        if (checkoutConfirmationPanel != null)
            checkoutConfirmationPanel.SetActive(false);

        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}