using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Move : MonoBehaviour
{

     Vector3 pointA = new Vector3(6.256599f, -5.110321f, 0f);
     Vector3 pointB = new Vector3(6.256599f, -5.22f, 0f);

    public float speed;
     

     void Update() 
     {
         transform.localPosition = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * speed, 1));
     } 

}
