using UnityEngine;

public class SKScript : MonoBehaviour
{
    private Player playerScript;  // プレイヤーオブジェクトの参照

    void Start()
    {
        // プレイヤーオブジェクトのスクリプトを取得
        playerScript = GetComponentInParent<Player>();
    }

    // 衝突時に親のメソッドを呼び出して処理を変更
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // プレイヤーのメソッドを呼び出す
            playerScript.OnChildCollision("Enemy");

            // 衝突したオブジェクト（敵）を非表示にする
            other.gameObject.SetActive(false);

            // 敵の親オブジェクトを非表示にする
            if (other.transform.parent != null)
            {
                other.transform.parent.gameObject.SetActive(false);
            }

            // このオブジェクト（SKScriptがアタッチされているオブジェクト）を非表示にする
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy2"))
        {
            // プレイヤーのメソッドを呼び出す
            playerScript.OnChildCollision("Enemy2");

            // 衝突したオブジェクト（敵）を非表示にする
            other.gameObject.SetActive(false);

            // 敵の親オブジェクトを非表示にする
            if (other.transform.parent != null)
            {
                other.transform.parent.gameObject.SetActive(false);
            }

            // このオブジェクト（SKScriptがアタッチされているオブジェクト）を非表示にする
            gameObject.SetActive(false);
        }
    }
}
