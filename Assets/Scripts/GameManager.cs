using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Global Variables
    public bool crunchingState, plinkoState;
    public int maxNums;

    public bool awake;
    
    public static GameManager S;

    void Awake()
    {
        S = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // generally 35, but less for testing purposes
        maxNums = 10;
    }

    // Update is called once per frame


}
