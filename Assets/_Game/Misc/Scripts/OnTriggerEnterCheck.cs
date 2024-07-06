using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterCheck : MonoBehaviour
{
    [SerializeField] private string otherTag = "Player";

    [SerializeField] private UnityEvent EnteredTriggerArea;
    [SerializeField] private UnityEvent LeftTriggerArea;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(otherTag))
            EnteredTriggerArea?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(otherTag))
            LeftTriggerArea?.Invoke();
    }
}
