// -----------------------------------------------------------------------
//  <copyright file="Layout.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using KoumakanModelExploder.Scripts.ModelExploder.Analyze;
using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder.Layout
{
    public enum SortMagnitude
    {
        XZ,
        X,
        Z
    }

    public class Layout
    {
        protected Layout(List<ModelNode> nodes)
        {
            Nodes = nodes;
        }

        public readonly List<ModelNode> Nodes;

        public void SortByXZMagnitude()
        {
            Nodes.Sort((a, b) =>
            {
                var diagA = new Vector2(a.Bounds.size.x, a.Bounds.size.z).magnitude;
                var diagB = new Vector2(b.Bounds.size.x, b.Bounds.size.z).magnitude;
                return diagB.CompareTo(diagA);
            });
        }

        public void SortByXMagnitude()
        {
            Nodes.Sort((a, b) =>
            {
                var aX = Math.Abs(a.Bounds.size.x);
                var bX = Math.Abs(b.Bounds.size.x);
                return bX.CompareTo(aX);
            });
        }

        public void SortByZMagnitude()
        {
            Nodes.Sort((a, b) =>
            {
                var aX = Math.Abs(a.Bounds.size.z);
                var bX = Math.Abs(b.Bounds.size.z);
                return bX.CompareTo(aX);
            });
        }

        public void SortBy(SortMagnitude sm)
        {
            switch (sm)
            {
                case SortMagnitude.X:
                    SortByXMagnitude();
                    break;
                case SortMagnitude.Z:
                    SortByZMagnitude();
                    break;
                case SortMagnitude.XZ:
                    SortByXZMagnitude();
                    break;
            }
        }
    }
}