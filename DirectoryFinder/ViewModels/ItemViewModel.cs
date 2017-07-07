using System.Collections.Generic;
using System.Linq;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Data;
using HumanBytes;

namespace DirectoryFinder.ViewModels
{
    public class ItemViewModel : ViewModel<Item>
    {
        public ItemViewModel(Item value)
            : base(value)
        {
            if (this.Value.Items == null)
                return;

            this.Items = this.Value.Items.Select(o => new ItemViewModel(o)).ToArray();
        }

        public string ReadableSize
        {
            get
            {
                return this.Value.Size.Bytes().ToString();
            }
        }

        public IEnumerable<ItemViewModel> Items
        {
            get;
        }
    }
}