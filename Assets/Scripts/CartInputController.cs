using UnityEngine;
using UnityEngine.UI;

public class CartInputController : MonoBehaviour
{
    public GameObject cartPanel;
    public Button openCartButton;
    public PlayerMovement move;

    private bool isCartOpen = false;

    private void Awake()
    {
        if (cartPanel != null)
            cartPanel.SetActive(false);
        if (openCartButton != null)
            openCartButton.onClick.AddListener(ToggleCart);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCart();
        }
    }

    void ToggleCart()
    {
        isCartOpen = !isCartOpen;

        if (cartPanel != null)
            cartPanel.SetActive(isCartOpen);

        if (isCartOpen)
            PauseGame();
        else
            ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (move != null)
            move.enabled = false;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (move != null)
            move.enabled = true;
    }
}
