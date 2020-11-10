using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI UITimer;
    public TextMeshProUGUI MushroomCount;
    public TextMeshProUGUI EndText;
    public Canvas EndScreen;
    public LayerMask LayerOfGround;

    public Animator PlayerAnimator;
    public Rigidbody2D PlayerRigidBody;

    public float Speed;
    public float RunningModifier;
    public float JumpForce;
    public float NumberOfJumps;

    private int mushroomsCollected;

    private bool isLeftPressed;
    private bool isRightPressed;
    private bool isJumpPressed;
    private float totalTimer;
    private float directionTimer;
    private bool isAlive;
    private bool facingRight=true;
    private float actualJumps;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        totalTimer = 0f;
        actualJumps = NumberOfJumps;
    }

    void Update()
    {
        if (!isAlive)
            return;

#if UNITY_EDITOR
        //For easy testing
        if (Input.GetKeyDown(KeyCode.RightArrow))
            onRightBtClick();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            onLeftBtClick();
        if (Input.GetKeyDown(KeyCode.Space))
            onJumpBtClick();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            onRightBtRelease();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            onLeftBtRelease();
#endif

        float moveDirection = isLeftPressed? -1 : (isRightPressed? 1 : 0); //-1,0,1 = left, idle, right
        ChangeGameObjectOrientation(moveDirection);
        if(gameObject.transform.localPosition.y < -3.0f)
        {
            Die();
        }
        
        totalTimer += Time.deltaTime;
        UITimer.text = Utils.GetFormattedTime(totalTimer);

        if (moveDirection != 0)
        {
            directionTimer += Time.deltaTime;
            if (directionTimer> 1f)
            {
                //automatically increases speed
                moveDirection *= RunningModifier;
                PlayerAnimator.SetFloat("Movement", 2f);
            }
            else
            {
                PlayerAnimator.SetFloat("Movement", 1f);
            }                
        }
        else
        {
            directionTimer = 0f;
            PlayerAnimator.SetFloat("Movement", 0f);
        }
            
        PlayerRigidBody.velocity = new Vector2(moveDirection*Speed ,PlayerRigidBody.velocity.y);

        Vector2 centerOfFeet = (Vector2)gameObject.transform.position + new Vector2(facingRight ? -0.3f : 0.3f, -0.48f);
        if (Physics2D.OverlapCircle(centerOfFeet,0.15f, LayerOfGround) != null
            && PlayerRigidBody.velocity.y < 0.02f) //is either falling or on ground
        {
            //Debug.Log(centerOfFeet);
            actualJumps = NumberOfJumps;
            PlayerAnimator.SetBool("isJumping", false);
        }

        if (isJumpPressed)
        {
            isJumpPressed = false;
            if (actualJumps > 0)
            {
                --actualJumps;
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, JumpForce);
                PlayerAnimator.SetBool("isJumping", true);
            }
        }
    }

    #region ButtonsControl
    public void onLeftBtClick()
    {
        isLeftPressed = true;
    }

    public void onLeftBtRelease()
    {
        isLeftPressed = false;
    }

    public void onRightBtClick()
    {
        isRightPressed = true;
    }

    public void onRightBtRelease()
    {
        isRightPressed = false;
    }

    public void onJumpBtClick()
    {
        isJumpPressed = true;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
         switch(collision.gameObject.tag)
        {
            case "GoodMushroom":
                collision.gameObject.SetActive(false);
                mushroomsCollected += 1;
                AnalyticsController.I.SendMushroomCollected(mushroomsCollected);
                MushroomCount.text = string.Format("X {0}", mushroomsCollected);
                if (mushroomsCollected >= 3)
                    NumberOfJumps = 2;
                CheckWin();
                break;
            case "BadMushroom":
            case "Water":
                Die();
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        isAlive = false;
        PlayerAnimator.SetBool("isDead", true);
        EndText.text = "Você perdeu!";
        StartCoroutine("ShowEndScreen", 1.2f);
        AnalyticsController.I.SendDie();
    }

    private IEnumerator ShowEndScreen(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EndScreen.gameObject.SetActive(true);
    }

    private void ChangeGameObjectOrientation(float direction)
    {
        Vector3 scale = gameObject.transform.localScale;
        if (facingRight && direction < 0)
        {
            facingRight = false;
            scale.x *= -1;
            gameObject.transform.localPosition += new Vector3(-0.58f, 0f);
            gameObject.transform.localScale = scale;
        }
        else if (!facingRight && direction > 0)
        {
            facingRight = true;
            scale.x *= -1;
            gameObject.transform.localPosition += new Vector3(+0.58f, 0f);
            gameObject.transform.localScale = scale;
        }
    }

    private void CheckWin()
    {
        if (mushroomsCollected > 4f)
        {
            string playTime = Utils.GetFormattedTime(totalTimer);
            EndText.text = String.Format("Você ganhou em {0} !",playTime);

            float old_record = PlayerPrefs.GetFloat("Record");
            if(old_record == 0 || old_record > totalTimer)
            {
                PlayerPrefs.SetFloat("Record", totalTimer);
            }

            StartCoroutine("ShowEndScreen", 0.1f);
            isAlive = false;
            PlayerAnimator.SetFloat("Movement", 0f);
            AnalyticsController.I.SendWin(totalTimer);
        }
    }
}
