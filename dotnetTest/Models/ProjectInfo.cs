using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace dotnetTest.Models;

public class ProjectInfo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    //[BsonIgnoreIfDefault]
    public string? ProjectId { get; set; }
    
    [Required]
    public string ProjectName  { get; set; }
    
    [Required]
    public string ProjectDescription  { get; set; }
    
    [Required]  // Ensures at least one task is provided
    [MinLength(1, ErrorMessage = "At least one task is required.")]
    public List<string> Tasks { get; set; }
    
    
    public override string ToString()
    {
        return ProjectName + " - " + ProjectDescription;
    }
}