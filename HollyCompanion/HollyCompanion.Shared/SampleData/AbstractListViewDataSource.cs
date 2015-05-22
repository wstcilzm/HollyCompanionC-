using System;
using System.Collections.Generic;
using System.Text;
using Expression.Blend.SampleData.SampleDataSource;

namespace HolyServiceDemo.SampleData
{
    public abstract class AbstractListViewDataSource
    {
        protected Item item;

        private ItemCollection _Collection = new ItemCollection();
        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }

        public abstract void SetListViewItems();
    }
}
