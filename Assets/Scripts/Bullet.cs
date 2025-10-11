using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    private float life;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnEnable() { life = lifeTime; }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);

        life -= Time.fixedDeltaTime;
        if (life <= 0f) gameObject.SetActive(false); // später: Pool zurück
    }

    // Physikalische Kollision
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Enemy"))
            Destroy(col.collider.gameObject); // Enemy stirbt

        gameObject.SetActive(false); // Bullet weg
    }

    // Trigger-Kollision (weil Enemy-Collider isTrigger=true)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);   // Enemy stirbt
            gameObject.SetActive(false); // Bullet weg
        }
    }
}
