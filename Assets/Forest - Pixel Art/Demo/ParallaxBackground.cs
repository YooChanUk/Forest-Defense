using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] private bool       m_isParallax = true;
    [SerializeField] private bool       m_isEndless = true;
    [SerializeField] private float      m_backgroundWidth;
    [SerializeField] private float      m_parallaxSpeed;

    private float                       m_viewZone;
    private Transform                   m_cameraTransform;
    private Transform[]                 m_layers;
    private int                         m_leftIndex;
    private int                         m_rightIndex;
    private float                       m_lastCameraX;

	// Use this for initialization
	void Start () {
        m_cameraTransform = Camera.main.transform;
        m_lastCameraX = m_cameraTransform.position.x;
        m_layers = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) {
            m_layers[i] = transform.GetChild(i);
        }

        m_leftIndex = 0;
        m_rightIndex = m_layers.Length - 1;

        m_viewZone = Camera.main.orthographicSize * Camera.main.aspect;

        //Prevent stuttering
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isParallax) {
            float deltaX = m_cameraTransform.position.x - m_lastCameraX;
            float newX = transform.position.x + deltaX * m_parallaxSpeed;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            m_lastCameraX = m_cameraTransform.position.x;
        }
        
        // Check if the camera is near the edge of the background sprites
        if(m_isEndless)
        {
            if (m_cameraTransform.position.x < (m_layers[m_leftIndex].position.x + m_viewZone - (m_backgroundWidth / 2.0f))) {
                ScrollLeft();
            }
                
            else if (m_cameraTransform.position.x > (m_layers[m_rightIndex].position.x - m_viewZone + (m_backgroundWidth / 2.0f))) {
                ScrollRight();
            }
        }
    }

    // Move background sprites to the left
    private void ScrollLeft()
    {
        float newX = m_layers[m_leftIndex].localPosition.x - m_backgroundWidth;
        m_layers[m_rightIndex].localPosition = new Vector3(newX, m_layers[m_rightIndex].localPosition.y, m_layers[m_rightIndex].localPosition.z);
        m_leftIndex = m_rightIndex;
        m_rightIndex--;
        if(m_rightIndex < 0)
            m_rightIndex = m_layers.Length - 1;
    }

    // Move background sprites to the right
    private void ScrollRight()
    {
        float newX = m_layers[m_rightIndex].localPosition.x + m_backgroundWidth;
        m_layers[m_leftIndex].localPosition = new Vector3(newX, m_layers[m_leftIndex].localPosition.y, m_layers[m_leftIndex].localPosition.z);
        m_rightIndex = m_leftIndex;
        m_leftIndex++;
        if (m_leftIndex == m_layers.Length)
            m_leftIndex = 0;
    }
}
