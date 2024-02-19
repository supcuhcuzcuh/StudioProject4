using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandAnimations : MonoBehaviour
{
    [SerializeField] Transform animationTargetGroup;
    [SerializeField]  List<GameObject> animationTargets = new List<GameObject>();

    private void Start()
    {
        //for (int i = 0; i < animationTargetGroup.childCount; i++)
        //{
        //    animationTargets.Add(animationTargetGroup.GetChild(i).gameObject);
        //}
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHandAnimationManager>().PlayAnimation(animationTargets);
        }
    }
}
