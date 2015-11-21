using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

    public float p_takeOffForce;
    public float p_wallRunSpeed;
    public float p_wallRunSpeedDampenFactor;
    public float p_rotationForce;

    //collision management
   public int m_currentTrig = 0;

    //player orientation
    Transform m_playerOrientation;
    Rigidbody m_playerRigidbody;

    //rough head moves management
    float m_timePassed = 0;
    bool headbangCooldown = false;
    Vector3 m_orientAtBegin;
    public float p_timeInterval;
    public float p_roughThreshold;


	void Start () {

        //maybe initialise the current trig there with a sphere cast

        m_playerOrientation = GameObject.Find("CenterEyeAnchor").transform;
        m_playerRigidbody = GetComponent<Rigidbody>();
        m_orientAtBegin = m_playerOrientation.forward;


    }
	
	void Update () {
        handlePlayerRotation();
	}

    void FixedUpdate()
    {
        handleRoughHeadMoves();
    }


    void handleRoughHeadMoves()
    {
        m_timePassed += Time.fixedDeltaTime;
        if(m_timePassed > p_timeInterval)
        {
            if( !headbangCooldown )
            {
                float distance = Vector3.Distance(m_playerOrientation.forward, m_orientAtBegin);
                if (distance > p_roughThreshold)
                {
                    headbangCooldown = true;
                    Vector3 crossVect = -Vector3.Cross(m_playerOrientation.forward, m_orientAtBegin).normalized;
                    m_playerRigidbody.AddTorque(crossVect * p_rotationForce);
                }
            }
            else
            {
                headbangCooldown = false;
            }

            m_orientAtBegin = m_playerOrientation.forward;
            m_timePassed = 0;
        }
    }


    void handlePlayerRotation()
    {
        //si input de l'oculus balancez un momentum
        //sinon dampen le momentum
        /*if( Input.GetButtonDown("B"))
        {
            Debug.Log("inputb");
            //m_playerRigidbody.AddRelativeTorque(Vector3.up * p_rotationForce);
            m_playerRigidbody.AddTorque(Vector3.forward * p_rotationForce);
        }*/
        //the angular drag will take care of dampening the rotation
    }


    void handleForwardMove()
    {
        if (Input.GetButton("A"))
        {
            Vector3 orient = m_playerOrientation.forward;
            m_playerRigidbody.velocity = orient * p_wallRunSpeed;
        }
        else
        {
            //ralentissement des mouvements si l'on est contre un mur
            m_playerRigidbody.velocity = Vector3.Lerp(m_playerRigidbody.velocity, Vector3.zero, p_wallRunSpeedDampenFactor);
        }
    }

    void handleCapsuleCollider()
    {
        if(m_currentTrig > 0)
        {
            m_playerRigidbody.freezeRotation = true;
        }
        else
        {
            m_playerRigidbody.freezeRotation = false;
            Debug.Log("unfreeze");
        }
    }

    void OnTriggerEnter( Collider other)
    {
        if(other.tag == "world")
        {
            m_currentTrig++;
            //temp disable for torque
            //handleCapsuleCollider();
        }     
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "world")
        {
            handleForwardMove();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("triggerLeave");
        if (other.tag == "world")
        {
            m_currentTrig--;
            //handleCapsuleCollider();
            Debug.Log("decrement trig:" + m_currentTrig);

            if( m_currentTrig == 0)
            {
                m_playerRigidbody.AddForce(m_playerOrientation.forward * p_takeOffForce);
            }
        }
    }
}
