using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    private TMP_Text toolTip;

    // Start is called before the first frame update
    void Start()
    {
        toolTip = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowToolTip(string _text)
    {
        toolTip.text = _text;
        StartCoroutine("FadeEffect");
    }

    IEnumerator FadeEffect()
    {
        yield return new WaitForSeconds(2.0f);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            toolTip.color = new Color(toolTip.color.r, toolTip.color.g, toolTip.color.b, i);
            yield return null;
        }

        toolTip.text = "";
        toolTip.color = new Color(toolTip.color.r, toolTip.color.g, toolTip.color.b, 1);
    }
}
