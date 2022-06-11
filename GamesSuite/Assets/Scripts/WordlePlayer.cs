using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class WordlePlayer : MonoBehaviour
{

    public string playerInputWord;
    public string correctWord;
    public int attempts;

    private WordleUI wordleUI;

    public TMP_Text corrWord;

    TMP_InputField wordInputField;
    // Start is called before the first frame update
    void Start()
    {
        correctWord = GenerateWord.getWord();

        // REMOVE LATER, FOR DEBUGGING PURPOSES
        corrWord = GameObject.Find("CorrectWord").GetComponent<TMP_Text>();
        corrWord.text = "Correct Word: " + correctWord.ToUpper();

        wordleUI = GameObject.Find("Background").GetComponent<WordleUI>();

        attempts = 0;
        wordInputField = GameObject.Find("PlayerWordGuess").GetComponent<TMP_InputField>();
        wordInputField.ActivateInputField();
        Debug.Log(correctWord);
    }

    public void readPlayerInput(string playerGuess) {
        playerInputWord = playerGuess.ToLower();
    }

    void Update() {
        wordInputField.ActivateInputField();
        if (Input.GetKeyDown("return") && isValidWord()) {
            wordleUI.changeBlockColor();
            wordInputField.text = "";
            attempts++;
        }
    }

    // Determines if the word is valid (already constrained to 5 characters, but just in case)
    private bool isValidWord() {
        if (playerInputWord.Length == 5 && GenerateWord.wordsContains(playerInputWord)) {
            return true;
        }
        return false;
    }

    public string getWord() {
        if (isValidWord()) {
            return playerInputWord;
        }
        return null;
    }
}
