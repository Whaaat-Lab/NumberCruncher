using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberCruncher : MonoBehaviour
{
    public Image numberFill;
    private float numberAmt;
    public GameObject crunchAlert;
    private bool filledAlert;
    private Animator anim;
	private AudioSource numberCruncher_audio;

    
    // Start is called before the first frame update
    void Start() {
        numberFill.fillAmount = 0;
		anim = GetComponent<Animator>();
		numberCruncher_audio = GameObject.Find("numberCruncher_audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    numberAmt = this.transform.hierarchyCount - 13;
	    // get a percentage amount under 1
	    float numberFillDisplay = numberAmt / GameManager.S.maxNums;
	    numberFill.fillAmount = numberFillDisplay;
        
	    // Have we reached the max
	    if (numberAmt >= GameManager.S.maxNums && !filledAlert)
        {
            StartCoroutine(CrunchAlert());
            filledAlert = true;
        }
		if(filledAlert && Input.GetKeyDown(KeyCode.A)){
			StartCoroutine(Crunch());
		}
    }

	public void Scream(){
		numberCruncher_audio.Play();
	} 

    private IEnumerator CrunchAlert()
    {
        // first, let's tell the GameManager that we're moving into the crunching phase
		GameManager.S.crunchingState = true;

		crunchAlert.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        crunchAlert.SetActive(false);
        yield return new WaitForSeconds(0.5f);
		if (GameManager.S.crunchingState) StartCoroutine(CrunchAlert());
    }

	private IEnumerator Crunch(){
		// tell the game manager that the lever has been pulled
		GameManager.S.crunchingState = false;
		StopCoroutine(CrunchAlert());
		anim.Play("wallsClosingIn");
		Scream();
        yield return new WaitForSeconds(1f);

		filledAlert = false;
	}

	private IEnumerator Reveal(){
		GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");
		for(int i=0; i<numbers.Length; i++){
			Destroy(numbers[i].gameObject);
		}
		yield return new WaitForSeconds(2f);
		Rock.S.RevealRock();
		anim.Play("revealRock");
		yield return new WaitForSeconds(2F);
		mathsProblem.S.ReEnterMathsPhase();
	}
}
