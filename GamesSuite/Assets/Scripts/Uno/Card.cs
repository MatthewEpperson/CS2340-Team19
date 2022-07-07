using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] protected GameObject card;
    [SerializeField] protected string cardColor;

    public void setCardValues(string color) {
        setColor(color);
    }

    public void setColor(string color) {
        this.cardColor = color;
    }

    public string getColor() {
        return this.cardColor;
    }

}
