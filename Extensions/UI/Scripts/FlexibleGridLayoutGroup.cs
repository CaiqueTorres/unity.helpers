using System;
using UnityEngine;
using UnityEngine.UI;

namespace homehelp.Extenders
{
    public class FlexibleGridLayoutGroup : LayoutGroup
    {
        private enum FitType
        {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns
        }

        private Rect _parentRect;
        private Vector2 _cellSize;

        private Transform _transform;
        private RectOffset _paddingRectOffset;

        [SerializeField] private FitType fitType;

        [Space] [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 spacing;

        [Space] [SerializeField] private bool fitX;
        [SerializeField] private bool fitY;

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (fitType.Equals(FitType.Width) || fitType.Equals(FitType.Height) || fitType.Equals(FitType.Uniform))
            {
                fitX = fitY = true;

                var sqrRt = Mathf.Sqrt(_transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }

            switch (fitType)
            {
                case FitType.Width:
                case FitType.FixedColumns:
                    rows = Mathf.CeilToInt((float) _transform.childCount / columns);
                    break;
                case FitType.Height:
                case FitType.FixedRows:
                    columns = Mathf.CeilToInt((float) _transform.childCount / rows);
                    break;
            }

            _parentRect = rectTransform.rect;
            var parentWidth = _parentRect.width;
            var parentHeight = _parentRect.height;

            _paddingRectOffset = padding;

            try
            {
                var cellWidth = parentWidth / columns - spacing.x / columns * 2 -
                                (_paddingRectOffset.left / columns - _paddingRectOffset.right / columns);
                var cellHeight = parentHeight / rows - spacing.y / rows * 2 -
                                 (_paddingRectOffset.top / rows - _paddingRectOffset.bottom / rows);

                _cellSize.x = fitX ? cellWidth : _cellSize.x;
                _cellSize.y = fitY ? cellHeight : _cellSize.y;
            }
            catch (DivideByZeroException exception)
            {
            }

            var rowCount = 0;
            var columnCount = 0;

            for (var i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = _cellSize.x * columnCount + spacing.x * columnCount + _paddingRectOffset.left;
                var yPos = _cellSize.y * rowCount + spacing.y * rowCount + _paddingRectOffset.top;

                SetChildAlongAxis(item, 0, xPos, _cellSize.x);
                SetChildAlongAxis(item, 1, yPos, _cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical() { }

        public override void SetLayoutHorizontal() { }

        public override void SetLayoutVertical() { }
    }
}
