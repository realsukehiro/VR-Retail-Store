using UnityEngine;
using UnityEngine.UI;

public class CheckoutConfirmationView : MonoBehaviour
{
    public Button closeButton;

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnCloseClicked()
    {
        // Find UI Manager and close this panel
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
            uiManager.CloseCheckoutConfirmation();
    }
}