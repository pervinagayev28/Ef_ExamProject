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
            MyStatusCommand = new Command(ExecuteMyStatusCommand);
            MouseEnteredCommand = new Command(ExecuteMouseEneterdCommand);
            MouseLeftCommand = new Command(ExecuteMouseLeftCommand);
            ProfileCommand = new Command(ExecuteProfileCommand);
            ChangeColorCommand = new Command(ExecuteChangeColorCommand);

            //Async Commands Initialize
            CreateGroupCommand = new CommandAsync(ExecuteCreateGroupCommand, CanExecuteCreateGroupCommand);
            SendMessageCommand = new CommandAsync(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
            GetGroupsCommand = new Command(ExecuteGetGroupsCommandAsync, CanExecuteGetGroupsCommandAsync);
            OnlyChatUsersCommand = new CommandAsync(ExecuteOnlyChatUsersCommand, CanExecuteOnlyChatUsersCommand);
            DeleteCommand = new CommandAsync(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            DeleteStatusCommand = new CommandAsync(ExecuteDeleteStatusCommand, CanExecuteDeleteStatusCommand);
            AddStatusCommand = new CommandAsync(ExecuteAddStatusCommand);
            SelectedUserStatusCommand = new CommandAsync(ExecutSelectedUserStatusCommand);
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
            timerForGroup?.Stop();
            //timer?.Stop();
            grid = (Grid)arg;
            currentSelectedGroupId = (Groups[(int)((ListView)grid.FindName("listgroups")).SelectedIndex] as Group)!.Id;
            timerForGroup?.Start();
            //timer?.Start();
        }

        private bool CanExecuteCreateGroupCommand(object obj) =>
            ((ListView)obj)?.SelectedIndex != -1;

        private async Task ExecuteCreateGroupCommand(object obj)
        {
            timer?.Stop();
            if (Groups is null)
                Groups = new(await (await unitOfWork.GetRepository<Group, int>().GetAll()).Include(g => g.GroupAndUsers).ToListAsync());
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

        private async Task ExecutSelectedUserStatusCommand(object arg)
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

        private async Task ExecuteOnlyChatUsersCommand(object obj)
        {
            ((ListView)((Grid)obj).FindName("list")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("list2")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("GroupsList")).Visibility = Visibility.Hidden;
            ((ListView)((Grid)obj).FindName("listgroups")).Visibility = Visibility.Hidden;
            timerForGroup?.Stop();
            timer?.Start();
            check = false;
            await GetUsers();
        }


        private async void ExecuteGetGroupsCommandAsync(object obj)
        {
            timer?.Stop();
            check = true;
            ((ListView)((Grid)obj).FindName("GroupsList")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("listgroups")).Visibility = Visibility.Visible;
            ((ListView)((Grid)obj).FindName("list")).Visibility = Visibility.Hidden;
            ((Button)((Grid)obj).FindName("SendMessageToGroupCommand")).Visibility = Visibility.Visible;
            ((Button)((Grid)obj).FindName("SendMEssageCommand")).Visibility = Visibility.Hidden;
            ((ListView)((Grid)obj).FindName("list2")).Visibility = Visibility.Hidden;
            Groups = new(await (await unitOfWork.GetRepository<Group, int>().GetAll())
                .Include(g => g.GroupAndUsers)!.ThenInclude(u => u.User)
                .Where(g => g.GroupAndUsers.Any(gu => gu.UserId == User.Id))
                .ToListAsync()!)!;
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
            if (currentSelectedUserId != currentSelectedIdTemp)
                Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
            currentSelectedIdTemp = currentSelectedUserId;
            //await GetUsers();
            timer?.Start();
            ((TextBox)obj).Text = "";
        }

        #endregion
        #region Command

        private void ExecuteMyStatusCommand(object obj)
        {
            CanDeleteStatus = true;
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
            //timerForGroup?.Stop();
            checkStatus = true;
            //timer?.Stop();
            grid = (Grid)obj;
            currentSelectedUserId = (Users[(int)((ListView)grid.FindName("list")).SelectedIndex] as UserDto)!.Id;
            //timer?.Start();
        }


        #endregion
        #region ChangeVideoPath
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

        #endregion
        #region StartUp
        private async void start(string Gmail)
        {
            User = UserMapper.Map<UserDto>(await (await unitOfWork.GetRepository<User, int>().GetAll())
                                 .Include(u => u.Status)
                                .Include(u => u.ConnectionFroms)
                                .Include(u => u.ConnectionTos)
                                .FirstOrDefaultAsync(u => u.Gmail == Gmail)!);
            ChangeVideoPath(User.Status);
            Thread th = new(GetUsersTest);
            th.Start();
            //await GetUsers();
            //connections = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += async (sender, e) => await TrickerDataBase();

            timerForGroup = new DispatcherTimer();
            timerForGroup.Interval = TimeSpan.FromSeconds(1);
            timerForGroup.Tick += async (sender, e) => await TrickerDataBaseForGroup();


        }

        private async void GetUsersTest(object? obj)
        {
            await GetUsers();
            await GetLastMessages();
            connections = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();

        }

        private async Task TrickerDataBaseForGroup()
        {
            timer?.Stop();
            var messages = await (await unitOfWork.GetRepository<GroupMessages, int>().GetAll())
                                        .Where(gm => gm.GroupId == currentSelectedGroupId)
                                        .Select(gm => new GroupMessageDto
                                        {
                                            From = new User() { Gmail = gm.From.Gmail },
                                            Message = gm.Message,
                                            RightOrLeft = gm.FromId == User.Id ? 1 : 0,
                                            Date = gm.Date,
                                            MessageForVisual = gm.Message + " " + gm.Date.ToString("HH:mm"),
                                        })
                                        .ToListAsync();

            if (messages?.Count > GroupMessages?.Count && tempId == currentSelectedGroupId)
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
            else if (tempId != currentSelectedGroupId)
            {
                CheckAddForGroupMessage = true;
                GroupMessages = new(messages);
                if (GroupMessages.Count != 0)
                    ((ListView)grid.FindName("GroupsList")).ScrollIntoView(GroupMessages.Last());
            }

            tempId = currentSelectedGroupId;

        }
        #endregion
        #region Async Methods
        private async Task GetLastMessages()
        {
            timerForGroup?.Stop();
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
            if (temp < (users?.FirstOrDefault(u => u!.Id == currentSelectedUserId) as UserDto)?.LastMessageDate || timer is null)
                Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
            temp = (users?.FirstOrDefault(u => u?.Id == currentSelectedUserId) as UserDto)?.LastMessageDate;
            connections = await (await unitOfWork.GetRepository<UserConnection, int>().GetAll()).ToListAsync();
            timer?.Start();
        }




        private async Task GetUsers()
        {
            timerForGroup?.Stop();
            timer?.Stop();
            Users = new(UserMapper.Map<List<UserDto>>(await (await unitOfWork.GetRepository<User, int>().GetAll())
                                                                                             .Where(u => u.Id != User!.Id)
                                                                                             .ToListAsync()));
            LoadingVisibility = Visibility.Hidden;
            await GetLastMessages();
            //timer?.Start();
        }
        class d
        {
            public DateTime date { get; set; }
        }
        public async Task TrickerDataBase()
        {
            bool getlastmessage = true;
            checkTimer = true;
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
                  .Select(x => new MessageDto
                  {
                      RightOrLeft = x.FromId == currentSelectedUserId ? 0 : 1,
                      message = x.message,
                      Date = x.Date,
                      MessageForVisual = x.message + " " + x.Date.ToString("HH:mm"),
                      From = new UserDto { Gmail = x.From.Gmail },
                  })
                  .OrderBy(m => m.Date)
                  .ToListAsync();


            if (tempId == currentSelectedUserId)
            {
                if (messages.Count != 0 && Messages.Count == 0)
                {
                    getlastmessage = false;
                    Messages.Add(new MessageDto()
                    {
                        message = messages.Last().message,
                        MessageForVisual = messages.Last().MessageForVisual,
                        RightOrLeft = messages.Last().RightOrLeft,
                        Date = messages.Last().Date,
                    });

                    ((ListView)grid.FindName("list2")).ScrollIntoView(Messages.Last());
                    if (CheckMessage)
                        Users = new(Users.OrderByDescending(u => u?.LastMessageDate).ToList());
                    CheckMessage = false;
                }
                if (messages.Count != 0 && Messages.Count != 0)
                {
                    if (Messages?.Last()?.Date != messages?.Last()?.Date)
                    {
                        Messages.Add(new MessageDto()
                        {
                            message = messages.Last().message,
                            MessageForVisual = messages.Last().MessageForVisual,
                            RightOrLeft = messages.Last().RightOrLeft,
                            Date = messages.Last().Date,
                        });

                        ((ListView)grid.FindName("list2")).ScrollIntoView(Messages.Last());
                    }
                }

            }
            else
            {
                getlastmessage = false;
                CheckMessage = true;
                Messages = new(messages);
                if (grid is not null)
                    ((ListView)grid.FindName("list2")).ScrollIntoView(Messages);

                if (messages.Count != 0 && grid is not null)
                {
                    ((ListView)grid.FindName("list2")).
                        ScrollIntoView(((ListView)grid.FindName("list2")).
                        Items[((ListView)grid.FindName("list2")).Items.Count - 1]);
                }
            }
            if (!getlastmessage)
                await GetLastMessages();
            tempId = currentSelectedUserId;
            checkTimer = false;

        }
        #endregion


    }
}

