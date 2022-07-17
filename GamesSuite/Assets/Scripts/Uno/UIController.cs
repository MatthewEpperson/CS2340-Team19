using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject pickColorPanel;

    private Card card;

    public Image playerBar;
    public Image opponent1Bar;
    public Image opponent2Bar;
    public Image opponent3Bar;

    // Start is called before the first frame update
    void Start()
    {
        pickColorPanel.SetActive(false);
    }

    void Update()
    {
        if (GameController.currTurn == "Player") {
            playerBar.color = new Color32(255, 255, 0, 255);
        } else {
            playerBar.color = new Color32(100, 100, 100, 255);
        }

        if (GameController.currTurn == "Opponent 1") {
            opponent1Bar.color = new Color32(255, 255, 0, 255);
        } else {
            opponent1Bar.color = new Color32(100, 100, 100, 255);
        }

        if (GameController.currTurn == "Opponent 2") {
            opponent2Bar.color = new Color32(255, 255, 0, 255);
        } else {
            opponent2Bar.color = new Color32(100, 100, 100, 255);
        }

        if (GameController.currTurn == "Opponent 3") {
            opponent3Bar.color = new Color32(255, 255, 0, 255);
        } else {
            opponent3Bar.color = new Color32(100, 100, 100, 255);
        }
    }


    public void activatePickColorUI(Card cardInfo) {
        // pickColorPanel = GameObject.Find("Choose Color Panel");
        card = cardInfo;
        if (pickColorPanel.activeSelf == false) {
            pickColorPanel.SetActive(true);
        }
    }

    public void deactivatePickColorUI() {
        if (pickColorPanel.activeSelf == true) {
            pickColorPanel.SetActive(false);
        }
    }

    public void setCardColor(string color) {
        card.setColor(color);
        deactivatePickColorUI();
    }

}
