using Hangfire;
using WebApiEsp32Master.Context;

namespace WebApiEsp32Master.Domain;

public static class TaskDeletionScheduler
{
    public static void ScheduleTaskDeletion(IServiceProvider serviceProvider)
    {
        var backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
        backgroundJobClient.Schedule(() => DeleteTasks(serviceProvider), TimeSpan.FromDays(1)); // Programa la tarea para que se ejecute a las 12 am todos los días
    }

    public static void DeleteTasks(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ZonaDbContext>();

            var today = DateTime.Today;
            var tasksToDelete = dbContext.Tasks.Where(t => t.EndTime < today).ToList();

            dbContext.Tasks.RemoveRange(tasksToDelete);
            dbContext.SaveChanges();
        }
    }
}