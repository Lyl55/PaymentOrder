using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.ViewModels;

namespace PaymentOrder.AdminPanel.Commands
{
    public class RejectCommand<T> : BaseCommand where T : BaseModel, new()
    {
        private readonly BaseControlViewModel<T> _viewModel;
        public RejectCommand(BaseControlViewModel<T> viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.SelectedValue = null;
        }
    }
}