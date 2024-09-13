using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.Miscellaneous.Model
{
    public class GenericModal<T>
    {
        public EventCallback<T> Event { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public T Data { get; set; }
        public int Action { get; set; }

        public GenericModal(EventCallback<T> Event, string text, string title)
        {
            this.Event = Event;
            this.Text = text;
            this.Title = title;
        }
        public GenericModal(EventCallback<T> Event, string text, string title, T data, int action)
        {
            this.Event = Event;
            this.Text = text;
            this.Title = title;
            Data = data;
            Action = action;
        }

        public GenericModal(EventCallback<T> Event, string text, string title, int action)
        {
            this.Event = Event;
            this.Text = text;
            this.Title = title;
            Action = action;
        }

        public GenericModal(EventCallback<T> Event, string title, T data)
        {
            this.Event = Event;
            Title = title;
            Data = data;
        }

        public GenericModal(string title, T data)
        {
            Title = title;
            Data = data;
        }

        public GenericModal(EventCallback<T> Event)
        {
            this.Event = Event;
        }

        public GenericModal()
        {
            
        }
    }
}
