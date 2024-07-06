using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [Space]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 maxPos;
    
    
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        cameraTransform.position = new Vector3(playerTransform.position.x, 
                                                    playerTransform.position.y,  
                                                        cameraTransform.position.z);
        
        float positionX = cameraTransform.position.x;
        float positionY = cameraTransform.position.y;
        float positionZ = cameraTransform.position.z;
        
        cameraTransform.position = new Vector3(Math.Clamp(positionX, minPos.x, maxPos.x),
                                                    Math.Clamp(positionY, minPos.y, maxPos.y),
                                                        positionZ);
    }
    
}
