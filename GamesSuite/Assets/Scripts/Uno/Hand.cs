using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private List<GameObject> cardsInHand;


    void Start() {
        StartCoroutine(dealInitialCards(cardsInHand, hand));
    }


    void drawCard(List<GameObject> hand) {
        hand.Add(Deck.getCardFromDeck());
    }


    void placeCard(GameObject card, GameObject cardsPlayed) {
        card.transform.parent = cardsPlayed.transform;
        GameController.cardsPlayed.Push(card);
    }


    IEnumerator dealInitialCards(List<GameObject> cardsInhand, GameObject hand) {
        yield return new WaitForSeconds(0.3f); // wait 0.3 seconds to allow deck to be shuffled before dealing cards
        for (int i = 0; i < 7; i++) {
            GameObject card = Deck.getCardFromDeck();
            cardsInhand.Add(Deck.getCardFromDeck());
            card.transform.SetParent(hand.transform, false);
        }
        Debug.Log("Successfully Dealt Hand");
    }
}
