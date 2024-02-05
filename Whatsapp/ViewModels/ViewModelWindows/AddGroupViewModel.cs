using ChatAppModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Whatsapp.Dtos;
using Whatsapp.UnitOfWorks.BaseUnitOfWorks;
using ChatAppService.Services;
using Whatsapp.Commands;
using Microsoft.Win32;
using System.IO;
using ChatAppModelsLibrary.Models.Concrets;

namespace Whatsapp.ViewModels.ViewModelWindows
{
    public  class AddGroupViewModel:ServiceINotifyPropertyChanged
    {
        public ICommand? ChangeImageFromPCCommand { get; set; }
        public ICommand? CloseCommand { get; set; }
        public ICommand? CommandGetImage { get; set; }
        public ICommand? CloseOpenedImageCommand { get; set; }
        private GroupDto? group;
        public Group GroupEntity { get; set; }
        public GroupDto? Group { get => group; set { group = value; OnPropertyChanged(); } }

        private readonly IUnitOfWork unitOfWork;
        public AddGroupViewModel(Group group, IUnitOfWork unitOfWork)
        {
            Group = new();
            GroupEntity = group;
            this.unitOfWork = unitOfWork;
            ChangeImageFromPCCommand = new Command(ExecuteChangeImageFromPCCommand);
            CloseCommand = new Command(ExecuteCloseCommand);
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

       

       
        private  void ExecuteCloseCommand(object obj)
        {
            GroupEntity.ImagePath = Group.ImagePath;
            GroupEntity.Name = Group.Name;
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
                Group!.ImagePath = $@"\Images\{Path.GetFileName(fileDialog.FileName)}";
            }
        }

        private void ExecuteChangeImageUrlCommand(object obj) =>
            Group.ImagePath = obj.ToString();
     
       

    }
}

