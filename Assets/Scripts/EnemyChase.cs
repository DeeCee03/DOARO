using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyChase : MonoBehaviour
{
    public Transform target;     
    public float moveSpeed = 3.0f;
    public float stopDistance = 0.5f; 

    void Update()
    {
        if (!target) return;

        Vector3 to = target.position - transform.position;
        to.y = 0f;
        float dist = to.magnitude;

        if (dist > stopDistance)
        {
            Vector3 dir = to.normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            
            if (dir.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); 
 
            gameObject.SetActive(false);
        }
    }
}
