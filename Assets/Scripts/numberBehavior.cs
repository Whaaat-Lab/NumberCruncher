using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberBehavior : MonoBehaviour {
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Jitter());

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            EnterCruncher();
        }

    }

    public void EnterCruncher() {
        rb.gravityScale = 1;
        StartCoroutine(MoveParent());
    }

    private IEnumerator MoveParent() {
        yield return new WaitForSeconds(1); 
        transform.parent = GameObject.Find("numberCruncher").transform;

    }

    private IEnumerator Jitter() {
        float xVariant   = Random.Range(-.05f, .05f);
        float yVariant   = Random.Range(-.05f, .05f);
        float rotVariant = Random.Range(-.05f, .05f);
        transform.position = new Vector2(
            transform.position.x + xVariant, 
            transform.position.y + yVariant
            );
        transform.rotation = new Quaternion(
            transform.rotation.x,
            transform.rotation.y,
            transform.rotation.z + rotVariant,
            transform.rotation.w
        );
        yield return new WaitForSeconds(.2f);
        StartCoroutine(Jitter());
    }
}
