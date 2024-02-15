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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(toStart.position, toStart.position + toStart.transform.forward * 10);
    }

    private void Update()
    {
        //if (IsDetected())
        //{
        //    Debug.Log("DETECTED PLAYER");
        //}
    }
    // Update is called once per frame
    public bool IsDetected()
    {
        if (Physics.Raycast(toStart.position, toStart.transform.forward, out RaycastHit hit, 500f, toIgnore)) // Detected Something
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject == toCompare)
            {
                // Player Detected
                //Debug.Log("TAN");
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
