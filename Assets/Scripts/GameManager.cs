using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerController playerController;
    private EnemyController enemyController;

    public float actionDuration = 3.0f; // Duration of each action
    public float actionInterval = 1.0f; // Time interval between enemy actions
    public Text timerText;

    private float timer = 0.0f;
    private bool isPerformingAction = false;
    private bool enemyActionPerformed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Get references to enemy and player controllers
        enemyController = FindObjectOfType<EnemyController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        // Get references to enemy and player controllers
        //enemyController = FindObjectOfType<EnemyController>();
        //playerController = FindObjectOfType<PlayerController>();

        // Start the game loop
        StartGameLoop();
    }

    void StartGameLoop()
    {
        // Start the timer
        timer = 0.0f;
        isPerformingAction = true;
        enemyActionPerformed = false;

        // Reset player and enemy actions
        playerController.ResetAction();
        enemyController.ResetAction();
        actionDuration = 3;
        //actionDuration =Mathf.RoundToInt( Random.Range(1, 6));
    }

    void Update()
    {
        // Update the timer if an action is currently being performed
        if (isPerformingAction)
        {
            timer += Time.deltaTime;
            timerText.text = Mathf.RoundToInt(actionDuration-timer).ToString();

            // Check if the action duration has elapsed
            if (timer >= actionDuration)
            {
                // Perform player action
                enemyController.PerformAction();
                playerController.PerformAction();

                // If enemy action hasn't been performed, perform it
                //if (!enemyActionPerformed)
                //{

                //    enemyController.PerformAction();
                //    enemyActionPerformed = true;
                //}
                StartCoroutine(DetermineOutcome(playerController.currentAction, enemyController.currentAction));
                isPerformingAction = false;
                // Reset timer
                Invoke("StartGameLoop", actionInterval);
            }
        }
    }

    

    private void OnEnable()
    {
        // Start the game loop when the script is enabled
        StartGameLoop();
    }


    // Define a 2D array to represent the relationships between player and enemy actions
    // Rows represent player actions, columns represent enemy actions
    private readonly int[,] outcomeMatrix = new int[7, 7]
    {
        //         LowAttack   MidAttack   HighAttack  DefendLow   DefendMid   DefendHigh  Idle
        /* Low */    { 2,          1,          1,          2,          0,          0,          0 },
        /* Mid */    { 0,          2,          1,          0,          2,          2,          0 },
        /* High */   { 0,          0,          2,          0,          0,          1,          0 },
        /* DefLow */ { 2,          0,          1,          2,          2,          2,          0 },
        /* DefMid */ { 1,          2,          1,          2,          2,          2,          0 },
        /* DefHigh */{ 1,          2,          2,          2,          2,          2,          0 },
        /* Idle */   { 1,          1,          1,          1,          1,          1,          2 }
    };

    // Function to determine the outcome of the current actions
    IEnumerator DetermineOutcome(PlayerAction playerAction, PlayerAction enemyAction)
    {
        // Map player and enemy actions to array indices
        int playerIndex = (int)playerAction;
        int enemyIndex = (int)enemyAction;

        // Get the outcome from the array
        int outcome = outcomeMatrix[playerIndex, enemyIndex];

        // Apply damage based on the outcome
        switch (outcome)
        {
            case 0: // Player wins
                switch (playerAction)
                {
                    case PlayerAction.LowAttack:
                        yield return new WaitForSeconds(0.8f);
                        enemyController.TakeDamage(20);
                        break;
                    case PlayerAction.MidAttack:
                        yield return new WaitForSeconds(0.8f);
                        enemyController.TakeDamage(20);
                        break;
                    case PlayerAction.HighAttack:
                        yield return new WaitForSeconds(0.8f);
                        enemyController.TakeDamage(20);
                        break;
                    default:
                        break;
                }
                break;
            case 1: // Enemy wins
                switch (enemyAction)
                {
                    case PlayerAction.LowAttack:
                        yield return new WaitForSeconds(0.8f);
                        playerController.TakeDamage(20);
                        break;
                    case PlayerAction.MidAttack:
                        yield return new WaitForSeconds(0.8f);
                        playerController.TakeDamage(20);
                        break;
                    case PlayerAction.HighAttack:
                        yield return new WaitForSeconds(0.8f);
                        playerController.TakeDamage(20);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

       
    }
}
