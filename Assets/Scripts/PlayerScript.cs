using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpforce;

    public GameObject winScreen;

    public GameObject loseScreen;
    public Text score;

    public Text lives;

    private int scoreValue = 0;

    private int lifeValue = 3;

    public GameObject level2Spawn;

    bool gameOver;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    
    int i;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = lifeValue.ToString();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == false)
        {
            musicSource.clip = musicClipOne;
            musicSource.loop = true;
           
            if (musicSource.isPlaying == false)
            {
                musicSource.Play();
            }
            

        }
        if(gameOver == true)
        {
            if (i == 0)
            {
                musicSource.Stop();
                Debug.Log("here1");
                i++;
                musicSource.loop = false;
            }
            
            
            musicSource.clip = musicClipTwo;
            if (musicSource.isPlaying == false)
            {
                if(i == 1)
                {
                    musicSource.Play();
                    i++;
                }
                
            }
        }

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (scoreValue == 4)
        {
            Teleport();
        }
        if (scoreValue == 8)
        {
            winScreen.SetActive(true);
            gameOver = true;
        }
        if (lifeValue == 0)
        {
            loseScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void Teleport()
    {
        gameObject.transform.position = level2Spawn.transform.position;
        Destroy(level2Spawn);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        

        if (collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            lives.text = lifeValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}