using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;

namespace RMS.DataLayer.RmsDb;

public partial class RmsContext : DbContext
{
    public RmsContext()
    {
    }

    public RmsContext(DbContextOptions<RmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCapacity> EmployeeCapacities { get; set; }

    public virtual DbSet<EmployeePriority> EmployeePriorities { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }

    public virtual DbSet<LeaveTransaction> LeaveTransactions { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TaskProgress> TaskProgresses { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<WorkCalendar> WorkCalendars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=New_RMS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>

        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F2808815B");

            entity.ToTable("employees", tb => tb.HasTrigger("trg_employees_Update_UpdatedAt"));
           /*
            entity.ToTable("employees", tb =>
            {
                tb.HasTrigger("trg_employees_Update_UpdatedAt");
            });
*/
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F962105F8");


            entity.HasIndex(e => e.Email, "uq_employees_email").IsUnique();

            entity.HasIndex(e => e.Username, "uq_employees_username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .HasColumnName("password_hash");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Aktif")
                .HasColumnName("status");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TitleId).HasColumnName("title_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserRole)
                .HasMaxLength(250)
                .HasColumnName("user_role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Team).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_team_id");

            entity.HasOne(d => d.Title).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_title_id");
        });

        modelBuilder.Entity<EmployeeCapacity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83FAC83F20A");

            entity.ToTable("employee_capacities");

            entity.HasIndex(e => new { e.EmployeeId, e.ProjectId }, "uq_employee_capacities_employee_project").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCapacities)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_employee_capacities_employee_id");

            entity.HasOne(d => d.Project).WithMany(p => p.EmployeeCapacities)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("fk_employee_capacities_project_id");
        });

        modelBuilder.Entity<EmployeePriority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F9119EC33");

            entity.ToTable("employee_priorities");

            entity.HasIndex(e => e.PriorityName, "uq_employee_priorities_priority_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(200)
                .HasColumnName("priority_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_re__3213E83FF93B9455");

            entity.ToTable("leave_requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.LeaveStatusId).HasColumnName("leave_status_id");
            entity.Property(e => e.LeaveTypeId)
                .HasColumnName("leave_type_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TotalDays).HasColumnName("total_days");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_leave_requests_employee_id");

            entity.HasOne(d => d.LeaveType)
                .WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_leave_requests_leave_status_id")
                .HasConstraintName("fK_leave_requests_leave_types"); // Bu sonradan eklendi bozarsa bunu silicez
        });

        modelBuilder.Entity<LeaveStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_st__3213E83FE5F6C025");

            entity.ToTable("leave_statuses");

            entity.HasIndex(e => e.LeaveStatusName, "uq_leave_statuses_leave_status_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.LeaveStatusName)
                .HasMaxLength(50)
                .HasColumnName("leave_status_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LeaveTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_tr__3213E83F4E5EF82D");

            entity.ToTable("leave_transactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LeaveRequestId).HasColumnName("leave_request_id");
            entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");
            entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType).HasColumnName("transaction_type");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_leave_transactions_employee_id");

            entity.HasOne(d => d.LeaveRequest).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveRequestId)
                .HasConstraintName("fk_leave_transactions_leave_request_id");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveTransactions_LeaveTypes");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_ty__3213E83FFD812BE3");

            entity.ToTable("leave_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DefaultDays).HasColumnName("default_days");
            entity.Property(e => e.IsPaid).HasColumnName("is_paid");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prioriti__3213E83F2E773FB6");

            entity.ToTable("priorities");

            entity.HasIndex(e => e.PriorityName, "uq_priorities_priority_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(200)
                .HasColumnName("priority_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__projects__3213E83FFB670F72");

            entity.ToTable("projects");

            entity.HasIndex(e => e.ProjectName, "uq_projects_project_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ProjectDescription).HasColumnName("project_description");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(500)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStatusId).HasColumnName("project_status_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Priority).WithMany(p => p.Projects)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_projects_priority_id");

            entity.HasOne(d => d.ProjectStatus).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_projects_project_status_id");
        });

        modelBuilder.Entity<ProjectEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83F1DFBE105");

            entity.ToTable("project_employees");

            entity.HasIndex(e => new { e.ProjectId, e.EmployeeId }, "uq_project_employees_project_employee").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeePriorityId).HasColumnName("employee_priority_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectRoleId).HasColumnName("project_role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_project_employees_employee_id");

            entity.HasOne(d => d.EmployeePriority).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.EmployeePriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_project_employees_employee_priority_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("fk_project_employees_project_id");

            entity.HasOne(d => d.ProjectRole).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.ProjectRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_project_employees_project_role_id");
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83FD6EE4040");

            entity.ToTable("project_roles");

            entity.HasIndex(e => e.ProjectRoleName, "uq_project_roles_project_role_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectRoleName)
                .HasMaxLength(500)
                .HasColumnName("project_role_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83F7BACF03C");

            entity.ToTable("project_statuses");

            entity.HasIndex(e => e.ProjectStatus1, "uq_project_statuses_project_status").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectStatus1)
                .HasMaxLength(250)
                .HasColumnName("project_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83F2E183CDE");

            entity.ToTable("project_tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TaskName)
                .HasMaxLength(250)
                .HasColumnName("task_name");
            entity.Property(e => e.TaskStatus)
                .HasMaxLength(100)
                .HasColumnName("task_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("fk_project_tasks_project_id");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task_ass__3213E83FDA64F04F");

            entity.ToTable("task_assignments");

            entity.HasIndex(e => new { e.TaskId, e.EmployeeId }, "uq_task_assignments_task_employee").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.AssignedHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("assigned_hours");

            // --- YENİ EKLENEN KOLONLAR BURADA ---
            entity.Property(e => e.CompletedHours)
                .HasColumnName("completed_hours")
                .HasColumnType("decimal(6, 2)")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.LeftHours)
                .HasColumnName("left_hours")
                .HasColumnType("decimal(7, 2)")
                .HasComputedColumnSql("([assigned_hours]-[completed_hours])", false);

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(50)
                .HasDefaultValueSql("('Başlamadı')");
            // ------------------------------------

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_task_assignments_employee_id");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("fk_task_assignments_task_id");
        });

        modelBuilder.Entity<TaskProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task_pro__3213E83FF9DC1483");

            entity.ToTable("task_progresses");

            entity.HasIndex(e => e.TaskAssignmentId, "uq_task_progresses_task_assignment_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletedHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("completed_hours");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.LeftHours)
                .HasComputedColumnSql("([total_hours]-[completed_hours])", true)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("left_hours");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Başlamadı")
                .HasColumnName("status");
            entity.Property(e => e.TaskAssignmentId).HasColumnName("task_assignment_id");
            entity.Property(e => e.TotalHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("total_hours");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.TaskAssignment).WithOne(p => p.TaskProgress)
                .HasForeignKey<TaskProgress>(d => d.TaskAssignmentId)
                .HasConstraintName("fk_task_progresses_task_assignment_id");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teams__3213E83F000A45CD");

            entity.ToTable("teams");

            entity.HasIndex(e => e.TeamName, "uq_teams_team_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.TeamName)
                .HasMaxLength(250)
                .HasColumnName("team_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__titles__3213E83F2FB786E6");

            entity.ToTable("titles");

            entity.HasIndex(e => e.TitleName, "uq_titles_title_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.TitleName)
                .HasMaxLength(500)
                .HasColumnName("title_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<WorkCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_cal__3213E83F7DB799D6");

            entity.ToTable("work_calendars");

            entity.HasIndex(e => new { e.EmployeeId, e.CalendarDate }, "uq_work_calendars_employee_date").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CalendarDate).HasColumnName("calendar_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsWorkingDay).HasColumnName("is_working_day");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkingHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("working_hours");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkCalendars)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_WorkCalendars_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
