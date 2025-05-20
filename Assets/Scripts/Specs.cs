using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Specs : MonoBehaviour
{
    public string specs;
    public void Interact()
    {
        Debug.Log($"{gameObject.name} Specs: {specs}");
    }
}
