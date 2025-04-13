using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public enum MoveMode { Forward, Backward }

    [SerializeField] Transform[] positions;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] MoveMode moveMode = MoveMode.Forward;
    [SerializeField] float[] waitTimes;
    [SerializeField] float defaultWaitTime = 0f;// ⬅️ 每个点的停留时间

    private int i = 0;
    private int movingDirection = 1;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Start()
    {
        transform.position = positions[0].position;
        if (waitTimes == null || waitTimes.Length != positions.Length)
        {
            waitTimes = new float[positions.Length];
            for (int j = 0; j < waitTimes.Length; j++)
            {
                waitTimes[j] = defaultWaitTime;
            }
        }
    }

    void Update()
    {
        if (positions.Length == 0) return;

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
            }
            return;
        }

        i = Mathf.Clamp(i, 0, positions.Length - 1);
        transform.position = Vector2.MoveTowards(transform.position, positions[i].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, positions[i].position) < 0.02f)
        {
            // 到达目标点，开始等待
            if (waitTimes.Length > i)
                waitTimer = waitTimes[i];
            else
                waitTimer = 0f;

            isWaiting = true;

            // 准备下一个点
            i += movingDirection;

            switch (moveMode)
            {
                case MoveMode.Forward:
                    if (i >= positions.Length)
                        i = 0;
                    break;

                case MoveMode.Backward:
                    if (i >= positions.Length || i <= 0)
                        movingDirection *= -1;
                    break;
            }
        }
    }
}