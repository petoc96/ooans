using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI.Extensions;

public class LoadScript : MonoBehaviour
{
    public Button loadButton;
    public GameObject panel;
    public GameObject objectPrefab;
    public GameObject linePrefab;
    public GameObject messagePrefab;

    private List<object> objectList = new List<object>();
    private List<GameObject> gameObjectList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        loadButton.onClick.AddListener(LoadFromFile);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadFromFile()
    {
        //string path = "@setupPatternAnimator.txt";
        string path = "C:/Users/peter.ocelik/Desktop/setupPatternAnimator.txt";
        string line;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string imagePath = reader.ReadLine();

        while (!(line = reader.ReadLine()).StartsWith("-"))
        {
            var objectInfo = GetObjectInfo(line);

            DrawObject(objectInfo.name, objectInfo.posx, objectInfo.posy);
        }

        while (!(line = reader.ReadLine()).StartsWith("-"))
        {
            var associationInfo = GetAssociationInfo(line);

            DrawAssociation(gameObjectList[associationInfo.src], gameObjectList[associationInfo.dest], associationInfo.name);
        }

        reader.Close();
    }

    private (string name, int src, int dest) GetAssociationInfo(string line) 
    {
        string[] words = line.Split(',');
        int src = Int32.Parse(words[0])-1;
        int dest = Int32.Parse(words[1])-1;
        var matches = Regex.Matches(words[2], "(?<=\")[\\w\\s\\S]+(?=\")");
        return (name: matches[0].ToString(), src: src, dest: dest);
    }

    private (string name, int posx, int posy) GetObjectInfo(string line) 
    {
        string[] words = line.Split(',');
        var matches = Regex.Matches(words[0], "(?<=\")[\\w\\s\\S]+(?=\")");
        var posx = words[1][0] - 'A';
        var posy = (int)Char.GetNumericValue(words[1][1])-1;
        objectList.Add((matches[0].ToString(), posx, posy));
        return (name: matches[0].ToString(), posx: posx, posy: posy);
    }

    private void DrawObject(string name, int col, int row) 
    {
        Transform tile = panel.transform.GetChild(row).GetChild(col);
        var myObject = Instantiate(objectPrefab, new Vector3(tile.position.x,tile.position.y, tile.position.z), Quaternion.identity);
        myObject.transform.parent = tile;
        myObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = name;
        gameObjectList.Add(myObject);
    }

    private void DrawAssociation(GameObject object1, GameObject object2, string name) 
    {
        // set the points of the objects
        Vector2 point1 = new Vector2(object1.transform.position.x+200, object1.transform.position.y);
        Vector2 point2 = new Vector2(object2.transform.position.x+200, object2.transform.position.y); 
        
        float middleX = (object1.transform.position.x+200+object2.transform.position.x+200)/2;
        float middleY = (object1.transform.position.y+object2.transform.position.y)/2;
        float subX = (object2.transform.position.x-object1.transform.position.x);
        float subY = (object2.transform.position.y-object1.transform.position.y);
        

        // instantiate a line
        var myLine = Instantiate(linePrefab, new Vector3(0,0,0), Quaternion.identity);
        var myMessage = Instantiate(messagePrefab, new Vector3(middleX,middleY+20,0), Quaternion.identity);
        myMessage.transform.parent = myLine.transform.GetChild(0);
        var angle = Mathf.Atan2(subY, subX)*180 / Mathf.PI;
        myMessage.transform.eulerAngles = new Vector3(0, 0, angle);
        myMessage.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;

        // set parent of line
        myLine.transform.parent = panel.transform.parent;
        // set points for the line
        myLine.transform.GetChild(0).GetComponent<UILineRenderer>().m_points = new Vector2[] {point1, point2};
        //set line into the background
        myLine.transform.SetAsFirstSibling();
    }
}
