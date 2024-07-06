using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform pressEnterObject;
    [Space]
    [SerializeField] private SInputReader inputReader;
    [Space] 
    [SerializeField] private UnityEvent PrepareToLoadGameScene;
    
    private bool _inLoadGameScene;
    
    private void OnEnable()
    {
        inputReader.EnableInputActions();
        inputReader.Enter += LoadGameScene;
    }

    private void OnDisable()
    {
        inputReader.DisableInputActions();
        inputReader.Enter -= LoadGameScene;
    }

    void Start()
    {
        pressEnterObject.DOLocalMoveY(-380.6757f, 0.75f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void LoadGameScene(bool _)
    {
        if (_inLoadGameScene) return;

        _inLoadGameScene = true;
        
        PrepareToLoadGameScene?.Invoke();
        
        StartCoroutine(LoadSceneDelayed());
    }

    private IEnumerator LoadSceneDelayed()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainGameScene");
    }

    
}
