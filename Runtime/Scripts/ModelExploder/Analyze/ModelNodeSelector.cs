// -----------------------------------------------------------------------
//  <copyright file="ModelNodeSelector.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace KoumakanModelExploder.Scripts.ModelExploder.Analyze
{
    public static class ModelNodeSelector
    {
        public static List<ModelNode> SelectSameLayerNodes(ModelNode root, int layer)
        {
            List<ModelNode> layers = new();
            _SelectSameLayerNodes(layers, root, layer, 0);
            return layers;
        }

        private static void _SelectSameLayerNodes(List<ModelNode> result, ModelNode parent, int layer, int currentLayer)
        {
            if (currentLayer > layer || parent == null)
            {
                return;
            }

            if (currentLayer == layer)
            {
                result.Add(parent);
            }
            else if (currentLayer < layer)
            {
                currentLayer += 1;
                foreach (var child in parent.Children)
                {
                    _SelectSameLayerNodes(result, child, layer, currentLayer);
                }
            }
        }
    }
}