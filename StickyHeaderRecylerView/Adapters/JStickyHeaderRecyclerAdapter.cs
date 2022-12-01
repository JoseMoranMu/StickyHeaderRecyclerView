using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using JStickyHeaderRecylerView.ItemDecoration;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace JStickyHeaderRecylerView.Adapters
{
    public abstract class JStickyHeaderRecyclerAdapter<S, T> : Adapter, IJStickyHeaderRecyclerAdapter
    {
        public int HeaderLayoutResourceId { get; set; }
        public JRecyclerSectionItemDecoration<S> ItemDecoration { get; set; }


        private IEnumerable<IGrouping<S, T>> _itemSource;
        internal IEnumerable<object> UniqueItemSource;
        protected int HeaderViewType = int.MaxValue;

        public Type HeaderItemType => typeof(S);
        public Type ItemType => typeof(T);

        public JStickyHeaderRecyclerAdapter()
        {
        }
        public sealed override int ItemCount => UniqueItemSource?.Count() ?? 0;

        public IEnumerable<IGrouping<S, T>> ItemSource
        {
            get => _itemSource;
            set
            {
                _itemSource = value;
                var list = ConvertToUniqueList(value);
                UniqueItemSource = list;
                if (ItemDecoration == null)
                    return;
                ItemDecoration.Source = list;
            }
        }

        private static List<object> ConvertToUniqueList(
            IEnumerable<IGrouping<S, T>> value)
        {
            var list = new List<object>();

            foreach (var element in value)
            {
                list.Add(element.Key);
                list.AddRange(element.ToList().Cast<object>());
            }

            return list;
        }

        public sealed override int GetItemViewType(int position)
        {
            return UniqueItemSource.ElementAt(position) is S ? HeaderViewType : GetViewTypeForItem(position);
        }

        public virtual int GetViewTypeForItem(int position)
        {
            return 1;
        }

        public sealed override void OnBindViewHolder(ViewHolder holder, int position)
        {
            if (UniqueItemSource.ElementAt(position) is S)
            {
                OnBindHeader(holder, (S)UniqueItemSource.ElementAt(position));
            }
            else
                OnBindItem(holder, (T)UniqueItemSource.ElementAt(position), position);
        }

        public sealed override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);

            if (inflater == null) throw new Exception("No infalter View");

            return viewType == HeaderViewType
                ? OnCreateHeaderViewHolder(inflater.Inflate(HeaderLayoutResourceId, parent, false))
                : OnCreateItemViewHolder(inflater, parent, viewType);
        }

        public abstract void OnBindHeader(ViewHolder holder, S headerItem);

        public abstract ViewHolder OnCreateHeaderViewHolder(View headerView);

        public abstract ViewHolder OnCreateItemViewHolder(LayoutInflater inflater, ViewGroup parent,
            int viewType);

        public abstract void OnBindItem(ViewHolder holder, T item, int position);
    }
}