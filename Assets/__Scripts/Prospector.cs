﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector 	S;

	[Header("Set in Inspector")]
	public TextAsset			deckXML;


	[Header("Set Dynamically")]
	public Deck					deck;

    public Layout layout;
    public TextAsset layoutXML;

	void Awake(){
		S = this;
	}

    public List<CardProspector> drawPile;

	void Start() {
		deck = GetComponent<Deck> ();
		deck.InitDeck (deckXML.text);
        Deck.Shuffle(ref deck.cards);
        layout = GetComponent<Layout>();    //Get the layout
        layout.ReadLayout(layoutXML.text);  //Pass the LayoutXML to it
        drawPile = ConvertListCardsToListCardProspectors(deck.cards);
    }

    List<CardProspector> ConvertListCardsToListCardProspectors(List<Card> lCD)
    {
        List<CardProspector> lCP = new List<CardProspector>();
        CardProspector tCP;
        foreach (Card tCD in lCD)
        {
            tCP = tCD as CardProspector;
            lCP.Add(tCP);
        }
        return (lCP);
    }


}
