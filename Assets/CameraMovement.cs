using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    
    public GameObject player;
    private Vector3 playerPos;
    private Vector3 originalPos;
    private float yDiff;
    void Start()
    { 
        originalPos = transform.position;
        playerPos = player.transform.position;
        yDiff = originalPos.y - playerPos.y;
    }
    
    void LateUpdate()
    {
        playerPos = player.transform.position;
        transform.position =  new Vector3(playerPos.x, playerPos.y + yDiff, originalPos.z);
    }
}
