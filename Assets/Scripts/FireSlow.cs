using UnityEngine;

public class FireSlow:FireBase, IReflection, ISlow
{
    [SerializeField] float linearDrag;   // ← 減速の強さ
    [SerializeField] float stopThreshold;
    FirePhase phase = FirePhase.enterPhase;
    void Start()
    {
        rb.linearDamping = linearDrag;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnReflected(collision);
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("CheckLayer"))
        {  
            SetActive();
            
        }
    }
    public void OnReflected(Collision2D collision)
    {
        Vector2 n = collision.contacts[0].normal;
        Vector2 dir = Vector2.Reflect(velocity, n).normalized;
        float s = velocity.magnitude;                             // ★ 反射後も“直前の速さ”を維持
        rb.linearVelocity = dir * s; 
    }
    void Update()
    {
        Slow();
    }
    public void Slow()
    {
        velocity = rb.linearVelocity;
        if (rb.linearVelocity.sqrMagnitude < stopThreshold * stopThreshold)
            rb.linearVelocity = Vector2.zero;
    }
    void SetActive()
    {
        gameObject.layer = LayerMask.NameToLayer("ActiveFire");
        phase = FirePhase.activePhase;
    }
    void SetExit()
    {
        gameObject.layer = LayerMask.NameToLayer("ExitFire");
        phase = FirePhase.exitPhase;
    }
    
}