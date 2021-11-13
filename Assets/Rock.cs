using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int rockHealth;
    public bool activeRock;
    public KeyCode h1, h2, h3;
    public GameObject rockImage;
    public GameObject rockChip;
    public Sprite[] rockChips;
    public GameObject coin;
    public GameObject screen2_bottom;
    public AudioSource choir, rockHit, anvil;
    public AudioClip[] rockHits;
    private Rigidbody2D rb;
    public Animator a;

    public static Rock S;
    
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        rb = rockImage.GetComponent<Rigidbody2D>();
        ResetRock();
    }

    void Start()
    {
        a = GetComponent<Animator>();
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            a.Play("rockBlink");
            Debug.Log("blinking?");
        }
        
        if (activeRock)
        {
            if (Input.GetKeyDown(h1)) DamageRock(1);
            if (Input.GetKeyDown(h2)) DamageRock(2);
            if (Input.GetKeyDown(h3)) DamageRock(3);
        }
    }
    
    public void DamageRock(int damageAmt)
    {
        if (rockHealth > 0)
        {
            rockHealth -= damageAmt;
            float forceY = damageAmt * 25;

            rb.AddForce(transform.up * forceY);

            for (int i = 0; i <= damageAmt; i++)
            {
                Chip();
            }
        }
        else
        {
            rockImage.SetActive(false);
            StartCoroutine(RevealCoin());
        }
    }

    public IEnumerator RevealCoin()
    {
        coin.SetActive(true);
        choir.Play();
        yield return new WaitForSeconds(2);
        screen2_bottom.SetActive(false);
        yield return new WaitForSeconds(2);
        screen2_bottom.SetActive(true);
        coin.SetActive(false);
        coin.transform.localPosition = new Vector2(-.75f, -11f);
        ResetRock();

    }

    public void ResetRock()
    {
        rockHealth = 100;
        activeRock = false;
        rockImage.SetActive(false);
        rockImage.transform.localPosition = new Vector2(-1.1f, -0.797f);

    }

    public void RevealRock()
    {
        activeRock = true;
        rockImage.SetActive(true);
        mathsProblem.S.ReEnterMathsPhase();
    }

    public void Chip()
    {
        float xPos = Random.Range(-2f, 2f);
        GameObject go = Instantiate(rockChip, new Vector2(xPos, -4f), Quaternion.identity);
        int s = (int) Random.Range(0, rockChips.Length);
        go.GetComponent<SpriteRenderer>().sprite = rockChips[s];
        int rh = (int) Random.Range(0, rockHits.Length);
        rockHit.PlayOneShot(rockHits[rh]);
        float anvilPitch = Random.Range(0.5f, 1.5f);
        anvil.pitch = anvilPitch;
        anvil.Play();
    }

    public IEnumerator Blink()
    {
        float r = Random.Range(1, 6);
        yield return new WaitForSeconds(r);
        a.Play("rock_idle");
        StartCoroutine(Blink());
        Debug.Log("blink?");
    }
}
