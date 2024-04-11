using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class EnemyController : MonoBehaviour
{
    public AnimationManager animationManager;
    public Animator animator;
    public Text damageText;
    public Text actionText;
    public Slider healthSlider;
    public float actionTimerMin = 1.0f; // Minimum time to perform an action
    public float actionTimerMax = 3.0f; // Maximum time to perform an action
    public EnemyProfileControll enemyProfile;
    public PlayerAction currentAction = PlayerAction.Idle;

    public AudioManager audioManager;

    public GameObject camObject;

    private bool isPerformingAction = false;
    private int currentHealth = 100;

    private int defensevalue;
    private int attackvalue;
    private int actionvalue;

    private void OnEnable()
    {
          currentAction = PlayerAction.Idle;
    }
    private void Start()
    {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
       // Invoke("PerformAction", Random.Range(actionTimerMin, actionTimerMax)); // Start initial action
        animationManager.PlayAnimation(animator, currentAction);
        actionText.text = currentAction.ToString();
        GetValues();
    }

    public void PerformAction()
    {
        if (!isPerformingAction)
        {
            isPerformingAction = true;

            // Generate a random number to determine if the enemy will attack or defend
           // int randomValue = Random.Range(1, 100);


            if (actionvalue < enemyProfile.eProfile.attackValue)
            {
                PerformAttack();
            }
            else if (actionvalue < enemyProfile.eProfile.attackValue + enemyProfile.eProfile.defensiveValue)
            {
                PerformDefend();
            }
            else
            {
                PerformIdle();
            }

            animationManager.PlayAnimation(animator, currentAction);
           
            // Start action timer
           // Invoke("ResetAction", Random.Range(actionTimerMin, actionTimerMax));
        }
    }

    void PerformAttack()
    {
       // int randomValue = Random.Range(1, 100);
        if(attackvalue< enemyProfile.eProfile.lowAttackProbability)
        {
            currentAction = PlayerAction.LowAttack;
            
            //hit audio
            audioManager.PlayHitSound("low", 0.6f);
        }
        else if(attackvalue< enemyProfile.eProfile.highAttackProbability)
        {
            currentAction = PlayerAction.HighAttack;
            
            //hit audio
            audioManager.PlayHitSound("high", 0.6f);
        }
        else
        {
            currentAction = PlayerAction.MidAttack;
            
            //hit audio
            audioManager.PlayHitSound("mid", 0.6f);
        }
        actionText.text = currentAction.ToString();

        Debug.Log("Enemy performing: "+currentAction);
        // Implement attack logic here
    }

    void PerformHint()
    {
        int randomValue = Random.Range(1, 100);
        PlayerAction hintAction = PlayerAction.Idle;

        if (randomValue > 35)
        {
            hintAction = PlayerAction.LowAttack;
        }
        else if (randomValue > 80)
        {
            hintAction = PlayerAction.HighAttack;
        }
        else
        {
            hintAction = PlayerAction.MidAttack;
        }
        actionText.text = "Hint: "+currentAction.ToString();
        // Play the hint animation
        animationManager.ShowHintAnimation(animator,hintAction);

        Debug.Log("Enemy performing Hint!");
        // Implement attack logic here
    }


    void PerformDefend()
    {
      //  int randomValue = Random.Range(1, 100);
        if (defensevalue > enemyProfile.eProfile.lowDefenseProbability)
        {
            currentAction = PlayerAction.DefendLowAttack;

        }
        else if (defensevalue > enemyProfile.eProfile.highDefenseProbability)
        {
            currentAction = PlayerAction.DefendHighAttack;
        }
        else
        {
            currentAction = PlayerAction.DefendMidAttack;
        }
        actionText.text = currentAction.ToString();

        Debug.Log("Enemy performing defense!"+currentAction.ToString());
        // Implement defense logic here
    }

    void PerformIdle()
    {
        animationManager.PlayAnimation(animator, PlayerAction.Idle);
        currentAction = PlayerAction.Idle;
        actionText.text = currentAction.ToString();
        Debug.Log("Enemy idling!");
        // Implement idle logic here
    }
   

    public void ResetAction()
    {
        isPerformingAction = false;

        // Return to idle state
        currentAction = PlayerAction.Idle;
        actionText.text = currentAction.ToString();
        GetValues();
       
        // COMMENTING THIS LINE FIXED THE ENEMY DELAY ISSUE
        //animationManager.PlayAnimation(animator, currentAction);
    }

    public void TakeDamage(int damage)
    {
        // Play hurt animation
        animationManager.PlayHurtAnimation(animator);
        
        //play damage audio
        audioManager.PlayTakeDamageSound(-0.5f);
        
        //shake camera
        StartCoroutine(ShakeDelay(-0.5f));

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            GameManager._instance.GameCompleted(false);
        }

        // Display damage text
        damageText.text = "Enemy Damage: " + damage.ToString();

      
    }
    
    IEnumerator ShakeDelay(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        camObject.GetComponent<ShakeCamera>().shakeTest = true;
    }

    void GetValues()
    {
        actionvalue = Random.Range(1, 100);
        defensevalue = Random.Range(1, 100);
        attackvalue = Random.Range(1, 100);

    }


    public int ShowHint()
    {
        if (actionvalue < enemyProfile.eProfile.attackValue)
        {
            if (attackvalue < enemyProfile.eProfile.lowAttackProbability)
            {
                return 1;
            }
            else if (attackvalue < enemyProfile.eProfile.highAttackProbability)
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            return 0;
        }
    }

}
