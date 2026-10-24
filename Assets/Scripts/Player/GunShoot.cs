using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private float rayDistance;
    private Vector3 origin;
    private Vector3 direction;
    
    private bool isDestructible = false;
    private RaycastHit hit;
    [SerializeField] private Vector3 offset;
    private bool hasHit = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            //origin = gun.position + offset; //local
            origin = gun.TransformPoint(offset); //global

            direction = gun.up;
            hasHit = Physics.Raycast(origin, direction, out hit, rayDistance);

            if (hasHit)
            {
                isDestructible = hit.collider.CompareTag("Destructible");
            }
            else
            {
                isDestructible = false;
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isDestructible ? Color.green : Color.red;
        if(hasHit && hit.point != Vector3.zero)
        {
            Gizmos.DrawLine(origin, hit.point);
        }
        else
        {
            Gizmos.DrawLine(origin, origin + (gun.up * rayDistance));
        }
    }
}
