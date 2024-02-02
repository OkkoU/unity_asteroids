using UnityEngine;

public class ShipController : MonoBehaviour
{
    float rotationSpeed = 200.0f;
    float thrustForce = 3f;

    public GameObject bullet;
    private GameController gameController;


    void Start()
    {
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        gameController = gameControllerObject.GetComponent<GameController>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }


    void FixedUpdate()
    {
        // Rotate the ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        // Thrust the ship if necessary
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Anything except a bullet is an asteroid
        if (collision.gameObject.tag != "Bullet")
        {
            // Move the ship to the centre of the screen
            transform.position = Vector3.zero;

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            gameController.DecrementLives();
        }
    }


    void ShootBullet()
    {
        // Spawn a bullet
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}

