using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public float CenterAreaLimitTop;
    public float CenterAreaLimitBottom;
    public float CenterAreaLimitLeft;
    public float CenterAreaLimitRight;

    public float LimitUp;
    public float LimitDown;
    public float LimitLeft;
    public float LimitRight;

    public GameObject target;

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = gameObject.transform.position;
        Vector3 targetPos = target.transform.position;

        bool isAbove = myPos.y + CenterAreaLimitTop < targetPos.y;
        bool isBelow = myPos.y - CenterAreaLimitBottom > targetPos.y;
        bool isLeft = myPos.x - CenterAreaLimitLeft > targetPos.x;
        bool isRight = myPos.x + CenterAreaLimitRight < targetPos.x;

        if (isAbove && myPos.y < LimitUp)
        {
            myPos.y += (Mathf.Abs(targetPos.y - myPos.y) -CenterAreaLimitTop) * moveSpeed;
        }
        else if (isBelow && myPos.y > LimitDown)
        {
            myPos.y -= (Mathf.Abs(targetPos.y - myPos.y) - CenterAreaLimitBottom) * moveSpeed; 
        }

        if (isLeft && myPos.x > LimitLeft)
        {
            myPos.x -= (Mathf.Abs(targetPos.x - myPos.x) - CenterAreaLimitBottom) * moveSpeed;
        }
        else if (isRight && myPos.x < LimitRight)
        {
            myPos.x += (Mathf.Abs(targetPos.x - myPos.x) - CenterAreaLimitBottom) * moveSpeed;
        }

        gameObject.transform.position = myPos;
    }
}

