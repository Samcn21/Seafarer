using UnityEngine;
using System.Collections;

public class PlayerSync : Photon.MonoBehaviour 
{
    private Vector3 _realPosition = Vector3.zero;
    private Quaternion _realRotation = Quaternion.identity;
	void Start () 
    {
	
	}
	
	void Update ()
    {
        if (photonView.isMine)
        {
            //it's local photon and no need to predict my next position
        }
        else // belongs to other players
        {
            transform.position = Vector3.Lerp(transform.position, _realPosition, GameManager.Instance.GetPlayerSpeed() / 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, _realRotation, GameManager.Instance.GetPlayerSpeed() / 10);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting)
        {
            //This is our player and we should send this info to the network for the other uses
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else //stream is reading
        { 
            //this is someone else's player and we need to recieve their position (as a few miliseconds ago)
            //and update our version of that player
            _realPosition = (Vector3)stream.ReceiveNext();
            _realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
