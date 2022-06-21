using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordleUI : MonoBehaviour
{
    public GameObject letterBlock; 
    private Transform blocksPanel;
    private List<List<GameObject>> letterBlocksList;
    private WordlePlayer wordlePlayer;
    private string playerInputWord;

    private int playerWordIndex = 0;

    private Dictionary<char, int> letterCount = new Dictionary<char, int>(); // Dictionary to count amount of each letter in word

    private bool isRotating = false;
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


    private int blockRow = 0; // Used to keep track of which row of blocks to change
    void Update() {

        /* NOTE 1: Handle logic to update every block as player types in Input Field. 
            Current issue is that backspace doesn't work properly. There might be a better way to do this,
            but I did this at 5AM.
        */
        playerInputWord = wordlePlayer.playerInputWord;
        // int playerWordIndex = 0;
        string letter = " ";

        if (playerInputWord.Length <= 0 || Input.GetKeyDown("backspace")) { 
            playerWordIndex = playerInputWord.Length <= 0 ? 0 : playerInputWord.Length;
            letter = " ";
        } else if (playerInputWord.Length > 0 && !Input.GetKeyDown("backspace")) {
            playerWordIndex = playerInputWord.Length - 1;
            letter = playerInputWord[playerWordIndex].ToString();
        }

        Transform blockText = letterBlocksList[blockRow][playerWordIndex].transform.Find("Text");
        blockText.GetComponent<TMP_Text>().text = letter;
        // END OF NOTE 1
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



    // This coroutine add a rotation animation and applies the color when the image is flat
    IEnumerator rotateBlock(GameObject block, Color32 color) {
        Quaternion oldPosition = block.transform.rotation;
        float rotateSpeed = 350f;
        Quaternion endingPos = Quaternion.Euler(new Vector3(90, 0, 0));
        while (Vector3.Distance(block.transform.rotation.eulerAngles, endingPos.eulerAngles) > 0.01f) {
            block.transform.rotation = Quaternion.RotateTowards(block.transform.rotation, endingPos, rotateSpeed * Time.deltaTime);
            yield return null;
        }

        endingPos = Quaternion.Euler(new Vector3(0, 0, 0));
        while (Vector3.Distance(block.transform.rotation.eulerAngles, endingPos.eulerAngles) > 0.01f) {
            if (block.transform.rotation.x >= -0.50f) {
                block.GetComponent<Image>().color = color;
            }
            block.transform.rotation = Quaternion.RotateTowards(block.transform.rotation, endingPos, rotateSpeed * Time.deltaTime);
            yield return null;
        }
        block.GetComponent<Image>().color = color;
        block.transform.rotation = endingPos;
    }




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
                StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], green));
                letterCount[currLetter] = letterCount[currLetter] - 1;
                continue;
            }
        }

        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i];
            char guessLetter = playerInputWord[i];
            if (currLetter != guessLetter && correctWord.Contains(guessLetter)) { // Turn block yellow if in wrong position
                if (letterCount[guessLetter] > 0) {
                    StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], yellow));
                    letterCount[guessLetter] = letterCount[guessLetter] - 1;
                    continue;
                }
            }
            if (currLetter != guessLetter) {
                StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], gray));
            }
        }

        blockRow++;
    }

}
