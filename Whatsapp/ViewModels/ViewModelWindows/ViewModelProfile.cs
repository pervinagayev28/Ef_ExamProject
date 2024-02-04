using ChatAppDatabaseLibraryy.Contexts;
using ChatAppModelsLibrary.Models;
using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Whatsapp.Commands;
using Whatsapp.Dtos;
using Whatsapp.UnitOfWorks.BaseUnitOfWorks;

namespace Whatsapp.ViewModels.ViewModelWindows
{
    public class ViewModelProfile : ServiceINotifyPropertyChanged
    {
        public ICommand? ChangePasswordCommand { get; set; }
        public ICommand? ChangeImageUrlCommand { get; set; }
        public ICommand? ChangeImageFromPCCommand { get; set; }
        public ICommand? ChangeBioCommand { get; set; }
        public ICommand? CloseCommand { get; set; }
        public ICommand? CommandGetImage { get; set; }
        public ICommand? CloseOpenedImageCommand { get; set; }
        private UserDto? user;
        public UserDto? User { get => user; set { user = value; OnPropertyChanged(); } }

        private readonly IUnitOfWork unitOfWork;
        public ViewModelProfile(UserDto user, IUnitOfWork unitOfWork)
        {
            User = user;
            this.unitOfWork = unitOfWork;
            ChangePasswordCommand = new Command(ExecuteChangePasswordCommand, CanExecuteChangePasswordCommand);
            ChangeImageUrlCommand = new Command(ExecuteChangeImageUrlCommand, CanExecuteChangeImageUrlCommand);
            ChangeImageFromPCCommand = new Command(ExecuteChangeImageFromPCCommand);
            CloseCommand = new Command(ExecuteCloseCommand);
            ChangeBioCommand = new Command(ExecuteChangeBioCommand, CanExecuteChangeBioCommand);
            CommandGetImage = new Command(ExecuteCommandGetImage, CanExecuteCommandGetImage);
            CloseOpenedImageCommand = new Command(ExecuteCloseOpenedImageCommand);
        }

        private void ExecuteCloseOpenedImageCommand(object obj) =>
              ((Grid)obj).Visibility = Visibility.Hidden;
        private bool CanExecuteCommandGetImage(object obj) =>
            obj is not null;

        private void ExecuteCommandGetImage(object obj)
        {
            ((Grid)obj).Visibility = Visibility.Visible;
        }

        private bool CanExecuteChangeBioCommand(object obj) =>
            User?.Bio != obj?.ToString();

        private void ExecuteChangeBioCommand(object obj) =>
            User.Bio = obj.ToString();

        private bool CanExecuteChangeImageUrlCommand(object obj) =>
            User.ImagePath != obj.ToString();
        private async void ExecuteCloseCommand(object obj)
        {
            var modifiedUser = await unitOfWork.GetRepository<User, int>().Get(User.Id);
            modifiedUser.Bio = User.Bio;
            modifiedUser.ImagePath = User.ImagePath;
            modifiedUser.Password= User.Password;
            unitOfWork?.GetRepository<User, int>().Update(modifiedUser);
            await unitOfWork?.Commit()!;
            ((Window)obj).Close();
        }

        private void ExecuteChangeImageFromPCCommand(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (!File.Exists($"..\\..\\..\\Images\\{Path.GetFileName(fileDialog.FileName)}"))
                    File.Copy(fileDialog.FileName, $"..\\..\\..\\Images\\{Path.GetFileName(fileDialog.FileName)}");

                string filename = Path.GetFileName(fileDialog.FileName);
                User!.ImagePath = $@"\Images\{Path.GetFileName(fileDialog.FileName)}";
            }
        }

        private void ExecuteChangeImageUrlCommand(object obj) =>
            User.ImagePath = obj.ToString();
        private bool CanExecuteChangePasswordCommand(object obj) =>
            Regex.IsMatch(obj.ToString()!, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$")
            && User.Password != obj.ToString()!;
        private void ExecuteChangePasswordCommand(object obj) =>
            User!.Password = obj.ToString()!;

    }
}
