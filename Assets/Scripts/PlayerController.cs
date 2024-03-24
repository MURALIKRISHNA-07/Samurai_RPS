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
            if (Input.GetKeyDown(KeyCode.A)) // Low Attack
            {
                PerformAction(PlayerAction.LowAttack);
            }
            else if (Input.GetKeyDown(KeyCode.S)) // Mid Attack
            {
                PerformAction(PlayerAction.MidAttack);
            }
            else if (Input.GetKeyDown(KeyCode.D)) // High Attack
            {
                PerformAction(PlayerAction.HighAttack);
            }
            else if (Input.GetKeyDown(KeyCode.J)) // Defend Low Attack
            {
                PerformAction(PlayerAction.DefendLowAttack);
            }
            else if (Input.GetKeyDown(KeyCode.K)) // Defend Mid Attack
            {
                PerformAction(PlayerAction.DefendMidAttack);
            }
            else if (Input.GetKeyDown(KeyCode.L)) // Defend High Attack
            {
                PerformAction(PlayerAction.DefendHighAttack);
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
        animationManager.PlayAnimation(animator, currentAction);

        // Display action performed text
       // actionText.text = currentAction.ToString();
        performNow = false;
    }
    public void TakeDamage(int damage)
    {
        animationManager.PlayHurtAnimation(animator);
        // Calculate damage based on action and update health
      //  int damage = GameManager.Instance.CalculatePlayerDamage(action);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = currentHealth;

        // Display damage text
        damageText.text = "Damage: " + damage.ToString();

    }

    public void ResetAction()
    {
        isPerformingAction = false;
        currentAction = PlayerAction.Idle;
        actionText.text = currentAction.ToString();
        performNow = true;
    }
}
