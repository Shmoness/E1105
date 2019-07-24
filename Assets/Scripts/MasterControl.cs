using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControl : MonoBehaviour
{
    public Animator m_CameraMotion;
    public ParticleSystem m_Water;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        m_CameraMotion.SetBool("Next", true);
    }

    public void ActivateWater()
    {
        m_Water.Play();
    }

    public void DeactivateWater()
    {
        m_Water.Stop();
    }
}
