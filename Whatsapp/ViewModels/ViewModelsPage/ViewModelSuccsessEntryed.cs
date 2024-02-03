using ChatAppDatabaseLibraryy.Contexts;
using ChatAppModelsLibrary.Models;
using ChatAppModelsLibrary.Models.BaseModels;
using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Win32;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.XPath;
using Whatsapp.Commands;
using Whatsapp.UnitOfWorks.BaseUnitOfWorks;
using Whatsapp.UnitOfWorks.Concrets;
using Whatsapp.ViewModels.ViewModelWindows;
using Whatsapp.Views.ViewPages;
using Whatsapp.Views.ViewWindows;

namespace Whatsapp.ViewModels.ViewModelsPage
{


    class ViewModelSuccsessEntryed : ServiceINotifyPropertyChanged
    {

        //UnitOfWork using
        private readonly UnitOfWork unitOfWork;


        //Bool types 
        private bool checkStatus;

        //Privates Fields
        private DispatcherTimer? timer;
        private Grid grid;
        private int currentSelectedUserId;
        private User? user;
        private string selectedUserImagePath;
        private bool check = false;
        private DateTime? temp;
        private int tempId;
        private bool checkTimer { get; set; }

        //Private Collections
        private List<UserConnection>? connections;
        private ObservableCollection<User> users;

        private ObservableCollection<Message> messages = new();
        private ObservableCollection<Status> statuses = new();

        // Commands
        public ICommand? SelectedChatUser { get; set; }
        public ICommand? SendMessageCommand { get; set; }
        public ICommand? LogOutCommand { get; set; }
        public ICommand? AllUsersCommand { get; set; }
        public ICommand? OnlyChatUsersCommand { get; set; }
        public ICommand? ProfileCommand { get; set; }
        public ICommand? CloseOpenedImageCommand { get; set; }
        public ICommand? GetImageCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? ChatCommand { get; set; }
        public ICommand? SharesCommand { get; set; }
        public ICommand? AddStatusCommand { get; set; }
        public ICommand? DeleteStatusCommand { get; set; }
        public ICommand? MyStatusCommand { get; set; }
        public ICommand? PlayPauseCommand { get; set; }
        public ICommand? MouseEnteredCommand { get; set; }
        public ICommand? MouseLeftCommand { get; set; }


        //Obersvable Collections 
        public ObservableCollection<User> Users { get => users; set { users = value; OnPropertyChanged(); } }
        public ObservableCollection<Message> Messages { get => messages; set { messages = value; OnPropertyChanged(); } }
        public ObservableCollection<Status> Statuses { get => statuses; set { statuses = value; OnPropertyChanged(); } }

        //Properties
        public User? User { get => user; set { user = value; OnPropertyChanged(); } }
        public string SelectedUserImagePath { get => selectedUserImagePath; set { selectedUserImagePath = value; OnPropertyChanged(); } }




        //Constructors
        public ViewModelSuccsessEntryed(string Gmail, ChatAppDb? myChatingAppContext)
        {
            //Initialize UnitOfWork
            unitOfWork = new UnitOfWork();

            //Simple Commands Initialize
            SelectedChatUser = new CommandAsync(ExecuteSelectedChatUser);
            LogOutCommand = new Command(ExecuteLogOutCommand);
            CloseOpenedImageCommand = new Command(ExecuteCloseOpenedImageCommand);
            GetImageCommand = new Command(ExecuteGetImageCommand);
            ChatCommand = new Command(ExecuteChatCommand, CanExecuteChatCommand);
            SharesCommand = new Command(ExecuteSharesCommand, CanExecuteSharesCommand);
            MyStatusCommand = new Command(ExecuteMyStatusCommand);
            PlayPauseCommand = new Command(ExecutePlayPauseCommand);
            MouseEnteredCommand = new Command(ExecuteMouseEneterdCommand);
            MouseLeftCommand = new Command(ExecuteMouseLeftCommand);

            //Async Commands Initialize
            SendMessageCommand = new CommandAsync(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
            AllUsersCommand = new CommandAsync(ExecuteAllUsersCommandAsync, CanExecuteAllUsersCommandAsync);
            OnlyChatUsersCommand = new CommandAsync(ExecuteOnlyChatUsersCommand, CanExecuteOnlyChatUsersCommand);
            ProfileCommand = new Command(ExecuteProfileCommand);
            DeleteCommand = new CommandAsync(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            DeleteStatusCommand = new CommandAsync(ExecuteDeleteStatusCommand, CanExecuteDeleteStatusCommand);
            AddStatusCommand = new CommandAsync(ExecuteAddStatusCommand);

            //Start Mehtod Calling
            start(Gmail);
        }

        private void ExecuteMouseLeftCommand(object obj)=>
            ((MediaElement)obj).LoadedBehavior = MediaState.Pause;

        private void ExecuteMouseEneterdCommand(object obj) =>
            ((MediaElement)obj).LoadedBehavior = MediaState.Play;

        private void ExecutePlayPauseCommand(object obj)
        {
            if (((MediaElement)obj).LoadedBehavior == MediaState.Play)
                ((MediaElement)obj).LoadedBehavior = MediaState.Pause;
            else
                ((MediaElement)obj).LoadedBehavior = MediaState.Play;
        }

        private void ExecuteMyStatusCommand(object obj) =>
            ChangeVideoPath(User?.Status!);

        private async Task ExecuteDeleteStatusCommand(object arg)
        {
            //var entity = await unitOfWork.GetRepository<Status, int>().Get(Statuses[((ListView)arg).SelectedIndex].Id);
            User?.Status?.Remove(await unitOfWork.GetRepository<Status, int>().Get(Statuses[((ListView)arg).SelectedIndex].Id));
            await unitOfWork?.Commit()!;
            ChangeVideoPath(User?.Status!);

        }
        private bool CanExecuteDeleteStatusCommand(object obj) =>
            ((ListView)obj)?.SelectedIndex != -1
            && Statuses?.Count == User?.Status?.Count;

        private async Task ExecuteAddStatusCommand(object obj)
        {
            ((Button)obj).Visibility = Visibility.Visible;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (!File.Exists($"..\\..\\..\\Database\\Statues\\{Path.GetFileName(fileDialog.FileName)}"))
                    File.Copy(fileDialog.FileName, $"..\\..\\..\\Database\\Statues\\{Path.GetFileName(fileDialog.FileName)}");
                string filename = Path.GetFileName(fileDialog.FileName);
                User!.Status.Add(new Status()
                {
                    UserId = User.Id,
                    VideoPath = $@"\Database\Statues\{Path.GetFileName(fileDialog.FileName)}",
                    Title = "no title"
                });
                await unitOfWork.Commit()!;
                ChangeVideoPath(User.Status);
                ((Button)obj).Visibility = Visibility.Hidden;
                await Task.CompletedTask;
            }
        }





        //Can Execute Funcs

        #region Can Execute Methods
        private bool CanExecuteSharesCommand(object obj) =>
                    ((Grid)((Page)obj)?.FindName("MainGridStatus")).Visibility != Visibility.Visible;

        private bool CanExecuteChatCommand(object obj) =>
          ((ListView)((Page)obj)?.FindName("list2")!)?.Visibility != Visibility.Visible;
        private bool CanExecuteSendMessageCommand(object obj) =>
          currentSelectedUserId != 0;
        private bool CanExecuteAllUsersCommandAsync(object obj) =>
            !check;

        private bool CanExecuteOnlyChatUsersCommand(object obj) =>
            check;

        private bool CanExecuteDeleteCommand(object obj) =>
            ((ListView)obj)?.SelectedItems?.Count > 0;
        #endregion




        //Command Asyncs


        #region Command Async
        private async Task ExecuteOnlyChatUsersCommand(object obj)
        {
            check = false;
            await GetUsers();
        }


        private async Task ExecuteAllUsersCommandAsync(object obj)
        {
            check = true;
            await GetAllUsers();
        }
        private async Task ExecuteDeleteCommand(object arg)
        {
            timer?.Stop();

            while (true)
            {
                if (!checkTimer)
                {
                    foreach (var item in ((ListView)arg)?.SelectedItems!)
                    {
                        foreach (var connect in connections!)
                        {
                            if (connect.FromId == User.Id && connect.ToId == (item as User)?.Id)
                                connect.SofDeleteFrom = true;
                            else if (connect.ToId == User.Id && connect.FromId == (item as User)?.Id)
                                connect.SoftDeleteTo = true;
                        }
                    }
                    ((ListView)arg)?.SelectedItems?.Clear();
                    currentSelectedUserId = default;
                    await unitOfWork.Commit();
                    await GetUsers();
                    break;
                }
            }
            timer?.Start();
        }
        private async Task ExecuteSendMessageCommand(object obj)
        {
            timer?.Stop();
            //File.AppendAllText("fooo.txt", DateTime.Now.ToString()+"stopeed\n");
            while (true)
            {

                //File.AppendAllText("foo2.txt", "cycles\t");
                if (!checkTimer)
                {
                    bool check = true;
                    foreach (var item in connections!)
                    {
                        if ((item.FromId == User.Id && item.ToId == currentSelectedUserId) || (item.ToId == User.Id && item.FromId == currentSelectedUserId))
                        {
                            check = false;
                            if (item.SofDeleteFrom)
                                item.FromConnectedDate = DateTime.Now;
                            if (item.SoftDeleteTo)
                                item.ToConnectedDate = DateTime.Now;

                            item.SoftDeleteTo = false;
                            item.SofDeleteFrom = false;
                        }

                    }
                    if (check)
                        await unitOfWork.GetRepository<UserConnection, int>().Add(new UserConnection()
                        {
                            FromId = User.Id,
                            ToId = currentSelectedUserId,
                            FromConnectedDate = DateTime.Now,
                            ToConnectedDate = DateTime.Now
                        });

                    await unitOfWork.GetRepository<Message, int>().Add(new Message() { FromId = User.Id, message = ((TextBox)obj).Text, Date = DateTime.Now, ToId = currentSelectedUserId });
                    await unitOfWork.Commit();
                    await GetUsers();
                    //File.AppendAllText("foo2.txt", "\n");
                    break;
                }
            }
            timer?.Start();
            //File.AppendAllText("fooo.txt", DateTime.Now.ToString() + "start\n");
            ((TextBox)obj).Text = "";
        }

        #endregion




        //Command
        #region Command
        private void ExecuteSharesCommand(object obj)
        {
            ((Grid)((Page)obj)?.FindName("MainGridStatus")).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("SendMessageGrid")).Visibility = Visibility.Hidden;
            ((ListView)((Page)obj)?.FindName("list2")).Visibility = Visibility.Hidden;

        }



        private void ExecuteChatCommand(object obj)
        {
            ((ListView)((Page)obj)?.FindName("list2")).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("SendMessageGrid")).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("MainGridStatus")).Visibility = Visibility.Hidden;
        }

        private void ExecuteProfileCommand(object obj)
        {
            var page = new WindowProfile();
            timer.Stop();
            page.DataContext = new ViewModelProfile(User!, unitOfWork);
            page.ShowDialog();
            timer.Start();
        }
        private void ExecuteLogOutCommand(object obj)
        {
            timer.IsEnabled = false;
            //timer?.Stop();
            var page = new ViewEntry();
            page.DataContext = new ViewModelEntry();
            ((Page)obj).NavigationService.Navigate(page);
        }

        private void ExecuteGetImageCommand(object arg)
        {
            if ((((ListView)((Grid)arg).FindName("list")).SelectedItem is not null))
            {

                ((Grid)((Grid)arg).FindName("OpenedImageGrid")).Visibility = Visibility.Visible;
                SelectedUserImagePath = (((ListView)((Grid)arg).FindName("list")).SelectedItem as User)!.ImagePath!;
            }
        }

        private void ExecuteCloseOpenedImageCommand(object arg)
        {
            ((Grid)arg).Visibility = Visibility.Hidden;
        }
        private async Task ExecuteSelectedChatUser(object obj)
        {
            checkStatus = true;
            timer?.Stop();
            grid = (Grid)obj;
            currentSelectedUserId = (Users[(int)((ListView)grid.FindName("list")).SelectedIndex] as User)!.Id;

            if (checkStatus)
            {
                var query = await unitOfWork.GetRepository<User, int>().GetAll();
                var datas = await query.Include(u => u.Status).FirstOrDefaultAsync(u => u.Id == currentSelectedUserId);
                ChangeVideoPath(datas?.Status!);
                checkStatus = false;
            }
            //ChangeVideoPath((await(await unitOfWork.GetRepository<User, int>().GetAll()).Include(u=>u.Status).FirstOrDefaultAsync(u=>u.Id==currentSelectedUserId))?.Status!);
            timer?.Start();
        }


        #endregion
        private void ChangeVideoPath(ICollection<Status> videos)
        {
            Statuses = new();
            if (videos is null)
                return;
            foreach (Status video in videos.Reverse())
                Statuses.Add(new Status()
                {
                    Id = video.Id,
                    Title = video.Title,
                    UserId = video.Id,
                    VideoPath = Environment.CurrentDirectory + "..\\..\\..\\..\\" + video.VideoPath
                });

        }
        // Start Method
        private async void start(string Gmail)
        {
            //User = await context?.UsersTbs.FirstOrDefaultAsync(u => u.Gmail == Gmail)!;
            User = await (await unitOfWork.GetRepository<User, int>().GetAll())
                                 .Include(u => u.Status)
                                .Include(u => u.ConnectionFroms)
                                .Include(u => u.ConnectionTos)
                                .FirstOrDefaultAsync(u => u.Gmail == Gmail)!;

            ChangeVideoPath(User.Status);


            await GetUsers();
            await GetLastMessages();
            connections = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += async (sender, e) => await TrickerDataBase();
        }


        //Async Methods (Task)
        #region Async Methods
        private async Task GetLastMessages()
        {
            timer?.Stop();
            foreach (var item in users)
            {
                var messages = await (await unitOfWork.GetRepository<Message, int>().GetAll())
                  .Where(message => message.From.Id == User.Id
                                 && message.To.Id == item!.Id
                                 || message.To.Id == User!.Id
                                 && message.From.Id == item!.Id)
                  .Include(x => x.From)
                  .Include(y => y.To)
                  .OrderByDescending(m => m.Date)
                  .FirstOrDefaultAsync();
                item!.LastMessage = messages?.message;
                item!.LastMessageDate = messages?.Date;
            }
            if (temp < (users?.FirstOrDefault(u => u!.Id == currentSelectedUserId) as User)?.LastMessageDate || timer is null)
                Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
            temp = (users?.FirstOrDefault(u => u?.Id == currentSelectedUserId) as User)?.LastMessageDate;
            connections = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();
            timer?.Start();
        }


        private async Task GetAllUsers()
        {
            timer?.Stop();
            while (true)
            {
                if (!checkTimer)
                {
                    Users = new(await (await unitOfWork.GetRepository<User, int>().GetAll()).Where(u => u.Id != User.Id).ToListAsync());
                    break;
                }
            }
        }

        private async Task GetUsers()
        {
            timer?.Stop();
            while (true)
            {
                if (!checkTimer)
                {
                    Users = new(await (await unitOfWork.GetRepository<User, int>().GetAll())
                                .Where(u => (u.Id != User!.Id) && (u.MessagesFroms!
                                           .Any(m => m.ToId == User.Id || m.FromId == User.Id) || u.MessagesTo
                                           .Any(m => m.ToId == User.Id || m.FromId == User.Id))
                                            && u.ConnectionFroms!
                                           .Any(c => c.FromId == User.Id && !c.SofDeleteFrom || c.ToId == User.Id && !c.SoftDeleteTo) || u.ConnectionTos!
                                           .Any(c => c.FromId == User.Id && !c.SofDeleteFrom || c.ToId == User.Id && !c.SoftDeleteTo) && u.Id != User.Id)
                               .Include(u => u.MessagesFroms)
                               .Include(u => u.MessagesTo)
                               .ToListAsync());
                    await GetLastMessages();
                    break;
                }
            }

        }
        class d
        {
            public DateTime date { get; set; }
        }
        public async Task TrickerDataBase()
        {
            checkTimer = true;
            //File.AppendAllText("fooo.txt", DateTime.Now.ToString() + "began\n");

            var data = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll())
                            .Where(c => c.FromId == User.Id && c.ToId == currentSelectedUserId
                                || c.ToId == User.Id && c.FromId == currentSelectedUserId)
                                .Select(c => new d
                                {
                                    date = c.FromId == User.Id ? c.FromConnectedDate : c.ToConnectedDate
                                })
                            .FirstOrDefaultAsync();

            if (data is null)
                data = new() { date = DateTime.Parse("12/12/2002") };


            var messages = await (await unitOfWork.GetRepository<Message, int>().GetAll())
                                .Where(message => (message.Date >= data.date) &&
                                 (message.From.Id == User.Id && message.To.Id == currentSelectedUserId ||
                                 message.To.Id == User.Id && message.From.Id == currentSelectedUserId))
                  .Include(x => x.From)
                  .Include(y => y.To)
                  .Select(x => new Message
                  {
                      RightOrLeft = x.FromId == currentSelectedUserId ? 0 : 1,
                      message = x.message,
                      Date = x.Date,
                      MessageForVisual = x.message + " " + x.Date.ToString("HH:mm")
                  })
                  .OrderBy(m => m.Date)
                  .ToListAsync();


            if (tempId == currentSelectedUserId)
            {
                if (messages.Count != 0 && Messages.Count == 0)
                {
                    Messages.Add(new Message()
                    {
                        message = messages.Last().message,
                        MessageForVisual = messages.Last().MessageForVisual,
                        RightOrLeft = messages.Last().RightOrLeft,
                        Date = messages.Last().Date,
                    });

                    ((ListView)grid.FindName("list2")).ScrollIntoView(Messages.Last());
                    Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
                }
                if (messages.Count != 0 && Messages.Count != 0)
                {
                    if (Messages?.Last()?.Date != messages?.Last()?.Date)
                    {
                        Messages.Add(new Message()
                        {
                            message = messages.Last().message,
                            MessageForVisual = messages.Last().MessageForVisual,
                            RightOrLeft = messages.Last().RightOrLeft,
                            Date = messages.Last().Date,
                        });

                        ((ListView)grid.FindName("list2")).ScrollIntoView(Messages.Last());
                        Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
                    }
                }

            }
            //if (Messages.Count == 0 && messages.Count != 0)
            else
            {

                if (grid is not null)
                    ((ListView)grid.FindName("list2")).ScrollIntoView(Messages);
                Messages = new(messages);

                if (messages.Count != 0 && grid is not null)
                {
                    ((ListView)grid.FindName("list2")).
                        ScrollIntoView(((ListView)grid.FindName("list2")).
                        Items[((ListView)grid.FindName("list2")).Items.Count - 1]);
                }
            }

            await GetLastMessages();
            tempId = currentSelectedUserId;
            checkTimer = false;
            //File.AppendAllText("fooo.txt", DateTime.Now.ToString() + "finished\n");

        }
        #endregion


    }
}

