using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : ButtonUI
{
    [Header("Serialize")]
    [SerializeField] private RectTransform menuRectTransform;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    private bool isOpen;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        isOpen = false;
    }

    override public void OnClick()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(SlideMenu(closedPosition));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(SlideMenu(openPosition));
        }
    }

    private IEnumerator SlideMenu(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = menuRectTransform.anchoredPosition3D;

        while (elapsedTime < slideDuration)
        {
            float t = elapsedTime / slideDuration;

            // Use EaseInOutQuad interpolation
            t = t * t * (3f - 2f * t);

            menuRectTransform.anchoredPosition3D = Vector3.Lerp(startingPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is exact
        menuRectTransform.anchoredPosition3D = targetPosition;
    }
}
