using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    float speed =3.0f;

    void Update() 
    {
        if (Input.GetAxis("Horizontal") !=0f)
        {
            transform.Translate(new Vector2(Input.GetAxis("Horizontal") *
            Time.deltaTime *speed, 0f));
        }
    }
}
