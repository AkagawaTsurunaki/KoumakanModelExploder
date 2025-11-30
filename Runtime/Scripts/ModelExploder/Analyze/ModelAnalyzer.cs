// -----------------------------------------------------------------------
//  <copyright file="ModelAnalyzer.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------

using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder.Analyze
{
    public class ModelAnalyzer
    {
        private readonly Transform _root;
        public ModelNode ModelTree { get; private set; }

        public ModelAnalyzer(Transform root)
        {
            _root = root;
        }

        public void Analyze()
        {
            ModelTree = BuildTree(_root.transform, 1);
        }

        private ModelNode BuildTree(Transform parent, int depth)
        {
            if (parent == null) return null;
            var node = new ModelNode(parent.transform)
            {
                Bounds = GetWorldBounds(parent.transform),
                Depth = depth,
                OriginalPosition = new Vector3(
                    parent.position.x,
                    parent.position.y,
                    parent.position.z
                )
            };
            depth += 1;
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                node.AddChild(BuildTree(child, depth));
            }

            return node;
        }

        public static Bounds GetWorldBounds(Transform transform)
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            foreach (Renderer r in transform.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(r.bounds);
            }

            return bounds;
        }
    }
}