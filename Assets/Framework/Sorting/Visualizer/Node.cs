using System;
using UnityEngine;
using UnityEngine.UI;

//A node inbar
namespace Sorting.Visualization
{
    /// <summary>
    /// Controls a visualization node
    /// </summary>
    [RequireComponent(typeof(LayoutElement), typeof(Image))]
    public class Node : MonoBehaviour , IComparable
    {
        LayoutElement layout;
        Image image;
        Color startColor;
        
        public int Value { get; private set; }
        float height;


        /// <summary>
        /// Create a visualizer node
        /// </summary>
        /// <param name="value"> The position of the node within the sorted sequence </param>
        /// <param name="height"> The height-size of the visualizer node </param>
        /// <param name="color"> The color of the visualizer node in the sorted sequence </param>
        public void Initialize(int value, float height, Color color)
        {
            //Cache
            startColor = color;
            Value = value;
            this.height = height;

            //References
            layout = gameObject.GetComponent<LayoutElement>();
            image = gameObject.GetComponent<Image>();

            //Initialize attributes
            SetHeight(height);
            SetColor(color);
        }

        /// <summary>
        /// Highlight the node and set it to blue
        /// </summary>
        public void SetSelectedBlue(bool isSelected)
        {
            SetColor(isSelected ? Color.blue : startColor);
        }

        /// <summary>
        /// Highlight the node and set it to red
        /// </summary>
        public void SetSelectedRed (bool isSelected)
        {
            SetColor(isSelected ? Color.red : startColor);
        }

        //Expression body methods to help writing self documenting code

        /// <summary>
        /// Set node height
        /// </summary>
        void SetHeight (float height) => layout.preferredHeight = height;

        /// <summary>
        /// Set node color
        /// </summary>
        void SetColor (Color color) => image.color = color;

        /// <summary>
        /// Custom iComparator
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Node other = (Node)obj;
            if (Value > other.Value)
                return 1;
            if (Value < other.Value)
                return -1;
            else
                return 0;
        }
    }
}