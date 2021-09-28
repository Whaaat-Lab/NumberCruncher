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
	private AudioSource scream;

    
    // Start is called before the first frame update
    void Start() {
        numberFill.fillAmount = 0;
		crunchAnim = GetComponent<Animator>();
		scream = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        numberAmt = (this.transform.hierarchyCount - 4) / 35f;
        numberFill.fillAmount = numberAmt;
        if (numberAmt >= 1 && !filledAlert)
        {
            StartCoroutine(CrunchAlert());
            filledAlert = true;
        }
		if(filledAlert && Input.GetKeyDown(KeyCode.A)){
			StartCoroutine(Crunch());
		}
    }

	public void Scream(){
		scream.Play();
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
		GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");
        yield return new WaitForSeconds(1f);
		for(int i=0; i<numbers.Length; i++){
			Destroy(numbers[i].gameObject);
		} 
	}
}
