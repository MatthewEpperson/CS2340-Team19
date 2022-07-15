using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private List<GameObject> cardsInHand;

    void Start() {

    }

    public GameObject getHand() {
        return hand;
    }

    public List<GameObject> getCardsInHand() {
        return cardsInHand;
    }

    public void drawCard(GameObject hand) {
        GameObject card = Deck.getCardFromDeck();
        card.transform.SetParent(hand.transform, false);
        applyCardPosition(card, cardsInHand, hand);
        hand.GetComponent<Hand>().cardsInHand.Add(card);
    }


    public void playCard(GameObject card, GameObject playArea) {
        card.transform.SetParent(playArea.transform, false);
    }


    /**********************************
    ************ HAND UI **************
    ***********************************/
    public static void applyCardPosition(GameObject card, List<GameObject> cardsInHand, GameObject hand) {
        float xOffset = 20f;
        float zOffset = 0.5f;

        try {
            GameObject prevCard = cardsInHand[cardsInHand.Count - 1];
            card.transform.position = hand.GetComponent<RectTransform>().anchoredPosition;
            card.transform.position = new Vector3(prevCard.transform.position.x + xOffset, 
                                        hand.transform.position.y,
                                        prevCard.transform.position.z - zOffset);
        } catch (ArgumentOutOfRangeException) {
            card.transform.position = new Vector3(hand.transform.position.x, 
                                        hand.transform.position.y,
                                        card.transform.position.z);
        }
    }

}
