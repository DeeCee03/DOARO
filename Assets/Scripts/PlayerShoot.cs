using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;            // leeres Child vorne an der Kapsel
    public float fireRate = 8f;         // Schüsse pro Sekunde
    public float muzzleOffset = 0.6f;   // Abstand vor der Kapsel, falls kein Muzzle gesetzt

    float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        bool wantFire = Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space);
        float interval = 1f / Mathf.Max(0.01f, fireRate);

        if (wantFire && fireTimer >= interval)
        {
            fireTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 spawnPos = muzzle ? muzzle.position : (transform.position + transform.forward * muzzleOffset + Vector3.up * 0.5f);
        Quaternion rot = transform.rotation;

        var bullet = Instantiate(bulletPrefab, spawnPos, rot);
        // später ersetzen wir Instantiate durch ein Pooling-System
    }
}