using System.Collections;
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

    private string[] m_States = {"Rotate_1", "Zoom_2", "Rotate_3", "Rotate_4", "Zoom_5", "Rotate_6", "Rotate_7", "Zoom_8", "Zoom_9", "Rotate_10", "Rotate_11", "Rotate_12", "Zoom_13", "End"};
    private int m_CurrentState;

    private bool waterActive;

    private bool singleClick;
    private float doubleClickTime = 0.5f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        m_Water.Stop();
        waterActive = false;

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

        UpdateText();

        if(singleClick)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > doubleClickTime)
                singleClick = false;
        }
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
