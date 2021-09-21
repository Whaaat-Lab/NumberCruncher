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

    
    // Start is called before the first frame update
    void Start() {
        numberFill.fillAmount = 0;
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
    }

    private IEnumerator CrunchAlert()
    {
        crunchAlert.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        crunchAlert.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CrunchAlert());
    }
}
