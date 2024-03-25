using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class EnemyProfile
{
    public EnemyType enemyType;
   
    // Add any other parameters you want to define for the enemy profile
    [Range(0, 100)]
    public int attackValue;
    [Range(0, 100)]
    public int defensiveValue;
    [Range(0, 100)]
    public int idleValue;
}

public enum EnemyType
{
    Aggressive,
    Defensive,
    Normal
}

public class EnemyController : MonoBehaviour
{
    public AnimationManager animationManager;
    public Animator animator;
    public Text damageText;
    public Text actionText;
    public Slider healthSlider;
    public float actionTimerMin = 1.0f; // Minimum time to perform an action
    public float actionTimerMax = 3.0f; // Maximum time to perform an action
    public EnemyProfile enemyProfile;
    public PlayerAction currentAction = PlayerAction.Idle;
    public AudioSource hit;

    private bool isPerformingAction = false;
    private int currentHealth = 100;

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
    }

    public void PerformAction()
    {
        if (!isPerformingAction)
        {
            isPerformingAction = true;

            // Generate a random number to determine if the enemy will attack or defend
            int randomValue = Random.Range(1, 100);


            if (randomValue < enemyProfile.attackValue)
            {
                PerformAttack();
            }
            else if (randomValue < enemyProfile.attackValue + enemyProfile.defensiveValue)
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
        int randomValue = Random.Range(1, 100);
        if(randomValue<35)
        {
            currentAction = PlayerAction.LowAttack;
            
            //hit audio
            hit.pitch = 0.9f;
            hit.PlayDelayed(0.6f);
        }
        else if(randomValue<80)
        {
            currentAction = PlayerAction.HighAttack;
            
            //hit audio
            hit.pitch = 1.1f;
            hit.PlayDelayed(0.6f);
        }
        else
        {
            currentAction = PlayerAction.MidAttack;
            
            //hit audio
            hit.pitch = 1f;
            hit.PlayDelayed(0.6f);
        }
        actionText.text = currentAction.ToString();

        Debug.Log("Enemy performing attack!"+currentAction.ToString());
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
        int randomValue = Random.Range(1, 100);
        if (randomValue > 35)
        {
            currentAction = PlayerAction.DefendLowAttack;

        }
        else if (randomValue > 80)
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
        animationManager.PlayAnimation(animator, currentAction);
    }

    public void TakeDamage(int damage)
    {
        // Play hurt animation
        animationManager.PlayHurtAnimation(animator);
        
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = currentHealth;

        // Display damage text
        damageText.text = "Damage: " + damage.ToString();

      
    }
}
