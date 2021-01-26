using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public interface IListItem
    {
        #region property

        string Display { get; }
        object RawValue { get; }

        #endregion
    }

    public class ListItem: IListItem
    {
        public ListItem(string display, object rawValue)
        {
            Display = display;
            RawValue = rawValue;
        }

        #region IListItem

        public string Display { get; }
        public object RawValue { get; }

        #endregion

        #region object

        public override string ToString() => Display;

        #endregion
    }

    public class ListItem<T>: ListItem
    {
        public ListItem(string display, T value)
            : base(display, value)
        { }

        #region property

        public T Value => (T)RawValue;

        #endregion
    }

    public class PlatformListItem: ListItem<string>
    {
        public PlatformListItem(string display, string value)
            : base(display, value)
        { }
    }
}
