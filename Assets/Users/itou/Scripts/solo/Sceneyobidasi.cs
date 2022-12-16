using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sceneyobidasi : MonoBehaviour
{
    GameObject ManageObject;
    Sceneseni fadeManager;
    public GameObject button;
    public Text karitext;

    // Start is called before the first frame update
    void Start()
    {
        ManageObject = GameObject.Find("ManageObject");
        fadeManager = ManageObject.GetComponent<Sceneseni>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("on");
        fadeManager.fadeOutStart(0, 0, 0, 0, "SampleScene");
        Destroy(button);
        Destroy(karitext);
    }
}
