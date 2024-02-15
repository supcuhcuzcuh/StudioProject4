using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void TimeTravel()
   {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inPresent)
            {
                inPresent = false;
                NotifyOnTimeTravelResponse();
            }
            else
            {
                inPresent = true;
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
            offtimetravelResponse.OnTimeTravel();
        }
    }

    public void SubscribeOffTimeTravel(ITimeTravelResponse offtimetravelResponse)
    {
        offtimetravelResponses.Add(offtimetravelResponse);
    }

}
