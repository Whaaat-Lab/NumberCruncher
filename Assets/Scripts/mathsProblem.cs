using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mathsProblem : MonoBehaviour {
	
	private TextAsset   mathsProblem_asset;
	private string[]	mathsProblems;
	private Text		mathsProblem_display;
	public Animator		timer;
	private float		timerTime;

	public string	answer;

	public static mathsProblem S;

	void Awake() {
		S = this;
	}
	
	// Start is called before the first frame update
    void Start() {
		timer.SetBool("sleeping", false);
		mathsProblem_display = GetComponentInChildren<Text>();
		mathsProblem_display.text = "";
		StartCoroutine(GetTextFromFile());

	}
	
	IEnumerator GetTextFromFile() {
		mathsProblem_asset = Resources.Load("mathsProblems") as TextAsset;
		mathsProblems = mathsProblem_asset.text.Split ('#');

		yield return null;
	}
	public void Asleep()
    {
		timer.Play("Timer", 1, 0f);
		timer.SetBool("sleeping", true);
		mathsProblem_display.text = "....";
    }

	public void WakeUp()
	{
		if (timer.GetBool("sleeping"))
		{
			timer.SetBool("sleeping", false);
			UpdateProblem();
		}
    }
	public void UpdateProblem() {
		
		
			timer.enabled = true;
			if (!GameManager.S.crunchingState)
			{
				int r = Random.Range(0, mathsProblems.Length);
				string[] problem = mathsProblems[r].Split('=');
				string question = problem[0].Replace("\n", "");
				answer = problem[1].Replace(" ", "");
				mathsProblem_display.text = question;
				timer.Play("Timer", -1, 0f);
			}
			else
			{
				timer.Play("Timer", -1, 0f);
				timer.enabled = false;
				mathsProblem_display.text = "Time to Crunch!";
			}
		
	}
/*
	public IEnumerator Timer(){
		timer.fillAmount = timerTime;
		timerTime += .1f;
		yield return new WaitForSeconds(1);
		if(timerTime < 1f){
			StartCoroutine(Timer());
		} else {
			UpdateProblem();
		}
	}
*/

	public void ReEnterMathsPhase(){
		timer.enabled = true;		
		UpdateProblem();
		GameManager.S.crunchingState = false;
	}

}
