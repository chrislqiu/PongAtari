using UnityEngine;

public class P1Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;

    private Rigidbody2D rb;
    private Vector2 playerMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gets it off our object
        rb = GetComponent<Rigidbody2D>();

        //checks if user pressed single or multiplayer
        if (gameObject.name == "P2 Racket")
        {
            isAI = GameManager.Instance.isAI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAI)
        {
            AIControls();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        // gets players (0, y) bc we dont want it to move on x axis
        //playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));

        /*
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            playerMove = new Vector2(0, 1);
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            playerMove = new Vector2(0, -1);
        else
            playerMove = Vector2.zero;
        */
        playerMove = Vector2.zero;

        if (Input.GetKey(moveUp))
            playerMove = new Vector2(0, 1);
        else if (Input.GetKey(moveDown))
            playerMove = new Vector2(0, -1);

    }

    private void AIControls()
    {
        // if ball is above where the paddle is, move it up
        if (ball.transform.position.y > transform.position.y + .5f)
        {
            playerMove = new Vector2(0, 1);
        }
        // if ball is below paddle, move down
        else if (ball.transform.position.y < transform.position.y - .5f)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }
    // important to write it as it is "FixedUpdate". This happens an even amount of time over the course of a second. This way no matter the fps of the user, the physics will be consistent and not dependent on the frame rate
    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }

    public void SetAI(bool value)
    {
        isAI = value;
    }
}