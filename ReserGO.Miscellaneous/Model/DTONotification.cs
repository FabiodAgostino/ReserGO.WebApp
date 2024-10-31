using ReserGO.Miscellaneous.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.Miscellaneous.Model
{
    public class DTONotification
    {
        public string Text { get; set; }
        public NotificationColor Color { get; set; }
        public string Position { get; set; }
        public bool Block { get; set; }

        public DTONotification()
        {
            
        }

        public DTONotification(string text, NotificationColor color, string position, bool block)
        {
            Text = text;
            Color = color;
            Position = position;
            Block = block;
        }
    }
}
