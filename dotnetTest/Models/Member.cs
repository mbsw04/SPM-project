using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetTest.Models;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? MemberId { get; set; }
    
    public string? UserId { get; set; }
    public string? TaskId {get; set;}
    
    public string? UserName { get; set; }
    
    public string? MemberFirstName { get; set; }
    public string? MemberLastName { get; set; }
    
    [Required]
    public string? MemberRole{ get; set; }

}