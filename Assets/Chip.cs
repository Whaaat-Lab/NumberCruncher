using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Awake()
    {
        float xForce = Random.Range(200f, 400f);
        float r = Random.Range(0, 100);
        int xDir = 0;
        if (r < 50) xDir = 1;
        else xDir = -1;
        float yForce = Random.Range(400f, 600f);
        float zRot = Random.Range(0, 360);

        transform.rotation = Quaternion.Euler(0, 0, zRot);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Debug.Log(xForce * xDir);
        rb.AddForce(new Vector2(xForce * xDir, yForce));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.y < -8)
        {
            Destroy(this.gameObject);
        }
    }
}
