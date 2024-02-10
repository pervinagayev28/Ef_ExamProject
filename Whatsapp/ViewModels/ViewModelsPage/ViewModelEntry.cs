using ChatAppDatabaseLibraryy.Contexts;
using ChatAppModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Whatsapp.Commands;
using Whatsapp.UnitOfWorks.BaseUnitOfWorks;
using Whatsapp.UnitOfWorks.Concrets;
using Whatsapp.Views.ViewPages;

namespace Whatsapp.ViewModels.ViewModelsPage
{
    public class ViewModelEntry
    {
        private List<string> gmails = new();
        public ICommand? LogInCommand { get; set; }
        public ICommand? RegistrationCommand { get; set; }
        public ICommand? CloseCommand { get; set; }
        private IUnitOfWork unitOfWork;
        public ViewModelEntry()
        {
            unitOfWork = new UnitOfWork();
            GetUserGmails();
            LogInCommand = new Command(ExecuteLogInCommand, CanExecuteLogInCommand);
            RegistrationCommand = new Command(ExecuteRegistrationCommand);
            CloseCommand = new Command(ExecuteCloseCommand);
        }

        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Page child)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(child);

                while (parent != null && !(parent is NavigationWindow))
                    parent = VisualTreeHelper.GetParent(parent);
                if (parent != null)
                    (parent as NavigationWindow)!.Close();
            }
        }

        private async void GetUserGmails()
        {
            gmails = await unitOfWork.GetRepository<User, int>().GetAll().Select(u => u.Gmail).ToListAsync();
        }
        private void ExecuteRegistrationCommand(object obj)
        {
            var page = new ViewRegstration();
            page.DataContext = new ViewModelRegistration();
            ((Page)obj).NavigationService.Navigate(page);
        }

        private bool CanExecuteLogInCommand(object obj) =>
                  gmails.Any(g => g == ((PasswordBox)((Page)obj).FindName("GmailTextBox")).Password);

        private async void ExecuteLogInCommand(object obj)
        {
            var user = await unitOfWork.GetRepository<User, int>().GetAll()
                .Where(u => u.Gmail == ((PasswordBox)((Page)obj).FindName("GmailTextBox")).Password)
                .FirstOrDefaultAsync();

            if (user.IsUsing)
            {
                MessageBox.Show("This Account uses by another Client");
                return;
            }
            user.IsUsing = true;
            await unitOfWork.Commit();
            var page = new SuccessfulLogin();
            page.DataContext = new ViewModelSuccsessEntryed(((PasswordBox)((Page)obj).FindName("GmailTextBox")).Password);
            ((Page)obj).NavigationService.Navigate(page);
        }
    }
}
