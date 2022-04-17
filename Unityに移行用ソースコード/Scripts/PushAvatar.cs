using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushAvatarX : MonoBehaviour
{
    public GameObject avatar;
    private int buttonID;
    private int imageID;
    public DynamicBoneCollider handColliderR;
    public DynamicBoneCollider handColliderL;
    private XRController xr;
    private DynamicBone[] avatarDyBone;
    // Start is called before the first frame update
    void Start()
    {
        xr = (XRController)GameObject.FindObjectOfType(typeof(XRController));
    }
    public void ActivateHaptic()
    {
        xr.SendHapticImpulse(0.7f, 2f);
    }
    private void Update()
    {
        if (avatar != null)
        {
            avatar.transform.Rotate(Vector3.up * 30 * Time.deltaTime);
        }
        

    }
    // Update is called once per frame
    public void CreatAvatar()
    {
        if (avatar != null)
        {
            Destroy(avatar);
        }
        
        imageID = ButtonDownX.avatarID;
        string fname = "Avatar/" +imageID.ToString();
        Debug.Log(fname);
        avatar = Instantiate((GameObject)Resources.Load(fname));
        avatar.transform.position = this.transform.position;
        avatar.transform.rotation = this.transform.rotation;
        avatarDyBone = GameObject.FindObjectsOfType<DynamicBone>();
        foreach(DynamicBone t in avatarDyBone)
        {
            t.m_Colliders.Add(handColliderR);
            t.m_Colliders.Add(handColliderL);
        }
    }
}
