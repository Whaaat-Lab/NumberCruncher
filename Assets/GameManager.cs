using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Global Variables
    public int maxNums;
    
    public static GameManager S;

    void Awake()
    {
        S = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        maxNums = 35;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
