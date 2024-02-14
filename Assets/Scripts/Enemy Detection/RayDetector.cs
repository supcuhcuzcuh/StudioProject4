using UnityEngine;
public class RayDetector : MonoBehaviour
{
    [Header("Colliders to Ignore")]
    [SerializeField] private LayerMask toIgnore;
    [Header("To Start - Ray Origin  |  toCompare - Ray comparator")]
    [SerializeField] private Transform toStart;

    [SerializeField] private GameObject toCompare; // Player or any object that requires LOS detection
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(toStart.position, transform.forward);
    }

    private void Update()
    {
        if (IsDetected())
        {
            Debug.Log("ONG");
        }
        Debug.DrawRay(toStart.position, toStart.transform.forward, Color.red);
    }
    // Update is called once per frame
    public bool IsDetected()
    {
        RaycastHit hit;
        if (Physics.Raycast(toStart.position, toStart.transform.forward, out hit,  100, toIgnore)) // Detected Something
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject == toCompare)
            {
                // Player Detected
                Debug.Log("PLAYER DETECTED");
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
