using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The SLotDef class is not a subclass of Monobehaviour, so it doesn't need a
//  separate C# file.

[System.Serializable]       //This makes SlotDefs visible in the Unity Inspector pane
public class SlotDef
{
    public float x;
    public float y;
    public bool faceUp = false;
    public string layerName = "Default";
    public int layerID = 0;
    public int id;
    public List<int> hiddenBy = new List<int>();
    public string type = "slot";
    public Vector2 stagger;
}

public class Layout : MonoBehaviour {

    public PT_XMLReader xmlr;       //Just like Deck, this has a PT_XMLReader
    public PT_XMLHashtable xml;     //THis variable is for easier xml access
    public Vector2 multipler;       //Sets the spacing of tableau

    //SlotDef references
    public List<SlotDef> slotDefs;      //All the SlotDefs for Row0-Row3
    public SlotDef drawPile;
    public SlotDef discardPile;

    //THis holds all of the possible names for the layers set by layerID
    public string[] sortingLayerNames = new string[] {"Row0",
                                                      "Row1",
                                                      "Row2",
                                                      "Row3",
                                                      "Discard",
                                                      "Draw" };

    //This function is called to read in the LayerOutXML.xml file
    public void ReadLayout(string xmlText)
    {
        xmlr = new PT_XMLReader();
        xmlr.Parse(xmlTest);        //The XML is parsed 
        xml = xmlr.xml["xml"][0];   //And xml is set as a shortcut to the XML

        //Read in the multiplier, whuich sets card spacing
        multipler.x = float.Parse(xml["multipler"][0].att("x"));
        multipler.y = float.Parse(xml["multipler"][0].att("y"));
    }

}
