// -----------------------------------------------------------------------
//  <copyright file="ModelNode.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder.Analyze
{
    public class ModelNode
    {
        public readonly Transform Transform;
        public Vector3 TargetPosition { get; set; } = Vector3.positiveInfinity;
        public Vector3 OriginalPosition { get; set; }
        public readonly List<ModelNode> Children = new();
        public Bounds Bounds { get; set; }
        public Vector3 TargetBoundsCenter { get; set; }
        public int Depth { get; set; }

        public ModelNode(Transform transform)
        {
            Transform = transform;
        }

        public ModelNode(Transform transform, List<ModelNode> children)
        {
            Transform = transform;
            Children = children;
        }

        public void AddChild(ModelNode child)
        {
            if (child != null)
                Children.Add(child);
        }
    }
}