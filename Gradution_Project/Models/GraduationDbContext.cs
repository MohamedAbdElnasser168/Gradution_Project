using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Gradution_Project.Models
{
    public class GraduationDbContext: IdentityDbContext<ApplicationUser>
    {
        //public GraduationDbContext(DbContextOptions<GraduationDbContext> options)
        //: base(options)
        //{
        //}


        public GraduationDbContext(DbContextOptions<GraduationDbContext> options)
            : base(options)
        {
        }
        

        // =========================
        // Core
        // =========================
        public DbSet<User> Users { get; set; }
        public DbSet<ActivityLevel> ActivityLevels { get; set; }
        public DbSet<UserGoal> UserGoals { get; set; }
        public DbSet<DailyStat> DailyStats { get; set; }

        // =========================
        // Meals
        // =========================
        public DbSet<Meal> Meals { get; set; }
        public DbSet<UserMealPlan> UserMealPlans { get; set; }

        // =========================
        // Workouts
        // =========================
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserWorkoutPlan> UserWorkoutPlans { get; set; }

        // =========================
        // Restaurants
        // =========================
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }
        public DbSet<RestaurantReview> RestaurantReviews { get; set; }

        // =========================
        // Notifications & Devices
        // =========================
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Device> Devices { get; set; }

        // =========================
        // AI
        // =========================
        public DbSet<AIRequestLog> AIRequestLogs { get; set; }
        public DbSet<AIResponseLog> AIResponseLogs { get; set; }
        public DbSet<AIDietRecommendation> AIDietRecommendations { get; set; }
        public DbSet<AIWorkoutRecommendation> AIWorkoutRecommendations { get; set; }
        public DbSet<AIModel> AIModels { get; set; }
        public DbSet<AIModelExecution> AIModelExecutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // User
            // =========================
            modelBuilder.Entity<User>()
                .HasOne(u => u.ActivityLevel)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.ActivityLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Goal)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GoalId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // DailyStats
            // =========================
            modelBuilder.Entity<DailyStat>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailyStats)
                .HasForeignKey(d => d.UserId);

            // =========================
            // UserMealPlan
            // =========================
            modelBuilder.Entity<UserMealPlan>()
                .HasOne(ump => ump.User)
                .WithMany(u => u.MealPlans)
                .HasForeignKey(ump => ump.UserId);

            modelBuilder.Entity<UserMealPlan>()
                .HasOne(ump => ump.Meal)
                .WithMany(m => m.UserMealPlans)
                .HasForeignKey(ump => ump.MealId);

            // =========================
            // UserWorkoutPlan
            // =========================
            modelBuilder.Entity<UserWorkoutPlan>()
                .HasOne(uwp => uwp.User)
                .WithMany(u => u.WorkoutPlans)
                .HasForeignKey(uwp => uwp.UserId);

            modelBuilder.Entity<UserWorkoutPlan>()
                .HasOne(uwp => uwp.Exercise)
                .WithMany(e => e.UserWorkoutPlans)
                .HasForeignKey(uwp => uwp.ExerciseId);

            // =========================
            // Restaurants
            // =========================
            modelBuilder.Entity<RestaurantMenu>()
                .HasOne(rm => rm.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(rm => rm.RestaurantId);

            modelBuilder.Entity<RestaurantReview>()
                .HasOne(rr => rr.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(rr => rr.RestaurantId);

            // =========================
            // Notifications
            // =========================
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId);

            // =========================
            // Devices
            // =========================
            modelBuilder.Entity<Device>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            // =========================
            // AI Logs
            // =========================
            modelBuilder.Entity<AIRequestLog>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<AIResponseLog>()
                .HasOne(r => r.Request)
                .WithOne(q => q.Response)
                .HasForeignKey<AIResponseLog>(r => r.RequestId);

            // =========================
            // AI Recommendations
            // =========================
            modelBuilder.Entity<AIDietRecommendation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<AIDietRecommendation>()
                .HasOne<Meal>()
                .WithMany()
                .HasForeignKey(r => r.MealId);

            modelBuilder.Entity<AIWorkoutRecommendation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<AIWorkoutRecommendation>()
                .HasOne<Exercise>()
                .WithMany()
                .HasForeignKey(r => r.ExerciseId);

            // =========================
            // AI Model Execution
            // =========================
            modelBuilder.Entity<AIModelExecution>()
                .HasOne<AIModel>()
                .WithMany()
                .HasForeignKey(e => e.ModelId);

            modelBuilder.Entity<AIModelExecution>()
                .HasOne<AIRequestLog>()
                .WithMany()
                .HasForeignKey(e => e.RequestId);
        }
    }
}
