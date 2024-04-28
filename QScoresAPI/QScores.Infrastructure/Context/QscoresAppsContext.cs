using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QScores.Domain.QScoresAppsModels;

namespace QScores.Infrastructure.Context;

public partial class QscoresAppsContext : DbContext
{
    public QscoresAppsContext()
    {
    }

    public QscoresAppsContext(DbContextOptions<QscoresAppsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblApplication> TblApplications { get; set; }

    public virtual DbSet<TblAuthentication> TblAuthentications { get; set; }

    public virtual DbSet<TblFilePermission> TblFilePermissions { get; set; }

    public virtual DbSet<TblFooter> TblFooters { get; set; }

    public virtual DbSet<TblGroup> TblGroups { get; set; }

    public virtual DbSet<TblGroupFilePermission> TblGroupFilePermissions { get; set; }

    public virtual DbSet<TblGroupPermission> TblGroupPermissions { get; set; }

    public virtual DbSet<TblMonitor> TblMonitors { get; set; }

    public virtual DbSet<TblPermission> TblPermissions { get; set; }

    public virtual DbSet<TblReport> TblReports { get; set; }

    public virtual DbSet<TblTally> TblTallies { get; set; }

    public virtual DbSet<TblZipFile> TblZipFiles { get; set; }

    public virtual DbSet<TblZipFiles1> TblZipFiles1s { get; set; }

    public virtual DbSet<TmpTvqpermission> TmpTvqpermissions { get; set; }

    public virtual DbSet<TmpTvqstudyDate> TmpTvqstudyDates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QScoresApps;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblApplication>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblApplications");

            entity.HasIndex(e => e.DbaseName, "IDX_tblApplications_DBaseName");

            entity.HasIndex(e => e.ParentApplicationId, "IDX_tblApplications_ParentApplicationId");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.AppLocation).HasMaxLength(50);
            entity.Property(e => e.Application).HasMaxLength(50);
            entity.Property(e => e.DbaseName)
                .HasMaxLength(50)
                .HasColumnName("DBaseName");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsTvq).HasColumnName("IsTVQ");
            entity.Property(e => e.ShowInApp).HasDefaultValueSql("((1))");
            entity.Property(e => e.SubApplication).HasMaxLength(50);
        });

        modelBuilder.Entity<TblAuthentication>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblAuthentication");

            entity.HasIndex(e => e.EmailAddress, "IDX_tblAuthentication_EmailAddress");

            entity.HasIndex(e => e.GroupId, "IDX_tblAuthentication_GroupID");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.AuditDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CellPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasColumnName("EMailAddress");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.OfficePhone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<TblFilePermission>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblFilePermission");

            entity.HasIndex(e => e.FileId, "IDX_tblFilePermission_FileID");

            entity.HasIndex(e => new { e.UserId, e.FileId }, "IDX_tblFilePermission_UserFile").IsUnique();

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.Include)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TblFooter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblFooter");

            entity.Property(e => e.Body).IsUnicode(false);
            entity.Property(e => e.FooterId).HasColumnName("FooterID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MonthYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RightFooterHeading)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("tblGroups");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.GroupName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblGroupFilePermission>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblGroupFilePermission");

            entity.HasIndex(e => e.FileId, "IDX_tblGroupFilePermission_FileId");

            entity.HasIndex(e => new { e.GroupId, e.FileId }, "IDX_tblGroupFilePermission_GrpFileId").IsUnique();

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
        });

        modelBuilder.Entity<TblGroupPermission>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblGroupPermission");

            entity.HasIndex(e => e.ApplicationId, "IDX_tblGroupPermission_ApplicationID");

            entity.HasIndex(e => new { e.GroupId, e.ApplicationId, e.PermissionId }, "IDX_tblGroupPermission_GrpAppPerm").IsUnique();

            entity.HasIndex(e => e.PermissionId, "IDX_tblGroupPermission_PermissionID");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
        });

        modelBuilder.Entity<TblMonitor>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblMonitor");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.CommandText)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Executed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblPermission>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblPermissions");

            entity.HasIndex(e => e.ApplicationId, "IDX_tblPermissions_ApplicationID");

            entity.HasIndex(e => e.PermissionId, "IDX_tblPermissions_PermissionID");

            entity.HasIndex(e => new { e.UserId, e.ApplicationId, e.PermissionId }, "IDX_tblPermissions_UserAppPerm").IsUnique();

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.Include)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TblReport>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblReports");

            entity.HasIndex(e => e.ApplicationId, "IDX_tblReports_ApplicationID");

            entity.HasIndex(e => e.UserId, "IDX_tblReports_UserId");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DemoCodes).HasMaxLength(500);
            entity.Property(e => e.ReportName).HasMaxLength(50);
            entity.Property(e => e.RptType).HasMaxLength(100);
            entity.Property(e => e.ScalePoints).HasMaxLength(100);
            entity.Property(e => e.Studydates).HasMaxLength(100);
            entity.Property(e => e.Targets).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TblTally>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblTally");
        });

        modelBuilder.Entity<TblZipFile>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblZipFiles");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ZipFile).HasMaxLength(200);
        });

        modelBuilder.Entity<TblZipFiles1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblZipFiles1");

            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.ZipFile).HasMaxLength(200);
        });

        modelBuilder.Entity<TmpTvqpermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpTVQPermissions");
        });

        modelBuilder.Entity<TmpTvqstudyDate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpTVQStudyDates");

            entity.Property(e => e.AirDate).HasMaxLength(255);
            entity.Property(e => e.StudyType).HasMaxLength(100);
            entity.Property(e => e.StudyYear).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
