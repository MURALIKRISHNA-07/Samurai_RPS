// RockPaperScissors.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Choice
{
    None,
    Rock,
    Paper,
    Scissors
}

public class RockPaperScissors : MonoBehaviour
{
    public Image player1ChoiceImage;
    public Image player2ChoiceImage;
    public Text resultText;
    public Text player1HealthText;
    public Text player2HealthText;
    public Text timerText;
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;
    public int player1Health = 3;
    public int player2Health = 3;
    public float timerDuration = 5f;

    private Choice player1Choice;
    private Choice player2Choice;

    private void Start()
    {
        // Initialize texts
        player1HealthText.text = "P1 Health: " + player1Health;
        player2HealthText.text = "P2 Health: " + player2Health;

        // Start the game
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (player1Health > 0 && player2Health > 0)
        {
            // Reset choices
            player1Choice = Choice.None;
            player2Choice = Choice.None;
            UpdateImage(player1ChoiceImage, Choice.None);
            UpdateImage(player2ChoiceImage, Choice.None);

            // Wait for player 1 choice
            yield return WaitForPlayerChoice(true);

            // Wait for player 2 choice
            yield return WaitForPlayerChoice(false);

            DetermineWinner();
            yield return new WaitForSeconds(2f); // Display result for 2 seconds
        }

        // Display winner
        if (player1Health <= 0)
        {
            resultText.text = "Player 2 Wins!";
        }
        else
        {
            resultText.text = "Player 1 Wins!";
        }
    }

    IEnumerator WaitForPlayerChoice(bool isPlayer1)
    {
        float timer = timerDuration;

        while (timer > 0)
        {
            if ((isPlayer1 && player1Choice != Choice.None) || (!isPlayer1 && player2Choice != Choice.None))
                break;

            timer -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.CeilToInt(timer);
            yield return null;
        }
    }

    public void OnPlayerChoice(Choice choice, bool isPlayer1)
    {
        if (isPlayer1)
        {
            player1Choice = choice;
            UpdateImage(player1ChoiceImage, choice);
        }
        else
        {
            player2Choice = choice;
            UpdateImage(player2ChoiceImage, choice);
        }
    }

    void DetermineWinner()
    {
        if (player1Choice == player2Choice)
        {
            resultText.text = "It's a tie!";
        }
        else if ((player1Choice == Choice.Rock && player2Choice == Choice.Scissors) ||
                 (player1Choice == Choice.Paper && player2Choice == Choice.Rock) ||
                 (player1Choice == Choice.Scissors && player2Choice == Choice.Paper))
        {
            resultText.text = "Player 1 wins!";
            player2Health--;
        }
        else
        {
            resultText.text = "Player 2 wins!";
            player1Health--;
        }

        player1HealthText.text = "P1 Health: " + player1Health;
        player2HealthText.text = "P2 Health: " + player2Health;
    }

    void UpdateImage(Image image, Choice choice)
    {
        // Update image based on the choice
        switch (choice)
        {
            case Choice.None:
                // Set sprite to none
                break;
            case Choice.Rock:
                image.sprite = rockSprite;
                break;
            case Choice.Paper:
                image.sprite = paperSprite;
                break;
            case Choice.Scissors:
                image.sprite = scissorsSprite;
                break;
        }
    }
}
