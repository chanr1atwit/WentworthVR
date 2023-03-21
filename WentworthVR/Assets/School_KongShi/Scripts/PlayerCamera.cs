using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform orientation;
    private readonly Vector3 offset = new(0, 1f, 0);

    public float sensX;
    public float sensY;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        DialogueManager.instance.StartTalking += ShowMouse;
        DialogueManager.instance.StopTalking += HideMouse;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (!Cursor.visible) 
        { 
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    private void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        DialogueManager.instance.StartTalking -= ShowMouse;
        DialogueManager.instance.StopTalking -= HideMouse;
    }
}
