using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private List<GameObject> cardsInHand;


    void Start() {
        StartCoroutine(dealInitialCards(cardsInHand, hand));
    }


    public void drawCard(GameObject hand) {
        GameObject card = Deck.getCardFromDeck();
        hand.GetComponent<Hand>().cardsInHand.Add(card);
        card.transform.SetParent(hand.transform);
        Debug.Log("drew card");
    }


    void placeCard(GameObject card, GameObject cardsPlayed) {
        card.transform.parent = cardsPlayed.transform;
        GameController.cardsPlayed.Push(card);
    }


    IEnumerator dealInitialCards(List<GameObject> cardsInhand, GameObject hand) {
        yield return new WaitForSeconds(0.3f); // wait 0.3 seconds to allow deck to be shuffled before dealing cards
        for (int i = 0; i < 7; i++) {
            GameObject card = Deck.getCardFromDeck();
            card.transform.SetParent(hand.transform, false);
            applyCardPosition(card, cardsInhand);
            cardsInhand.Add(card);
        }
    }



    /**********************************
    ************ HAND UI **************
    ***********************************/
    public void applyCardPosition(GameObject card, List<GameObject> cardsInHand) {
        float xOffset = 20f;
        float zOffset = 0.5f;

        try {
            GameObject prevCard = cardsInHand[cardsInHand.Count - 1];
            Debug.Log($"Card Pos: {prevCard.transform.position.x}");
            card.transform.position = new Vector3(prevCard.transform.position.x + xOffset, 
                                        card.transform.position.y,
                                        prevCard.transform.position.z + zOffset);
            Debug.Log("Successfully applied position");
        } catch (ArgumentOutOfRangeException) {
            Debug.Log("Hand is empty");
        }
    }

}
