using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isHovering = false;

    protected AudioManager audioManager;
    protected Image image;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        image = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (isHovering && Input.GetMouseButtonDown(0)) OnClick();
    }

    virtual public void OnClick() 
    {
        if (audioManager) audioManager.PlaySoundEffect("ButtonPressed");
        StartCoroutine(ButtonFlash());
    }

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

    protected IEnumerator ButtonFlash()
    {
        float timer = 0.1f;
        image.color = new Color(0.5f, 0, 0, 0.5f);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        image.color = new Color(1, 1, 1, 0.298f);
    }
}
