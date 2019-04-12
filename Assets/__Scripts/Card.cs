using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//THis is an enum, which defines a type of variable that only has few possible
//  values. The CardState variable type has one of four values:
//  drawpile, tableau, target, & discard

public enum CardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector: Card
{
    //THis is how you use the enum CardState
    public CardState state = CardState.drawpile;

    //The hiddenBy list stores which other cards will keep this one face down
    public List<CardProspector> hiddenBy = new List<CardProspector>();

    //LayoutID matches this card to a Layout XML id if it's a tableau card
    public int layoutID;
    //The SlotDef class stores information pulled in from the LayoutXML <slot>
    public SlotDef slotDef;

}

public class Card : MonoBehaviour {

	public string    suit;
	public int       rank;
	public Color     color = Color.black;
	public string    colS = "Black";  // or "Red"
	
	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();
	
	public GameObject back;  // back of card;
	public CardDefinition def;  // from DeckXML.xml		


	public bool faceUp {
		get {
			return (!back.activeSelf);
		}

		set {
			back.SetActive(!value);
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
} // class Card

[System.Serializable]
public class Decorator{
	public string	type;			// For card pips, tyhpe = "pip"
	public Vector3	loc;			// location of sprite on the card
	public bool		flip = false;	//whether to flip vertically
	public float 	scale = 1.0f;
}

[System.Serializable]
public class CardDefinition{
	public string	face;	//sprite to use for face cart
	public int		rank;	// value from 1-13 (Ace-King)
	public List<Decorator>	
					pips = new List<Decorator>();  // Pips Used
}
