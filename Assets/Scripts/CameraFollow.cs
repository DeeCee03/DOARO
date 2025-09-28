using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Offset & Verhalten")]
    public Vector3 offset = new Vector3(0f, 18f, -14f); // Startversatz zur Spielerposition
    [Range(1f, 20f)] public float followSmooth = 8f;    // höher = schnelleres Folgen
    public float lookAtHeight = 1f;                     // Blickpunkt leicht über Boden

    private Vector3 velocity; // für SmoothDamp

    void LateUpdate()
    {
        if (!target) return;

        // weich zur Zielposition (Spieler + Offset)
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPos, ref velocity, 1f / followSmooth
        );

        // sanft Richtung Spieler blicken
        Vector3 lookPoint = target.position + Vector3.up * lookAtHeight;
        Quaternion desiredRot = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * followSmooth);
    }
}