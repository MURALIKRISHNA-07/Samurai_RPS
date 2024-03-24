using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public RockPaperScissors gameLogic;
    public bool isplayer1;

    // Assign these in the inspector
    public Button rockButton;
    public Button paperButton;
    public Button scissorsButton;

    void Start()
    {
        // Assign the wrapper functions to the button click events
        rockButton.onClick.AddListener(() => OnRockButtonClick());
        paperButton.onClick.AddListener(() => OnPaperButtonClick());
        scissorsButton.onClick.AddListener(() => OnScissorsButtonClick());
    }

    void OnRockButtonClick()
    {
        gameLogic.OnPlayerChoice(Choice.Rock, isplayer1);
    }

    void OnPaperButtonClick()
    {
        gameLogic.OnPlayerChoice(Choice.Paper, isplayer1);
    }

    void OnScissorsButtonClick()
    {
        gameLogic.OnPlayerChoice(Choice.Scissors, isplayer1);
    }
}
