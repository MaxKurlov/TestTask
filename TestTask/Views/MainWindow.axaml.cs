using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using TestTask.Services;
using TestTask.Models;

namespace TestTask.Views
{
    public partial class MainWindow : Window
    {
        ListBox _songsList;

        public MainWindow()
        {
            InitializeComponent();
            _songsList= this.FindControl<ListBox>("songsList");
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.FindControl<Button>("btn1").Click += Btn1_Click;
        }
        
        private void Btn1_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            PlaylistService p = new PlaylistService();
            var response = p.GetHtml(this.FindControl<TextBox>("playlistLink").Text);
            List<Track> MyItems = p.GetSongs(response);
            this.FindControl<ListBox>("songs").Items = MyItems;
            this.FindControl<TextBlock>("playName").Text = p.GetPlaylistName(response);
            this.FindControl<Image>("Poster").Source = p.SetImage(response);
            this.FindControl<StackPanel>("result").IsVisible = true;
            
        }
    }
}
