using System.Collections;
using Terresquall;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NewSmogBehaviour : MonoBehaviour
{
    public GameObject cube;
    public GameObject cannon;
    public Vector3[] cannonPos;
    public Variants smogVariants;

    private int bowlFilledWith;
    private bool bowlFullfilled;


    public int count ;
    public List<int> uuidOfCatchedCoins;
    public List<int> CatchedCoins;



    void Awake(){
        cannonPos = new Vector3[]{new Vector3(-17,0.9f,5),
                new Vector3(-18,0.9f,-6),
                new Vector3(-15,0.9f,-6)};

        smogVariants.uuidOfCatchedCoins = new List<int>();
        uuidOfCatchedCoins = smogVariants.uuidOfCatchedCoins;
        count = 0;
    }

    // Start is called before the first frame updatSS
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = VirtualJoystick.GetAxis("Horizontal")*(-1);
        float x = VirtualJoystick.GetAxis("Vertical")*(1f);
        cube.transform.position += 2 * new Vector3(x,0,y) * Time.deltaTime;
        
        CatchedCoins = uuidOfCatchedCoins.Distinct().ToList();
        count = CatchedCoins.Count ;
        Debug.Log(count);
        
    }


    IEnumerator Thisisit(){
        for(int j=0; j<cannonPos.Length; j++){
            cannon.transform.position = cannonPos[j];
            yield return null;  //WaitUntil(()=>bowlFullfilled);
        }
    }


}
