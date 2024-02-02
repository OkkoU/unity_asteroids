using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        // Set the bullet to destroy itself after 1 seconds
        Destroy(gameObject, 1f);

        // Push the bullet in the direction it is facing
        GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
    }
}
