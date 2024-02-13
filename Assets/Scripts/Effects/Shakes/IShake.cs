using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShake
{
    IEnumerator PerformShake();
    void StartShake();
    void EndShake();
}
