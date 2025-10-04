using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 25f;
    public float lifeTime = 2f;

    Rigidbody rb;
    float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnEnable() => timer = lifeTime;

    void FixedUpdate()
    {
        // Bewegung straight forward
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);

        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        // später: Schaden usw.
        Destroy(gameObject);
    }
}
