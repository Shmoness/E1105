﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterControl : MonoBehaviour
{
    public Animator m_CameraMotion;
    public ParticleSystem m_Water;
    public CanvasGroup m_Text;
    public Image m_TextBox;
    public Sprite[] m_TextSprites = new Sprite[5];

    public Animator m_PIP;
    public Camera m_PIPCam;

    public GameObject m_TextBoxBackground;

    private string[] m_States = {"Rotate_1", "Zoom_2", "Rotate_3", "Rotate_4", "Zoom_5", "Rotate_6", "Zoom_9", "Rotate_10", "Rotate_11", "Zoom_13", "End"};
    private int m_CurrentState;

    private bool waterActive;
    private bool pipActive;

    private bool singleClick;
    private float doubleClickTime = 0.5f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        m_Water.Stop();
        waterActive = false;
        pipActive = false;

        m_CurrentState = 0;
        singleClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CameraMotion.GetBool("Water") && !waterActive)
            ActivateWater();
        else if (!m_CameraMotion.GetBool("Water") && waterActive)
            DeactivateWater();

        if (m_CameraMotion.GetBool("PIP") && !pipActive)
            ActivatePIP();
        else if (!m_CameraMotion.GetBool("PIP") && pipActive)
            DeactivatePIP();

        UpdateText();

        if(singleClick)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > doubleClickTime)
                singleClick = false;
        }

        if(m_CameraMotion.GetInteger("State") == 0)
            m_TextBoxBackground.SetActive(true);
        else
            m_TextBoxBackground.SetActive(false);
    }

    public void Next()
    {
        //m_CameraMotion.SetBool("Next", true);

        if (m_CurrentState < m_States.Length - 1)
        {
            m_CurrentState++;
            m_CameraMotion.Play("Camera|Cam" + m_States[m_CurrentState], 0, 0f);

        }
    }

    public void Prev()
    {
        //m_CameraMotion.SetBool("Prev", true);

        //double click nested ifs
        if (singleClick)
            if (timeElapsed <= doubleClickTime)
                if (m_CurrentState > 0)
                    m_CurrentState--;

        singleClick = true;
        timeElapsed = 0f;

        m_CameraMotion.Play("Camera|Cam" + m_States[m_CurrentState], 0, 0f);
    }

    public void ActivateWater()
    {
        m_Water.Play();
        waterActive = true;
    }

    public void DeactivateWater()
    {
        m_Water.Stop();
        waterActive = false;
    }

    private void ActivatePIP()
    {
        m_PIPCam.depth = 0;
        m_PIP.Play("Picture in Picture Animation", 0, 0f);
        pipActive = true;
    }

    private void DeactivatePIP()
    {
        m_PIPCam.depth = -1;
        pipActive = false;
    }

    public void UpdateText()
    {
        m_TextBox.sprite = m_TextSprites[m_CameraMotion.GetInteger("State")];
    }

    public void Reset()
    {
        m_CameraMotion.Play("Camera|Cam" + m_States[0], 0, 0f);

        m_CurrentState = 0;
        singleClick = false;
    }
}


/*
private void Update()
{
    if (Input.GetKeyDown(KeyCode.F))
    {
        StartCoroutine(Lerp_MeshRenderer_Color(value, value, value, value));
    }
}
private IEnumerator Lerp_MeshRenderer_Color(MeshRenderer target_MeshRender, float lerpDuration, Color startLerp, Color targetLerp)
{
    float lerpStart_Time = Time.time;
    float lerpProgress;
    bool lerping = true;
    while (lerping)
    {
        yield return new WaitForEndOfFrame();
        lerpProgress = Time.time - lerpStart_Time;
        if (target_MeshRender != null)
        {
            target_MeshRender.material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
        }
        else
        {
            lerping = false;
        }


        if (lerpProgress >= lerpDuration)
        {
            lerping = false;
        }
    }
    yield break;
}

    */