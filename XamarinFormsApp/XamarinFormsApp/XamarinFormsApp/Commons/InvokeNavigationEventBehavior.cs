using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsApp.Commons
{
    public class InvokeNavigationEventBehavior : Behavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Appearing += this.Bindable_Appearing;
            bindable.Disappearing += this.Bindable_Disappearing;
        }

        private void Bindable_Appearing(object sender, EventArgs e)
        {
            (((Page)sender).BindingContext as INavigationEventAware)?.Appearing();
        }

        private void Bindable_Disappearing(object sender, EventArgs e)
        {
            (((Page)sender).BindingContext as INavigationEventAware)?.Disappearing();
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Disappearing -= this.Bindable_Disappearing;
            bindable.Appearing -= this.Bindable_Appearing;
        }
    }

    public interface INavigationEventAware
    {
        void Appearing();
        void Disappearing();
    }
}
