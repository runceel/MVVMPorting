using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MVVMApp.UWP.Commons
{
    public class ItemClickInvokeCommandAction : DependencyObject, IAction
    {

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemClickInvokeCommandAction), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object Execute(object sender, object parameter)
        {
            var e = parameter as ItemClickEventArgs;
            if (e == null) { return null; }

            this.Command?.Execute(e.ClickedItem);
            return null;
        }
    }
}
