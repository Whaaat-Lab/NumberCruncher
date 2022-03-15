using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public float awakeAMT;
    public float sleepSpeed;
    public float sleepTimer = 10f;


    private SpriteRenderer sleepyFade;
    
    private float startTime;
    private float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        awakeAMT = 0;
        startTime = Time.time;
        sleepyFade = GameObject.Find("SleepyFade").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime > sleepTimer)
        {
            awakeAMT += sleepSpeed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Wake Up!
            startTime = Time.time;
            awakeAMT += .1f;
            awakeAMT = 0;
            
        }

        sleepyFade.color = new Color(
            sleepyFade.color.r,
            sleepyFade.color.g,
            sleepyFade.color.b,
            awakeAMT);
    }
}
