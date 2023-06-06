using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    Seat east;
    Seat west;
    Seat south;
    Seat north;


    Student occupant;



    // Start is called before the first frame update
    void Start()
    {
        InitializeNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
       
            
            RaycastDirection(Vector3.left);
            RaycastDirection(Vector3.right);
            RaycastDirection(Vector3.forward);
            RaycastDirection(Vector3.back);
       
    }


    void RaycastDirection(Vector3 direction)
    {
        RaycastHit hit;

        // Send a raycast in the specified direction
        if (Physics.Raycast(transform.position, direction, out hit, 55))
        {
            // A collision occurred, handle it
            Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        }
        else
        {
            // No collision, draw the ray as green
            Debug.DrawRay(transform.position, direction * 55, Color.green);
        }
    }



    #region Meth-ods

    void InitializeNeighbors()
    {

    }


    #endregion

}
