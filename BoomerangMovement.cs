using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    public float maxDistance = 3f;  // 最大移動距離
    public float returnSpeed = 10f;  // 戻る速度
    private Transform spawnPoint;   // プレイヤーの位置（Transform）

    private Vector3 startPosition;  // 初期位置
    private float traveledDistance = 0f;  // 進んだ距離
    private bool isReturning = false;  // 戻っているかどうか
    private float timeAlive = 0f;  // ブーメランの生存時間
    public float lifeTime = 5f;  // ブーメランの寿命（例: 5秒）

    private static int boomerangCount = 0;  // 現在アクティブなブーメランの数

    // 戻ってきたことを通知するイベント
    public event System.Action OnBoomerangReturn;

    void Start()
    {
        startPosition = transform.position;  // 初期位置を保存
        boomerangCount++;  // 新しいブーメランが発射されたらカウントを増加
    }

    void Update()
    {
        // 時間が経過したらプレイヤーに戻る
        timeAlive += Time.deltaTime;

        // 3秒経過したらプレイヤーに戻る
        if (timeAlive >= 3f)
        {
            isReturning = true;
        }

        if (!isReturning)
        {
            // 前進
            MoveBoomerang();
        }
        else
        {
            // プレイヤーに向かって戻る
            ReturnToPlayer();
        }

        // ブーメランの寿命を超えた場合は削除
        if (timeAlive > lifeTime)
        {
            Destroy(gameObject);  // ブーメランを削除
            boomerangCount--;  // カウントを減らす
        }
    }

    // ブーメランを前進させるメソッド
    void MoveBoomerang()
    {
        // 移動距離が最大移動距離に達していない場合
        if (traveledDistance < maxDistance)
        {
            // プレイヤー方向に前進
            transform.Translate(Vector3.forward * returnSpeed * Time.deltaTime);
            traveledDistance += returnSpeed * Time.deltaTime;
        }
        else
        {
            // 最大距離を超えたら戻り始める
            isReturning = true;
        }
    }

    // プレイヤーに戻るメソッド
    void ReturnToPlayer()
    {
        if (spawnPoint == null) return;  // spawnPointがnullの場合は戻らない

        // プレイヤーの位置を目指す方向を計算
        Vector3 direction = (spawnPoint.position - transform.position).normalized;

        // プレイヤーに向かって移動
        transform.Translate(direction * returnSpeed * Time.deltaTime);

        // プレイヤーに近づいたら戻り完了
        if (Vector3.Distance(transform.position, spawnPoint.position) < 0.1f)
        {
            // 戻り完了を通知
            OnBoomerangReturn?.Invoke();
            Destroy(gameObject);  // 初期位置に戻ったら削除
            boomerangCount--;  // カウントを減らす
        }
    }

    // spawnPoint を設定するメソッド
    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
