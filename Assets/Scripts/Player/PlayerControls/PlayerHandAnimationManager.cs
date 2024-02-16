using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandAnimationManager : MonoBehaviour
{

    List<GameObject> transformTargets = new List<GameObject>();
    List<int> speed = new List<int>();
    bool startAnimation = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnimation == true)
        {
            if (!TargetReached(transform.position, transformTargets[index].transform.position))
            {
                transform.position = Vector3.Lerp(transform.position, transformTargets[index].transform.position, 0.5f * Time.deltaTime);
            }
            else
            {
                if (index == transformTargets.Count -1)
                {
                    startAnimation = false;
                    index = 0;
                    Debug.Log("Done");
                }
                else
                {
                    index += 1;
                }
            }
        }
    }

    public void HandAnimationUpdate()
    {
       
    }

    bool TargetReached(Vector3 ori, Vector3 target) 
    {
        //Debug.Log("OriX: " + Mathf.Abs(ori.x) + " TargetX: " + Mathf.Abs(target.x) + " | " + "OriY: " + Mathf.Abs(ori.y) + " TargetY: " + Mathf.Abs(target.y) + " | " + "OriZ: " + Mathf.Abs(ori.z) + " TargetZ: " + Mathf.Abs(target.z));

        if (Mathf.Abs(ori.x) > Mathf.Abs(target.x) - 0.1f && Mathf.Abs(ori.y) > Mathf.Abs(target.y) - 0.1f && Mathf.Abs(ori.z) > Mathf.Abs(target.z) - 0.1f)
        {
            return true;
        }
        return false;
    }

    public void PlayAnimation(List<GameObject> _gameObjects, List<int> _speeds)
    {
        transformTargets = _gameObjects;
        speed = _speeds;
        startAnimation = true;
    }
}
