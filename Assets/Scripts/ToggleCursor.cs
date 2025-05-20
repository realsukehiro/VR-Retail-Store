using UnityEngine;

public class ToggleCursor : MonoBehaviour
{
    private bool cursorVisible = false;
    private PlayerMovement move;
    // Start is called before the first frame update
    void Start()
    {
        ToggleCursorClick(cursorVisible);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cursorVisible = !cursorVisible;
            ToggleCursorClick(cursorVisible);

            
        }

    }

    void ToggleCursorClick(bool visibleCursor)
    {
        if (visibleCursor)
        {
            if (move != null)
                move.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if (move != null)
                move.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
