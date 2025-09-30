using UnityEngine;
public class FireGameOver:MonoBehaviour
{
    public FireBase fire;
    public string tagName;
    public int layerTag;
    void OffActive()
    {
        bool isActive = gameObject.activeSelf;
        gameObject.layer = layerTag;
        if(isActive == true)
        {
            ObjectPool.Instance.ReturnToPool(fire, tagName);
        }

    }
    void OnEnable(){ GameManager.Instance.OnGameOver += OffActive; }
    void OnDisable(){ GameManager.Instance.OnGameOver -= OffActive; }

}