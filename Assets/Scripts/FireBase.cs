using UnityEngine;
enum FirePhase{enterPhase, activePhase, exitPhase}
[RequireComponent(typeof(Rigidbody2D))]
public class FireBase : MonoBehaviour
{
    [SerializeField]
    float speed;
    public Vector2 velocity;
    public Rigidbody2D rb;
    [SerializeField] public FireBase prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.linearVelocity;   
    }
    public virtual void Launch(Vector2 dir)
    {
        rb.linearVelocity = dir.normalized * speed;
    }
    
}
