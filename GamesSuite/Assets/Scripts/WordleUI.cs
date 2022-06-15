using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleUI : MonoBehaviour
{
    public GameObject letterBlock; // 
    private Transform blocksPanel;
    private List<List<GameObject>> letterBlocksList;
    private WordlePlayer wordlePlayer;

    // Start is called before the first frame update
    void Start()
    {
        blocksPanel = GameObject.Find("BlocksPanel").GetComponent<Transform>();
        letterBlocksList = new List<List<GameObject>>();
        wordlePlayer = GameObject.Find("Player").GetComponent<WordlePlayer>();

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


    private int blockRow = 0; // Used to keep track of which row of blocks for indexing letterBlocksList

    public void changeBlockColor() {
        string playerInputWord = wordlePlayer.getWord();
        string correctWord = wordlePlayer.correctWord;
        int attempts = wordlePlayer.attempts;

        if (attempts <= 0) {
            return;
        }

        Color32 yellow = new Color32(255, 255, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 gray = new Color32(75, 75, 75, 255);


        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i]; // current letter of the correct word to compare it to the player's guess
            if (currLetter == playerInputWord[i]) { // if player's letter is in correct position
                letterBlocksList[blockRow][i].GetComponent<Image>().color = green;
                continue;
            }
            if (currLetter != playerInputWord[i] && correctWord.Contains(playerInputWord[i])) { // if player's letter is in correct word but wrong position
                letterBlocksList[blockRow][i].GetComponent<Image>().color = yellow;
                continue;
            }
            letterBlocksList[blockRow][i].GetComponent<Image>().color = gray;
        }

        blockRow++;
    }

}
