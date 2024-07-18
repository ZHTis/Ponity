using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    public targetCharacter targetCharacter;
    public camShelfCharacter camShelf;
    private Vector3 lastPostion = Vector3.zero;
   GameObject center;


    // Start is called before the first frame update
    void Start()
    {
       center = new GameObject(); 
        camShelf.cam_as_origin = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        BuildShelf();}


    void BuildShelf(bool Debug=false)
    {   
        camShelf._camerapos.Clear();

        camShelf.location = (targetCharacter.location + targetCharacter.location_2) /2;
        center.transform.position = camShelf.location;

        float pi = (float)Math.PI;
        List<float> x = new List<float>();
        List<float> y = new List<float>();
        for (int i = 0; i <= camShelf.numberOfCams; i++)
        { 
            x.Add(Mathf.Cos((i*2 * pi)/ camShelf.numberOfCams) * camShelf.radius);
            y.Add(Mathf.Sin((i*2 * pi)/ camShelf.numberOfCams) * camShelf.radius);
            camShelf._camerapos.Add(new Vector3(x[i], camShelf.h+camShelf.neck, y[i]));  

            if (Debug == true)//show all potential cam positions.
            {
                //Debug.Log(x[i]);
                GameObject prefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                GameObject target = Instantiate(prefab, camShelf._camerapos[i],Quaternion.identity);
                target.transform.SetParent(center.transform, !camShelf.cam_as_origin );
            }
            else{}
        }

        SetCam(camShelf.camID);

    }

     void SetCam(int i)
    {
        Vector3 relativePos = camShelf._camerapos[i]*(-1);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
       if(camShelf.cam_as_origin == true)
       {transform.position = camShelf .location+camShelf._camerapos[i]; ;}
       else{transform.position = camShelf._camerapos[i]; ;}
        
        transform.rotation = rotation;   
    }



}
