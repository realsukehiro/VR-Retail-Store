using UnityEngine;

public class CartCheckoutHandler : MonoBehaviour
{
    public GameObject confirmationPanel; // Assign in Inspector
    public GameObject cartPanel; // Optional: to hide cart

    public void OnBuyButtonClicked()
    {
        Debug.Log("Buy button clicked.");

        // Optionally clear the cart
        CartManager.Instance.GetCartItems().Clear();
        CartUIManager.instance.RefreshCartUI();

        // Hide cart and show confirmation
        if (cartPanel != null) cartPanel.SetActive(false);
        if (confirmationPanel != null) confirmationPanel.SetActive(true);

        //Time.timeScale = 0f; // Pause game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnCloseConfirmation()
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(false);

        //Time.timeScale = 1f; // Resume game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
