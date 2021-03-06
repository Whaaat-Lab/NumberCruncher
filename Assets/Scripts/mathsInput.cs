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

    // Sprite Version
    private KeyCode enterKey = KeyCode.Return;
    private int numNums;
    private GameObject  numberManager;
    
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
        numberManager = GameObject.Find("numberManager");
        checkSound = GameObject.Find("ResultSound").GetComponent<AudioSource>();
        crowdReaction = GameObject.Find("audienceReaction").GetComponent<AudioSource>();
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        // go through the possible alpha keys
        for (int i = 0; i < numberInputs.Length; i++) {
            if (Input.GetKeyDown(numberInputs[i])) {

                // add it to the string if it was pressed
                mathsInput_string += i.ToString();
                // Display String
                // mathsInput_displayText.text = mathsInput_string;

                // or Display Sprites
                GameObject go = GameObject.Instantiate(animatedNumbers[i]) as GameObject;
                go.transform.parent = numberManager.transform;
                float xPos = numNums * 1.4f;
                go.transform.localPosition = new Vector2(xPos, 0);
                numNums++;
                // Are there already numbers on the board?
                if (numNums > 1) {
                    // Move existing Numbers
                    float moveAmmount = (numNums - 1) * 0.7f;
                    numberManager.transform.position = new Vector2(
                        -moveAmmount,
                        numberManager.transform.position.y
                    );
                }
                
            }
        }

        if (Input.GetKeyDown(enterKey)) {
            // Check the Answer
            StartCoroutine(Check());
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
            foreach(Transform child in numberManager.transform) {
                child.gameObject.GetComponent<numberBehavior>().EnterCruncher();
            }
        }
        else {
            background.color = new Color(1, 0, 0, 1);
            checkSound.clip = checkSounds[1];
            crowdReaction.clip = aww;
        }
        
        checkSound.Play();
        crowdReaction.Play();

        yield return new WaitForSeconds(1.5f);
        background.color = new Color(.4f, .46f, .61f, 1);
        Clear();
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
