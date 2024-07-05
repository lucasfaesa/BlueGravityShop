using System;
using UnityEngine;

public class InteractBanner : MonoBehaviour
{
    [SerializeField] private SUIEvents uiEvents;
    [Space] 
    [SerializeField] private Transform banner;
    
    private void OnEnable()
    {
        uiEvents.ShowInteractionBanner += ShowInteractBanner;
        uiEvents.HideInteractionBanner += HideInteractBanner;
    }

    private void OnDisable()
    {
        uiEvents.ShowInteractionBanner -= ShowInteractBanner;
        uiEvents.HideInteractionBanner -= HideInteractBanner;
    }

    private void ShowInteractBanner()
    {
        banner.gameObject.SetActive(true);
    }

    private void HideInteractBanner()
    {
        banner.gameObject.SetActive(false);
    }
}
