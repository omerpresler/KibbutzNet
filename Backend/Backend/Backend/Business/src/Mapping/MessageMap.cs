using Backend.Business.src.Utils;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class MessageMap : ClassMap<Message<string>>
{
    public MessageMap()
    {
        Table("message");
        Id(m => m.messageID);
        Map(m => m.message, "content");
        Map(m => m.date);
        References(m => m.sender, "userId");
    }
}