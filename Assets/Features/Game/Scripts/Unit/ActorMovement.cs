using UnityEngine;

public class ActorMovement : MonoBehaviour, IMovable
{
    public event System.Action<bool> OnStateChanged;

    public float moveSpeed = 5f;
    public float rayDistance = 1f; // Ray 길이
    public LayerMask wallLayer; // 벽 레이어 설정

    private Vector2 currentDirction;

    void Start()
    {
        wallLayer = LayerMask.GetMask("AllyBoss", "EnemyBoss");
    }

    // 이동을 처리하는 메서드 (IMovable 인터페이스 구현)
    public void Move(float direction)
    {
        OnStateChanged?.Invoke(true);

        currentDirction.x = direction;
        
        // ✅ 벽 감지 Ray
        bool isBlocked = Physics2D.Raycast(transform.position, currentDirction, rayDistance, wallLayer);

        if (!isBlocked)
        {
            transform.position += moveSpeed * Time.deltaTime * (Vector3)currentDirction;   
        }
    }

    private void OnDrawGizmos()
    {
        // ✅ Debug Ray 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)currentDirction * rayDistance);
    }
}
