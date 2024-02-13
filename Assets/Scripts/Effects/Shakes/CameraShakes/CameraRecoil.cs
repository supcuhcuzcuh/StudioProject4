using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour, IShootResponse
{
    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] GunController gunController;

    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);  //return to original rotation
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);  //between current and orignal rotation
        transform.localRotation = Quaternion.Euler(currentRotation);       
    }

    public void OnMouse1()
    {
        if (gunController.currWeapon.clipSizeCurr > 0)
        {           
            targetRotation += transform.right * -(gunController.currWeapon.GetRecoilStrength()); //push up camera rotation
        }

    }

    public void OnMouse2()
    {

    }
}
