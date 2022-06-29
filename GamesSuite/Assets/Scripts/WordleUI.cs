using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WordleUI : MonoBehaviour
{
    public GameObject letterBlock; 
    private Transform blocksPanel;
    private List<List<GameObject>> letterBlocksList;
    private WordlePlayer wordlePlayer;
    public static bool isBlockAnimPlaying;
    private string playerInputWord;

    private int playerWordIndex = 0;

    private Dictionary<char, int> letterCount = new Dictionary<char, int>(); // Dictionary to count amount of each letter in word


    // Start is called before the first frame update
    void Start()
    {
        isBlockAnimPlaying = false;
        blocksPanel = GameObject.Find("BlocksPanel").GetComponent<Transform>();
        letterBlocksList = new List<List<GameObject>>();
        wordlePlayer = GameObject.Find("Game Manager").GetComponent<WordlePlayer>();
        /* The inner loop create the 5 blocks horizontally for the words.
            We want 6 rows so we do it 6 times with the outer loop. 
        */
        // for (int j = 0; j < wordlePlayer.attempts; j++) {
        //     letterBlocksList.Add(new List<GameObject>());
        //     for (int i = 0; i < 5; i++) {
        //         GameObject gameObj = Instantiate(letterBlock, new Vector3(0,0,0), 
        //                                     Quaternion.identity, blocksPanel);
        //         letterBlocksList[j].Add(gameObj);
        //     }
        // }

        int j = 0;
        int count = 0;
        letterBlocksList.Add(new List<GameObject>());
        foreach (Transform child in blocksPanel) {
            if (count % 5 == 0 && count != 0) {
                letterBlocksList.Add(new List<GameObject>());
                j++;
            }
            GameObject block = child.gameObject;
            letterBlocksList[j].Add(block);
            count++;
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

        if (!isBlockAnimPlaying) {
            if (playerInputWord.Length <= 0 || Input.GetKey("backspace")) {
                playerWordIndex = playerInputWord.Length <= 0 ? 0 : playerInputWord.Length;
                letter = " ";
            } else if (playerInputWord.Length > 0 && !Input.GetKey("backspace")) {
                playerWordIndex = playerInputWord.Length <= 0 ? 0 : playerInputWord.Length - 1;
                letter = playerInputWord[playerWordIndex].ToString();
            }
            if (playerWordIndex >= 5) {
                playerWordIndex -= 1;
            }
            Transform blockText = letterBlocksList[blockRow][playerWordIndex].transform.Find("Text");
            blockText.GetComponent<TMP_Text>().text = letter;
        }

        // END OF NOTE 1
    }


    // Populate letterCount dictionary with the occurrences of each letter in Correct Word
    private void countLettersCorrectWord() {
        letterCount.Clear();
        foreach (char letter in WordlePlayer.correctWord) {
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



    // Bounces block when player guesses correct word similar to actual wordle
    IEnumerator bounceBlock(GameObject block) {

        Vector3 oldBlockPos = block.transform.position;

        float block_x = block.transform.position.x;
        float block_y = block.transform.position.y;
        float block_z = block.transform.position.z;
    
        float bounceSpeed = 175f;

        while (block_y < (oldBlockPos.y + 25f)) {
            block_y += bounceSpeed * Time.deltaTime;
            block.transform.position = new Vector3(block_x, block_y, block_z);
            yield return null;
        }

        while (block_y > oldBlockPos.y) {
            block_y -= bounceSpeed * Time.deltaTime;
            block.transform.position = new Vector3(block_x, block_y, block_z);
            yield return null;
        }

        block.transform.position = oldBlockPos;
        yield return null;
    }



    public IEnumerator changeBlockColor() {
        isBlockAnimPlaying = true;
        countLettersCorrectWord();

        string playerInputWord = wordlePlayer.playerInputWord;
        string correctWord = WordlePlayer.correctWord;

        int attempts = wordlePlayer.attempts;

        if (attempts <= 0) { // if player is out of attempts, don't execute 
            yield return null;
        }

        Color32 yellow = new Color32(255, 255, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 gray = new Color32(75, 75, 75, 255);
        Color32 red = new Color32(255, 0, 0, 255);
        Color32 defaultColor = new Color32(147, 147, 147, 255);

        // Stores correct color of a block. This is way we can handle block animation after handling logic.
        string[] blockColors = new string[5];


        // Flashes blocks red signifying that the user did not input a valid word
        if (!wordlePlayer.isValidWord()) {
            for (int i = 0; i < 3; i++) { // flash three times
                for (int j = 0; j < 5; j++) {
                    letterBlocksList[blockRow][j].GetComponent<Image>().color = red;
                }
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < 5; j++) {
                    letterBlocksList[blockRow][j].GetComponent<Image>().color = defaultColor;
                }
                yield return new WaitForSeconds(0.1f);
            }
            isBlockAnimPlaying = false;
            yield break;
        }

       
       // Counts green blocks AKA correct positions
        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i];
            char guessLetter = playerInputWord[i];

            if (currLetter == guessLetter) { // Turn block green if guessLetter is in correct position
                blockColors[i] = "green";
                letterCount[currLetter] = letterCount[currLetter] - 1;
                continue;
            }
        }


        // Counts the yellow and gray blocks
        for (int i = 0; i < playerInputWord.Length; i++) {
            char currLetter = correctWord[i];
            char guessLetter = playerInputWord[i];
            if (currLetter != guessLetter && correctWord.Contains(guessLetter)) { // Turn block yellow if in wrong position
                if (letterCount[guessLetter] > 0) {
                    blockColors[i] = "yellow";
                    letterCount[guessLetter] = letterCount[guessLetter] - 1;
                    continue;
                }
            }
            if (currLetter != guessLetter) {
                blockColors[i] = "gray";
            }
        }


        // Logic for animation of blocks
        for (int i = 0; i < blockColors.Length; i++) {
            if (blockColors[i].Equals("green")) {
                StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], green));
            } else if (blockColors[i].Equals("yellow")) {
                StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], yellow));
            } else if (blockColors[i].Equals("gray")) {
                StartCoroutine(rotateBlock(letterBlocksList[blockRow][i], gray));
            }
            yield return new WaitForSeconds(0.3f);
        }


        if (WordlePlayer.gameOver) {
            for (int i = 0; i < 5; i++) {
                StartCoroutine(bounceBlock(letterBlocksList[blockRow][i]));
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("EndScreenWordle");
        }

        WordlePlayer.wordInputField.text = ""; // Clear input field after animations are done

        isBlockAnimPlaying = false;
        
        if (blockRow + 1 >= 6) {
            blockRow = 5;
        } else {
            blockRow++;
        }
    }
}