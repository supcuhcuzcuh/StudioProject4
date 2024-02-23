using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton : ButtonUI
{
    [Header("Scene To Go To Onclick")]
    [SerializeField] private string sceneName;

    public override void OnClick()
    {
        base.OnClick();
        SceneManager.LoadScene(sceneName);
    }
}
