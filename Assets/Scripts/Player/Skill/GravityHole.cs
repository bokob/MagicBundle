using UnityEngine;

public class GravityHole : MonoBehaviour
{
    Rigidbody2D _rb;
    float _radius = 3f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (collider.TryGetComponent(out Rigidbody2D otherRb))
                {
                    Debug.Log("끌어당길 물체: " + otherRb.name);
                    Vector2 direction = (Vector2)transform.position - otherRb.position;
                    float distance = direction.magnitude;
                    float forceMagnitude = 10f;
                    otherRb.AddForce(direction.normalized * forceMagnitude);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}