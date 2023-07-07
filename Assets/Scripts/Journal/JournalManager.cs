using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    bool hasWon
    {
        get
        {
            bool b = true;
            foreach (var item in currentPage)
            {
                if (!item.IsSolved)
                {
                    b = false;
                }
            }
            return b;
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
    public List<JournalPageObject> firstPage = new();
    public List<JournalPageObject> secondPage = new();
     List<List<JournalPageObject>> sequenceObjects = new();

    List<List<JournalPage>> allPages = new();

    TextMeshProUGUI txt_Sentence;
    public List<JournalAnswerButton> buttons = new();

    List<JournalPage> currentPage = new();
    string CURR_PAGE_TEXT
    {
        get
        {
            string b = "";
            foreach (var item in currentPage)
            {
                b += item.Content;
            }
            return b;
        }
    }
    public void OnClickButton(string s)
    {
        bool b = false;
        foreach (var item in currentPage)
        {
            item.TrySolve(s);
        }
        Refresh();
    }
    void Refresh()
    {
        txt_Sentence.text = CURR_PAGE_TEXT;
        bool b = true;
        foreach (var item in currentPage)
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
    /// initializes the page in the stack. if next is true, we initialize the second
    /// </summary>
    void InitializePage(bool next)
    {
        if (next)
        {
            if (allPages[allPages.IndexOf(currentPage) + 1] == null)
            {
                Win();
                return;
            }
            currentPage = allPages[allPages.IndexOf(currentPage) + 1];
        }
        else
        {
            if (allPages == null)
            {
                throw new System.Exception("JournalManager@InitializePage() - allPages was NULL for some bad reason.");
            }
            if (allPages[0] == null)
            {
                throw new System.Exception("JournalManager@InitializePage() - did not have any sequences and thus could not initialize a page.");
            }
            currentPage = allPages[0];
        }

        txt_Sentence.text = CURR_PAGE_TEXT;
        //initializes buttons
        for (int i = 0; i < currentPage.Count; i++)
        {
            buttons[i].text.text = currentPage[i].Word;
        }

    }
    void Win()
    {
        Debug.Log("No more sequences left - won the game.");

    }
    void Init()
    {

        //allsequences has pages
        //each p


        sequenceObjects.Add(firstPage);
        sequenceObjects.Add(secondPage);


        foreach (var item in sequenceObjects)
        {
            List<JournalPage> b = new();
            foreach (var t in item)
            {
                b.Add(new JournalPage(t.good, t.word));
            }
            allPages.Add(b);
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
