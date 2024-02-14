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

    private List<ITimeTravelResponse> ontimetravelResponses = new List<ITimeTravelResponse>();
    private List<ITimeTravelResponse> offtimetravelResponses = new List<ITimeTravelResponse>();

    bool activatetimeTravelAnim = false;
    bool reverseAnim = false;

    [SerializeField] private GameObject timetravelDevice;
    [SerializeField] private Rig fingers;
    [SerializeField] private TwoBoneIKConstraint armMover;
    [SerializeField] private TwoBoneIKConstraint thumbMover2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void TimeTravel()
   {
        if (Input.GetKeyDown(KeyCode.F) && activatetimeTravelAnim == false)
        {
                timetravelDevice.SetActive(true);
                fingers.weight = 1;
                activatetimeTravelAnim = true;                             
        }

        if(activatetimeTravelAnim == true)
        {
            if (armMover.weight < 1)
            {
                armMover.weight += 2.0f * Time.deltaTime;
            }
            else if (thumbMover2.weight < 1)
            {
                thumbMover2.weight += 2.0f * Time.deltaTime;
            }
            else
            {
                reverseAnim = true;
                activatetimeTravelAnim = false;
                if (inPresent)
                {
                    inPresent = false;
                    NotifyOnTimeTravelResponse();
                }
                else
                {
                    inPresent = true;
                    NotifyOffTimeTravelResponse();
                }
            }
        }
        else if (reverseAnim == true)
        {            
            if (armMover.weight > 0)
            {
                armMover.weight -= 3.0f * Time.deltaTime;
            }
            else
            {
                timetravelDevice.SetActive(false);
                fingers.weight = 0;
                thumbMover2.weight = 0;
                reverseAnim = false;
            }       
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

}
