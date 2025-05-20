using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CartItemData
{
    public ProductData product;
    public int quantity;

    public CartItemData(ProductData product, int quantity = 1)
    {
        this.product = product;
        this.quantity = quantity;
    }
}