using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : ButtonUI
{
    override public void OnClick()
    {
        base.OnClick();
        Application.Quit();
    }
}
