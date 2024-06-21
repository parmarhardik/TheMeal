using System;
using Microsoft.Maui.Controls;
namespace TheMeal.Behavior
{
    public class InitializeViewModelBehavior : Behavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            if (bindable.BindingContext is IQueryAttributable viewModel)
            {
                var queries = new Dictionary<string, object>();
                string query = Shell.Current.CurrentState.Location?.Query;
                queries.Add("", query);
                if (query != null)
                {
                    viewModel.ApplyQueryAttributes(queries);
                }
            }
        }
    }
}

