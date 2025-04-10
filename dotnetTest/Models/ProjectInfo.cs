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
    public string? Id { get; set; }
    
    [Required]
    public string ProjectName  { get; set; }
    
    [Required]
    public string ProjectDueDate  { get; set; }
    
    [Required]
    public string ProjectDescription  { get; set; }
    
    [Required]
    public bool ProjectActive { get; set; }
    
    [Required]
    public string ProjectManagerName  { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one member is required.")]
    public List<string> Members  { get; set; } 
    
    /*
    [Required]  // Ensures at least one task is provided
    [MinLength(1, ErrorMessage = "At least one task is required.")]
    */
    public List<string> Tasks { get; set; }
    
    /*
    [Required]  // Ensures at least one task is provided
    [MinLength(1, ErrorMessage = "At least one task is required.")]
    public List<string> Risks { get; set; }
    */
    
    public override string ToString()
    {
        return ProjectName + " - " + ProjectDescription;
    }
}