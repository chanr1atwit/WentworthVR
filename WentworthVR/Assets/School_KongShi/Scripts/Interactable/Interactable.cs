using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{
    private IInteractable interactable;
    protected bool inRange;
    protected bool isInteracting;

    protected virtual void Start()
    {
        interactable = GetComponent<IInteractable>();
        inRange = false;
        isInteracting = false;
    }

    protected virtual void Update()
    {
        if (inRange && !isInteracting && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
            isInteracting = true;
        }
    }

    private void OnTriggerEnter(Collider other) => inRange = true;

    private void OnTriggerExit(Collider other) => inRange = false;
}
