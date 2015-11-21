using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

    public float p_forceNoWall;
    public float p_speedAgainstWall;
    public float p_speedDampenfactor;

    //collision management
    int m_currentTrig = 0;

    //player orientation
    Transform m_playerOrientation;
    Rigidbody m_playerRigidbody;

	void Start () {

        //maybe initialise the current trig there with a sphere cast

        m_playerOrientation = GameObject.Find("CenterEyeAnchor").transform;
        m_playerRigidbody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if( m_currentTrig != 0 )
        {
           
            
		}
	}

    /*void OnCollisionEnter( Collision col)
    {
        if(col.gameObject.tag== "world")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("collision");
        }
    }*/
    void handleForwardMove()
    {
        if (Input.GetButton("A"))
        {
            Debug.Log("input a");
            Vector3 orient = m_playerOrientation.forward;
            /*GetComponent<Rigidbody>().AddForce(orient.normalized * force);*/
            m_playerRigidbody.velocity = orient * p_speedAgainstWall;
        }
        else
        {
            m_playerRigidbody.velocity = Vector3.Lerp(m_playerRigidbody.velocity, Vector3.zero, p_speedDampenfactor);
            //dampen velocity
        }
    }

    void handleCapsuleCollider()
    {
        if(m_currentTrig > 0)
        {
            Debug.Log("freezerot");
            m_playerRigidbody.freezeRotation = true;
        }
        else
        {
            m_playerRigidbody.freezeRotation = false;
        }
    }

    void OnTriggerEnter( Collider other)
    {
        if(other.tag == "world")
        {
            m_currentTrig++;
            handleCapsuleCollider();
        }     
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "world")
        {
            handleForwardMove();
        }
    }

    void OnTriggerleave(Collider other)
    {
        if (other.tag == "world")
        {
            m_currentTrig--;
            handleCapsuleCollider();


            if( m_currentTrig == 0)
            {
                //addforce to the player
                m_playerRigidbody.AddForce(m_playerOrientation.forward * p_forceNoWall);
            }
        }
    }
}
