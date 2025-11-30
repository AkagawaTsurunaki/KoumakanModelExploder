// -----------------------------------------------------------------------
//  <copyright file="ModelTreePrinter.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using KoumakanModelExploder.Scripts.ModelExploder.Analyze;
using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder.Util
{
    public static class ModelTreePrinter
    {
        private static string GetPrefix(int depth, bool last)
        {
            return new string(' ', depth * 2) + (last ? "└" : "├");
        }

        private static void _PrintTree(ModelNode node, int maxDepth = 100)
        {
            if (maxDepth < node.Depth) return;
            for (int i = 0; i < node.Children.Count; i++)
            {
                var child = node.Children[i];
                bool isLast = i == node.Children.Count - 1;
                Debug.Log($"{GetPrefix(child.Depth, isLast)} [{child.Depth}] {child.Transform.name}");
                _PrintTree(child, maxDepth);
            }
        }

        public static void PrintTree(ModelNode node, int maxDepth = 100)
        {
            Debug.Log($"* [{node.Depth}] {node.Transform.name}");
            _PrintTree(node, maxDepth);
        }

        public static void PrintNodes(List<ModelNode> nodes, int maxDepth = 0)
        {
            foreach (var n in nodes)
                PrintTree(n, maxDepth);
        }
    }
}