using UnityEngine;

public class SmallFire : FireBase
{
    public string tagName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("OutSide"))
        {
            ObjectPool.Instance.ReturnToPool(this, tagName);
        }
    }

}