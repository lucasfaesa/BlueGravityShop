using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image _blackFade;
    [Space] 
    [SerializeField] private bool _canFade = true;
    [SerializeField] private float _fadeTime = 1f;

    private float elapsedTime;
    
    private void Awake()
    {
        _blackFade.color = Color.black;
        FadeOut();
    }

    private void FadeOut()
    {
        _blackFade.DOFade( 0f, _fadeTime);
    }

    public void FadeIn()
    {
        _blackFade.DOFade( 1f, _fadeTime);
    }
}
