using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour
{
    public Transform player;  // プレイヤーオブジェクトの参照
    public float moveSpeed = 5f;  // 移動速度
    public float throwDistance = 2f;  // 投げる距離
    public float coolDownTime = 3f;  // クールタイム（発射後の待機時間）
    public float lifeTime = 3f;  // ブーメランの寿命
    public int damage = 2;  // ダメージ

    private Vector3 startPosition;  // 初期位置
    private bool isReturning = false;  // 反転後に戻るかどうか
    private float traveledDistance = 0f;  // 飛行した距離
    private float timeSinceThrow = 0f;  // 発射から経過した時間
    private bool canThrow = true;  // 発射可能かどうか

    void Start()
    {
        // プレイヤーの参照がない場合は、タグで探す
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // 初期位置を保存
        startPosition = transform.position;
    }

    void Update()
    {
        if (!canThrow)
        {
            // クールタイムが終了するまで待機
            timeSinceThrow += Time.deltaTime;
            if (timeSinceThrow >= coolDownTime)
            {
                canThrow = true;
                timeSinceThrow = 0f;  // クールタイムリセット
            }
            return;
        }

        // プレイヤーが近い場合にブーメランを発射
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= throwDistance && canThrow)
        {
            FireBoomerang();
        }

        // ブーメランの寿命が尽きたら消える
        if (timeSinceThrow > lifeTime)
        {
            Destroy(gameObject);  // ブーメランを消す
        }
    }

    void FireBoomerang()
    {
        canThrow = false;  // 発射後はクールタイム中
        timeSinceThrow = 0f;  // 発射からの経過時間リセット

        // 飛行する方向を決定
        Vector3 direction = (player.position - transform.position).normalized;

        // 飛行を開始
        StartCoroutine(MoveBoomerang(direction));
    }

    // ブーメランを動かすコルーチン
    private IEnumerator MoveBoomerang(Vector3 direction)
    {
        float traveledDistance = 0f;

        // プレイヤーの方向に飛ぶ
        while (traveledDistance < throwDistance && timeSinceThrow <= lifeTime)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            traveledDistance += moveSpeed * Time.deltaTime;
            yield return null;
        }

        // 反転して戻る
        isReturning = true;
        traveledDistance = 0f;

        // 初期位置に戻る
        while (isReturning && timeSinceThrow <= lifeTime)
        {
            Vector3 returnDirection = (startPosition - transform.position).normalized;
            transform.Translate(returnDirection * moveSpeed * Time.deltaTime, Space.World);
            traveledDistance += moveSpeed * Time.deltaTime;
            if (traveledDistance >= throwDistance)
            {
                isReturning = false;
                Destroy(gameObject);  // 戻ったらブーメランを削除
            }
            yield return null;
        }
    }

    // 衝突判定（プレイヤーに当たったらダメージを与える）
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);  // ダメージを与えた後はブーメランを削除
        }
    }
}
