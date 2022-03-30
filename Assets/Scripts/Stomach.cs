using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach : MonoBehaviour
{
    public GameObject blob;

    public static Stomach S;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateBlob();
        }
    }

    public void CreateBlob()
    {
        GameObject go = GameObject.Instantiate(blob) as GameObject;
        go.transform.parent = GameObject.Find("Blobs").transform;
        go.transform.position = new Vector2(0, 2);
    }
}
