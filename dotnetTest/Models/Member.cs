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
    //[BsonElement("profilePhotoUrl")]
    public string? ProfilePhotoUrl { get; set; }
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? MemberFirstName { get; set; }
    [Required]
    public string? MemberLastName { get; set; }
    
    public float? HoursContributing { get; set; }
    
    [Required]
    public string? MemberRole{ get; set; }

    public Member(string userId, string profilePhotoUrl, string username, string firstName, string lastName, float hours, string role)
    {
        UserId = userId;
        ProfilePhotoUrl = profilePhotoUrl;
        UserName = username;
        MemberFirstName = firstName;
        MemberLastName = lastName;
        HoursContributing = hours;
        MemberRole = role;
    }

}