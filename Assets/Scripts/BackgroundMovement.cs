using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject background_;
    public float speed;
    public float countdown;
    public float numerator;
    
    void FixedUpdate()
    {
        countdown += Time.deltaTime;
        if (countdown >= (numerator / speed))
        {
            Destroy(this.gameObject);    
        }
        background_.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }
}
