using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Java.Util.Zip;
using JStickyHeaderRecylerView;
using JStickyHeaderRecylerView.Adapters;
using JStickyHeaderRecylerView.ItemDecoration;
using StickyHeaderRecyclerView.Sample;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace JStickyHeaderRecyclerView.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var view = FindViewById<JJStickyHeaderRecyclerView>(Resource.Id.Jrecycler);
            view.HeaderLayoutResourceId = Resource.Layout.header;
            view.SetLayoutManager(new LinearLayoutManager(this));
            var adapter = new CustomAdapter();
            var list = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    ItemName = "1"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "2"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "3"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "4"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "5"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "6"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "7"
                },
                new Item()
                {
                    Id = 1,
                    ItemName = "8"
                },
                new Item()
                {
                    Id = 2,
                    ItemName = "1"
                },
                new Item()
                {
                    Id = 2,
                    ItemName = "2"
                },
                new Item()
                {
                    Id = 2,
                    ItemName = "3"
                },
                new Item()
                {
                    Id = 2,
                    ItemName = "4"
                },
                new Item()
                {
                    Id = 2,
                    ItemName = "5"
                },
                new Item()
                {
                    Id = 3,
                    ItemName = "1"
                },
                new Item()
                {
                    Id = 3,
                    ItemName = "2"
                },
                    new Item()
                {
                    Id = 4,
                    ItemName = "1"
                },
                new Item()
                {
                    Id = 4,
                    ItemName = "2"
                },
                new Item()
                {
                    Id = 4,
                    ItemName = "3"
                },
                new Item()
                {
                    Id = 5,
                    ItemName = "4"
                },
                new Item()
                {
                    Id = 6,
                    ItemName = "5"
                },
            };
            adapter.ItemSource = list.GroupBy(i => i.Id);
            view.SetAdapter(adapter, new ItemDecorationCuston());
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
    }

    public class ItemDecorationCuston : JRecyclerSectionItemDecoration<int>
    {
        public override void BindHeader(View view, int item)
        {
            var headerItemText = view.FindViewById<TextView>(Resource.Id.headerText);
            headerItemText.Text = $"Header {item}";
        }
    }

    public class CustomAdapter : JStickyHeaderRecyclerAdapter<int, Item>
    {
        public override void OnBindHeader(RecyclerView.ViewHolder holder, int headerItem)
        {
            var viewHolder = holder as HeaderViewHolder;
            viewHolder.HeaderItemText.Text = $"Header {headerItem}";
        }

        public override void OnBindItem(RecyclerView.ViewHolder holder, Item item, int position)
        {
            var viewHolder = holder as ItemViewHolder;
            viewHolder.ItemText.Text = $"Item {item.ItemName}";
        }

        public override RecyclerView.ViewHolder OnCreateHeaderViewHolder(View headerView)
        {
            return new HeaderViewHolder(headerView);
        }

        public override RecyclerView.ViewHolder OnCreateItemViewHolder(LayoutInflater inflater, ViewGroup parent, int viewType)
        {
            var view =
                inflater.Inflate(Resource.Layout.item, parent, false);

            return new ItemViewHolder(view);
        }
    }

    public class ItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView ItemText { get; private set; }

        public ItemViewHolder(View itemView) : base(itemView)
        {
            ItemText = itemView.FindViewById<TextView>(Resource.Id.itemText);
        }
    }

    public class HeaderViewHolder : RecyclerView.ViewHolder
    {
        public TextView HeaderItemText { get; private set; }

        public HeaderViewHolder(View itemView) : base(itemView)
        {
            HeaderItemText = itemView.FindViewById<TextView>(Resource.Id.headerText);

        }

    }

}
