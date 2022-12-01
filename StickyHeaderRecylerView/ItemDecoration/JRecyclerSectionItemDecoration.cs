using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Android.Graphics;
using Android.Systems;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using JStickyHeaderRecylerView.Adapters;

namespace JStickyHeaderRecylerView.ItemDecoration
{
    public abstract class JRecyclerSectionItemDecoration<T> : RecyclerView.ItemDecoration, IJRecyclerSectionItemDecoration
    {
        private View _headerView;
        public IEnumerable<object> Source { get; set; }
        public int HeaderLayoutResourceId { get; set; }

        public JRecyclerSectionItemDecoration()
        {
        }

        public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDrawOver(c, parent, state);

            if (_headerView == null)
                _headerView = InflateHeaderView(parent);

            var previousHeader = default(T);

            for (var i = 0; i < parent.ChildCount; i++)
            {
                var child = parent.GetChildAt(i);
                var position = parent.GetChildAdapterPosition(child);

                var item = SectionHeader(position);
                if (previousHeader != null && previousHeader.Equals(item) &&
                    !IsSection(position)) continue;

                BindHeader(_headerView, item);
                FixLayoutSize(_headerView, parent);
                DrawHeader(c, child, _headerView, parent, position);
                previousHeader = item;
            }
        }

        private void DrawHeader(Canvas c, View child, View headerView, RecyclerView parent, int position)
        {
            if (child.Top > headerView.Height)
                return;

            c.Save();
            c.Translate(0, Math.Max(0, child.Top));
            headerView.Draw(c);
            c.Restore();
        }

        private static void FixLayoutSize(View view, ViewGroup parent)
        {
            var widthSpec = View.MeasureSpec.MakeMeasureSpec(parent.Width, MeasureSpecMode.Exactly);
            var heightSpec = View.MeasureSpec.MakeMeasureSpec(parent.Height, MeasureSpecMode.Unspecified);

            var childWidth = ViewGroup.GetChildMeasureSpec(widthSpec, parent.PaddingLeft + parent.PaddingRight,
                view.LayoutParameters.Width);
            var childHeight = ViewGroup.GetChildMeasureSpec(heightSpec,
                parent.Top + parent.Bottom,
                view.LayoutParameters.Height);

            view.Measure(childWidth, childHeight);
            view.Layout(0, 0, view.MeasuredWidth, view.MeasuredHeight);
            view.RequestLayout();
        }

        private bool IsSection(int position)
        {
            return Source.ElementAt(position) is T;
        }

        private T SectionHeader(int position)
        {
            var header = default(T);

            for (var i = position; i >= 0; i--)
            {
                if (!(Source.ElementAt(i) is T)) continue;
                header = (T)Source.ElementAt(i);
                break;
            }

            return header;
        }

        private View InflateHeaderView(ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context)
                ?.Inflate(HeaderLayoutResourceId, parent, false);
            return view;
        }

        public abstract void BindHeader(View view, T item);
    }
}