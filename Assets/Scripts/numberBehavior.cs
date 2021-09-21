using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberBehavior : MonoBehaviour
{
    public int numberVal;
    private AudioSource voice;
    private AudioLowPassFilter lpf;
    public AudioClip[] vos;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        voice = GetComponent<AudioSource>();
        lpf = GetComponent<AudioLowPassFilter>();
        StartCoroutine(Jitter());
        StartCoroutine(Speak());

    }
    
    public void EnterCruncher() {
        rb.gravityScale = 1;
        StartCoroutine(MoveParent());
    }

    private IEnumerator MoveParent() {
        yield return new WaitForSeconds(1); 
        transform.parent = GameObject.Find("numberCruncher").transform;
        lpf.cutoffFrequency = 2000f;


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

    private IEnumerator Speak()
    {
        int r = Random.Range(0, vos.Length);
        voice.clip = vos[r];
        voice.Play();
        // wait between two to four seconds
        r = Random.Range(2, 4);
        yield return new WaitForSeconds(r);
        StartCoroutine(Speak());
    }
}
