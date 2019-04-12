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

        //Read in the slots
        SlotDef tSD;

        //slotsX is used tas shortcut to all <slot>s
        PT_XMLHashList slotsX = xml["slot"];

        for (int i = 0; i < slotsX.Count; i++)
        {
            tSD = new SlotDef();                //Create a new SlotDef instance
            if (slotsX[i].HasAtt("type"))       //If this <slot> has a type attribute parse it
            {
                tSD.type = slotsX[i].att("type");
            }
            else                                //If not, set its type to "slot"; it's tableur card
            {
                tSD.type = "slot";
            }

            //Various attributes are parsed into numerical values
            tSD.x = float.Parse(slotsX[i].att("x"));
            tSD.y = float.Parse(slotsX[i].att("y"));
            tSD.layerID = int.Parse(slotsX[i].att("layer"));

            //This converts the number of the layerID into a text layerName
            tSD.layerName = sortingLayerNames[tSD.layerID];
            //The layers used to make sure that the correct cards are on top of the others.
            //  In Unity 2D, all of our assets are effectively at the same Z depth, so the
            //  the layer is used to differntiate between them

            switch (tSD.type)
            {
                //pull additional attributes based on the type of this <slot>

                case "slot":
                    tSD.faceUp = (slotsX[i].att("faceup") == "1");
                    tSD.id = int.Parse(slotsX[i].att("id"));

                    if(slotsX[i].HasAtt("hiddenby"))
                    {
                        string[] hiding = slotsX[i].att("hiddenby").Split(',');
                        foreach(string s in hiding)
                        {
                            tSD.hiddenBy.Add(int.Parse(s));
                        }
                    }
                    slotDefs.Add(tSD);
                    break;
            }


        }
    }

}
