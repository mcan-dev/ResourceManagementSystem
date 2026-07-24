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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>

        {
            entity.ToTable("employees", tb =>
            {
                tb.HasTrigger("trg_employees_Update_UpdatedAt");
            });

            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F962105F8");


            entity.HasIndex(e => e.Email, "UQ__employee__AB6E6164CCBB0671").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__employee__F3DBC572D88B847A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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
                .HasDefaultValue("aktif")
                .HasColumnName("status");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TitleId).HasColumnName("title_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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
                .HasConstraintName("FK__employees__team___53385258");

            entity.HasOne(d => d.Title).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__employees__title__542C7691");
        });

        modelBuilder.Entity<EmployeeCapacity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83FA864FB40");

            entity.ToTable("employee_capacities");

            entity.HasIndex(e => new { e.EmployeeId, e.ProjectId }, "UQ__employee__2EE992487A808E67").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCapacities)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__employee___emplo__65570293");

            entity.HasOne(d => d.Project).WithMany(p => p.EmployeeCapacities)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__employee___proje__6462DE5A");
        });

        modelBuilder.Entity<EmployeePriority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F6D267D99");

            entity.ToTable("employee_priorities");

            entity.HasIndex(e => e.PriorityName, "UQ__employee__76D480EAAB6F8D21").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PriorityLevel).HasColumnName("priority_level");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(200)
                .HasColumnName("priority_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_re__3213E83F36F2553D");

            entity.ToTable("leave_requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.LeaveStatusId).HasColumnName("leave_status_id");
            entity.Property(e => e.LeaveType)
                .HasMaxLength(250)
                .HasColumnName("leave_type");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TotalDays).HasColumnName("total_days");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__leave_req__emplo__6A1BB7B0");

            entity.HasOne(d => d.LeaveStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveStatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__leave_req__leave__6C040022");
        });

        modelBuilder.Entity<LeaveStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_st__3213E83F280C4C54");

            entity.ToTable("leave_statuses");

            entity.HasIndex(e => e.LeaveStatusName, "UQ__leave_st__C9C25CAA78884A0A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LeaveStatusName)
                .HasMaxLength(50)
                .HasColumnName("leave_status_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LeaveTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_tr__3213E83F59F82ACB");

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
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType).HasColumnName("transaction_type");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__leave_tra__emplo__2818EA29");

            entity.HasOne(d => d.LeaveRequest).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveRequestId)
                .HasConstraintName("FK__leave_tra__leave__2A01329B");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__leave_tra__leave__290D0E62");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__leave_ty__3213E83FF0341CA4");

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
            entity.HasKey(e => e.Id).HasName("PK__prioriti__3213E83F46B009D2");

            entity.ToTable("priorities");

            entity.HasIndex(e => e.PriorityName, "UQ__prioriti__76D480EAB4D9B293").IsUnique();

            entity.HasIndex(e => e.PriorityLevel, "UQ__prioriti__7B12610834B20850").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PriorityLevel).HasColumnName("priority_level");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(200)
                .HasColumnName("priority_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__projects__3213E83F7E5DBF40");

            entity.ToTable("projects");

            entity.HasIndex(e => e.ProjectName, "UQ__projects__4A0B0D69FD4226FA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(500)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStatusId).HasColumnName("project_status_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Priority).WithMany(p => p.Projects)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__projects__priori__5CC1BC92");

            entity.HasOne(d => d.ProjectStatus).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__projects__projec__5BCD9859");
        });

        modelBuilder.Entity<ProjectEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83FDB9F5C57");

            entity.ToTable("project_employees");

            entity.HasIndex(e => new { e.ProjectId, e.EmployeeId }, "UQ__project___202B7EA434274142").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeePriorityId).HasColumnName("employee_priority_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectRoleId).HasColumnName("project_role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__project_e__emplo__73A521EA");

            entity.HasOne(d => d.EmployeePriority).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.EmployeePriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__project_e__emplo__758D6A5C");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__project_e__proje__72B0FDB1");

            entity.HasOne(d => d.ProjectRole).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.ProjectRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__project_e__proje__74994623");
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83FD5907A3B");

            entity.ToTable("project_roles");

            entity.HasIndex(e => e.ProjectRoleName, "UQ__project___15905646650733C9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectRoleName)
                .HasMaxLength(500)
                .HasColumnName("project_role_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83F944C491C");

            entity.ToTable("project_statuses");

            entity.HasIndex(e => e.ProjectStatus1, "UQ__project___52CB9C1BBE9DAB22").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectStatus1)
                .HasMaxLength(250)
                .HasColumnName("project_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project___3213E83F76BC98D9");

            entity.ToTable("project_tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
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
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__project_t__proje__7A521F79");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task_ass__3213E83F679719DD");

            entity.ToTable("task_assignments");

            entity.HasIndex(e => new { e.TaskId, e.EmployeeId }, "UQ__task_ass__98C0F4364CD2C302").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("assigned_hours");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__task_assi__emplo__0A888742");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__task_assi__task___09946309");
        });

        modelBuilder.Entity<TaskProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task_pro__3213E83FBA84420A");

            entity.ToTable("task_progresses");

            entity.HasIndex(e => e.TaskAssignmentId, "UQ__task_pro__D3B56036B12C6FC1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletedHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("completed_hours");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LeftHours)
                .HasComputedColumnSql("([total_hours]-[completed_hours])", true)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("left_hours");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("not_started")
                .HasColumnName("status");
            entity.Property(e => e.TaskAssignmentId).HasColumnName("task_assignment_id");
            entity.Property(e => e.TotalHours)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("total_hours");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.TaskAssignment).WithOne(p => p.TaskProgress)
                .HasForeignKey<TaskProgress>(d => d.TaskAssignmentId)
                .HasConstraintName("FK__task_prog__task___113584D1");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teams__3213E83F4DF5BE95");

            entity.ToTable("teams");

            entity.HasIndex(e => e.TeamName, "UQ__teams__29E35E0CBEACC16A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TeamName)
                .HasMaxLength(250)
                .HasColumnName("team_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__titles__3213E83F68B0998E");

            entity.ToTable("titles");

            entity.HasIndex(e => e.TitleName, "UQ__titles__6C3300428D97A63F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TitleName)
                .HasMaxLength(500)
                .HasColumnName("title_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<WorkCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_cal__3213E83F7CCC9A77");

            entity.ToTable("work_calendars");

            entity.HasIndex(e => new { e.EmployeeId, e.CalendarDate }, "UQ__work_cal__4D82F8F1A514BF48").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CalendarDate).HasColumnName("calendar_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsWorkingDay).HasColumnName("is_working_day");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkingHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("working_hours");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkCalendars)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__work_cale__emplo__02E7657A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
