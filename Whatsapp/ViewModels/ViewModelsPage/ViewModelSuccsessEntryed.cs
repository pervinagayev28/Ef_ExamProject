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
using System.Reflection.Metadata;
using System.Runtime.Serialization.DataContracts;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
using AutoMapper;
using Whatsapp.Dtos;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Whatsapp.ViewModels.ViewModelsPage
{


    class ViewModelSuccsessEntryed : ServiceINotifyPropertyChanged
    {
        #region UnitOfwork Using
        private readonly UnitOfWork unitOfWork;

        #endregion
        #region Bool types
        private bool checkStatus;
        private bool CanDeleteStatus;

        #endregion
        #region Private Fields
        private DispatcherTimer? timer;
        private DispatcherTimer? timerForGroup;
        private Grid grid;
        private int currentSelectedUserId;
        private int currentSelectedGroupId;
        private int currentSelectedIdTemp;
        private UserDto? user;
        private string selectedUserImagePath;
        private bool check = false;
        private bool CheckAddForGroupMessage = false;
        private bool CheckMessage = false;
        private DateTime? temp;
        private int tempId;
        private bool checkTimer { get; set; }


        #region VisibilityProperties
        private Visibility loadingVisibility;
        public Visibility LoadingVisibility { get => loadingVisibility; set { loadingVisibility = value; OnPropertyChanged(); } }

        private Visibility loadingVisibilityChat = Visibility.Hidden;
        public Visibility LoadingVisibilityChat { get => loadingVisibilityChat; set { loadingVisibilityChat = value; OnPropertyChanged(); } }



        #endregion
        #endregion
        #region Private Collections
        private List<UserConnection>? connections;
        private ObservableCollection<UserDto> users;

        private ObservableCollection<MessageDto> messages = new();
        private ObservableCollection<Status> statuses = new();
        private string selectedUser;
        private ObservableCollection<Group> groups;
        private ObservableCollection<GroupMessageDto> groupMessages;
        private ObservableCollection<UserDto> hasStatusUsers;

        #endregion
        #region Commands Initialiazes
        public ICommand? SelectedChatUser { get; set; }
        public ICommand? SelectedChatUserForGroup { get; set; }
        public ICommand? SendMessageCommand { get; set; }
        public ICommand? LogOutCommand { get; set; }
        public ICommand? GetGroupsCommand { get; set; }
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
        public ICommand? MouseEnteredCommand { get; set; }
        public ICommand? MouseLeftCommand { get; set; }
        public ICommand? SelectedUserStatusCommand { get; set; }
        public ICommand? ChangeColorCommand { get; set; }
        public ICommand? CreateGroupCommand { get; set; }
        public ICommand? SendMessageToGroupCommand { get; set; }
        public ICommand? DeleteGromGroupsCommand { get; set; }

        #region IMapper Initialiazer


        private readonly IMapper UserMapper;
        #endregion

        #endregion
        #region Observable Collections
        public ObservableCollection<UserDto> HasStatusUsers { get => hasStatusUsers; set { hasStatusUsers = value; OnPropertyChanged(); } }
        public ObservableCollection<UserDto> Users { get => users; set { users = value; OnPropertyChanged(); } }
        public ObservableCollection<MessageDto> Messages { get => messages; set { messages = value; OnPropertyChanged(); } }
        public ObservableCollection<Status> Statuses { get => statuses; set { statuses = value; OnPropertyChanged(); } }
        public ObservableCollection<Group> Groups { get => groups; set { groups = value; OnPropertyChanged(); } }
        public ObservableCollection<GroupMessageDto> GroupMessages { get => groupMessages; set { groupMessages = value; OnPropertyChanged(); } }

        #endregion
        #region Propertioes
        public UserDto? User { get => user; set { user = value; OnPropertyChanged(); } }
        public string SelectedUser { get => selectedUser; set { selectedUser = value; OnPropertyChanged(); } }
        public string SelectedUserImagePath { get => selectedUserImagePath; set { selectedUserImagePath = value; OnPropertyChanged(); } }
        #endregion
        #region Constructor

        public ViewModelSuccsessEntryed(string Gmail, ChatAppDb? myChatingAppContext)
        {
            //Initialize UnitOfWork
            unitOfWork = new UnitOfWork();

            //Simple Commands Initialize
            SelectedChatUser = new Command(ExecuteSelectedChatUser);
            SelectedChatUserForGroup = new Command(ExecuteSelectedChatUserForGroup);
            LogOutCommand = new Command(ExecuteLogOutCommand);
            CloseOpenedImageCommand = new Command(ExecuteCloseOpenedImageCommand);
            GetImageCommand = new Command(ExecuteGetImageCommand);
            ChatCommand = new Command(ExecuteChatCommand, CanExecuteChatCommand);
            SharesCommand = new Command(ExecuteSharesCommand, CanExecuteSharesCommand);
            MouseEnteredCommand = new Command(ExecuteMouseEneterdCommand);
            MouseLeftCommand = new Command(ExecuteMouseLeftCommand);
            ProfileCommand = new Command(ExecuteProfileCommand);
            ChangeColorCommand = new Command(ExecuteChangeColorCommand);
            SelectedUserStatusCommand = new Command(ExecutSelectedUserStatusCommand);
            OnlyChatUsersCommand = new Command(ExecuteOnlyChatUsersCommand, CanExecuteOnlyChatUsersCommand);
            GetGroupsCommand = new Command(ExecuteGetGroupsCommandAsync, CanExecuteGetGroupsCommandAsync);

            //Async Commands Initialize
            MyStatusCommand = new CommandAsync(ExecuteMyStatusCommand);
            CreateGroupCommand = new CommandAsync(ExecuteCreateGroupCommand, CanExecuteCreateGroupCommand);
            SendMessageCommand = new CommandAsync(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
            DeleteCommand = new CommandAsync(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            DeleteStatusCommand = new CommandAsync(ExecuteDeleteStatusCommand, CanExecuteDeleteStatusCommand);
            AddStatusCommand = new CommandAsync(ExecuteAddStatusCommand);
            SendMessageToGroupCommand = new CommandAsync(ExecutSendMessageToGroupCommand, CanExecutSendMessageToGroupCommand);


            //Initialize Mapper
            UserMapper = AutoMapperConfiguration.GetMapperConfiguration<User, UserDto>();

            //Start Mehtod Calling
            start(Gmail);
        }



        private bool CanExecutSendMessageToGroupCommand(object obj) =>
            currentSelectedGroupId != 0 && ((TextBox)obj)?.Text != "";

        private async Task ExecutSendMessageToGroupCommand(object arg)
        {
            timerForGroup?.Stop();
            await unitOfWork.GetRepository<GroupMessages, int>().Add(new GroupMessages() { FromId = User.Id, Message = ((TextBox)arg).Text, Date = DateTime.Now, GroupId = currentSelectedGroupId });
            await unitOfWork.Commit();
            ((TextBox)arg).Text = "";
            timerForGroup?.Start();
        }

        private void ExecuteSelectedChatUserForGroup(object arg)
        {
            LoadingVisibilityChat = Visibility.Visible;
            timerForGroup?.Stop();
            grid = (Grid)arg;
            currentSelectedGroupId = (Groups[(int)((ListView)grid.FindName("listgroups")).SelectedIndex] as Group)!.Id;
            timerForGroup?.Start();
        }

        private bool CanExecuteCreateGroupCommand(object obj) =>
            ((ListView)obj)?.SelectedIndex != -1 && ((ListView)obj)?.Visibility == Visibility.Visible;

        private async Task ExecuteCreateGroupCommand(object obj)
        {
            timer?.Stop();
            if (Groups is null)
                Groups = new(await (unitOfWork.GetRepository<Group, int>().GetAll()).Include(g => g.GroupAndUsers).ToListAsync());
            Group group = new();
            var page = new ViewAddGroup();
            page.DataContext = new AddGroupViewModel(group);
            page.ShowDialog();
            Groups.Add(group);
            foreach (var user in ((ListView)obj).SelectedItems)
            {
                if (group.GroupAndUsers is null)
                    group.GroupAndUsers = new List<GroupAndUser>();
                group?.GroupAndUsers?.Add(new GroupAndUser()
                {
                    UserId = (user as UserDto).Id,
                });
            }
            group?.GroupAndUsers?.Add(new GroupAndUser()
            {
                UserId = User.Id,
            });

            group.Messages = new List<GroupMessages>()
            {
                new GroupMessages()
                {
                FromId = User.Id,
                Message = "Hello Friends,I Created This Group",
                Date = DateTime.Now
                }
             };

            await unitOfWork.GetRepository<Group, int>().Add(group);
            await unitOfWork.Commit();
        }

        private void ExecuteChangeColorCommand(object obj)
        {
            byte[] colorBytes = new byte[3];
            Random.Shared.NextBytes(colorBytes);
            ((Border)obj).Background =
                new SolidColorBrush(Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]));
        }

        private void ExecutSelectedUserStatusCommand(object arg)
        {
            SelectedUser = (arg as UserDto)?.Gmail!;
            ChangeVideoPath((arg as UserDto)?.Status!);
            CanDeleteStatus = false;

        }
        #endregion

        private bool CanExecuteDeleteStatusCommand(object obj) =>
            CanDeleteStatus && int.Parse(obj?.ToString()!) != -1;


        #region Can Execute Methods
        private bool CanExecuteSharesCommand(object obj) =>
                    ((Grid)((Page)obj)?.FindName("MainGridStatus")).Visibility != Visibility.Visible;

        private bool CanExecuteChatCommand(object obj) =>
          ((ListView)((Page)obj)?.FindName("list2")!)?.Visibility != Visibility.Visible;
        private bool CanExecuteSendMessageCommand(object obj) =>
          currentSelectedUserId != 0 && ((TextBox)obj)?.Text != "";
        private bool CanExecuteGetGroupsCommandAsync(object obj) =>
            !check;

        private bool CanExecuteOnlyChatUsersCommand(object obj) =>
            check;

        private bool CanExecuteDeleteCommand(object obj) =>
            ((ListView)obj)?.SelectedItems?.Count > 0;
        #endregion
        #region Command Async

        private async Task ExecuteDeleteStatusCommand(object arg)
        {

            User?.Status?.Remove(await unitOfWork.GetRepository<Status, int>().Get(Statuses[int.Parse(arg.ToString()!)].Id));
            await unitOfWork?.Commit()!;
            ChangeVideoPath(User?.Status!);

        }
        private async Task ExecuteAddStatusCommand(object obj)
        {
            ((Button)obj).Visibility = Visibility.Visible;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (!File.Exists($"..\\..\\..\\Database\\Statues\\{Path.GetFileName(fileDialog.FileName)}"))
                    File.Copy(fileDialog.FileName, $"..\\..\\..\\Database\\Statues\\{Path.GetFileName(fileDialog.FileName)}");
                string filename = Path.GetFileName(fileDialog.FileName);
                User?.Status.Add(new Status()
                {
                    UserId = User.Id,
                    VideoPath = $@"\Database\Statues\{Path.GetFileName(fileDialog.FileName)}",
                    Title = "no title"
                });

                unitOfWork?.GetRepository<Status, int>().Add(User?.Status?.Last()!);
                await unitOfWork?.Commit()!;
                ChangeVideoPath(User?.Status);
                ((Button)obj).Visibility = Visibility.Hidden;
            }
        }

        private void ExecuteOnlyChatUsersCommand(object obj)
        {
            tempId = 0;
            Thread th = new(GetUsersAsync);
            th.Start();
            LoadingVisibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("list")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("list2")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("GroupsList")).Visibility = Visibility.Hidden;
            ((ListView)((Grid)obj).FindName("listgroups")).Visibility = Visibility.Hidden;
            timerForGroup?.Stop();
            check = false;
        }


        private  void ExecuteGetGroupsCommandAsync(object obj)
        {
            tempId = 0;
            timer?.Stop();
            LoadingVisibility = Visibility.Visible;
            Thread th = new(GetGroupAsync);
            th.Start();
            check = true;
            ((ListView)((Grid)obj).FindName("GroupsList")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("listgroups")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("list")).Visibility = Visibility.Hidden;
            ((Button)((Grid)obj).FindName("SendMessageToGroupCommand")).Visibility = Visibility.Visible;
            ((Button)((Grid)obj).FindName("SendMEssageCommand")).Visibility = Visibility.Hidden;
            ((ListView)((Grid)obj).FindName("list2")).Visibility = Visibility.Hidden;
        }

        private async void GetGroupAsync(object? obj)
        {
            Groups = new(await(unitOfWork.GetRepository<Group, int>().GetAll())
                            .Include(g => g.GroupAndUsers)!.ThenInclude(u => u.User)
                            .Where(g => g.GroupAndUsers.Any(gu => gu.UserId == User.Id))
                            .ToListAsync()!)!;
            LoadingVisibility = Visibility.Hidden;
        }

        private async Task ExecuteDeleteCommand(object arg)
        {
            timer?.Stop();


            foreach (var item in ((ListView)arg)?.SelectedItems!)
            {
                foreach (var connect in connections!)
                {
                    if (connect.FromId == User.Id && connect.ToId == (item as UserDto)?.Id)
                        connect.SofDeleteFrom = true;
                    else if (connect.ToId == User.Id && connect.FromId == (item as UserDto)?.Id)
                        connect.SoftDeleteTo = true;
                }
            }
                    ((ListView)arg)?.SelectedItems?.Clear();
            currentSelectedUserId = default;
            await unitOfWork.Commit();
            await GetUsers();


            timer?.Start();
        }
        private async Task ExecuteSendMessageCommand(object obj)
        {
            timerForGroup?.Stop();
            timer?.Stop();
            await unitOfWork.GetRepository<Message, int>().Add(new Message() { FromId = User.Id, message = ((TextBox)obj).Text, Date = DateTime.Now, ToId = currentSelectedUserId });
            await unitOfWork.Commit();
            #region Comment
            //bool check = true;
            //foreach (var item in connections!)
            //{
            //    if ((item.FromId == User.Id && item.ToId == currentSelectedUserId) || (item.ToId == User.Id && item.FromId == currentSelectedUserId))
            //    {
            //        check = false;
            //        if (item.SofDeleteFrom)
            //            item.FromConnectedDate = DateTime.Now;
            //        if (item.SoftDeleteTo)
            //            item.ToConnectedDate = DateTime.Now;

            //        item.SoftDeleteTo = false;
            //        item.SofDeleteFrom = false;
            //    }

            ////}
            //if (check)
            //    await unitOfWork.GetRepository<UserConnection, int>().Add(new UserConnection()
            //    {
            //        FromId = User.Id,
            //        ToId = currentSelectedUserId,
            //        FromConnectedDate = DateTime.Now,
            //        ToConnectedDate = DateTime.Now
            //    });



            //if (currentSelectedUserId != currentSelectedIdTemp)
            //    Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
            //currentSelectedIdTemp = currentSelectedUserId;
            //await GetUsers();
            #endregion

            timer?.Start();
            ((TextBox)obj).Text = "";
        }

        #endregion
        #region Command

        private async Task ExecuteMyStatusCommand(object obj)
        {
            CanDeleteStatus = true;
            User.Status = await (unitOfWork.GetRepository<Status, int>().GetAll())
                    .Where(s => s.UserId == User.Id).ToListAsync();
            ChangeVideoPath(User?.Status!);
        }

        private void ExecuteMouseLeftCommand(object obj) =>
         ((MediaElement)obj).LoadedBehavior = MediaState.Pause;

        private void ExecuteMouseEneterdCommand(object obj) =>
            ((MediaElement)obj).LoadedBehavior = MediaState.Play;

        private void ExecuteSharesCommand(object obj)
        {
            timer?.Stop();
            timerForGroup?.Stop();
            ((Grid)((Page)obj)?.FindName("MainGridStatus")!).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("SendMessageGrid")!).Visibility = Visibility.Hidden;
            ((ListView)((Page)obj)?.FindName("list2")!).Visibility = Visibility.Hidden;
            ((ListView)((Page)obj)?.FindName("GroupsList")!).Visibility = Visibility.Hidden;

        }



        private void ExecuteChatCommand(object obj)
        {
            bool checktimer = true;
            if (!timer.IsEnabled)
                timer?.Start();
            else
                timerForGroup?.Start();
            if (check)
                ((ListView)((Page)obj)?.FindName("GroupsList")!).Visibility = Visibility.Visible;
            else
                ((ListView)((Page)obj)?.FindName("list2")!).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("SendMessageGrid")!).Visibility = Visibility.Visible;
            ((Grid)((Page)obj)?.FindName("MainGridStatus")!).Visibility = Visibility.Hidden;
        }

        private void ExecuteProfileCommand(object obj)
        {
            var page = new WindowProfile();
            bool checktimer = true;
            if (timer.IsEnabled)
                checktimer = false;
            timer?.Stop();
            timerForGroup?.Stop();
            page.DataContext = new ViewModelProfile(User, unitOfWork);
            page.ShowDialog();
            if (!checktimer)
                timer?.Start();
            else
                timerForGroup?.Start();
        }
        private void ExecuteLogOutCommand(object obj)
        {
            timer?.Stop();
            timerForGroup?.Stop();
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
                SelectedUserImagePath = (((ListView)((Grid)arg).FindName("list")).SelectedItem as UserDto)!.ImagePath!;
            }
        }

        private void ExecuteCloseOpenedImageCommand(object arg)
        {
            ((Grid)arg).Visibility = Visibility.Hidden;
        }
        private void ExecuteSelectedChatUser(object obj)
        {
            LoadingVisibilityChat = Visibility.Visible;
            //timerForGroup?.Stop();
            checkStatus = true;
            //timer?.Stop();
            grid = (Grid)obj;
            currentSelectedUserId = (Users[(int)((ListView)grid.FindName("list")).SelectedIndex] as UserDto)!.Id;
            timer?.Start();
        }


        #endregion
        #region ChangeVideoPath
        private void ChangeVideoPath(ICollection<Status> videos)
        {
            if (videos is null)
                return;
            Statuses = new();
            foreach (Status video in videos.Reverse())
                Statuses.Add(new Status()
                {
                    Id = video.Id,
                    Title = video.Title,
                    UserId = video.Id,
                    VideoPath = Environment.CurrentDirectory + "..\\..\\..\\..\\" + video.VideoPath
                });

        }

        #endregion
        #region StartUp
        private async void start(string Gmail)
        {
            User = UserMapper.Map<UserDto>(await (unitOfWork.GetRepository<User, int>().GetAll())
                                .FirstOrDefaultAsync(u => u.Gmail == Gmail)!);
            //ChangeVideoPath(User.Status!);
            Thread th = new(GetUsersAsync);
            th.Start();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += async (sender, e) => await TrickerDataBase();

            timerForGroup = new DispatcherTimer();
            timerForGroup.Interval = TimeSpan.FromSeconds(1);
            timerForGroup.Tick += async (sender, e) => await TrickerDataBaseForGroup();


        }

        private async void GetUsersAsync(object? obj)
        {
            await GetUsers();
            //connections = await (unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();
            //LoadingVisibility = Visibility.Hidden;
        }

        private async Task TrickerDataBaseForGroup()
        {
            timer?.Stop();
            int currentId = currentSelectedGroupId;
            var messages = await (unitOfWork.GetRepository<GroupMessages, int>().GetAll())
                                        .Where(gm => gm.GroupId == currentId)
                                        .Select(gm => new GroupMessageDto
                                        {
                                            From = new User() { Gmail = gm.From.Gmail },
                                            Message = gm.Message,
                                            RightOrLeft = gm.FromId == User.Id ? 1 : 0,
                                            Date = gm.Date,
                                            MessageForVisual = gm.Message + " " + gm.Date.ToString("HH:mm"),
                                        })
                                        .ToListAsync();

            if (messages?.Count > GroupMessages?.Count && tempId == currentId)
            {
                GroupMessages.Add(new()
                {
                    Message = messages.Last().Message,
                    MessageForVisual = messages.Last().MessageForVisual,
                    RightOrLeft = messages.Last().RightOrLeft,
                    Date = messages.Last().Date,
                });
                if (GroupMessages.Count != 0)
                    ((ListView)grid.FindName("GroupsList")).ScrollIntoView(GroupMessages?.Last());
                if (CheckAddForGroupMessage)
                    Groups = new(Groups?.OrderByDescending(u => u.Messages?.Last()?.Date).ToList()!);
                CheckAddForGroupMessage = false;
            }
            else if (tempId != currentId)
            {
                CheckAddForGroupMessage = true;
                GroupMessages = new(messages);
                LoadingVisibilityChat = Visibility.Hidden;
                if (GroupMessages.Count != 0)
                    ((ListView)grid.FindName("GroupsList")).ScrollIntoView(GroupMessages.Last());
            }

            tempId = currentSelectedGroupId;

        }
        #endregion
        #region Async Methods
        private async Task GetLastMessages()
        {
            var ids = new List<int>();
            foreach (var item in users)
                ids.Add(item.Id);

            var Lastmessages = await (unitOfWork.GetRepository<Message, int>().GetAll())
              .Where(message => message.From.Id == User.Id && ids.Contains(message.ToId)
                              || message.To.Id == User.Id && ids.Contains(message.FromId))
              .Include(x => x.From)
              .Include(y => y.To)
              .GroupBy(m => User.Id == m.FromId ? m.ToId : m.FromId)
              .Select(g => g.OrderByDescending(m => m.Date).FirstOrDefault())
              .ToListAsync();
            foreach (var message in Lastmessages)
                foreach (var user in users)
                {
                    if (message.FromId == user.Id || message.ToId == user.Id)
                    {
                        user.LastMessage = message.message;
                        user.LastMessageDate = message.Date;
                    }

                }
            if (!timer.IsEnabled || !checkTimer)
                Users = new(Users.OrderByDescending(u => u.LastMessageDate).ToList());

        }




        private async Task GetUsers()
        {
            timerForGroup?.Stop();
            timer?.Stop();
            Users = new(UserMapper.Map<List<UserDto>>(await unitOfWork.GetRepository<User, int>()
                .GetAllListAsync()).Where(u => u.Id != User.Id));
            await GetLastMessages();
            LoadingVisibility = Visibility.Hidden;
        }
        class d
        {
            public DateTime date { get; set; }
        }
        public async Task TrickerDataBase()
        {
            int currentid = currentSelectedUserId;
            bool getlastmessage = true;
            checkTimer = true;
            #region comment
            //var data = await (unitOfWork.GetRepository<UserConnection, int>().GetAll())
            //                .Where(c => c.FromId == User.Id && c.ToId == currentid
            //                    || c.ToId == User.Id && c.FromId == currentid)
            //                    .Select(c => new d
            //                    {
            //                        date = c.FromId == User.Id ? c.FromConnectedDate : c.ToConnectedDate
            //                    })
            //                .FirstOrDefaultAsync();

            //if (data is null)
            //    data = new() { date = DateTime.Parse("12/12/2002") };


            //var messages = await (unitOfWork.GetRepository<Message, int>().GetAll())
            //                    .Where(message => (message.Date >= data.date) &&
            //                     (message.From.Id == User.Id && message.To.Id == currentid ||
            //                     message.To.Id == User.Id && message.From.Id == currentid))
            //      .Include(x => x.From)
            //      .Include(y => y.To)
            //      .Select(x => new MessageDto
            //      {
            //          RightOrLeft = x.FromId == currentid ? 0 : 1,
            //          message = x.message,
            //          Date = x.Date,
            //          MessageForVisual = x.message + " " + x.Date.ToString("HH:mm"),
            //          From = new UserDto { Gmail = x.From.Gmail },
            //      })
            //      .OrderBy(m => m.Date)
            //      .ToListAsync();
            #endregion

            var messages = await (unitOfWork.GetRepository<Message, int>().GetAll())
                               .Where(message =>
                                (message.From.Id == User.Id && message.To.Id == currentid ||
                                message.To.Id == User.Id && message.From.Id == currentid))
                 .Include(x => x.From)
                 .Include(y => y.To)
                 .Select(x => new MessageDto
                 {
                     RightOrLeft = x.FromId == currentid ? 0 : 1,
                     message = x.message,
                     Date = x.Date,
                     MessageForVisual = x.message + " " + x.Date.ToString("HH:mm"),
                     From = new UserDto { Gmail = x.From.Gmail },
                 })
                 .OrderBy(m => m.Date)
                 .ToListAsync();

            if (tempId == currentSelectedUserId && messages.Count > Messages.Count)
            {
                getlastmessage = false;
                Messages.Add(new MessageDto()
                {
                    message = messages.Last().message,
                    MessageForVisual = messages.Last().MessageForVisual,
                    RightOrLeft = messages.Last().RightOrLeft,
                    Date = messages.Last().Date,
                });
                checkTimer = false;
                //Users = new(Users.OrderByDescending(u => u.LastMessageDate).ToList());
                ((ListView)grid.FindName("list2")).ScrollIntoView(Messages.Last());
            }

            else  if(tempId!= currentSelectedUserId)
            {
                LoadingVisibilityChat = Visibility.Hidden;
                Messages = new(messages);
                if (messages.Count != 0 && grid is not null)
                {
                    ((ListView)grid.FindName("list2")).
                        ScrollIntoView(((ListView)grid.FindName("list2")).
                        Items[((ListView)grid.FindName("list2")).Items.Count - 1]);
                }
            }
            if (!getlastmessage)
                await GetLastMessages();
            tempId = currentid;
            checkTimer = false;

        }
        #endregion


    }
}

