using UnityEngine;

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
        if (life <= 0f) gameObject.SetActive(false); // später: via Pool zurückgeben
    }

    void OnCollisionEnter(Collision col)
    {
        // TODO: hier EnemyHealth schädigen, wenn vorhanden
        gameObject.SetActive(false);
    }
}