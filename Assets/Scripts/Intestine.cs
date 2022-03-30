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
    public Animator stomachAnim;
    public string portName;


    private float fill;

    private SerialPort _port;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
       
 
    }

    void Start()
    {
        //Serial setup;
        _port = new SerialPort(portName, 9600);
        _port.Open();
        _port.ReadTimeout =5000;
        _port.WriteTimeout =5000;
    }

    // Update is called once per frame
    public void Fill()
    {
        fill += fillSpeed;
        intestineFill.fillAmount = fill;
        stomachAnim.ResetTrigger("poop");

        if (fill >= 1)
        {
            if (_port.IsOpen)
            {
               _port.Write("1");
                Debug.Log(1);
            }
            stomachAnim.SetTrigger("poop");
            fill = 0;
            intestineFill.fillAmount = 0;

            
        

        }
    }
}
