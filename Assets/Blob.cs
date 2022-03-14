using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float minSize;

    private Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * 300f);
            this.transform.localScale -= scaleChange;
        }
    }
}
