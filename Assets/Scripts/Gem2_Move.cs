using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem2_Move : MonoBehaviour
{
     Vector3 pointA = new Vector3(0f, 4.121f, 0f);
     Vector3 pointB = new Vector3(0f, 3.988f, 0f);

    public float speed;
     

     void Update() 
     {
         transform.localPosition = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * speed, 1));
     } 
}
