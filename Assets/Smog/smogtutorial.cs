using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Terresquall;
using UnityEngine.SceneManagement;

public class smogtutorial : MonoBehaviour
{
    public GameObject Interlude;
    private string[] lines;
    private Text text;
    public GameObject cube;
    public Variants variants;
    public List<int> uuidOfCatchedCoins;
    public List<int> CatchedCoins;
    public int count ;


    void Awake(){
        text = Interlude.GetComponent<Text>();
       variants.Reset();
        uuidOfCatchedCoins = variants.uuidOfCatchedCoins;
        count = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Let's take a few minates to get used to the second task.";
        StartCoroutine(tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        float y = VirtualJoystick.GetAxis("Horizontal")*(-1);
        float x = VirtualJoystick.GetAxis("Vertical")*(1f);
        cube.transform.position += 2 * new Vector3(x,0,y) * Time.deltaTime;

        CatchedCoins = uuidOfCatchedCoins.Distinct().ToList();
        count = CatchedCoins.Count ;
        Debug.Log("count:"+ count);
        if(count == 1){
            variants.spawn = false;
        }
    }

    IEnumerator tutorial(){
        yield return new WaitForSeconds(3f);
         lines = new string[]
            {"移动鼠标 点住左下角白色圆盘不放",
            "同时移动鼠标",
            "这样可以 移动盒子"};
        text.text = string.Join("\n", lines);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(3f);

        lines = new string[]
            {"现在移动盒子尝试接住飞来的小球",
            };
        text.text = string.Join("\n", lines);
        yield return new WaitForSeconds(1.5f);
        variants.spawn = true;
        yield return new WaitForSeconds(10f);
        yield return new WaitUntil(() => variants.spawn == false);

        lines = new string[]
            {"你成功了！",
            "下面准备开始正式测试",
            };
        text.text = string.Join("\n", lines);
        yield return new WaitForSeconds(3f);
       
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Unloading scene:  "+ SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("smog");
        Debug.Log("UnloadDone scene:  "+ SceneManager.GetActiveScene().name);
    }

    void OnDestroy(){
        
    }
}
