// -----------------------------------------------------------------------
//  <copyright file="Exploder.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using KoumakanModelExploder.Scripts.ModelExploder.Analyze;
using KoumakanModelExploder.Scripts.ModelExploder.Layout;
using KoumakanModelExploder.Scripts.ModelExploder.Util;
using UnityEngine;

namespace KoumakanModelExploder.Scripts.ModelExploder
{
    public class MoveAnim
    {
        private MonoBehaviour _coroutineHolder;
        public MoveAnim(MonoBehaviour coroutineHolder)
        {
            _coroutineHolder = coroutineHolder;
        }
        
        public Coroutine DoMove(Transform tr, Vector3 end, float dur,
            System.Action onComplete = null)
        {
            return _coroutineHolder.StartCoroutine(_DoMove(tr, end, dur, onComplete));
        }

        private static IEnumerator _DoMove(Transform tr, Vector3 end, float dur,
            System.Action onComplete)
        {
            Vector3 start = tr.position;
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / dur;
                tr.position = Vector3.Lerp(start, end, EaseOutQuart(t));
                yield return null;
            }

            tr.position = end;
            onComplete?.Invoke();
        }

        private static float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
    }

    public class Exploder : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private ModelNode _root;
        private List<ModelNode> _nodes;
        private AxisLayout _layout;
        [SerializeField] private float padding = 10;

        private ModelNode GetTree()
        {
            var analyzer = new ModelAnalyzer(target);
            analyzer.Analyze();
            _root = analyzer.ModelTree;
            return _root;
        }

        public void PrintTree()
        {
            ModelTreePrinter.PrintTree(GetTree());
        }

        [SerializeField] private int layerDepth = 4;
        [SerializeField] private AxisLayoutDirection layoutDirection = AxisLayoutDirection.Z;
        [SerializeField] private SortMagnitude sortBy = SortMagnitude.X;

        public void CalculateLayout()
        {
            var root = GetTree();
            _nodes = ModelNodeSelector.SelectSameLayerNodes(root, layerDepth);
            _layout = new AxisLayout(_nodes, padding);
            _layout.SortBy(sortBy);
            _layout.LayoutBy(layoutDirection);
        }

        private bool _exploded;
        [SerializeField] private float duration = 1;

        public void ApplyAnimation()
        {
            var anim = new MoveAnim(this);
            foreach (var node in _layout.Nodes)
            {
                // node.Transform.position = node.TargetPosition;
                anim.DoMove(node.Transform, node.TargetPosition, duration);
            }

            _exploded = true;
        }

        public void RevertAnimation()
        {
            var anim = new MoveAnim(this);
            foreach (var node in _layout.Nodes)
            {
                // node.Transform.position = node.OriginalPosition;
                anim.DoMove(node.Transform, node.OriginalPosition, duration);
            }

            _exploded = false;
        }

        private void OnDrawGizmos()
        {
            DrawSelectedModelsBounds();
            DrawTargetExplodedModelsBounds();
            if (_exploded)
            {
                DrawExplodedModelBounds();
            }
        }

        private void DrawSelectedModelsBounds()
        {
            if (_nodes == null) return;
            Gizmos.color = Color.cyan;
            foreach (var n in _nodes)
            {
                Gizmos.DrawWireCube(n.Bounds.center, n.Bounds.size);
            }
        }

        private void DrawTargetExplodedModelsBounds()
        {
            if (_layout?.Nodes == null) return;
            Gizmos.color = Color.yellow;
            for (int i = 0; i < _layout.Nodes.Count; i++)
            {
                var center = _layout.TargetBoundsCenterList[i];
                var node = _nodes[i];
                Gizmos.DrawWireCube(center, node.Bounds.size);
            }
        }

        private void DrawExplodedModelBounds()
        {
            if (_layout.Nodes == null) return;
            Gizmos.color = Color.green;
            foreach (var n in _layout.Nodes)
            {
                var bounds = ModelAnalyzer.GetWorldBounds(n.Transform);
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
    }
}