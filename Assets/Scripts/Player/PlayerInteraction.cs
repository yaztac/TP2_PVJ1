using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform raycastOrigin;
    
    void Update()
    {
        CheckInteractableObject();
    }

    private void CheckInteractableObject()
    {
        Vector3 origin = raycastOrigin != null ? raycastOrigin.position : transform.position + Vector3.up * 1f;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * interactionDistance, Color.green);
        

        RaycastHit hit;

       
        if (Physics.Raycast(origin, direction, out hit, interactionDistance, interactableLayer))
        {
            
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

            if (interactableObject != null)
            {
                Debug.Log("Has chocado con un interactuable");
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableObject.Interact();
                }
            }
        }
    }
}
