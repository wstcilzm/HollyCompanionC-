using System;
using System.Collections.Generic;
using System.Text;

namespace HollyCompanion.Data
{
    /// <summary>
    /// Publisher
    /// </summary>
    public class Item:System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged; //define event,binding to delegate

        protected virtual void OnPropertyChanged(string propertyName) //trigger event
        {
            if(this.PropertyChanged!=null)
            {
                this.PropertyChanged(this,new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private string title=string.Empty;
        public string Titile
        {
            get{return this.title;}
            set{this.title=value;}
        }
    }


    // Workaround: data binding works best with an enumeration of objects that does not implement IList
    public class ItemCollection : IEnumerable<Object>
    {
        private System.Collections.ObjectModel.ObservableCollection<Item> itemCollection = new System.Collections.ObjectModel.ObservableCollection<Item>();

        public IEnumerator<Object> GetEnumerator()
        {
            return itemCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Item item)
        {
            itemCollection.Add(item);
        }
    }
}
