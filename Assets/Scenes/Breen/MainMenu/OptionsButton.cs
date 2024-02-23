using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : ButtonUI
{
    [Header("Serialize")]
    [SerializeField] private RectTransform menuRectTransform;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    private bool isOpen;
    private bool opening;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        image = gameObject.GetComponent<Image>();
        isOpen = false;
        opening = false;
    }

    override public void OnClick()
    {
        base.OnClick();

        if (!opening)
        {
            isOpen = !isOpen;
            StartCoroutine(SlideMenu());
        }
    }

    private IEnumerator SlideMenu()
    {
        Vector3 targetPosition;
        opening = true;

        // Check isOpen and go to the coresponding position
        if (isOpen)
            targetPosition = closedPosition;
        else
            targetPosition = openPosition;

        // Moving
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
        opening = false;  // So that can't press button while it is sliding
    }
}
