using System.ComponentModel;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = .25f;
    [SerializeField] private TMP_Text P1Score;
    [SerializeField] private TMP_Text P2Score;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private GameObject winPanel;

    private int hitCounter;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void resetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        // calls startBall after two secs
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;
        //if the position of the ball is going right and makes contact, change direction to negative (goes left)
        if (transform.position.x >  0)
        {
            xDirection = -1;
        }
        else
        //vice versa
        {
            xDirection= 1; 
        }
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        // in the slim chance ball hits center of the racket, we dont want it to go straight
        if (yDirection == 0)
        {
            yDirection = .25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "P1 Racket" || collision.gameObject.name == "P2Score Racket")
        {
            PlayerBounce(collision.transform);
        }
    }

    //updates the score
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            resetBall();
            P1Score.text = (int.Parse(P1Score.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            resetBall();
            P2Score.text = (int.Parse(P2Score.text) + 1).ToString();
        }

        int p1 = int.Parse(P1Score.text);
        int p2 = int.Parse(P2Score.text);

        if (p1 >= 15)
        {
            EndGame("Player 1 Wins!");
        }
        else if (p2 >= 15)
        {
            EndGame("Player 2 Wins!");
        }
    }

    private void EndGame(string msg)
    {
        rb.velocity = Vector2.zero; // Stop the ball
        rb.simulated = false; // stops the ball completely
        winText.text = msg;
        winPanel.SetActive(true);
        winText.gameObject.SetActive(true); // Show the message

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Menu");
    }

}
