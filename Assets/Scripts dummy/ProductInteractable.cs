using UnityEngine;

public class ProductInteractable : MonoBehaviour
{
    [SerializeField] private ProductData _productData;
    public ProductData ProductData => _productData;
}