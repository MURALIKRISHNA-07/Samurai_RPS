using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerAction
{
    LowAttack,
    MidAttack,
    HighAttack,
    DefendLowAttack,
    DefendMidAttack,
    DefendHighAttack,Idle
}

public class PlayerController : MonoBehaviour
{
    public AnimationManager animationManager;
    public Animator animator;
    public Text damageText;
    public Text actionText;
    public Slider healthSlider;
    public float actionTimer = 2.0f; // Timer for performing an action
    private bool isPerformingAction = false;
    private int currentHealth = 100;
    public PlayerAction currentAction = PlayerAction.Idle;
    public bool performNow = false;

    public AudioManager audioManager;

   
    //public AudioSource takeDMG;
    //public AudioSource hit;

    public GameObject camObject;
    private void OnEnable()
    {
        currentAction = PlayerAction.Idle;
    }
    private void Start()
    {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
        
     

    }

    private void Update()
    {
        if (!isPerformingAction)
        {
            if (Input.GetKeyDown(KeyCode.A)) // High Attack
            {
                PerformAction(PlayerAction.HighAttack);
                GameManager._instance.DisplayIndicators(1);
            }
            else if (Input.GetKeyDown(KeyCode.S)) // Mid Attack
            {
                PerformAction(PlayerAction.MidAttack);
                GameManager._instance.DisplayIndicators(2);
            }
            else if (Input.GetKeyDown(KeyCode.D)) // Low Attack
            {
                PerformAction(PlayerAction.LowAttack);
                GameManager._instance.DisplayIndicators(3);
            }
            else if (Input.GetKeyDown(KeyCode.J)) // Defend High Attack
            {
                PerformAction(PlayerAction.DefendHighAttack);
            }
            else if (Input.GetKeyDown(KeyCode.K)) // Defend Mid Attack
            {
                PerformAction(PlayerAction.DefendMidAttack);
            }
            else if (Input.GetKeyDown(KeyCode.L)) // Defend Low Attack
            {
                PerformAction(PlayerAction.DefendLowAttack);
            }

            if (Input.GetKeyDown(KeyCode.H)) // Defend Low Attack
            {
                GameManager._instance.isHint = true;
            }
        }
    }

    public void PerformAction(PlayerAction action)
    {
        if (performNow)
        {
            
           // isPerformingAction = true;
            currentAction = action;
           // animationManager.PlayAnimation(animator, currentAction);

            // Display action performed text
            actionText.text = action.ToString();
          //  performNow = false;


            // Start action timer
          //  Invoke("ResetAction", actionTimer);
        }
    }
    public void PerformAction()
    {
        isPerformingAction = true;
        GameManager._instance.DisplayIndicators(0);
        animationManager.PlayAnimation(animator, currentAction);

        //play hit audio
        switch (currentAction)
        {
            case PlayerAction.LowAttack:
                audioManager.PlayHitSound("low", 0.6f);
                break;
            case PlayerAction.MidAttack:
                audioManager.PlayHitSound("mid", 0.6f);
                break;
            case PlayerAction.HighAttack:
                audioManager.PlayHitSound("high", 0.6f);
                break;
            case PlayerAction.DefendLowAttack:
                break;
            case PlayerAction.DefendMidAttack:
                break;
            case PlayerAction.DefendHighAttack:
                break;
            case PlayerAction.Idle:
                break;
            default:
                break;
        }
        
        // Display action performed text
       // actionText.text = currentAction.ToString();
        performNow = false;
    }
    public void TakeDamage(int damage)
    {
        animationManager.PlayHurtAnimation(animator);
        
        //play damage audio
        audioManager.PlayTakeDamageSound(-0.5f);
        
        //shake camera
        StartCoroutine(ShakeDelay(-0.5f));

        // Calculate damage based on action and update health
      //  int damage = GameManager.Instance.CalculatePlayerDamage(action);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = currentHealth;

        if(currentHealth<=0)
        {
            GameManager._instance.GameCompleted(false);
        }

        // Display damage text
        damageText.text = "Player Damage: " + damage.ToString();
    }

    IEnumerator ShakeDelay(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        camObject.GetComponent<ShakeCamera>().shakeTest = true;
    }

    public void ResetAction()
    {
        isPerformingAction = false;
        currentAction = PlayerAction.Idle;
        actionText.text = currentAction.ToString();
        performNow = true;
    }
}
