using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsApp.Views
{
    public class DisposeBindableContextBehavior : Behavior<Page>
    {
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            ((Page)bindable).Disappearing += this.DisposeBindableContextBehavior_Disappearing;
        }

        private void DisposeBindableContextBehavior_Disappearing(object sender, EventArgs e)
        {
            (((Page)sender).BindingContext as IDisposable)?.Dispose();
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            ((Page)bindable).Disappearing -= this.DisposeBindableContextBehavior_Disappearing;
        }
    }
}
