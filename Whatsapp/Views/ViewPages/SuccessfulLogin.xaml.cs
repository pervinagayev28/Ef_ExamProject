using ChatAppModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Whatsapp.ViewModels.ViewModelsPage;

namespace Whatsapp.Views.ViewPages
{

    public partial class SuccessfulLogin : Page
    {

        public SuccessfulLogin()
        {
            InitializeComponent();

        }




        public void selected(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex >= 0)
            {
                SelectedUserBtn.CommandParameter = AllList;
                SelectedUserBtn.Command.Execute(AllList);
            }

        }



        private void MouseEntered(object sender, MouseEventArgs e) =>
            MouseEnteredBtn.Command.Execute(sender as MediaElement);


        private void MouseLeft(object sender, MouseEventArgs e) =>
            MouseLeftBtn.Command.Execute(sender as MediaElement);

        private void selectedGroup(object sender, SelectionChangedEventArgs e)
        {
            if (listgroups.SelectedIndex >= 0)
            {
                SelectedUserBtnForGroup.CommandParameter = AllList;
                SelectedUserBtnForGroup.Command.Execute(AllList);
            }
        }

      
    }
}


