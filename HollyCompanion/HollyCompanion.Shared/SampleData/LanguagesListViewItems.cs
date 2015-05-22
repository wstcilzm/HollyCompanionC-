using System;
using System.Collections.Generic;
using System.Text;
using Expression.Blend.SampleData.SampleDataSource;
using HolyServiceDemo.Common;

namespace HolyServiceDemo.SampleData
{
    public class LanguagesListViewItems:AbstractListViewDataSource
    {
        public override void SetListViewItems()
        {
            var topUserLanguages = Windows.Globalization.ApplicationLanguages.ManifestLanguages;
            foreach (var str in topUserLanguages)
            {
                item = new Item();
                item.Title = Utilities.LanguageTagToText(str);
                if (item.Title != String.Empty)
                {
                    Collection.Add(item);
                }
            }      
        }
    }
}
