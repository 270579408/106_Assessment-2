using System;
using System.Collections.Generic;
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
using MongoDB.Driver;
using System.Windows.Media;

namespace _106_Assessment_2.View.Pages.Admin
{
        public partial class AdminDashboardPage : Page
        {
            private readonly IMongoCollection<UserModel> usersCollection;
            private readonly IMongoCollection<EventModel> eventsCollection;
            private readonly IMongoCollection<MessageModel> messagesCollection;

            public AdminDashboardPage()
            {
                InitializeComponent();

                var client = new MongoClient("YOUR_MONGODB_CONNECTION");
                var db = client.GetDatabase("OnewheroBay");

                usersCollection = db.GetCollection<UserModel>("users");
                eventsCollection = db.GetCollection<EventModel>("events");
                messagesCollection = db.GetCollection<MessageModel>("messages");

                LoadDashboardData();
            }

            private void LoadDashboardData()
            {
                // 1. New members (today)
                var today = DateTime.Today;
                var newMembers = usersCollection.Find(u => u.CreatedDate >= today).ToList();
                NewMembersText.Text = newMembers.Count.ToString();

                // 2. Total visitors
                var total = usersCollection.CountDocuments(_ => true);
                TotalVisitorsText.Text = total.ToString();

                // 3. Active Events
                var activeEvents = eventsCollection.CountDocuments(ev => ev.Date >= DateTime.Today);
                ActiveEventsText.Text = activeEvents.ToString();

                LoadUpcomingEvents();
                LoadRecentMessages();
            }

            private void LoadUpcomingEvents()
            {
                var events = eventsCollection.Find(ev => ev.Date >= DateTime.Today)
                                             .SortBy(ev => ev.Date)
                                             .Limit(10)
                                             .ToList();

                UpcomingEventsPanel.Children.Clear();

                foreach (var ev in events)
                {
                    var card = new Border
                    {
                        Width = 260,
                        Margin = new Thickness(0, 0, 20, 0),
                        Padding = new Thickness(20),
                        Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                        CornerRadius = new CornerRadius(12)
                    };

                    var stack = new StackPanel();

                    stack.Children.Add(new TextBlock
                    {
                        Text = ev.Name,
                        FontSize = 18,
                        FontWeight = FontWeights.Bold
                    });

                    stack.Children.Add(new TextBlock
                    {
                        Text = ev.Date.ToString("MMM dd, yyyy"),
                        FontSize = 14,
                        Foreground = Brushes.Gray,
                        Margin = new Thickness(0, 5, 0, 0)
                    });

                    stack.Children.Add(new TextBlock
                    {
                        Text = $"{ev.BookedCount} booked",
                        FontSize = 14,
                        Foreground = Brushes.DimGray,
                        Margin = new Thickness(0, 8, 0, 0)
                    });

                    card.Child = stack;
                    UpcomingEventsPanel.Children.Add(card);
                }
            }

            private void LoadRecentMessages()
            {
                var msgs = messagesCollection.Find(_ => true)
                                             .SortByDescending(m => m.Time)
                                             .Limit(5)
                                             .ToList();

                MessagesPanel.Children.Clear();

                foreach (var msg in msgs)
                {
                    var txt = new TextBlock
                    {
                        Text = $"• {msg.Name}: {msg.Content}",
                        FontSize = 16,
                        Margin = new Thickness(0, 5, 0, 5)
                    };

                    MessagesPanel.Children.Add(txt);
                }
            }
        }


        // ---------------- Models -----------------

        public class UserModel
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class EventModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public int BookedCount { get; set; }
        }

        public class MessageModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public DateTime Time { get; set; }
        }
    }

