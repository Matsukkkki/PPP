using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 5f;
    private float jumpForce;
    private float horizontalMove;
    private float verticalMove;
    private bool isGrounded;

    // ジャンプ用のチェック
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f; // 地面チェックの半径を指定

    // プレイヤーの高さ
    private float playerHeight;

    // ジャンプの最大高さ（主人公の3倍）
    private float maxJumpHeightMultiplier = 3f;

    // HPの設定
    public int maxHealth = 10;  // 最大HP
    private int currentHealth;   // 現在のHP

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // プレイヤーの高さを取得
        playerHeight = transform.localScale.y;

        // ジャンプ力を計算（3倍の高さにする）
        jumpForce = Mathf.Sqrt(2 * 9.81f * playerHeight * maxJumpHeightMultiplier);

        // Rigidbody の設定を確認
        rb.useGravity = true; // 重力を使用
        rb.freezeRotation = true; // 回転を固定（回転させたくない場合）

        // 初期HP設定
        currentHealth = maxHealth;
    }

    void Update()
    {
        // 水平移動
        horizontalMove = Input.GetAxis("Horizontal"); // A/Dまたは左矢印/右矢印
        verticalMove = Input.GetAxis("Vertical"); // W/Sまたは上矢印/下矢印

        // 地面にいるかチェック (Ground Layer と接触しているか)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // 地面にいるかどうかをログに出力
        Debug.Log("Is Grounded: " + isGrounded);

        // ジャンプ処理
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping!");  // ジャンプ開始のログ
            // 現在の速度のX, Z方向を維持し、Y方向にジャンプ力を加える
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        // ジャンプ中の移動処理
        if (!isGrounded)
        {
            // ジャンプ中に移動した場合、軌道に影響を与える
            Vector3 movement = new Vector3(horizontalMove, 0, verticalMove) * moveSpeed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        }
        else
        {
            // 地面にいる時は通常の移動
            Vector3 move = new Vector3(horizontalMove, 0, verticalMove) * moveSpeed;
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        }
    }

    // ダメージを受ける処理（例: 衝突や敵からの攻撃）
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        // HPの変化をログに出力
        Debug.Log("Current Health: " + currentHealth);
    }

    // プレイヤーが死亡する処理
    private void Die()
    {
        Debug.Log("Player has died.");
        // 死亡時の処理（例えば、ゲームオーバー画面を表示）
    }
}
