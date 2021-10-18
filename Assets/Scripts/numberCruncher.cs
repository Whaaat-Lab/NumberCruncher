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
    private Animator crunchAnim;
	private AudioSource numberCruncher_audio;

    
    // Start is called before the first frame update
    void Start() {
        numberFill.fillAmount = 0;
		crunchAnim = GetComponent<Animator>();
		numberCruncher_audio = GameObject.Find("numberCruncher_audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    numberAmt = this.transform.hierarchyCount - 4;
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
        crunchAlert.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        crunchAlert.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CrunchAlert());
    }

	private IEnumerator Crunch(){
		crunchAnim.SetTrigger("Slam");
		Scream();
		GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");
        yield return new WaitForSeconds(1f);
		for(int i=0; i<numbers.Length; i++){
			Destroy(numbers[i].gameObject);
		} 
	}
}
