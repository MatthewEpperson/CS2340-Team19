using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject pickColorPanel;

    private Card card;

    // Start is called before the first frame update
    void Start()
    {
        pickColorPanel.SetActive(false);
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
