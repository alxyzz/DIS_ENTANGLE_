using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Act
{
    One,
    Two,

}

public enum Level
{
    Menu,
    Classroom,
    Wolfblade,
    Paige,
    Janitor
}
public class GameManager : MonoBehaviour
{

    public Act CurrentAct;
    public Level CurrentLevel;

    public void ChangeLevel()
    {
        switch (switch_on)
        {
            default:
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
