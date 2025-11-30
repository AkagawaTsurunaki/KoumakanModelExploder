// -----------------------------------------------------------------------
//  <copyright file="AxisLayout.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using KoumakanModelExploder.Scripts.ModelExploder.Analyze;
using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder.Layout
{
    public enum AxisLayoutDirection
    {
        X,
        Y,
        Z
    }

    public class AxisLayout : Layout
    {
        public AxisLayout(List<ModelNode> nodes, float padding = 10) : base(nodes)
        {
            _padding = padding;
        }

        private readonly float _padding;
        public readonly List<Vector3> TargetBoundsCenterList = new();
        private float _lastZAxis;

        public void ZAxisLayout()
        {
            foreach (var node in Nodes)
            {
                var targetBoundsCenter = new Vector3(node.Bounds.extents.x, node.Bounds.extents.y,
                    _lastZAxis + node.Bounds.extents.z);
                _lastZAxis += _padding + node.Bounds.size.z;
                node.TargetBoundsCenter = targetBoundsCenter;
                TargetBoundsCenterList.Add(targetBoundsCenter);
                var offset = targetBoundsCenter - node.Bounds.center;
                node.TargetPosition = node.Transform.position + offset;
            }
        }

        private float _lastYAxis;

        public void YAxisLayout()
        {
            foreach (var node in Nodes)
            {
                var targetBoundsCenter = new Vector3(node.Bounds.extents.x, _lastYAxis + node.Bounds.extents.y,
                    node.Bounds.extents.z);
                _lastYAxis += _padding + node.Bounds.size.y;
                node.TargetBoundsCenter = targetBoundsCenter;
                TargetBoundsCenterList.Add(targetBoundsCenter);
                var offset = targetBoundsCenter - node.Bounds.center;
                node.TargetPosition = node.Transform.position + offset;
            }
        }

        private float _lastXAxis;

        public void XAxisLayout()
        {
            foreach (var node in Nodes)
            {
                var targetBoundsCenter = new Vector3(_lastXAxis + node.Bounds.extents.x, node.Bounds.extents.y,
                    node.Bounds.extents.z);
                _lastXAxis += _padding + node.Bounds.size.x;
                node.TargetBoundsCenter = targetBoundsCenter;
                TargetBoundsCenterList.Add(targetBoundsCenter);
                var offset = targetBoundsCenter - node.Bounds.center;
                node.TargetPosition = node.Transform.position + offset;
            }
        }

        public void LayoutBy(AxisLayoutDirection direction)
        {
            switch (direction)
            {
                case AxisLayoutDirection.X:
                    XAxisLayout();
                    break;
                case AxisLayoutDirection.Y:
                    YAxisLayout();
                    break;
                case AxisLayoutDirection.Z:
                    ZAxisLayout();
                    break;
            }
        }
    }
}