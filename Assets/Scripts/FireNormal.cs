using UnityEngine;

public class FireNormal:FireBase, IReflection
{
    [SerializeField]
    public int maxReflectionCnt;
    public int reflectionCnt;
    FirePhase phase = FirePhase.enterPhase;
    public string tagName;
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnReflected(collision);
        reflectionCnt += 1;
        if (reflectionCnt >= maxReflectionCnt)
        {
            SetExit();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("CheckLayer"))
        {  
            SetActive();
            
        }
        if(other.CompareTag("OutSide"))
        {
            ObjectPool.Instance.ReturnToPool(this, tagName);
        }
    }
    public void OnReflected(Collision2D collision)
    {
        Vector2 n = collision.contacts[0].normal;
        Vector2 dir = Vector2.Reflect(velocity, n).normalized;
        float s = velocity.magnitude;                             // ★ 反射後も“直前の速さ”を維持
        rb.linearVelocity = dir * s; 
    }

    void SetActive()
    {
        gameObject.layer = LayerMask.NameToLayer("ActiveFire");
        phase = FirePhase.activePhase;
        reflectionCnt = 0;
    }
    void SetExit()
    {
        gameObject.layer = LayerMask.NameToLayer("ExitFire");
        phase = FirePhase.exitPhase;
    }
    
}