using UnityEngine;
using System.Collections;

public class playerBehaviour2 : MonoBehaviour {

    public GameObject handedObject =null;
    public float armLength = 20;
    public float dropRotationForce;

    ///player orientation
    Transform m_playerOrientation;
    Rigidbody m_playerRigidbody;
    Transform m_visor;

    //sound Integration
    soundManager m_soundMng;


    void Start()
    {
        //maybe initialise the current trig there with a sphere cast
        m_soundMng = GameObject.Find("GameManager").GetComponent<soundManager>();
        m_playerOrientation = GameObject.Find("CenterEyeAnchor").transform;
        m_visor = GameObject.Find("Visor").transform;
        m_playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
	
        if(Input.GetButtonDown("Right Bumper"))
        {
            RaycastHit hit;
            Vector3 playerOrientation = m_playerOrientation.transform.forward;
            Ray playerRay = new Ray(transform.position, playerOrientation);
            if (Physics.Raycast(playerRay, out hit, armLength))
            {
                Debug.Log(hit.collider.gameObject.name);
                Collider col = hit.collider;
                if (col.tag == "key" || col.tag == "randomItem")
                {
                    handedObject = col.gameObject;

                    handedObject.transform.SetParent(m_visor, true);
                    handedObject.transform.localPosition = Vector3.zero;

                    handedObject.GetComponent<Rigidbody>().isKinematic = true;
                    m_soundMng.Play(soundManager.soundTypes.grabObject);
                }
            }
        }
        if(Input.GetButtonUp("Right Bumper"))
        {
            if( handedObject)
            {
                handedObject.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 playerOrientation = m_playerOrientation.transform.forward;
                handedObject.transform.parent = null;
                Rigidbody rgbd = handedObject.GetComponent<Rigidbody>();
           
                rgbd.AddForce(playerOrientation.normalized);
                rgbd.AddTorque((Vector3.right + Vector3.forward).normalized * dropRotationForce);
                handedObject = null;
                m_soundMng.Play(soundManager.soundTypes.dropObject);
            }
            //let go
        }
        if (Input.GetButtonDown("Left Bumper"))
        {
            if( handedObject)
            {
                handedObject.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 playerOrientation = m_playerOrientation.transform.forward;
                handedObject.transform.parent = null;
                Rigidbody rgbd = handedObject.GetComponent<Rigidbody>();
                rgbd.AddForce(playerOrientation.normalized * 70);
                rgbd.AddTorque((Vector3.right + Vector3.forward).normalized * dropRotationForce * 15);
                handedObject = null;
                m_soundMng.Play(soundManager.soundTypes.pushObject);
            }
            //Throw
        }


    }
}
