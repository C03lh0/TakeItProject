using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Services.Commands;
using Windows.UI.Xaml.Controls;

namespace TakeIt.UWP.Services
{
    public class DialogService
    {
        public Command ExitePageCommand { get; private set; }
        public DialogService(Action exitPage)
        {
            ExitePageCommand = new Command(exitPage);
        }

        public async void DispalyMessageErroDialog(string message)
        {
            ContentDialog messageErroDialog = new ContentDialog
            {
                Title = "Status da Operação",
                Content = message,
                CloseButtonText = "Tente Novamente",
                DefaultButton = ContentDialogButton.Close
            };
            await messageErroDialog.ShowAsync();
        }

        public async void DispalyMessageSuccessfullyDialog(string message)
        {
            ContentDialog messageSuccessfullyDialog = new ContentDialog
            {
                Title = "Status da Operação",
                Content = message,
                CloseButtonText = "Ok",
                CloseButtonCommand = ExitePageCommand,
                DefaultButton = ContentDialogButton.Close
            };
            await messageSuccessfullyDialog.ShowAsync();
        }

        public async Task<ContentDialogResult> DisplayConfirmationDialog(string message)
        {
            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Confirmação de Operação",
                Content = message,
                PrimaryButtonText = "Sim",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await confirmationDialog.ShowAsync();
            return result;
        }

        public async void DispalyMessageReturnAlertDialog(string message)
        {
            ContentDialog messageErroDialog = new ContentDialog
            {
                Title = "Alerta De Devolução",
                Content = message,
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close
            };
            await messageErroDialog.ShowAsync();
        }
    }
}
