using UnityEngine;
public class RayDetector : MonoBehaviour
{
    [Header("Colliders to Ignore")]
    [SerializeField] private LayerMask toIgnore;
    [Header("To Start - Ray Origin  |  toCompare - Ray comparator")]
    [SerializeField] private Transform toStart;
    [SerializeField] private GameObject toCompare; // Player or any object that requires LOS detection
    [Header("Ray Distance")]
    [SerializeField] private float distance = 500f;
    [SerializeField] private float sphereCastRadius = 5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(toStart.position, toStart.position + toStart.transform.forward * 10);
        Gizmos.DrawWireSphere(toStart.position, 3);
    }

    // Update is called once per frame
    public bool IsDetected()
    {
        if (Physics.Raycast(toStart.position, toStart.transform.forward, out RaycastHit hit, distance, toIgnore)) // Detected Something
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject == toCompare)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Update is called once per frame
    public bool IsSurroundingDetected()
    {
        if (Physics.SphereCast(toStart.position, sphereCastRadius, toStart.transform.forward, out RaycastHit hit, distance, toIgnore)) // Detected Something
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject == toCompare)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
