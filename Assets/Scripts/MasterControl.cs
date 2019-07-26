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

    private bool waterActive;

    // Start is called before the first frame update
    void Start()
    {
        m_Water.Stop();
        waterActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CameraMotion.GetBool("Water") && !waterActive)
            ActivateWater();
        else if (!m_CameraMotion.GetBool("Water") && waterActive)
            DeactivateWater();

        UpdateText();
    }

    public void Next()
    {
        m_CameraMotion.SetBool("Next", true);
    }

    public void Prev()
    {
        m_CameraMotion.SetBool("Prev", true);
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
        m_CameraMotion.Play("Camera_1", 0, 0f);
    }
}
