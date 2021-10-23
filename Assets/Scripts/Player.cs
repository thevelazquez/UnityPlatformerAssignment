using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public GameObject player;
    public Text score;
    public Text winText;
    public Text livesText;
    public Text loseText;
    private int scoreValue = 0;
    private int lives = 3;
    public AudioClip musicBackground;
    public AudioClip musicWin;
    public AudioSource musicSource;
    private bool facingRight = true;
    public float jumpForce;
    Animator anim;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = $"Score: {scoreValue.ToString()}";
        livesText.text = $"Lives: {lives.ToString()}";
        winText.text = "";
        loseText.text = "";
        musicSource.clip = musicBackground;
        musicSource.loop = true;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (!isOnGround) {
            anim.SetInteger("State",3);
        } else {
            if (hozMovement != 0) {
                if (hozMovement == 1 || hozMovement == -1) {
                    anim.SetInteger("State", 2);
                } else {
                    anim.SetInteger("State",1);
                }
            } else {
                anim.SetInteger("State",0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (facingRight == false && hozMovement > 0) {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0) {    
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = $"Score: {scoreValue.ToString()}";
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4) {
                transform.position = new Vector3(60.0f, 2.0f, 0.0f);
                lives = 3;
                livesText.text = $"Lives: {lives.ToString()}";
            }
            if (scoreValue == 8) {
                musicSource.Stop();
                musicSource.clip = musicWin;
                musicSource.Play();
                winText.text="You Win!\nGame created by:\nEdgardo Velazquez";
            }
        }
        
        if (collision.collider.tag == "Enemy") {
            lives -= 1;
            livesText.text = $"Lives: {lives.ToString()}";
            Destroy(collision.collider.gameObject);
            if (lives == 0) {
                loseText.text="You Lose.";
                player.SetActive(false);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround) {
            if (Input.GetKey(KeyCode.W)) {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        } 
    }

    void Flip() {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}