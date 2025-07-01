using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour
{
    // 現在のWASD判定
    private string currentKey = "";

    // 表示するオブジェクト（WASD毎に切り替えるオブジェクト）
    public GameObject wObject;  // Wに対応するオブジェクト
    public GameObject aObject;  // Aに対応するオブジェクト
    public GameObject sObject;  // Sに対応するオブジェクト
    public GameObject dObject;  // Dに対応するオブジェクト

    private GameObject currentObject; // 現在表示されているオブジェクト

    // UI Text の参照 (オプション)
    public UnityEngine.UI.Text wasdText;  // UI表示用

    void Update()
    {
        // WASDキーの判定
        CheckWASDInput();

        // Returnキーが押されたときにオブジェクトを表示
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleReturnKeyPress();
        }
    }

    // WASDキーの入力判定
    private void CheckWASDInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            UpdateUI("W key pressed: Move Up");
            currentKey = "W";
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateUI("A key pressed: Move Left");
            currentKey = "A";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateUI("S key pressed: Move Down");
            currentKey = "S";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateUI("D key pressed: Move Right");
            currentKey = "D";
        }
    }

    // Returnキーが押された時の処理
    private void HandleReturnKeyPress()
    {
        // 既にオブジェクトが表示されていれば削除
        if (currentObject != null)
        {
            currentObject.SetActive(false);
        }

        // 現在選択されているWASDキーに応じたオブジェクトを表示
        if (currentKey == "W")
        {
            currentObject = wObject;
        }
        else if (currentKey == "A")
        {
            currentObject = aObject;
        }
        else if (currentKey == "S")
        {
            currentObject = sObject;
        }
        else if (currentKey == "D")
        {
            currentObject = dObject;
        }

        // 新しいオブジェクトを表示
        if (currentObject != null)
        {
            currentObject.SetActive(true);
            // 1秒後に非表示にするコルーチンを開始
            StartCoroutine(HideObjectAfterDelay(currentObject, 1f));
        }
    }

    // 1秒後にオブジェクトを非表示にするコルーチン
    private IEnumerator HideObjectAfterDelay(GameObject obj, float delay)
    {
        // 指定した時間待つ
        yield return new WaitForSeconds(delay);

        // 非表示にする
        obj.SetActive(false);
    }

    // UI Textを更新するメソッド
    private void UpdateUI(string message)
    {
        if (wasdText != null)
        {
            wasdText.text = message;
        }
    }
}
