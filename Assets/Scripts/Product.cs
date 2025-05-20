using UnityEngine;
using UnityEngine.UI;


public class Product : MonoBehaviour
{
    public string productName;
    public float price;
    public Sprite pamphletSprite;
    public Sprite iconImage;

    public override bool Equals(object obj)
    {
        if (obj is Product other)
            return productName == other.productName;
        return false;
    }

    public override int GetHashCode()
    {
        return productName.GetHashCode();
    }



    public void Interact()
    {

        if (ProductDetailUI.Instance != null)                                         
        {                                                                               
            ProductDetailUI.Instance.ShowDetails(this);                                  
        }                                                                                  
        else                                                                               
        {                                                                                
            Debug.LogError("ProductDetailUI.Instance is null.");                         
        }                                                                                 
    }

    public void AddToCart()
    {
        CartManager.Instance.AddProduct(this);
        Debug.Log(productName + " added to cart.");
    }
}
