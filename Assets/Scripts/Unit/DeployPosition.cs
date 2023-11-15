using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPosition : MonoBehaviour
{
    public float angle;
    public float distance;
    public Transform PlayerCenter;
    private float playerX;
    private float playerY;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePosition()
    {
        playerX = PlayerCenter.transform.position.x;
        playerY = PlayerCenter.transform.position.y;

        float radi = angle * Mathf.Deg2Rad;
        float x = playerX + distance * Mathf.Cos(radi);
        float y = playerY + distance * Mathf.Sin(radi);

        Vector2 spawnPosition = new Vector2(x, y);
        transform.position = spawnPosition;
    }
}
