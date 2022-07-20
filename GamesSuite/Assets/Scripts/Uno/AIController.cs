using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject opponentObj1;
    [SerializeField] GameObject opponentObj2;
    [SerializeField] GameObject opponentObj3;


    [SerializeField] Hand oppHand1;
    [SerializeField] Hand oppHand2;
    [SerializeField] Hand oppHand3;
    [SerializeField] Hand playerHand;


    [SerializeField] GameController gameController;
    private Hand hand;
    private GameObject handGameObj;
    private List<GameObject> playableCards = new List<GameObject>();

    public static Dictionary<string, Hand> hands = new Dictionary<string, Hand>();
    public static Dictionary<string, GameObject> handObjects = new Dictionary<string, GameObject>();


    void Start()
    {

        hands.Add(GameController.players[0], playerHand);
        hands.Add(GameController.players[1], oppHand1);
        hands.Add(GameController.players[2], oppHand2);
        hands.Add(GameController.players[3], oppHand3);

        handObjects.Add(GameController.players[0], playerObj);
        handObjects.Add(GameController.players[1], opponentObj1);
        handObjects.Add(GameController.players[2], opponentObj2);
        handObjects.Add(GameController.players[3], opponentObj3);
        // StartCoroutine(findAllPlayableCards());
    }


    // Update is called once per frame
    void Update()
    {
        if (GameController.currTurn.Equals("Opponent 1")) {
            hand = oppHand1;
            handGameObj = opponentObj1;
        }
        if (GameController.currTurn.Equals("Opponent 2")) {
            hand = oppHand2;
            handGameObj = opponentObj2;
        }
        if (GameController.currTurn.Equals("Opponent 3")) {
            hand = oppHand3;
            handGameObj = opponentObj3;
        }

        if (GameController.currTurn != "Player") {
            StartCoroutine(findAllPlayableCards());
            int randNum = Random.Range(0, playableCards.Count);
            if (CountdownController.currentTime <= CountdownController.aiTimer) {
                StartCoroutine(CardUI.moveToPlayArea(playableCards[randNum], PlayAreaDeck.getPlayArea()));
                hand.playCard(checkCardType(playableCards[randNum]), PlayAreaDeck.getPlayArea());
            }
        }
    }


    private GameObject checkCardType(GameObject card) {
        isActionCard(card);
        isWildCard(card);
        return card;
    }


    private bool isActionCard(GameObject card) {
        Card cardInfo = card.GetComponent<Card>();
        if (cardInfo.GetType() == typeof(ActionCard)) {
            if (((ActionCard)cardInfo).getActionType() == "draw 2") {
                string nextTurn = GameController.checkNextTurn(); // This just checks the next turn, it does NOT set the next turn
                hands[nextTurn].drawCard(handObjects[nextTurn]);
                hands[nextTurn].drawCard(handObjects[nextTurn]);
                GameController.nextTurn();
            } else if (((ActionCard)cardInfo).getActionType() == "skip") {
                GameController.nextTurn();
            } else if (((ActionCard)cardInfo).getActionType() == "reverse") {
                GameController.isReversed = !GameController.isReversed;
            }
        } else {
            return false;
        }
        return true;
    }


    private bool isWildCard(GameObject card) {
        Card cardInfo = card.GetComponent<Card>();
        
        if (cardInfo.GetType() == typeof(WildCard)) {
            
            int randNum = Random.Range(0, CardCreator.colors.Length);
            cardInfo.setColor(CardCreator.colors[randNum]);
            
            if (((WildCard)cardInfo).getWildType() == "draw 4 wild") {
                string nextTurn = GameController.checkNextTurn(); // This just checks the next turn, it does NOT set the next turn
                for (int i = 0; i < 4; i++) {
                    hands[nextTurn].drawCard(handObjects[nextTurn]);
                }
                GameController.nextTurn();
            }
        } else {
            return false;
        }

        return true;
    }


    IEnumerator findAllPlayableCards() {
        playableCards.Clear();
        foreach(GameObject card in hand.getCardsInHand()) {
            if (isPlayable(card)) {
                playableCards.Add(card);
            }
        }
        if (playableCards.Count == 0) {
            hand.drawCard(handGameObj);
            GameObject newCard = hand.getCardsInHand()[hand.getCardsInHand().Count - 1];
            if (isPlayable(newCard)) {
                playableCards.Add(newCard);
            } else {
                GameController.nextTurn();
            }
        }
        yield return null;
    }


    private bool isPlayable(GameObject card) {
        Card cardInfo = card.GetComponent<Card>();
        Card cardOnPlayArea = PlayAreaDeck.getCardFromPlayArea().GetComponent<Card>();

        if (cardInfo.getColor() == cardOnPlayArea.getColor()) {
                return true;
        } else if (cardInfo.GetType() == typeof(NumberCard) &&
                    cardOnPlayArea.GetType() == typeof(NumberCard)) {
            if (((NumberCard)cardInfo).getNumber() == ((NumberCard)cardOnPlayArea).getNumber()) {
                return true;
            }
        } else if (cardInfo.GetType() == typeof(WildCard)) {
            return true;
        }

        return false;
    }

}
