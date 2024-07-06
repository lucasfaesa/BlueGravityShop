using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    [SerializeField] private SInputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.QuitGame += Quit;
    }

    private void OnDisable()
    {
        _inputReader.QuitGame -= Quit;
    }

    private void Quit(bool _)
    {
        Application.Quit();
    }
}
