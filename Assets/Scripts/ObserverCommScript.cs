using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Collections;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;


public class ObserverCommScript : MonoBehaviour
{

    public Button startButton;
    public Button switchButton;

    public GameObject subjectToObserverA;
    public GameObject subjectToObserverB;
    public GameObject subjectToObserverC;
    public GameObject observerAToSubject;
    public GameObject observerBToSubject;
    public GameObject observerCToSubject;
    public GameObject clientToSubject;
    public GameObject subjectToSubject;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(DoSomething);
        switchButton.onClick.AddListener(SwitchScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }

    private void DoSomething()
    {
        StartCoroutine(SetMyColor());
    }

    private IEnumerator SetMyColor()
    {

        Transform textSubjectToObserverA = subjectToObserverA.transform.GetChild(0);
        Transform lineSubjectToObserverA = subjectToObserverA.transform.GetChild(1);
        Transform textSubjectToObserverB = subjectToObserverB.transform.GetChild(0);
        Transform lineSubjectToObserverB = subjectToObserverB.transform.GetChild(1);
        Transform textSubjectToObserverC = subjectToObserverC.transform.GetChild(0);
        Transform lineSubjectToObserverC = subjectToObserverC.transform.GetChild(1);
        Transform textObserverAToSubject = observerAToSubject.transform.GetChild(0);
        Transform lineObserverAToSubject = observerAToSubject.transform.GetChild(1);
        Transform textObserverBToSubject = observerBToSubject.transform.GetChild(0);
        Transform lineObserverBToSubject = observerBToSubject.transform.GetChild(1);
        Transform textObserverCToSubject = observerCToSubject.transform.GetChild(0);
        Transform lineObserverCToSubject = observerCToSubject.transform.GetChild(1);
        Transform textClientToSubject = clientToSubject.transform.GetChild(0);
        Transform lineClientToSubject = clientToSubject.transform.GetChild(1);
        Transform textSubjectToSubject = subjectToSubject.transform.GetChild(0);
        Transform lineSubjectToSubject = subjectToSubject.transform.GetChild(1);
        
        
        // ATTACH
        yield return new WaitForSeconds(2);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textSubjectToObserverA.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverB.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverC.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textSubjectToObserverA.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverB.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverC.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;

        // SET STATE
        yield return new WaitForSeconds(1);
        lineClientToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineClientToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textClientToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineClientToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineClientToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textClientToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;

        // NOTIFY
        yield return new WaitForSeconds(1);
        lineSubjectToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textSubjectToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineSubjectToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textSubjectToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;

        // UPDATE
        yield return new WaitForSeconds(1);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textSubjectToObserverA.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverB.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverC.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textSubjectToObserverA.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverB.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverC.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;

        // GET STATE
        yield return new WaitForSeconds(1);
        lineObserverAToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineObserverAToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineObserverBToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineObserverBToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineObserverCToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineObserverCToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textObserverAToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textObserverBToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textObserverCToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineObserverAToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineObserverAToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineObserverBToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineObserverBToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineObserverCToSubject.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineObserverCToSubject.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textObserverAToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textObserverBToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textObserverCToSubject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;

        // DETACH
        yield return new WaitForSeconds(1);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.red;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.red;
        textSubjectToObserverA.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverB.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        textSubjectToObserverC.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(2);
        lineSubjectToObserverA.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverA.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverB.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverB.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        lineSubjectToObserverC.gameObject.GetComponent<UILineRenderer>().color = Color.black;
        lineSubjectToObserverC.GetChild(0).gameObject.GetComponent<UIPolygon>().color = Color.black;
        textSubjectToObserverA.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverB.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        textSubjectToObserverC.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;


    }
}
