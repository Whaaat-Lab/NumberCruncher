using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine.UI;
using UnityEngine;

public class Intestine : MonoBehaviour
{
    public static Intestine S;
    public float fillSpeed;
    public Image intestineFill;

    private float fill;

    private SerialPort _port;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
       
 
    }

    void Start()
    {
        // Serial setup
        //_port = new SerialPort(COM1, 9600);
        //_port.Open();
    }

    // Update is called once per frame
    public void Fill()
    {
        fill += fillSpeed;
        intestineFill.fillAmount = fill;

        if (fill >= 1)
        {
            //_port.Write(1);
            fill = 0;
            intestineFill.fillAmount = 0;
        }
    }
}
