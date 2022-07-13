using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Hand hand;
    [SerializeField] private List<GameObject> cardsInHand;

    public void drawCard(List<GameObject> hand) {
        hand.Add(Deck.getCardFromDeck());
    }
}
