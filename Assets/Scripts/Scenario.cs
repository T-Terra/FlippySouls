using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Scenario : MonoBehaviour
{
   public GameObject background;
   public float speed;
   private float cooldown;
   private float countdown = 0; 

    void Start()
    {
        cooldown = 18f / speed;
        Instantiate(background, new Vector3(0.8f, 0, 0), Quaternion.identity);
    }

    
    void FixedUpdate()
    {
        instantiateBackground();
    }

    void instantiateBackground()
    {
        countdown += Time.deltaTime;
        if (countdown >= cooldown){
            Instantiate(background, new Vector3(0.8f, 0, 0), Quaternion.identity);
            countdown = 0;
        }
    }    
}
