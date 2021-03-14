using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class Prisms : MonoBehaviour, IMineable, IPrism
    {
        [SerializeField] Material material;
        Color startColor;
        Color fadeOutColor = Color.clear;
        float timer;

        public void Charge(Color color)
        {
            material.color = color;
            fadeOutColor = color;
            fadeOutColor.a = 0f;
            timer = 1.2f;
        }

        public void Mine(float amount)
        {
        }

        void Start()
        {
            material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            timer -= Time.deltaTime;
            material.color = Color.Lerp(fadeOutColor, material.color, timer);
        }
    }

    public interface IPrism
    {
        void Charge(Color color);
    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class Prisms : MonoBehaviour, IMineable, IPrism
    {
        Renderer renderer;
        Color startColor;
        Color fadeOutColor = Color.clear;
        float timer;

        public void Charge(Color color)
        {
            renderer.material.color = color;
            fadeOutColor = color;
            fadeOutColor.a = 0f;
            timer = 1.2f;
        }

        public void Mine(float amount)
        {
        }

        void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        void Update()
        {
            timer -= Time.deltaTime;
            renderer.material.color = Color.Lerp(renderer.material.color, fadeOutColor, timer);
        }
    }

    public interface IPrism
    {
        void Charge(Color color);
    }

}

 */