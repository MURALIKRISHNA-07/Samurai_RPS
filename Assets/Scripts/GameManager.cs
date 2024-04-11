using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private PlayerController _playerController;
    private EnemyController _enemyController;
    private AudioManager _audioManager;

    public float actionDuration = 3.0f; // Duration of each action
    public float actionInterval = 1.0f; // Time interval between enemy actions
    public Text timerText;

    public GameObject highAttackIndicator;
    public GameObject midAttackIndicator;
    public GameObject lowAttackIndicator;

    private float _timer = 0.0f;
    private bool _isPerformingAction = false;
    private bool _enemyActionPerformed = false;
    private bool _gameover = false;

    public GameObject Gameover;
    public GameObject won;
    public GameObject failed;

    [SerializeField]
    private int _damagevalue=35;

    public GameObject Sword;
    public bool isHint = false;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Get references to enemy and player controllers
        _enemyController = FindObjectOfType<EnemyController>();
        _playerController = FindObjectOfType<PlayerController>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        // Get references to enemy and player controllers
        //enemyController = FindObjectOfType<EnemyController>();
        //playerController = FindObjectOfType<PlayerController>();

        // Start the game loop
        RepeatingBuffer();
    }

    private void StartGameLoop()
    {
        if (!_gameover)
        {
            // Start the timer
            _audioManager.PlayCountDown(0f);
            _timer = 0.0f;
            DisplayIndicators(0);
            _isPerformingAction = true;
            _enemyActionPerformed = false;

            // Reset player and enemy actions
            _playerController.ResetAction();
            _enemyController.ResetAction();
            actionDuration = 3;
            isHint = false;
            //actionDuration =Mathf.RoundToInt( Random.Range(1, 6));
        }
    }

    public void DisplayIndicators(int attack)
    {
        switch (attack)
        {
            case 1: // high attack indicator
                _audioManager.PlayIndicator("high", 0f);
                highAttackIndicator.SetActive(true);
                midAttackIndicator.SetActive(false);
                lowAttackIndicator.SetActive(false);
                break;
            case 2: // mid attack indicator
                _audioManager.PlayIndicator("mid", 0f);
                highAttackIndicator.SetActive(false);
                midAttackIndicator.SetActive(true);
                lowAttackIndicator.SetActive(false);
                break;
            case 3: // low attack indicator
                _audioManager.PlayIndicator("low", 0f);
                highAttackIndicator.SetActive(false);
                midAttackIndicator.SetActive(false);
                lowAttackIndicator.SetActive(true);
                break;
            case 0: // switch off
                highAttackIndicator.SetActive(false);
                midAttackIndicator.SetActive(false);
                lowAttackIndicator.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        // Update the timer if an action is currently being performed
        if (_isPerformingAction&&!_gameover)
        {
            _timer += Time.deltaTime;

            if (actionDuration - _timer < 1.5f)
            {
                if (isHint)
                {
                    Sword.SetActive(true);
                }
            }
            if (actionDuration-_timer < 1f)
            {
                timerText.text = "1";            
            }
            else
            {
                timerText.text = Mathf.RoundToInt(actionDuration-_timer).ToString();
            }

            if (actionDuration - _timer < 0.2f)
            {
                timerText.text = "GO";

                // timerText.text = "";
                // Perform player action

                if (isHint)
                {
                    Sword.SetActive(false);
                }
                _enemyController.PerformAction();
                _playerController.PerformAction();

                // If enemy action hasn't been performed, perform it
                //if (!enemyActionPerformed)
                //{

                //    enemyController.PerformAction();
                //    enemyActionPerformed = true;
                //}
                StartCoroutine(DetermineOutcome(_playerController.currentAction, _enemyController.currentAction));
                _isPerformingAction = false;
                // Reset timer
                Invoke(nameof(RepeatingBuffer), actionInterval);
            }
        }
    }

    public IEnumerator BufferTime()
    {
        yield return new WaitForSeconds(0.7f);
        timerText.text = "-";
        yield return new WaitForSeconds(2f);
        StartGameLoop();
    }
    private void RepeatingBuffer()
    {
        StartCoroutine(BufferTime());
    }
    

    private void OnEnable()
    {
        // Start the game loop when the script is enabled
        //RepeatingBuffer();
    }


    // Define a 2D array to represent the relationships between player and enemy actions
    // Rows represent player actions, columns represent enemy actions
    private readonly int[,] _outcomeMatrix = new int[7, 7]
    {
         //                                          ENEMY
        //         LowAttack   MidAttack   HighAttack  DefendLow   DefendMid   DefendHigh  Idle
        /* Low */    { 2,          1,          0,          2,          0,          0,          0 },
        /* Mid */    { 0,          2,          1,          0,          2,          0,          0 },
        /* High */   { 1,          0,          2,          0,          0,          2,          0 },
        /* DefLow */ { 2,          1,          1,          2,          2,          2,          0 },
        /* DefMid */ { 1,          2,          1,          2,          2,          2,          0 },
        /* DefHigh */{ 1,          1,          2,          2,          2,          2,          0 },
        /* Idle */   { 1,          1,          1,          1,          1,          1,          2 }
    };

    // Function to determine the outcome of the current actions
    IEnumerator DetermineOutcome(PlayerAction playerAction, PlayerAction enemyAction)
    {
        // Map player and enemy actions to array indices
        int playerIndex = (int)playerAction;
        int enemyIndex = (int)enemyAction;

        // Get the outcome from the array
        int outcome = _outcomeMatrix[playerIndex, enemyIndex];

        // Apply damage based on the outcome
        switch (outcome)
        {
            case 0: // Player wins
                switch (playerAction)
                {
                    case PlayerAction.LowAttack:
                        yield return new WaitForSeconds(0.8f);
                        _enemyController.TakeDamage(_damagevalue);
                        break;
                    case PlayerAction.MidAttack:
                        yield return new WaitForSeconds(0.8f);
                        _enemyController.TakeDamage(_damagevalue);
                        break;
                    case PlayerAction.HighAttack:
                        yield return new WaitForSeconds(0.8f);
                        _enemyController.TakeDamage(_damagevalue);
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
                        _playerController.TakeDamage(_damagevalue);
                        break;
                    case PlayerAction.MidAttack:
                        yield return new WaitForSeconds(0.8f);
                        _playerController.TakeDamage(_damagevalue);
                        break;
                    case PlayerAction.HighAttack:
                        yield return new WaitForSeconds(0.8f);
                        _playerController.TakeDamage(_damagevalue);
                        break;
                    default:
                        break;
                }
                break;
            default: // Draw or Clash
                break;
        }

       
    }

    public void GameCompleted(bool isplayerWon)
    {
        _gameover = true;
        if(isplayerWon)
        {
            won.SetActive(true);
            failed.SetActive(false);
        }
        else
        {
            won.SetActive(false);
            failed.SetActive(true);
        }
        _playerController.gameObject.SetActive(false);
        _enemyController.gameObject.SetActive(false);
        Gameover.SetActive(true);
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
}
