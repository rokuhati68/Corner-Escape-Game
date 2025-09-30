using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class FlashBlinker : MonoBehaviour
{
    [Header("Flash (チカチカ)")]
    [SerializeField, Min(0f)] float onTime  = 0.06f;  // 点灯している時間
    [SerializeField, Min(0f)] float offTime = 0.06f;  // 消灯している時間
    [SerializeField] bool playOnEnable = true;
    [SerializeField] bool ignoreTimeScale = true;     // 一時停止中も点滅するなら true

    CanvasGroup cg;
    Sequence seq;

    void Awake()
    {
        // 親ごと一括で透過させる
        cg = GetComponent<CanvasGroup>();
        if (!cg) cg = gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1f;
    }

    void OnEnable()
    {
        if (playOnEnable) StartBlink();
    }

    void OnDisable()
    {
        StopBlink();
    }

    public void StartBlink()
    {
        StopBlink(); // 二重再生ガード

        seq = DOTween.Sequence()
            .SetUpdate(ignoreTimeScale)
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy) // 破棄時自動Kill
            // 1サイクル：ON → 待機 → OFF → 待機
            .AppendCallback(() => cg.alpha = 1f)
            .AppendInterval(onTime)
            .AppendCallback(() => cg.alpha = 0f)
            .AppendInterval(offTime)
            .SetLoops(-1, LoopType.Restart);
    }

    public void StopBlink()
    {
        if (seq != null && seq.IsActive()) seq.Kill();
        cg.alpha = 1f; // 止めたら点灯状態に戻す
    }
}
