using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mathsProblem : MonoBehaviour {
	
	private TextAsset   mathsProblem_asset;
	private string[]	mathsProblems;
	private Text		mathsProblem_display;

	public string	answer;

	public static mathsProblem S;

	void Awake() {
		S = this;
	}
	
	// Start is called before the first frame update
    void Start() {
		mathsProblem_display = GetComponentInChildren<Text>();
		mathsProblem_display.text = "";
		StartCoroutine(GetTextFromFile());

	}
	
	IEnumerator GetTextFromFile() {
		mathsProblem_asset = Resources.Load("mathsProblems") as TextAsset;
		mathsProblems = mathsProblem_asset.text.Split ('#');

		yield return null;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A)) {
			UpdateProblem();
		}
    }

	public void UpdateProblem() {
		int      r        = Random.Range(0, mathsProblems.Length);
		string[] problem  = mathsProblems[r].Split('=');
		string   question = problem[0].Replace("\n", "");
		answer   = problem[1].Replace(" ","");
		mathsProblem_display.text = question;
	}
}
