using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform player;  // プレイヤーオブジェクトの参照
    public float moveSpeed = 0.5f;  // 移動速度
    public float detectionRange = 10f;  // 探知範囲

    private bool isChasing = false;  // プレイヤーを追いかけているか
    private Animator anim;  // アニメーション（必要に応じて）

    void Start()
    {
        if (player == null)
        {
            // プレイヤーのオブジェクトを検索
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        anim = GetComponent<Animator>();  // アニメーターを取得（必要に応じて）
    }

    void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // プレイヤーが探知範囲内にいる場合
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // プレイヤーを追いかける
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    // プレイヤーを追いかける処理
    void ChasePlayer()
    {
        // プレイヤーに向かって移動
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // アニメーションの設定（必要に応じて）
        if (anim != null)
        {
            anim.SetFloat("Speed", moveSpeed);  // 動きのアニメーション（必要に応じて）
        }
    }

    // デバッグ用：探知範囲を視覚的に確認するためにGizmoを表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);  // 探知範囲
    }
}
