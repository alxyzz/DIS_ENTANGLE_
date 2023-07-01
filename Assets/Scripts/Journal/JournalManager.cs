using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    #region singleton
    private static JournalManager _instance;
    public static JournalManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Multiple instances of JournalManager detected. Excess one was on gameobject - " + gameObject.name);

        }
        else
        {
            _instance = this;

        }
    }

    #endregion
    [SerializeReference] List<List<JournalSequenceObject>> sequenceObjects = new();

    List<List<JournalSegment>> allsequences = new();

    TextMeshProUGUI sentence;
    public List<JournalAnswerButton> buttons;
    List<JournalAnswerButton> usedButtons;
    List<JournalAnswerButton> unusedButtons;

    List<JournalSegment> sequence = new();
    string currentText
    {
        get
        {
            string b = "";
            foreach (var item in sequence)
            {
                b += item.Content;
            }
            return b;
        }
    }
    public void OnClickButton(string s)
    {
        bool b = false;
        foreach (var item in sequence)
        {
            item.TrySolve(s);
        }
        Refresh();
    }
    void Refresh()
    {
        sentence.text = currentText;
        bool b = true;
        foreach (var item in sequence)
        {
            if (!item.IsSolved)
            {
                b = false;
            }
        }
        if (b)
        {
            InitializePage(true);
        }
    }
    /// <summary>
    /// initializes the next page in the stack
    /// </summary>
    void InitializePage(bool next)
    {
        if (next)
        {
            if (allsequences[allsequences.IndexOf(sequence) + 1] == null)
            {
                Win();
                return;
            }
            sequence = allsequences[allsequences.IndexOf(sequence) + 1];
        }
        else
        {
            if (allsequences == null || allsequences[0] == null)
            {
                throw new System.Exception("JournalManager@InitializePage() - did not have any sequences and thus could not initialize a page.");
            }
            sequence = allsequences[0];
        }
        
        for (int i = 0; i < sequence.Count; i++)
        {
            buttons[i].text.text = sequence[i].Word;
        }
       
    }
    void Win()
    {
        Debug.Log("No more sequences left - won the game.");

    }
    void Init()
    {
        foreach (var item in sequenceObjects)
        {
            List<JournalSegment> b = new();
            foreach (var t in item)
            {
                b.Add(new JournalSegment(t.good, t.word));
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        InitializePage(false);
    }


    #region Text position finder

//    public TextMeshProUGUI textComp;
//    public int charIndex;
//    public Canvas canvas;
//    void PrintPos()
//    {
//        string text = textComp.text;

//        if (charIndex >= text.Length)
//            return;

//        TextGenerator textGen = new TextGenerator(text.Length);

//        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
//        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
//        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
//        if (indexOfTextQuad < textGen.vertexCount)
//        {
//            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
//            textGen.verts[indexOfTextQuad + 1].position +
//            textGen.verts[indexOfTextQuad + 2].position +
//            textGen.verts[indexOfTextQuad + 3].position) / 4f;

//            print(avgPos);
//            PrintWorldPos(avgPos);
//        }
//        else
//        {
//            Debug.LogError("Out of text bound");
//        }
//    }

//    void PrintWorldPos(Vector3 testPoint)
//    {
//        Vector3 worldPos = textComp.transform.TransformPoint(testPoint);
//        print(worldPos);
//        new GameObject("point").transform.position = worldPos;
//        Debug.DrawRay(worldPos, Vector3.up, Color.red, 50f);
//    }

//    void OnGUI()
//    {
//        if (GUI.Button(new Rect(10, 10, 100, 80), "Test"))
//        {
//            PrintPos();
//        }
//    }

////[82165-findletterposinuitext.zip|82165]


    #endregion
}
