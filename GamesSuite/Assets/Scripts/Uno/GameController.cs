using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject opponentHand1;
    [SerializeField] private GameObject opponentHand2;
    [SerializeField] private GameObject opponentHand3;
    [SerializeField] private GameObject playerHand;

    private List<GameObject> hands = new List<GameObject>();

    void Start()
    {
        hands.Add(playerHand);
        hands.Add(opponentHand1);
        hands.Add(opponentHand2);
        hands.Add(opponentHand3);

        StartCoroutine(dealStartCard(PlayAreaDeck.getPlayArea(), PlayAreaDeck.playAreaStack));

        foreach (GameObject hand in hands) {
            Hand currHand = hand.GetComponent<Hand>();
            StartCoroutine(dealInitialCards(currHand.getCardsInHand(), currHand.getHand()));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator dealInitialCards(List<GameObject> cardsInhand, GameObject hand) {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(0.3f); // wait 0.3 seconds to allow deck to be shuffled before dealing cards
        for (int i = 0; i < 7; i++) {
            GameObject card = Deck.getCardFromDeck();
            card.transform.SetParent(hand.transform, false);
            Hand.applyCardPosition(card, cardsInhand, hand);
            cardsInhand.Add(card);
        }
    }

    IEnumerator dealStartCard(GameObject playAreaDeck, Stack<GameObject> playAreaStack) {
        yield return new WaitForSeconds(0.3f);
        GameObject card = Deck.getCardFromDeck();
        card.transform.SetParent(playAreaDeck.transform, false);
        card.transform.position = playAreaDeck.transform.position;
        playAreaStack.Push(card);
        Debug.Log("Assigned");
    }
}
