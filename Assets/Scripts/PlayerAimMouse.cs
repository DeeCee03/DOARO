using UnityEngine;

public class PlayerAimMouse : MonoBehaviour
{
    [Header("Zielsteuerung")]
    public LayerMask aimLayerMask; 
    public float rotationSpeed = 720f;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!cam) return;


        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 
                            aimLayerMask.value == 0 ? ~0 : aimLayerMask))
        {

            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
