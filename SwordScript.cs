using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour
{
    // ���݂�WASD����
    private string currentKey = "";

    // �\������I�u�W�F�N�g�iWASD���ɐ؂�ւ���I�u�W�F�N�g�j
    public GameObject wObject;  // W�ɑΉ�����I�u�W�F�N�g
    public GameObject aObject;  // A�ɑΉ�����I�u�W�F�N�g
    public GameObject sObject;  // S�ɑΉ�����I�u�W�F�N�g
    public GameObject dObject;  // D�ɑΉ�����I�u�W�F�N�g

    private GameObject currentObject; // ���ݕ\������Ă���I�u�W�F�N�g

    // UI Text �̎Q�� (�I�v�V����)
    public UnityEngine.UI.Text wasdText;  // UI�\���p

    void Update()
    {
        // WASD�L�[�̔���
        CheckWASDInput();

        // Return�L�[�������ꂽ�Ƃ��ɃI�u�W�F�N�g��\��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleReturnKeyPress();
        }
    }

    // WASD�L�[�̓��͔���
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

    // Return�L�[�������ꂽ���̏���
    private void HandleReturnKeyPress()
    {
        // ���ɃI�u�W�F�N�g���\������Ă���΍폜
        if (currentObject != null)
        {
            currentObject.SetActive(false);
        }

        // ���ݑI������Ă���WASD�L�[�ɉ������I�u�W�F�N�g��\��
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

        // �V�����I�u�W�F�N�g��\��
        if (currentObject != null)
        {
            currentObject.SetActive(true);
            // 1�b��ɔ�\���ɂ���R���[�`�����J�n
            StartCoroutine(HideObjectAfterDelay(currentObject, 1f));
        }
    }

    // 1�b��ɃI�u�W�F�N�g���\���ɂ���R���[�`��
    private IEnumerator HideObjectAfterDelay(GameObject obj, float delay)
    {
        // �w�肵�����ԑ҂�
        yield return new WaitForSeconds(delay);

        // ��\���ɂ���
        obj.SetActive(false);
    }

    // UI Text���X�V���郁�\�b�h
    private void UpdateUI(string message)
    {
        if (wasdText != null)
        {
            wasdText.text = message;
        }
    }
}
