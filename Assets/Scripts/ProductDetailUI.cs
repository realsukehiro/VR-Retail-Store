using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductDetailUI : MonoBehaviour
{
    public static ProductDetailUI Instance;

    [Header("UI References")]                       
    public Image pamphletImage;
    public TMPro.TextMeshProUGUI priceText;
    private Product currentProduct;
    public GameObject panel;
    public PlayerMovement move;

    void Awake()
    {
        Instance = this;
        HideDetails();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && panel.activeSelf)
        {
            HideDetails();
        }   
    }
    public void ShowDetails(Product product)
    {
        ////pause movement in the background
        //if (move != null)
        //    move.enabled = false;


        currentProduct = product;
        int newprice = Mathf.RoundToInt(product.price);
        priceText.text = "Rs." + newprice.ToString();

        if (pamphletImage != null && product.pamphletSprite != null)
        {
            pamphletImage.sprite = product.pamphletSprite;
        }
        else
        {
            Debug.LogError("pamphletImage or product.pamphletSprite is null!");
        }

        pamphletImage.sprite = product.pamphletSprite;
        panel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
    }

    public void HideDetails()
    {
        ////continue movement in the background
        //if (move != null)
        //    move.enabled = true;


        panel.SetActive(false);
        currentProduct = null;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        
    }

    public void OnAddToCart()
    {
        Debug.Log("Add to cart button clicked.");
        currentProduct?.AddToCart();
        HideDetails();
    }
}

