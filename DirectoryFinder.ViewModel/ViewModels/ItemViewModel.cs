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
            if (this.Value.Items != null)
            {
                this.Items = this.Value.Items.Select(o => new ItemViewModel(o)).ToArray();
            }

            this.ReadableSize = this.Value.Size.Bytes().ToString();
            this.Attributes = string.Join(", ", this.Value.Attributes);
            this.Rights = string.Join("\n", this.Value.UserRights);
        }

        public string ReadableSize
        {
            get;
            private set;
        }

        public IEnumerable<ItemViewModel> Items
        {
            get;
            private set;
        }

        public string Attributes
        {
            get;
            private set;
        }

        public string Rights
        {
            get;
            private set;
        }
    }
}