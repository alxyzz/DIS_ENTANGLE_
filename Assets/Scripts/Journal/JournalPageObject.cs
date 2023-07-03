using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New JournalSequenceObject", menuName = "JournalSequenceObject")]
public class JournalPageObject : ScriptableObject
{
   [Header("Sentence when completed.")]
    public string good;

   [Header("Correct Word which is meant to be filled out.")]
    public string word;








}



public class JournalPage {

    public string Content
    {
        get
        {
            if (solved)
            {
                return good;
            }
            else return bad;
        }
    }


    public JournalPage(string g, string w)
    {
        good = g;
        word = w;
        string dots = new string('.', w.Length);
        string replaced = good.Replace(w, dots);
        bad =  replaced;
    }

    string good;
    string bad;
    string word;
    bool solved;
    public string Word{ get { return word; } }
    public bool IsSolved { get { return solved; } }


    public bool TrySolve(string b)
    {
        if (b == word)
        {
            solved = true;
            return true;
        }
        
        return false;
    }

}

