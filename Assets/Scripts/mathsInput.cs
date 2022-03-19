using System;
using System.Collections;
using UnityEngine;

public class mathsInput : MonoBehaviour {
    public String mathsInput_string;
    public GameObject mathsInput_displayObject;
    public GameObject[] animatedNumbers;
    public AudioClip[] checkSounds;
    public AudioClip[] yays;
    public AudioClip aww;
    private AudioSource checkSound;
    private AudioSource crowdReaction;
    public SpriteRenderer background;
    public bool awake; 

    public Animator animator;

    //from the eyes script
    public float sleepSpeed;
    public float sleepyThresh = 10f;
    public float asleepThresh = 20f;

    // Sprite Version
    private KeyCode enterKey = KeyCode.Return;
    private int numNums;
    private GameObject  numberManager;

    //from eye script
    private float startTime;
    private float elapsedTime;

    private KeyCode[] numberInputs = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
    };

    public static mathsInput S;

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start() {
        startTime = Time.time;
        awake = true;
        numberManager = GameObject.Find("numberManager");
        checkSound = GameObject.Find("ResultSound").GetComponent<AudioSource>();
        crowdReaction = GameObject.Find("audienceReaction").GetComponent<AudioSource>();
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (awake)
        {
            // go through the possible alpha keys
            for (int i = 0; i < numberInputs.Length; i++)
            {
                if (Input.GetKeyDown(numberInputs[i]))
                {

                    // add it to the string if it was pressed
                    mathsInput_string += i.ToString();
                    // Display String
                    // mathsInput_displayText.text = mathsInput_string;

                    // or Display Sprites
                    GameObject go = GameObject.Instantiate(animatedNumbers[i]) as GameObject;
                    go.transform.parent = numberManager.transform;
                    //Danny- changed from 1.4 to try to fit numbers better
                    float xPos = numNums * 1.0f;
                    go.transform.localPosition = new Vector2(xPos, 0);
                    numNums++;
                    // Are there already numbers on the board?
                    if (numNums > 1)
                    {
                        // Move existing Numbers (Danny- changed from 0.4 to try and fit numbers better)
                        float moveAmmount = (numNums - 1) * 1.5f;
                        numberManager.transform.position = new Vector2(
                            -moveAmmount,
                            numberManager.transform.position.y
                        );
                    }

                }


            }

            if (Input.GetKeyDown(enterKey))
            {
                // Check the Answer
                StartCoroutine(Check());
            }
        }

        // sleepy animations
        elapsedTime = Time.time - startTime;
        if (elapsedTime < sleepyThresh) { animator.SetInteger("sleepy", 0); }
        if (elapsedTime > sleepyThresh) { animator.SetInteger("sleepy", 1); }
        if (elapsedTime > asleepThresh)
        {
            animator.SetInteger("sleepy", 2);
            awake = false;
            mathsProblem.S.Asleep();

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Wake Up!
            startTime = Time.time;
            animator.SetInteger("sleepy", 0);
            awake = true;
            mathsProblem.S.WakeUp();
        }
    }



    public void ExternalCheck()
    {
        Debug.Log("checkingn here?");
        StartCoroutine(Check());
    }

    public IEnumerator Check()
    {
        if (mathsInput_string == mathsProblem.S.answer) {
            // Correct
            checkSound.clip = checkSounds[0];
            int r = UnityEngine.Random.Range(0, yays.Length);
            crowdReaction.clip = yays[r];
            background.color = new Color(.92f, .82f, .126f, 1);
            // Start the Chewing Animation
            animator.SetBool("eating",true);
            yield return new WaitForSeconds(.8f);
            // Delete the numbers
            foreach(Transform child in numberManager.transform)
            {
                Destroy(child.gameObject);
            }
            
            // And create a blob
            Stomach.S.CreateBlob();
            //reset animator
            animator.SetBool("eating", false);



        }
        else {
            background.color = new Color(1, 0, 0, 1);
            //run spitting animation
            animator.SetBool("spitting", true);
            checkSound.clip = checkSounds[1];
            crowdReaction.clip = aww;
        }
        
        checkSound.Play();
        crowdReaction.Play();

        //moved this clear before the spitting delay
        Clear();
        yield return new WaitForSeconds(.9f);
        animator.SetBool("spitting", false);
        
        background.color = new Color(.4f, .46f, .61f, 1);
        
        yield return null;
    }

    void Clear() {
        mathsInput_string = "";
        foreach(Transform child in numberManager.transform) {
            Destroy(child.gameObject);
        }

        float microOffset = UnityEngine.Random.Range(-0.5f, 0.5f);
        numberManager.transform.position = new Vector2(microOffset, numberManager.transform.position.y);
        numNums = 0;
        
        // build an if statement for number of numbers we have in our hopper
        mathsProblem.S.UpdateProblem();
    }
}
