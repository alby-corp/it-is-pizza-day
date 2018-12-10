namespace ItIsPizzaDay.Client.Pages.ChatComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.JSInterop;
    using Services.Abstract;
    using Services.Statics;

    public class ChatComponent : BlazorComponent, IDisposable
    {
        protected ElementRef scrollArea;

        [Inject]
        IAlby alby { get; set; }

        readonly CancellationTokenSource disposed = new CancellationTokenSource();
        readonly Channel<string> messageQueue = Channel.CreateUnbounded<string>();
        
        readonly Random random = new Random();

        protected bool IsOpen { get; set; }
        protected bool WasOpenedAtLeastOnce { get; set; }
        protected bool IsTyping { get; set; }
        protected string Text { get; set; } = string.Empty;

        protected ICollection<ChatMessage> Messages { get; } = new List<ChatMessage>();

        protected ChatComponent()
        {
            _ = Run(disposed.Token).IgnoreCancellation();
        }

        async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var text = await messageQueue.Reader.ReadAsync(cancellationToken);
                
                await Interop.Dom.ScrollToBottom(scrollArea);
                
                var response = alby.Reply(text);
                var delay = TimeSpan.FromMilliseconds(response.Length * 80);
                
                await Task.Delay(TimeSpan.FromMilliseconds(Math.Floor(random.NextDouble() * 300) + 300), cancellationToken);

                IsTyping = true;
                StateHasChanged();

                await Task.Delay(delay, cancellationToken);

                IsTyping = false;

                Messages.Add(new ChatMessage(Sender.Alby, response));
                StateHasChanged();

                await Interop.Dom.ScrollToBottom(scrollArea);
            }
        }

        protected void Send(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            Text = string.Empty;

            Messages.Add(new ChatMessage(Sender.User, text));

            messageQueue.Writer.TryWrite(text);
        }

        protected void Close()
        {
            IsOpen = false;
        }

        protected void Open()
        {
            IsOpen = true;
            WasOpenedAtLeastOnce = true;
        }

        public void Dispose()
        {
            disposed.Cancel();
            disposed.Dispose();
        }
    }

    public enum Sender
    {
        Alby,
        User
    }

    public class ChatMessage
    {
        public Sender Sender { get; }
        public string Text { get; }

        public ChatMessage(Sender sender, string text)
        {
            Sender = sender;
            Text = text;
        }
    }
}