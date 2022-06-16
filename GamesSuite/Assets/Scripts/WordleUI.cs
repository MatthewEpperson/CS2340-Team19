using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WordleUI : MonoBehaviour
{
    public GameObject letterBlock; 
    private Transform blocksPanel;
    private List<List<GameObject>> letterBlocksList;
    private WordlePlayer wordlePlayer;

    private Dictionary<char, int> letterCount = new Dictionary<char, int>(); // Dictionary to count amount of each letter in word

    // Start is called before the first frame update
    void Start()
    {
        blocksPanel = GameObject.Find("BlocksPanel").GetComponent<Transform>();
        letterBlocksList = new List<List<GameObject>>();
        wordlePlayer = GameObject.Find("Game Manager").GetComponent<WordlePlayer>();

        /* The inner loop create the 5 blocks horizontally for the words.
            We want 6 rows so we do it 6 times with the outer loop. 
        */
        for (int j = 0; j < wordlePlayer.attempts; j++) {
            letterBlocksList.Add(new List<GameObject>());
            for (int i = 0; i < 5; i++) {
                GameObject gameObj = Instantiate(letterBlock, new Vector3(0,0,0), 
                                            Quaternion.identity, blocksPanel);
                letterBlocksList[j].Add(gameObj);
            }
        }
    }


    // Populate letterCount dictionary with the occurrences of each letter in Correct Word
    private void countLettersCorrectWord() {
        letterCount.Clear();
        foreach (char letter in wordlePlayer.correctWord) {
            if (letterCount.ContainsKey(letter)) {
                letterCount[letter]++;
            } else {
                letterCount.TryAdd(letter, 1);
            }
        }
    }


    private int blockRow = 0; // Used to keep track of which row of blocks to change

    public void changeBlockColor() {
        countLettersCorrectWord();

        string playerInputWord = wordlePlayer.playerInputWord;
        string correctWord = wordlePlayer.correctWord;

        int attempts = wordlePlayer.attempts;

        if (attempts <= 0) { // if player is out of attempts, don't execute 
            return;
        }

        Color32 yellow = new Color32(255, 255, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 gray = new Color32(75, 75, 75, 255);
       
        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i];
            char guessLetter = playerInputWord[i];

            if (currLetter == guessLetter) { // Turn block green if guessLetter is in correct position
                letterBlocksList[blockRow][i].GetComponent<Image>().color = green;
                letterCount[currLetter] = letterCount[currLetter] - 1;
                continue;
            }

            letterBlocksList[blockRow][i].GetComponent<Image>().color = gray;
        }

        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i];
            char guessLetter = playerInputWord[i];
            if (currLetter != guessLetter && correctWord.Contains(guessLetter)) { // Turn block yellow if in wrong position
                if (letterCount[guessLetter] > 0) {
                    letterBlocksList[blockRow][i].GetComponent<Image>().color = yellow;
                    letterCount[guessLetter] = letterCount[guessLetter] - 1;
                    continue;
                }
            }
        }

        blockRow++;
    }

}
