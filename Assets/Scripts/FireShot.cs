using UnityEngine;

public class FireShot:FireBase, IReflection
{
    [SerializeField]
    public int reflectionCnt;
    public int maxReflectionCnt;
    FirePhase phase = FirePhase.enterPhase;
    public FireBase smallFire;
    public int burstCount;
    public float fanAngle;
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnReflected(collision);
        SmallFireShot();
        reflectionCnt += 1;
        if (reflectionCnt >=maxReflectionCnt)
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
            ObjectPool.Instance.ReturnToPool(this, "Shot");
        }
    }
    public void OnReflected(Collision2D collision)
    {
        Vector2 n = collision.contacts[0].normal;
        Vector2 dir = Vector2.Reflect(velocity, n).normalized;
        float s = velocity.magnitude;                             // ★ 反射後も“直前の速さ”を維持
        rb.linearVelocity = dir * s; 
    }
    public void SmallFireShot()
    {
        Vector2 origin = transform.position;
        Vector2 centerPosition = (Vector2.zero - origin).normalized;
        float stepDeg = (burstCount == 1) ? 0f : (fanAngle * 2f) / (burstCount - 1);
        float half    = (burstCount - 1) * 0.5f;
        for(int i = 0; i < burstCount; i++)
            {
                float ang = (i - half) * stepDeg; 
                Vector2 dir = Rotate(centerPosition,ang);
                var inst = ObjectPool.Instance.GetPoolObject("Small", origin);
                inst.Launch(dir);
            }
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
        reflectionCnt = 0;
    }
    static Vector2 Rotate(Vector2 v, float degrees)
    {
        float r = degrees * Mathf.Deg2Rad;
        float c = Mathf.Cos(r), s = Mathf.Sin(r);
        return new Vector2(v.x * c - v.y * s, v.x * s + v.y * c);
    }
    
}