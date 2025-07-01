using UnityEngine;

public class KNScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
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
