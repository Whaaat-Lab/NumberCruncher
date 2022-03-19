using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float minSize;
    public Sprite[] blobSprites;
    
    //how much one hit reduces size
    private Vector3 scaleChange = new Vector3(0.04f, 0.04f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        //choose a new random blob to drop in
        int b = UnityEngine.Random.Range(0, blobSprites.Length);
        GetComponent<SpriteRenderer>().sprite = blobSprites[b];
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

        if (this.transform.localPosition.y < -4f)
        {
            Intestine.S.Fill();
            Destroy(this.gameObject);
        }
    }
}
