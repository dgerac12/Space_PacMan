using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player component references
    private Rigidbody2D rb2d;
    public float speed;
    public bool hasMultiplier;
    public Vector2 playerStart;
    private Animator anim;

    public GameObject uiManager;
    private UIController uiController;

    //Jellyfish References for lvl 1
    public GameObject yellowJelly;
    public GameObject purpleJelly;
    public GameObject blueJelly;
    public GameObject pinkJelly;
    private Animator yellowAnim;
    private Animator purpleAnim;
    private Animator blueAnim;
    private Animator pinkAnim;

    //Jellyfish References for lvl 2
    public GameObject yellowJelly2;
    public GameObject purpleJelly2;
    public GameObject blueJelly2;
    public GameObject pinkJelly2;
    private Animator yellowAnim2;
    private Animator purpleAnim2;
    private Animator blueAnim2;
    private Animator pinkAnim2;

    //text references
    public Text pointsText;
    public Text livesText;
    public Text timerText;

    //numeric values references
    public int points;
    public int starCount = 0;
    public int lives;
    private int coCount = 0;
    private bool isVulnerable = false; //this is for jelly vulnerability
    public float timeLeft;
    private float tempSpeed;

    //audio references
    public AudioClip pickupSound;
    public AudioSource pickupSource;
    public AudioClip overSound;
    public AudioSource overSource;
    public AudioClip enemySound;
    public AudioSource enemySource;
    public AudioClip bgSound;
    public AudioSource bgSource;
    public AudioClip powerSound;
    public AudioSource powerSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        uiController = uiManager.GetComponent<UIController>();

        yellowAnim = yellowJelly.GetComponent<Animator>();
        purpleAnim = purpleJelly.GetComponent<Animator>();
        blueAnim = blueJelly.GetComponent<Animator>();
        pinkAnim = pinkJelly.GetComponent<Animator>();

        yellowAnim2 = yellowJelly2.GetComponent<Animator>();
        purpleAnim2 = purpleJelly2.GetComponent<Animator>();
        blueAnim2 = blueJelly2.GetComponent<Animator>();
        pinkAnim2 = pinkJelly2.GetComponent<Animator>();

        pickupSource.clip = pickupSound;
        overSource.clip = overSound;
        enemySource.clip = enemySound;
        bgSource.clip = bgSound;
        powerSource.clip = powerSound;

        //these can be changed or removed, in the case of lives. just leaving it here for testing
        tempSpeed = speed;
        points = 0;
        lives = 2;
        SetPointsText();
        SetLivesText();
        timerText.text = "";
    }

    private void Update()
    {
        if(coCount == 0)
        {
            hasMultiplier = false;
        }

        //timeLeft = Mathf.Round(timeLeft);
        timeLeft -= Time.deltaTime;
        if(timeLeft > 0 && uiController.mainMenu.activeSelf == false && uiController.controlsMenu.activeSelf == false && uiController.creditsMenu.activeSelf == false)
        {
            timerText.text = timeLeft.ToString("0");
            speed = 0;
        }
        else if(timeLeft < 0)
        {
            timerText.text = ("");
            speed = tempSpeed;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

        Vector2 moveDirection = rb2d.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            pickupSource.Play();
            points = points + 10;
            starCount++;
            SetPointsText();
        }

        if (other.gameObject.CompareTag("gem1"))
        {
            other.gameObject.SetActive(false);
            pickupSource.Play();
            points = points + 200;
            starCount++;
            SetPointsText();
        }

        if (other.gameObject.CompareTag("gem2"))
        {
            other.gameObject.SetActive(false);
            pickupSource.Play();
            points = points + 400;
            starCount++;
            SetPointsText();
        }

        if (other.gameObject.CompareTag("PickUp") && isVulnerable == true) //only happens during vuln coroutine
        {
            other.gameObject.SetActive(false);
            pickupSource.Play();
            points = points + 10;
            SetPointsText();
        }

        if (other.gameObject.CompareTag("Multiplier")) //triggers vulnerability coroutine
            {
                other.gameObject.SetActive (false);
                powerSource.Play();
                isVulnerable = true;
                hasMultiplier = true;
                coCount++;
                starCount++;

                yellowAnim.SetBool("isVuln", true);
                purpleAnim.SetBool("isVuln", true);
                blueAnim.SetBool("isVuln", true);
                pinkAnim.SetBool("isVuln", true);

                yellowAnim2.SetBool("isVuln", true);
                purpleAnim2.SetBool("isVuln", true);
                blueAnim2.SetBool("isVuln", true);
                pinkAnim2.SetBool("isVuln", true);
                
                StartCoroutine("VulnTime");
            }


        if (other.gameObject.CompareTag("Teleport")) 
        {
            rb2d.transform.position = new Vector3(-8f, 0.24f, 0f);
        }

        if (other.gameObject.CompareTag("Teleport2")) 
        {
            rb2d.transform.position = new Vector3(8f, 0.24f, 0f);
        }

        else if(other.gameObject.CompareTag("Enemy")  && hasMultiplier == true)
        {
            points = points + 100;
            enemySource.Play();
        }
        else if (other.gameObject.CompareTag("Enemy") && hasMultiplier == false)
        {
            lives = lives - 1;
            SetLivesText ();
            if (lives != 0) //so it doesn't overlap with game over sound
            {
                enemySource.Play();
                transform.position = playerStart;
                timeLeft = 3;
            }
        }
    }
    IEnumerator VulnTime()
    {
        yield return (new WaitForSeconds(5));
        isVulnerable = false;
        coCount--;

        yellowAnim.SetBool("isVuln", false);
        purpleAnim.SetBool("isVuln", false);
        blueAnim.SetBool("isVuln", false);
        pinkAnim.SetBool("isVuln", false);

        yellowAnim2.SetBool("isVuln", false);
        purpleAnim2.SetBool("isVuln", false);
        blueAnim2.SetBool("isVuln", false);
        pinkAnim2.SetBool("isVuln", false);
    }
    void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0) 
        {
            anim.SetBool("noLives", true);
            overSource.Play();
            Destroy(this);
        }
    }
}
