using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyChase : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3.0f;
    public float stopDistance = 0.5f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false; // Physik aktiv
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // kein Umkippen
    }

    void FixedUpdate()
    {
        if (!target) { rb.linearVelocity = Vector3.zero; return; }

        Vector3 to = target.position - rb.position;
        to.y = 0f;
        float dist = to.magnitude;

        if (dist <= stopDistance)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 dir = to.normalized;

        // Bewegung über Physik (Kollisionen werden korrekt aufgelöst)
        rb.linearVelocity = new Vector3(dir.x * moveSpeed, 0f, dir.z * moveSpeed);

        // hübsch ausrichten (optional)
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion look = Quaternion.LookRotation(dir, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, Time.fixedDeltaTime * 10f));
        }
    }

    // Spieler-Hit über physische Kollision (kein Trigger nötig)
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.gameObject.SetActive(false); // Player „tot“
            gameObject.SetActive(false);              // Enemy optional despawn
        }
    }
}
