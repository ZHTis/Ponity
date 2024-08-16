using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class pon : MonoBehaviour
{
    public ponCharacter  ponCharacter;
    public behaviorCenter behaviorCenter;

    private float age;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(wait(1.5f));
       GetComponent<Rigidbody>().isKinematic = false;
          GetComponent<Rigidbody>().velocity = new Vector3(ponCharacter.vel_x,ponCharacter.vel_y,0);
    }  
        
    IEnumerator wait(float time)
    {yield return new WaitForSeconds(time);}
   

    // Update is called once per frame
    void Update()
    {
        age = Time.time - behaviorCenter.initTime;
        if(age>behaviorCenter.ratio || ponCharacter.isKinematic == true)
        {
           GetComponent<Rigidbody>().isKinematic = true;
           GetComponent<Renderer>().enabled = false;
           if(age>behaviorCenter.ratio){ ponCharacter.kinematicTime = Time.time;}
        }   

    }
}

