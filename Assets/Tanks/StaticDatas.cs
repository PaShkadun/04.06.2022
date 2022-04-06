using System;
using UnityEngine;

public class StaticDatas : MonoBehaviour
{
    public static Transform player;
    public static Vector3 playerPosition;
    public static Vector3 _vector3 = new Vector3(0, 0, 0.05f);
    public static int index = -1;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = player.position;
    }
}