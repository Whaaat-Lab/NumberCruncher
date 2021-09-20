using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberCruncher : MonoBehaviour
{
    public Image numberFill;
    private float numberAmt;

    
    // Start is called before the first frame update
    void Start() {
        numberFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update() {
        numberAmt = (this.transform.hierarchyCount - 4) / 40f;
        numberFill.fillAmount = numberAmt;
    }
}
