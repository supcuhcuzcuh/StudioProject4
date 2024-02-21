using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isHovering = false;

    protected AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (isHovering && Input.GetMouseButtonDown(0)) OnClick();
    }

    virtual public void OnClick() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioManager) audioManager.PlaySoundEffect("ButtonHover");

        isHovering = true;
        transform.localScale = new Vector2(1.1f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        transform.localScale = new Vector2(1, 1);
    }
}
