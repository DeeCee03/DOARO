using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyChase : MonoBehaviour
{
    [Header("Bewegung")]
    public Transform target;
    public float moveSpeed = 3.0f;
    public float stopDistance = 0.5f;

    [Header("Boden-Check")]
    public float groundCheckDistance = 1.1f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // ✅ jetzt aktiv – Enemy fällt
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!target)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        // 🔹 Boden prüfen – Raycast direkt nach unten
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // Richtung zum Spieler
        Vector3 to = target.position - rb.position;
        to.y = 0f;
        float dist = to.magnitude;

        // Falls zu nah: stehen bleiben
        if (dist <= stopDistance)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        // Bewegung nur horizontal, Y übernimmt Gravitation
        Vector3 dir = to.normalized;
        Vector3 vel = new Vector3(dir.x * moveSpeed, rb.linearVelocity.y, dir.z * moveSpeed);
        rb.linearVelocity = vel;

        // Sanfte Rotation in Bewegungsrichtung
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion look = Quaternion.LookRotation(dir, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, Time.fixedDeltaTime * 10f));
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.gameObject.SetActive(false); // Player tot
            gameObject.SetActive(false);              // Enemy despawn
        }
    }

    void OnDrawGizmosSelected()
    {
        // Nur visuell im Editor – zeigt GroundCheck-Strahl
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
