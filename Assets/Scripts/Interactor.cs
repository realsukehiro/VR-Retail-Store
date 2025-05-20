using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 10f;

    void Start()
    {
        if (InteractorSource == null)
        {
            InteractorSource = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Product product = hitInfo.collider.GetComponent<Product>();     
                if (product != null)                                            
                {                                                               
                    product.Interact();                                      
                }                                                               
            }
        }
    }
}
