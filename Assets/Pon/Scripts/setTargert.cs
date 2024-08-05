using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class anotherTargert : MonoBehaviour
{   
    public targetCharacter targetCharacter;
    public ponCharacter ponCharacter;

    public Camera mainCamera ;

    private GameObject theOtherTarget;
  
    // Start is called before the first frame update
    void Start()
    {
        string name_TheOtherTarget = transform.GetChild(0).name;
        theOtherTarget = GameObject.Find(name_TheOtherTarget);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ponCharacter.isKinematic == true)
        {
        }
        if(targetCharacter.setNow == true)
        {
            SetPosition_theTarget();
            
            if (targetCharacter.makeInvisible == false)
            {
            SetPosition_theOtherTarget(targetCharacter.isParallelToViewCanvas); 
            theOtherTarget.GetComponent<MeshRenderer>().enabled = true; 
            }
            
            targetCharacter.setNow = false;}
        if (targetCharacter.makeInvisible == true)
             {makeInvisible(theOtherTarget);}

       
    }

    void SetPosition_theTarget()
    {
        transform.position = targetCharacter.location;
    }

    void SetPosition_theOtherTarget(bool parallel )
    {
        if(parallel == false){
            targetCharacter.location_2 = targetCharacter.location + targetCharacter.distance * Vector3.right *targetCharacter.flaseChoiceIsLeft;
            theOtherTarget.transform.position = targetCharacter.location_2;
        }
        if(parallel == true){//parallel to view canvas, target size are same.
                targetCharacter.location_2 = 
                targetCharacter.distance * mainCamera.GetComponent<Camera>().transform.right * targetCharacter.flaseChoiceIsLeft*(-1) 
                + targetCharacter.location;
                
                theOtherTarget.transform.position = targetCharacter.location_2;
            }
 
    }

    void makeInvisible(GameObject target)
    {
        target.GetComponent<MeshRenderer>().enabled = false;
    }
    
}
