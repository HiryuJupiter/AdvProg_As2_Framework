using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Visualization
{
    public class NodeFactory : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private Node   prefab;
        [SerializeField] private Color  startColor   = Color.red;
        [SerializeField] private Color  endColor     = Color.green;

        private int     nodeCount;
        private float   scaleStep;

        /// <summary>
        /// Create a sorted sequence 
        /// </summary>
        public Node[] BuildSequence (int length)
        {
            //Cache calculated values
            this.nodeCount = length;
            scaleStep = canvasRectTransform.rect.height / length;

            //Create an array sequence and then create a node for each index
            Node[] seq = new Node[length];
            for (int i = 0; i < length; i++)
            {
                seq[i] = CreateNode(i);
            }
            return seq;
        }

        /// <summary>
        /// Create a node
        /// </summary>
        private Node CreateNode(int index)
        {
            Node node = Instantiate(prefab, transform);
            node.gameObject.name = $"Node [{index}]";

            Color color = Color.Lerp(startColor, endColor, (float)index / nodeCount);
            float height = scaleStep * (index + 1);
            node.Initialize(index, height, color);
            return node;
        }
    }
}