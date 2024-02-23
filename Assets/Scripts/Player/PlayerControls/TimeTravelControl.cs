using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface ITimeTravelResponse
{
    void OnTimeTravel();
    void OffTimeTravel();
}

public class TimeTravelControl : MonoBehaviour
{

    bool inPresent = true;
    bool inCollision = false;
    bool deviceOut = false;

    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField] Camera altTimelineCam;

    [Header("Shader Stuff")]
    [SerializeField] private BasicPostFeature basicPostFeature;

    Coroutine pressandHold = null;

    private List<ITimeTravelResponse> ontimetravelResponses = new List<ITimeTravelResponse>();
    private List<ITimeTravelResponse> offtimetravelResponses = new List<ITimeTravelResponse>();
  
    bool activatetimeTravelAnim = false;
    
    bool thumbDownAnim = false;
    bool flickAnim = false;
    bool armWayAnim = false;

    [SerializeField] private GameObject timetravelDevice;
    [SerializeField] private Rig fingers;
    [SerializeField] private TwoBoneIKConstraint armMover;
    [SerializeField] private TwoBoneIKConstraint thumbMover2;

    [SerializeField] private ToolTipManager tooltipManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TimeTravel()
    {    
        if (Input.GetKeyDown(KeyCode.F) && pressandHold == null && playerStats.canTimeTravel == true)
        {
            if(deviceOut == false)
            {
                activatetimeTravelAnim = true;
                timetravelDevice.SetActive(true);
                fingers.weight = 1;             
            }
            pressandHold = StartCoroutine("DetectPressandHold");
        }
              

        if(activatetimeTravelAnim == true)
        {
            if (armMover.weight < 1)
            {
                armMover.weight += 2.0f * Time.deltaTime;
            }
            else
            {
                activatetimeTravelAnim = false;           
            }
        }
        else if (thumbDownAnim == true)
        {
            if (thumbMover2.weight < 1)
            {
                thumbMover2.weight += 4.0f * Time.deltaTime;
            } 
            else
            {
                basicPostFeature.TriggerShader();  // Trigger the time travel converging circle shader

                if (inPresent)
                {
                    inPresent = false;
                    Camera.main.cullingMask &= ~(1 << 9);   //Add present layermask into camera's culling mask (so that you can see all present objects)
                    Camera.main.cullingMask |= (1 << 10);   //Remove past layermask into camera's culling mask (so that you cannot see all past objects)

                    altTimelineCam.cullingMask &= ~(1 << 10);   //Do the opposite for the altTimeline camera
                    altTimelineCam.cullingMask |= (1 << 9);
                    NotifyOnTimeTravelResponse();
                }
                else
                {
                    inPresent = true;                  
                    Camera.main.cullingMask &= ~(1 << 10);  //Add past layermask into camera's culling mask (so that you can see all past objects)
                    Camera.main.cullingMask |= (1 << 9);    //Remove present layermask into camera's culling mask (so that you cannot see all present objects)

                    altTimelineCam.cullingMask &= ~(1 << 9);     //Do the opposite for the altTimeline camera
                    altTimelineCam.cullingMask |= (1 << 10);
                    NotifyOffTimeTravelResponse();
                }


                thumbDownAnim = false;
                armWayAnim = true;
            }
        }
        else if(flickAnim == true)
        {
            
        }
        else if(armWayAnim == true)
        {
            if (armMover.weight > 0)
            {
                armMover.weight -= 3.0f * Time.deltaTime;
            }
            else
            {
                armWayAnim = false;
                deviceOut = false;
                timetravelDevice.SetActive(false);
                fingers.weight = 0;
                thumbMover2.weight = 0;           
            }
        }
       
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == 9 || col.gameObject.layer == 10)
        {
            inCollision = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 9 || col.gameObject.layer == 10)
        {
            inCollision = false;
        }
    }

    private void NotifyOnTimeTravelResponse()
    {
        foreach (var ontimetravelResponse in ontimetravelResponses)
        {
            ontimetravelResponse.OnTimeTravel();
        }
    }

    public void SubscribeOnTimeTravel(ITimeTravelResponse ontimetravelResponse)
    {
        ontimetravelResponses.Add(ontimetravelResponse);
    }

    private void NotifyOffTimeTravelResponse()
    {
        foreach (var offtimetravelResponse in offtimetravelResponses)
        {
            offtimetravelResponse.OffTimeTravel();
        }
    }

    public void SubscribeOffTimeTravel(ITimeTravelResponse offtimetravelResponse)
    {
        offtimetravelResponses.Add(offtimetravelResponse);
    }

    IEnumerator DetectPressandHold()
    {
        yield return new WaitForSeconds(0.4f);    
        if(Input.GetKey(KeyCode.F))
        {
            if (deviceOut == false)
            {
                deviceOut = true;
            }
            else
            {
                deviceOut = false;
                armWayAnim = true;
            }             
        }
        else
        {
            if (inCollision == false)
            {
                thumbDownAnim = true;             
            }
            else
            {
                tooltipManager.ShowToolTip("Chrono-positional ERROR detected");
            }
        }

        pressandHold = null;
    }
}
