using UnityEngine;

public class WaitForReload : MonoBehaviour
{
    public bool isReloading = false;

    public void IsReloading() 
    {
        isReloading = true;
    }

    public void NotReloading()
    {
        isReloading = false;
    }
}
