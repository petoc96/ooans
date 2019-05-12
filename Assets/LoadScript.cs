using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    public Button loadButton;
    public Button startButton;
    public Button exitButton;
    public GameObject panel;
    public GameObject objectPrefab;
    public GameObject linePrefab;
    public GameObject messagePrefab;
    public GameObject messageLeftPrefab;
    public GameObject topPrefab;
    public GameObject bottomPrefab;
    

    private List<object> objectList = new List<object>();
    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<(GameObject object1, GameObject object2, GameObject msg)> communicationList = new List<(GameObject object1, GameObject object2, GameObject msg)>();
    private List<(int object1, int object2, GameObject line)> associationList = new List<(int object1, int object2, GameObject line)>();
    private List<int> animationIndices = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        loadButton.onClick.AddListener(LoadFromFile);
        startButton.onClick.AddListener(StartAnimation);
        exitButton.onClick.AddListener(Exit);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAnimation() 
    {
        StartCoroutine(Animate());
    }

    private void DestroyAllInstances()
    {
        objectList.Clear();

        foreach(GameObject obj in gameObjectList)
        {
            Destroy(obj);
        }

        gameObjectList.Clear();

        foreach ((int object1, int object2, GameObject line) obj in associationList)
        {
            Destroy(obj.line);
        }

        associationList.Clear();
        communicationList.Clear();
        animationIndices.Clear();

    }

    private void LoadFromFile()
    {
        DestroyAllInstances();

        //string path = "@setupPatternAnimator.txt";
        string path = "setupPatternAnimator.txt";
        string line;


        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        
      
        string imagePath = reader.ReadLine();


        byte[] fileData = File.ReadAllBytes(imagePath);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); 
        Sprite image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        GameObject ImagePanel = GameObject.Find("TempPanel");

        ImagePanel.GetComponent<Image>().sprite = image;
        ImagePanel.GetComponent<Image>().type = Image.Type.Simple;
        ImagePanel.GetComponent<Image>().preserveAspect = true;

        while (!(line = reader.ReadLine()).StartsWith("-"))
        {
            var objectInfo = GetObjectInfo(line);

            DrawObject(objectInfo.name, objectInfo.posx, objectInfo.posy);
        }

        while (!(line = reader.ReadLine()).StartsWith("-"))
        {
            var associationInfo = GetAssociationInfo(line);

            DrawAssociation(associationInfo.src, associationInfo.dest, associationInfo.name);
        }

        while ((line = reader.ReadLine()) != null)
        {
            int index = Int32.Parse(line)-1;
            animationIndices.Add(index);
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
        myObject.transform.SetParent(tile);
        myObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = name;

        gameObjectList.Add(myObject);
    }

    private void DrawAssociation(int src, int dest, string name) 
    {
        float subX, subY;
        GameObject myMessage;
        GameObject myLine;
        GameObject myLayout;
        GameObject object1 = gameObjectList[src]; 
        GameObject object2 = gameObjectList[dest];
        Debug.Log("SRC " + object1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        Debug.Log("DEST " + object2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        float middleX = (object1.transform.position.x+200+object2.transform.position.x+200)/2;
        float middleY = (object1.transform.position.y+object2.transform.position.y)/2;
        // set the points of the objects
        Vector2 point1 = new Vector2(object1.transform.position.x+200, object1.transform.position.y);
        Vector2 point2 = new Vector2(object2.transform.position.x+200, object2.transform.position.y); 

        // If association does NOT exist
        if (!associationList.Exists(m => ((m.object1 == src && m.object2 == dest)||(m.object1 == dest && m.object2 == src)))) 
        {
            Debug.Log("Association does not exist!");
            
            // instantiate a line
            myLine = Instantiate(linePrefab, new Vector3(0,0,0), Quaternion.identity);
            
            if(point1.x < point2.x || point1.y < point2.y)
            {
                myLayout = Instantiate(topPrefab, new Vector3(middleX,middleY,0), Quaternion.identity);
                myLayout.transform.SetParent(myLine.transform.GetChild(0));
            }

            else
            {
                myLayout = Instantiate(bottomPrefab, new Vector3(middleX, middleY, 0), Quaternion.identity);
                myLayout.transform.SetParent(myLine.transform.GetChild(0));
            }

            // set parent of line
            myLine.transform.SetParent(panel.transform.parent);
            // set points for the line
            myLine.transform.GetChild(0).GetComponent<UILineRenderer>().m_points = new Vector2[] {point1, point2};
            //set line into the background
            myLine.transform.SetAsFirstSibling();
            // add to list
            associationList.Add((object1: src, object2: dest, line: myLine));
        }
        // If association A->B exists
        else if(associationList.Exists(m => ((m.object1 == src && m.object2 == dest))))
        {
            Debug.Log("Association exist! Src -> Dest");
            var list = associationList.Find(m => (m.object1 == src && m.object2 == dest));
            myLine = list.line;

            string prefabName = "";

            if (point1.x < point2.x)
            {
                prefabName = "UPmessages(Clone)";
            }
            else if (point1.x > point2.x) 
            { 
                prefabName = "DOWNmessages(Clone)";
            }
            else if (point1.y < point2.y) 
            { 
                prefabName = "UPmessages(Clone)";
            }
            else
            {
                prefabName = "DOWNmessages(Clone)";
            }

            if (myLine.transform.GetChild(0).GetComponentInChildren<Transform>().Find(prefabName) != null)
            {
                myLayout = myLine.transform.GetChild(0).GetComponentInChildren<Transform>().Find(prefabName).gameObject;
            }
            else
            {
                if (prefabName == "UPmessages(Clone)")
                {
                    Debug.Log("TOP " + middleX + " " + middleY);
                    myLayout = Instantiate(topPrefab, new Vector3(middleX, middleY, 0), Quaternion.identity);
                    myLayout.transform.SetParent(myLine.transform.GetChild(0));
                }
                else 
                {
                    Debug.Log("BOTTOM " + middleX + " " + middleY);
                    myLayout = Instantiate(bottomPrefab, new Vector3(middleX, middleY, 0), Quaternion.identity);
                    myLayout.transform.SetParent(myLine.transform.GetChild(0));
                }
                
            }
        }
        // If association B->A exists
        else
        {
            Debug.Log("Association exist! Dest -> Src");

            var list = associationList.Find(m => (m.object1 == dest && m.object2 == src));
            myLine = list.line;

            string prefabName = "";

            if (point1.x < point2.x)
            {
                prefabName = "UPmessages(Clone)";
            }
            else if (point1.x > point2.x) 
            { 
                prefabName = "DOWNmessages(Clone)";
            }
            else if (point1.y < point2.y) 
            { 
                prefabName = "UPmessages(Clone)";
            }
            else
            {
                prefabName = "DOWNmessages(Clone)";
            }

            if (myLine.transform.GetChild(0).GetComponentInChildren<Transform>().Find(prefabName) != null) 
            {
                myLayout = myLine.transform.GetChild(0).GetComponentInChildren<Transform>().Find(prefabName).gameObject;
            }

            else
            {
                if (prefabName == "DOWNmessages(Clone)") 
                {
                    Debug.Log("BOTTOM " + middleX + " " + middleY);
                    myLayout = Instantiate(bottomPrefab, new Vector3(middleX, middleY, 0), Quaternion.identity);
                    myLayout.transform.SetParent(myLine.transform.GetChild(0));
                }
                else 
                {
                    Debug.Log("TOP " + middleX + " " + middleY);
                    myLayout = Instantiate(topPrefab, new Vector3(middleX, middleY, 0), Quaternion.identity);
                    myLayout.transform.SetParent(myLine.transform.GetChild(0));
                }
                
            }
        }

        if (point1.x < point2.x)
        {
            subX = (object2.transform.position.x - object1.transform.position.x);
            subY = (object2.transform.position.y - object1.transform.position.y);
        }
        else if (point1.x > point2.x) 
        { 
            subX = (object1.transform.position.x - object2.transform.position.x);
            subY = (object1.transform.position.y - object2.transform.position.y);
        }
        else if (point1.y < point2.y) 
        { 
            subX = (object2.transform.position.x - object1.transform.position.x);
            subY = (object2.transform.position.y - object1.transform.position.y);
        }
        else
        {
            subX = (object1.transform.position.x - object2.transform.position.x);
            subY = (object1.transform.position.y - object2.transform.position.y);
        }

        var angle = Mathf.Atan2(subY, subX)*180 / Mathf.PI;
        myLayout.transform.eulerAngles = new Vector3(0, 0, angle);


        //if (point1.x < point2.x || point1.y < point2.y)
        if (point1.x < point2.x)
        {
            Debug.Log("RIGHT x1" + point1.x + " x2" + point2.x);
            myMessage = Instantiate(messagePrefab, new Vector3(0, 0, 0), Quaternion.identity);

            // set parent of message
            myMessage.transform.SetParent(myLayout.transform);
            // set angle of message
            myMessage.transform.eulerAngles = new Vector3(0, 0, angle);
            // set name of message
            myMessage.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
        else if (point1.x > point2.x)
        {
            Debug.Log("LEFT x1" + point1.x + " x2" + point2.x);
            myMessage = Instantiate(messageLeftPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            // set parent of message
            myMessage.transform.SetParent(myLayout.transform);
            // set angle of message
            myMessage.transform.eulerAngles = new Vector3(0, 0, angle);
            // set name of message
            myMessage.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
        else if (point1.y < point2.y)
        {
            Debug.Log("RIGHT y1" + point1.y + " y2" + point2.y);
            myMessage = Instantiate(messagePrefab, new Vector3(0, 0, 0), Quaternion.identity);

            // set parent of message
            myMessage.transform.SetParent(myLayout.transform);
            // set angle of message
            myMessage.transform.eulerAngles = new Vector3(0, 0, angle);
            // set name of message
            myMessage.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
        else
        {
            myMessage = Instantiate(messageLeftPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            // set parent of message
            myMessage.transform.SetParent(myLayout.transform);
            // set angle of message
            myMessage.transform.eulerAngles = new Vector3(0, 0, angle);
            // set name of message
            myMessage.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = name;
        }

        communicationList.Add((object1: object1, object2: object2, msg: myMessage));

    }

    private void DrawMessage(GameObject line)
    {
        
    }      

    private IEnumerator Animate()
    {

        foreach(int index in animationIndices) 
        {
            GameObject object1 = communicationList[index].object1;
            GameObject object2 = communicationList[index].object2;
            GameObject msg = communicationList[index].msg;
            Color color2 = new Color32(159,152,240, 255); // violet
            Color color1 = new Color32(152,240,159, 255); // green
            //Color color1 = new Color32(240,159,152, 255); // red

            yield return new WaitForSeconds(2);
            SetColor(object1, object2, msg, Color.green, color1);
            yield return new WaitForSeconds(2);
            SetColor(object1, object2, msg, Color.black, color2);
        }
    }

    private void SetColor(GameObject object1, GameObject object2, GameObject msg, Color color, Color color2)
    {
        object1.GetComponent<Image>().color = color2;
        object2.GetComponent<Image>().color = color2;

        if (object1.transform.position.x < object2.transform.position.x)
        {
            msg.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
            msg.transform.GetChild(1).GetComponent<UILineRenderer>().color = color;
            msg.transform.GetChild(1).GetChild(0).GetComponent<UIPolygon>().color = color;
        }
        else if (object1.transform.position.x > object2.transform.position.x) 
        {
            msg.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
            msg.transform.GetChild(0).GetComponent<UILineRenderer>().color = color;
            msg.transform.GetChild(0).GetChild(0).GetComponent<UIPolygon>().color = color;
        }
        else if (object1.transform.position.y < object2.transform.position.y) 
        {
            msg.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
            msg.transform.GetChild(1).GetComponent<UILineRenderer>().color = color;
            msg.transform.GetChild(1).GetChild(0).GetComponent<UIPolygon>().color = color;
        }
        else
        {
            msg.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
            msg.transform.GetChild(0).GetComponent<UILineRenderer>().color = color;
            msg.transform.GetChild(0).GetChild(0).GetComponent<UIPolygon>().color = color;
        }
        
    }

    private void Exit()
    {
        Application.Quit();
    }

}
