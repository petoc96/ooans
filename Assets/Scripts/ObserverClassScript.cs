using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Collections;


public class ObserverClassScript : MonoBehaviour
{

    public Button startButton;
    public Button testButton;
    public GameObject subject;
    public GameObject CSubject;
    public GameObject observer;
    public GameObject observerA;
    public GameObject observerB;
    public GameObject observerC;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(DoSomething);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoSomething()
    {
        StartCoroutine(SetMyColor());
    }

    private IEnumerator SetMyColor()
    {

        // ATTACH
        GameObject methodsS = CSubject.transform.Find("Methods").gameObject;
        GameObject attributesS = CSubject.transform.Find("Attributes").gameObject;
        GameObject attributesOA = observerA.transform.Find("Attributes").gameObject;
        GameObject attributesOB = observerB.transform.Find("Attributes").gameObject;
        GameObject attributesOC = observerC.transform.Find("Attributes").gameObject;
        GameObject methodsOA = observerA.transform.Find("Methods").gameObject;
        GameObject methodsOB = observerB.transform.Find("Methods").gameObject;
        GameObject methodsOC = observerC.transform.Find("Methods").gameObject;

        TextMeshProUGUI textAttach = methodsS.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textStateS = attributesS.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textObserversS = attributesS.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textStateOA = attributesOA.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textStateOB = attributesOB.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textStateOC = attributesOC.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI textSetState = methodsS.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textGetState = methodsS.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textNotify = methodsS.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textDetach = methodsS.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textUpdateA = methodsOA.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textUpdateB = methodsOB.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textUpdateC = methodsOC.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

    

        yield return new WaitForSeconds(1);
        textAttach.color = Color.red;
        yield return new WaitForSeconds(1);
        textObserversS.color = Color.red;
        observerA.GetComponent<Image>().color = Color.red;
        textObserversS.text += " = oA";
        yield return new WaitForSeconds(1);
        observerB.GetComponent<Image>().color = Color.red;
        textObserversS.text += ", oB";
        yield return new WaitForSeconds(1);
        observerC.GetComponent<Image>().color = Color.red;
        textObserversS.text += ", oC";


        yield return new WaitForSeconds(2);
        textAttach.color = Color.black;
        observerA.GetComponent<Image>().color = Color.white;
        observerB.GetComponent<Image>().color = Color.white;
        observerC.GetComponent<Image>().color = Color.white;
        textObserversS.color = Color.black;

        // SET STATE
        yield return new WaitForSeconds(2);
        textSetState.color = Color.red;
        yield return new WaitForSeconds(1);
        textSetState.text = " + SetState(234)";
        yield return new WaitForSeconds(1);
        textStateS.color = Color.red;
        textStateS.text += " = 234";
        yield return new WaitForSeconds(1);
        textSetState.text = " + SetState(state)";
        textStateS.color = Color.black;
        textSetState.color = Color.black;

        // NOTIFY
        yield return new WaitForSeconds(1);
        textNotify.color = Color.red;

        // UPDATE
        yield return new WaitForSeconds(1);
        textUpdateA.color = Color.red;
        yield return new WaitForSeconds(1);
        textGetState.color = Color.red;
        textStateOA.color = Color.red;
        textStateOA.text += " = 234";
        yield return new WaitForSeconds(1);
        textUpdateA.color = Color.black;
        textGetState.color = Color.black;
        textStateOA.color = Color.black;
        yield return new WaitForSeconds(1);
        textUpdateB.color = Color.red;
        yield return new WaitForSeconds(1);
        textGetState.color = Color.red;
        textStateOB.color = Color.red;
        textStateOB.text += " = 234";
        yield return new WaitForSeconds(1);
        textUpdateB.color = Color.black;
        textGetState.color = Color.black;
        textStateOB.color = Color.black;
        yield return new WaitForSeconds(1);
        textUpdateC.color = Color.red;
        yield return new WaitForSeconds(1);
        textGetState.color = Color.red;
        textStateOC.color = Color.red;
        textStateOC.text += " = 234";
        yield return new WaitForSeconds(1);
        textUpdateC.color = Color.black;
        textGetState.color = Color.black;
        textStateOC.color = Color.black;
        //yield return new WaitForSeconds(1);
        textNotify.color = Color.black;

        // DETACH 
        yield return new WaitForSeconds(2);
        textDetach.color = Color.red;
        yield return new WaitForSeconds(1);
        textObserversS.color = Color.red;
        observerA.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1);
        textObserversS.text = " - observers = oB, oC";
        yield return new WaitForSeconds(1);
        observerB.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1);
        textObserversS.text = " - observers = oC";
        yield return new WaitForSeconds(1);
        observerC.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1);
        textObserversS.text = " - observers";

        yield return new WaitForSeconds(2);
        textDetach.color = Color.black;
        observerA.GetComponent<Image>().color = Color.white;
        observerB.GetComponent<Image>().color = Color.white;
        observerC.GetComponent<Image>().color = Color.white;
        textObserversS.color = Color.black;
        textStateOA.text = " - observerState";
        textStateOB.text = " - observerState";
        textStateOC.text = " - observerState";
        textStateS.text = " - subjectState";

    }
}
