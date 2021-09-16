using System;
using System.Collections.Generic;

namespace Domain
{
    public class Channel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }   
        public ICollection<Message> Messages { get; set; }
        public ChannelType ChannelType { get; set; }     
    }
}
