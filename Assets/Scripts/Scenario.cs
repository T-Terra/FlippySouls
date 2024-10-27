using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Scenario : MonoBehaviour
{
   public List<GameObject> backgrounds;
   BackgroundMovement bg;
   private float speed;
   private float cooldown;
   private float countdown = 0; 
   private int multiplier = 2;

    void Start()
    {   
        bg = GetComponentInChildren<BackgroundMovement>();
        
        if (bg != null)
        {
            speed = bg.speed;
        }

        else
        {
            Debug.LogError("Componente BackgroundMovement não encontrado no GameObject!");
            return;
        }

        /*for (int i = 0; i < backgrounds.Count; i++)
        {
            Instantiate(backgrounds[i], new Vector3(1.5f, 0, 0), Quaternion.identity);
        }*/
        cooldown = 20.75f / speed;      
    
    }

    
    void FixedUpdate()
    {

        countdown += Time.deltaTime;
        instantiateBackground();
    }

    void instantiateBackground()
    {   
        
        if (countdown >= cooldown){
            for (int i = 0; i < backgrounds.Count; i++)
            {    
                Instantiate(backgrounds[i], new Vector3(3f, 0, 0), Quaternion.identity);
            }
            cooldown = cooldown * multiplier;
            countdown = 0;
            multiplier = 1;
        }
    }    
}
