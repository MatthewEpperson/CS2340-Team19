using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Hand hand;
    [SerializeField] GameObject handGameObj;
    [SerializeField] GameController gameController;
    private List<GameObject> playableCards = new List<GameObject>();
    void Start()
    {
        // StartCoroutine(findAllPlayableCards());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.currTurn == "Opponent") {
            StartCoroutine(findAllPlayableCards());
            int randNum = Random.Range(0, playableCards.Count);
            hand.playCard(playableCards[randNum], PlayAreaDeck.getPlayArea());
            Debug.Log($"Current Playable Cards: {playableCards.Count}");
        }

        Debug.Log($"Top of Stack: {PlayAreaDeck.getCardFromPlayArea().gameObject.name}");
    }

    IEnumerator findAllPlayableCards() {
        foreach(GameObject card in hand.getCardsInHand()) {
            if (isPlayable(card)) {
                playableCards.Add(card);
            }
        }
        if (playableCards.Count == 0) {
            while (playableCards.Count == 0) {
                hand.drawCard(handGameObj);
                GameObject newCard = hand.getCardsInHand()[hand.getCardsInHand().Count - 1];
                if (isPlayable(newCard)) {
                    playableCards.Add(newCard);
                }
            }
        }
        yield return null;
    }


    private bool isPlayable(GameObject card) {
        Card cardInfo = card.GetComponent<Card>();
        Card cardOnPlayArea = PlayAreaDeck.getCardFromPlayArea().GetComponent<Card>();

        if (cardInfo.getColor() == cardOnPlayArea.getColor() ||
            cardInfo.GetType() == typeof(WildCard)) {

                return true;

        } else if (cardInfo.GetType() == typeof(NumberCard) &&
                    cardOnPlayArea.GetType() == typeof(NumberCard)) {
            if (((NumberCard)cardInfo).getNumber() == ((NumberCard)cardOnPlayArea).getNumber()) {
                return true;
            }
        }

        return false;
    }
}
