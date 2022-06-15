using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class WordlePlayer : MonoBehaviour
{

    public string playerInputWord; // String to keep track of what player types from input field
    public string correctWord;

    [SerializeField]
    public int attempts; // keeps track of attempts the player has used

    private WordleUI wordleUI; 

    public TMP_Text corrWord; // This is just for testing and displays on screen. Will remove once we merge into production.

    TMP_InputField wordInputField; // this is for input field itself


    // Start is called before the first frame update
    void Start()
    {
        correctWord = GenerateWord.getWord(); // pulls a random word from words.csv and assigns it

        // NOTE 1: Displays correct word on screen just so we can test when playing. Remove this later.
        corrWord = GameObject.Find("CorrectWord").GetComponent<TMP_Text>();
        corrWord.text = "Correct Word: " + correctWord.ToUpper();
        // END OF NOTE 1

        wordleUI = GameObject.Find("Background").GetComponent<WordleUI>();

        wordInputField = GameObject.Find("PlayerWordGuess").GetComponent<TMP_InputField>();
        wordInputField.ActivateInputField();
        Debug.Log(correctWord);
    }

    
    /* playerGuess is whatever the player types into the input field and automatically updates. 
        We have to assign it to playerInputWord so we can actually do logic with it in the code.
    */
    public void readPlayerInput(string playerGuess) {
        playerInputWord = playerGuess.ToLower();
    }

    // This is a default unity function. Whatever is in this function gets executed every single frame.
    void Update() {
        wordInputField.ActivateInputField();
        if (Input.GetKeyDown("return") && isValidWord()) {
            wordleUI.changeBlockColor();
            wordInputField.text = "";
            attempts--;
        }
    }

    // Determines if the word is valid (already constrained to 5 characters, but just in case)
    private bool isValidWord() {
        if (playerInputWord.Length == 5 && GenerateWord.wordsContains(playerInputWord)) {
            return true;
        }
        return false;
    }

    
    // Returns the word the player entered, but only if it is a valid word.
    public string getWord() {
        if (isValidWord()) {
            return playerInputWord;
        }
        return null;
    }
}
