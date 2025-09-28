using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Bewegung")]
    public float moveSpeed = 6f;        // Endgeschwindigkeit
    public float acceleration = 20f;    // wie schnell auf Zieltempo beschleunigt wird
    public float rotationSpeed = 720f;  // Grad/Sek, wie schnell die Kapsel in Bewegungsrichtung dreht

    private Rigidbody rb;
    private Vector3 input; // Input aus Update, in FixedUpdate verwendet

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Top-Down, Y ist fix
    }

    void Update()
    {
        // WASD/Arrow-Keys lesen (klassische Input-Achsen)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        input = new Vector3(h, 0f, v);
        if (input.sqrMagnitude > 1f) input.Normalize(); // gleichmäßige Diagonalen
    }

    void FixedUpdate()
    {
        // aktuelle & Zielgeschwindigkeit (nur XZ)
        Vector3 currentPlanarVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 targetVel       = input * moveSpeed;

        // sanft Richtung Zielgeschwindigkeit
        Vector3 newPlanarVel = Vector3.MoveTowards(
            currentPlanarVel, targetVel, acceleration * Time.fixedDeltaTime
        );

        // bewegen (Y bleibt dank Constraint fix)
        rb.MovePosition(rb.position + newPlanarVel * Time.fixedDeltaTime);

        // in Bewegungsrichtung drehen (falls wir uns bewegen)
        if (newPlanarVel.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(newPlanarVel, Vector3.up);
            rb.MoveRotation(Quaternion.RotateTowards(
                rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime
            ));
        }
    }
}