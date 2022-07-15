using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] protected GameObject card;
    [SerializeField] protected string cardColor;

    private Transform oldCardPos;

    void Start() {
        oldCardPos = card.transform;
    }

    public void setCardValues(string color) {
        setColor(color);
    }

    public bool OnMouseDown()
    {
        Debug.Log($"{this.card.name} Clicked!");
        return true;
    }

    public bool OnMouseOver()
    {
        Debug.Log($"{this.card.name} Hovering!");
        return true;
    }

    void OnMouseEnter()
    {
        card.transform.position = new Vector3(card.transform.position.x,
                                                card.transform.position.y + 20f,
                                                card.transform.position.z);
    }

    public bool OnMouseExit()
    {
        card.transform.position = new Vector3(oldCardPos.position.x, oldCardPos.position.y - 20f, oldCardPos.position.z);
        Debug.Log($"{this.card.name} off!");
        return true;
    }

    public void setColor(string color) {
        this.cardColor = color;
    }

    public string getColor() {
        return this.cardColor;
    }

}
