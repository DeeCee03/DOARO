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
        rb.useGravity = true; // Top-Down, Y ist fix
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

    Vector3 currentPlanarVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    Vector3 targetVel       = input * moveSpeed;


    Vector3 newPlanarVel = Vector3.MoveTowards(
        currentPlanarVel, targetVel, acceleration * Time.fixedDeltaTime
    );

   Vector3 move = newPlanarVel * Time.fixedDeltaTime;
    move.y = 0f; 
    rb.MovePosition(rb.position + move);
}
}