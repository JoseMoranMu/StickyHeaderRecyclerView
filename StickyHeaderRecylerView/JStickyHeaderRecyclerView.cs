using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using AndroidX.RecyclerView.Widget;
using JStickyHeaderRecylerView.Adapters;
using JStickyHeaderRecylerView.ItemDecoration;

namespace JStickyHeaderRecylerView
{
    public class JJStickyHeaderRecyclerView : RecyclerView
    {

        private int _headerLayoutResourceId;
        public int HeaderLayoutResourceId
        {
            get => _headerLayoutResourceId;
            set
            {
                _headerLayoutResourceId = value;
                if (StickyAdapter != null)
                {
                    StickyAdapter.HeaderLayoutResourceId = value;
                }

                if (JItemDecoration != null)
                {
                    JItemDecoration.HeaderLayoutResourceId = value;
                }
            }
        }

        internal IJStickyHeaderRecyclerAdapter StickyAdapter => GetAdapter() as IJStickyHeaderRecyclerAdapter;
        internal IJRecyclerSectionItemDecoration JItemDecoration { get; set; }
        public JJStickyHeaderRecyclerView(Context context) : base(context)
        {
        }

        public JJStickyHeaderRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public JJStickyHeaderRecyclerView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        protected JJStickyHeaderRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public void SetAdapter<S,T>(JStickyHeaderRecyclerAdapter<S, T> adapter, JRecyclerSectionItemDecoration<S> itemDecoration)
        {
            AddItemDecoration(itemDecoration);
            adapter.ItemDecoration = itemDecoration;
            adapter.HeaderLayoutResourceId = HeaderLayoutResourceId;
            itemDecoration.HeaderLayoutResourceId = HeaderLayoutResourceId;
            itemDecoration.Source = adapter.UniqueItemSource;
            base.SetAdapter(adapter);
        }

        public sealed override void SetAdapter(Adapter adapter)
        {
            throw
                new Exception("You should call " +
                "SetAdapter<S,T>(StickyHeaderRecyclerAdapter<S, T> adapter, RecyclerSectionItemDecoration<T> itemDecoration)" +
                " in order tu use StickyHeader");
        }
    }
}

