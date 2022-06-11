using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleUI : MonoBehaviour
{
    public GameObject letterBlock;
    private Transform background;
    private List<List<GameObject>> letterBlocks;
    private WordlePlayer wordlePlayer;

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Background").GetComponent<Transform>();
        letterBlocks = new List<List<GameObject>>();
        wordlePlayer = GameObject.Find("Main Camera").GetComponent<WordlePlayer>();
        
        int block_x = 400;
        int block_y = 525;

        for (int j = 0; j < 6; j++) {
            letterBlocks.Add(new List<GameObject>());
            for (int i = 0; i < 5; i++) {
                if (i % 5 == 0) {
                    block_y -= 65;
                    block_x = 400;
                }
                GameObject gameObj = Instantiate(letterBlock, new Vector3(block_x, block_y, 0), Quaternion.identity);
                gameObj.transform.SetParent(background);
                letterBlocks[j].Add(gameObj);
                block_x += 55;
            }
        }
    }

    public void changeBlockColor() {
        string playerInputWord = wordlePlayer.getWord();
        int attempts = wordlePlayer.attempts;

        if (attempts >= 6) {
            return;
        }

        Color32 yellow = new Color32(255, 255, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 gray = new Color32(75, 75, 75, 255);


        for (int i = 0; i < playerInputWord.Length; i++) {
            char letter = wordlePlayer.correctWord[i]; // letter of correct word
            if (letter == playerInputWord[i]) {
                letterBlocks[attempts][i].GetComponent<Image>().color = green;
                continue;
            }
            if (letter != playerInputWord[i] && playerInputWord.Contains(letter)) {
                letterBlocks[attempts][i].GetComponent<Image>().color = yellow;
                continue;
            }
            letterBlocks[attempts][i].GetComponent<Image>().color = gray;
        }
    }

}
