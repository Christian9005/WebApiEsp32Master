using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WebApiEsp32Master.Domain.Dtos;

public class TaskDto
{
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public int? PersonId { get; set; }
    public int? GroupId { get; set; }
    public int? TagId { get; set; }
}