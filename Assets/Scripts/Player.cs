using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMouseMover : MonoBehaviour
{
    [Header("追従")]
    public bool instant = true;        // true=マウス位置に即移動 / false=スムーズ追従
    public float moveSpeed;      // スムーズ追従時の速度(u/s)

    [Header("画面内に制限")]
    public bool clampToCamera = true;
    
    [System.Serializable]
    public class Bounds
    {
        public float xMin, xMax, yMin, yMax;
    }
    [SerializeField] Bounds bounds;
    Rigidbody2D rb;
    Vector3 targetWorld;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;  // 自力で動かすのでKinematic
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // 片方がTriggerなら判定できる。Player側をTriggerにして“押されない”ようにする
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Update()
    {
        // マウス座標をワールド座標へ
        targetWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetWorld.z = 0f;

        if (clampToCamera) targetWorld = ClampToCamera(targetWorld);
    }

    void FixedUpdate()
    {
        if (instant)
            rb.MovePosition(targetWorld);
        else
        {
            Vector2 next = Vector2.MoveTowards(rb.position, (Vector2)targetWorld, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(next);
        }
    }

    Vector3 ClampToCamera(Vector3 pos)
    {
        var cam = Camera.main;
        var z = pos.z;
        pos.x = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
        pos.y = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);
        pos.z = z;
        return pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) // 玉のTagを "Ball" にしておく
        {

            GameManager.Instance.ActGameOver();
            // ここでゲームオーバー処理へ。例: タイマー停止/リトライ表示など
            // enabled = false; // 入力停止したい場合
        }
    }
}
