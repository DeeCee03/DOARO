using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Referenzen")]
    public GameObject bulletPrefab;   // -> Bullet-Prefab aus Project
    public Transform muzzle;          // -> Empty vor der Kapsel

    [Header("Schuss-Einstellungen")]
    public float fireRate = 8f;       // Schüsse pro Sekunde
    public float muzzleOffset = 0.6f; // Fallback falls kein Muzzle gesetzt

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
    Vector3 spawnPos = muzzle
        ? muzzle.position
        : (transform.position + transform.forward * muzzleOffset + Vector3.up * 0.5f);


    Quaternion rot = muzzle ? muzzle.rotation : transform.rotation;

    GameObject bulletGO = Instantiate(bulletPrefab, spawnPos, rot);

    Collider playerCol = GetComponent<Collider>();
    Collider bulletCol = bulletGO.GetComponent<Collider>();
    if (playerCol && bulletCol)
        Physics.IgnoreCollision(playerCol, bulletCol, true);
}
}
