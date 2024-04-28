using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QScores.Domain.QScoresWebDbModels;

namespace QScores.Infrastructure.Context;

public partial class QscoresWebDbContext : DbContext
{
    public QscoresWebDbContext()
    {
    }

    public QscoresWebDbContext(DbContextOptions<QscoresWebDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CableQProgStd2Final> CableQProgStd2Finals { get; set; }

    public virtual DbSet<CmsList25ViewListOfRelease> CmsList25ViewListOfReleases { get; set; }

    public virtual DbSet<CmsList25ViewReleaseDetail> CmsList25ViewReleaseDetails { get; set; }

    public virtual DbSet<CodeKey> CodeKeys { get; set; }

    public virtual DbSet<CtkTblList25Live> CtkTblList25Lives { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<MasterCableQProgStd2Final> MasterCableQProgStd2Finals { get; set; }

    public virtual DbSet<Mytable> Mytables { get; set; }

    public virtual DbSet<StudyCode> StudyCodes { get; set; }

    public virtual DbSet<TblApplicationControl> TblApplicationControls { get; set; }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblBlogDataSampling> TblBlogDataSamplings { get; set; }

    public virtual DbSet<TblBlogDataSamplingLive> TblBlogDataSamplingLives { get; set; }

    public virtual DbSet<TblBlogIvrSystem> TblBlogIvrSystems { get; set; }

    public virtual DbSet<TblBlogIvrSystemsLive> TblBlogIvrSystemsLives { get; set; }

    public virtual DbSet<TblBlogtestBlog> TblBlogtestBlogs { get; set; }

    public virtual DbSet<TblBlogtestBlogLive> TblBlogtestBlogLives { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblClientPermission> TblClientPermissions { get; set; }

    public virtual DbSet<TblCmscomment> TblCmscomments { get; set; }

    public virtual DbSet<TblCmspermisson> TblCmspermissons { get; set; }

    public virtual DbSet<TblContent> TblContents { get; set; }

    public virtual DbSet<TblContentCategory> TblContentCategories { get; set; }

    public virtual DbSet<TblContentFolder> TblContentFolders { get; set; }

    public virtual DbSet<TblContentType> TblContentTypes { get; set; }

    public virtual DbSet<TblContentsVersioning> TblContentsVersionings { get; set; }

    public virtual DbSet<TblCustomX001> TblCustomX001s { get; set; }

    public virtual DbSet<TblCustomX001Old> TblCustomX001Olds { get; set; }

    public virtual DbSet<TblForum> TblForums { get; set; }

    public virtual DbSet<TblItemComment> TblItemComments { get; set; }

    public virtual DbSet<TblItemPropertiesLive> TblItemPropertiesLives { get; set; }

    public virtual DbSet<TblItemProperty> TblItemProperties { get; set; }

    public virtual DbSet<TblList> TblLists { get; set; }

    public virtual DbSet<TblList23> TblList23s { get; set; }

    public virtual DbSet<TblList23Live> TblList23Lives { get; set; }

    public virtual DbSet<TblList24> TblList24s { get; set; }

    public virtual DbSet<TblList24Live> TblList24Lives { get; set; }

    public virtual DbSet<TblList25> TblList25s { get; set; }

    public virtual DbSet<TblList25Live> TblList25Lives { get; set; }

    public virtual DbSet<TblMasterProperty> TblMasterProperties { get; set; }

    public virtual DbSet<TblMyTable2> TblMyTable2s { get; set; }

    public virtual DbSet<TblNavBar> TblNavBars { get; set; }

    public virtual DbSet<TblPage> TblPages { get; set; }

    public virtual DbSet<TblPageContent> TblPageContents { get; set; }

    public virtual DbSet<TblPageContentVersioning> TblPageContentVersionings { get; set; }

    public virtual DbSet<TblPageTemplate> TblPageTemplates { get; set; }

    public virtual DbSet<TblPageVersioning> TblPageVersionings { get; set; }

    public virtual DbSet<TblPagerelatedItem> TblPagerelatedItems { get; set; }

    public virtual DbSet<TblPagerelatedItemsVersioning> TblPagerelatedItemsVersionings { get; set; }

    public virtual DbSet<TblPagesPropertiesVersioning> TblPagesPropertiesVersionings { get; set; }

    public virtual DbSet<TblPagesProperty> TblPagesProperties { get; set; }

    public virtual DbSet<TblThread> TblThreads { get; set; }

    public virtual DbSet<TblView> TblViews { get; set; }

    public virtual DbSet<TblViewCode> TblViewCodes { get; set; }

    public virtual DbSet<TblVisitorTracking> TblVisitorTrackings { get; set; }

    public virtual DbSet<TblWorkFlowAssignment> TblWorkFlowAssignments { get; set; }

    public virtual DbSet<TblWorkFlowItem> TblWorkFlowItems { get; set; }

    public virtual DbSet<TblvisitorPageHit> TblvisitorPageHits { get; set; }

    public virtual DbSet<TmpMaster> TmpMasters { get; set; }

    public virtual DbSet<VwMediaResource> VwMediaResources { get; set; }

    public virtual DbSet<XxcustomMaster> XxcustomMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QscoresWebDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CableQProgStd2Final>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Cable_Q_Prog_Std_2_Final");

            entity.Property(e => e.TheMillers)
                .HasMaxLength(255)
                .HasColumnName("THE MILLERS");
        });

        modelBuilder.Entity<CmsList25ViewListOfRelease>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CMS_List_25_View_List_of_Release");

            entity.Property(e => e.Body).HasColumnType("ntext");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemID");
            entity.Property(e => e.PubDisplay).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CmsList25ViewReleaseDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CMS_List_25_View_ReleaseDetail");

            entity.Property(e => e.Body).HasColumnType("ntext");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemID");
            entity.Property(e => e.PubDisplay).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CodeKey>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("'Code Key$'");

            entity.Property(e => e.StudyName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<CtkTblList25Live>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ctk_tblList25_live");

            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemID");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Master$");

            entity.Property(e => e.F6).HasMaxLength(255);
            entity.Property(e => e.F7).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.StudyName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<MasterCableQProgStd2Final>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Master$Cable_Q_Prog_Std_2_Final");

            entity.Property(e => e.AndyWilliams)
                .HasMaxLength(255)
                .HasColumnName("ANDY WILLIAMS");
        });

        modelBuilder.Entity<Mytable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("mytable");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.StudyName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<StudyCode>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.StudyName)
                .HasMaxLength(255)
                .HasColumnName("Study Name");
        });

        modelBuilder.Entity<TblApplicationControl>(entity =>
        {
            entity.HasKey(e => e.ControlId);

            entity.ToTable("tblApplicationControls");

            entity.Property(e => e.ControlId)
                .ValueGeneratedNever()
                .HasColumnName("ControlID");
            entity.Property(e => e.ControlName).HasMaxLength(50);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("tblBlogs");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.BlogName).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Inherits)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Xslt)
                .HasColumnType("ntext")
                .HasColumnName("XSLT");
        });

        modelBuilder.Entity<TblBlogDataSampling>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogData Sampling");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblBlogDataSamplingLive>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogData Sampling_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Header).HasMaxLength(50);
        });

        modelBuilder.Entity<TblBlogIvrSystem>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogIVR Systems");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(1000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblBlogIvrSystemsLive>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogIVR Systems_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Header).HasMaxLength(1000);
        });

        modelBuilder.Entity<TblBlogtestBlog>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogtest blog");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblBlogtestBlogLive>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblBlogtest blog_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Header).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("tblCategories");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TblClientPermission>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblClientPermission");

            entity.HasIndex(e => e.GroupId, "IX_tblClientPermission");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCmscomment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("tblCMSComments");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCmspermisson>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblCMSPermissons");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.PermBitVal).HasDefaultValueSql("((255))");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("userID");
        });

        modelBuilder.Entity<TblContent>(entity =>
        {
            entity.HasKey(e => e.ContentId);

            entity.ToTable("tblContents");

            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.ControlId).HasColumnName("ControlID");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.StrippedContent).HasColumnType("ntext");
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<TblContentCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblContentCategory");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CategoryID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Inherits)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Inx).HasColumnName("inx");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Parent)
                .HasMaxLength(5)
                .HasDefaultValueSql("(N'S0')");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TblContentFolder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblContentFolders");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FolderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FolderID");
            entity.Property(e => e.FolderName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblContentType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblContentTypes");

            entity.Property(e => e.ContentTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ContentTypeID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TblContentsVersioning>(entity =>
        {
            entity.HasKey(e => e.VersiontId);

            entity.ToTable("tblContentsVersioning");

            entity.Property(e => e.VersiontId).HasColumnName("VersiontID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.BinaryContent).HasColumnType("image");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.ControlId).HasColumnName("ControlID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('na')");
            entity.Property(e => e.Inherits)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StrippedContent).HasColumnType("ntext");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblCustomX001>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblCustomX001");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.StudyName).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCustomX001Old>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblCustomX001_old");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.StudyName).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TblForum>(entity =>
        {
            entity.HasKey(e => e.ForumId);

            entity.ToTable("tblForums");

            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsOpen)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('EM')");
        });

        modelBuilder.Entity<TblItemComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("tblItemComments");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Comment).HasColumnType("ntext");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ItemType).HasMaxLength(50);
            entity.Property(e => e.PostedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblItemPropertiesLive>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblItemPropertiesLive");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Tag).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(500);
        });

        modelBuilder.Entity<TblItemProperty>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblItemProperties");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Tag).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(500);
        });

        modelBuilder.Entity<TblList>(entity =>
        {
            entity.ToTable("tblLists");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Inherits)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblList23>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList23");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("Posted_date");
            entity.Property(e => e.Product)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Corporate')");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblList23Live>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList23_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("Posted_date");
            entity.Property(e => e.Product)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Corporate')");
        });

        modelBuilder.Entity<TblList24>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList24");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("('Event description')")
                .HasColumnType("ntext");
            entity.Property(e => e.EventDate)
                .HasColumnType("datetime")
                .HasColumnName("Event_Date");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Event Name')")
                .HasColumnName("Event_Title");
            entity.Property(e => e.Link).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblList24Live>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList24_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("('Event description')")
                .HasColumnType("ntext");
            entity.Property(e => e.EventDate)
                .HasColumnType("datetime")
                .HasColumnName("Event_Date");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Event Name')")
                .HasColumnName("Event_Title");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Link).HasMaxLength(50);
        });

        modelBuilder.Entity<TblList25>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList25");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.Body)
                .HasDefaultValueSql("('Press release body')")
                .HasColumnType("ntext");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("('Description goes here')")
                .HasColumnType("ntext");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.PubDisplay).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'EM')");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblList25Live>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("tblList25_Live");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Body)
                .HasDefaultValueSql("('Press release body')")
                .HasColumnType("ntext");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("('Description goes here')")
                .HasColumnType("ntext");
            entity.Property(e => e.FkitemId).HasColumnName("FKItemID");
            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.PubDisplay).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblMasterProperty>(entity =>
        {
            entity.HasKey(e => e.PropertyId);

            entity.ToTable("tblMasterProperties");

            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Scriptname).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TblMyTable2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblMyTable2");

            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.StudyName).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TblNavBar>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblNavBar");

            entity.Property(e => e.ExternalUrl)
                .HasMaxLength(4000)
                .HasColumnName("ExternalURL");
            entity.Property(e => e.Hdr).HasMaxLength(50);
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.LinkId).HasColumnName("LinkID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NavId)
                .ValueGeneratedOnAdd()
                .HasColumnName("NavID");
            entity.Property(e => e.Pgname)
                .HasMaxLength(50)
                .HasColumnName("PGName");
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
        });

        modelBuilder.Entity<TblPage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPages");

            entity.Property(e => e.Createdon).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PageID");
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<TblPageContent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPageContent");

            entity.Property(e => e.Pgid).HasColumnName("PGID");
            entity.Property(e => e.Pgloc)
                .HasMaxLength(3)
                .HasColumnName("PGLoc");
            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblPageContentVersioning>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPageContentVersioning");

            entity.Property(e => e.Pgloc)
                .HasMaxLength(3)
                .HasColumnName("PGLoc");
            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<TblPageTemplate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPageTemplates");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PageUrl)
                .HasMaxLength(50)
                .HasColumnName("PageURL");
            entity.Property(e => e.TemplateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TemplateID");
        });

        modelBuilder.Entity<TblPageVersioning>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPageVersioning");

            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.Createdon).HasColumnType("datetime");
            entity.Property(e => e.Inherits)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.VersionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("VersionID");
            entity.Property(e => e.Wflstate).HasColumnName("WFLState");
        });

        modelBuilder.Entity<TblPagerelatedItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPagerelatedItems");

            entity.Property(e => e.ItemType).HasMaxLength(500);
            entity.Property(e => e.Link).HasMaxLength(500);
            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.RecId).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<TblPagerelatedItemsVersioning>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPagerelatedItemsVersioning");

            entity.Property(e => e.ItemType).HasMaxLength(500);
            entity.Property(e => e.Link).HasMaxLength(500);
            entity.Property(e => e.RecId).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<TblPagesPropertiesVersioning>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPagesPropertiesVersioning");

            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Inx).HasColumnName("inx");
            entity.Property(e => e.IsSearchable)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Keywords).HasColumnType("ntext");
            entity.Property(e => e.MenuLabel).HasMaxLength(50);
            entity.Property(e => e.PageTitle).HasMaxLength(500);
            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<TblPagesProperty>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblPagesProperties");

            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Inx).HasColumnName("inx");
            entity.Property(e => e.IsSearchable)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Keywords).HasColumnType("ntext");
            entity.Property(e => e.MenuLabel).HasMaxLength(50);
            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.PageTitle).HasMaxLength(500);
            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RecID");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TblThread>(entity =>
        {
            entity.HasKey(e => e.ThreadId);

            entity.ToTable("tblThreads");

            entity.Property(e => e.ThreadId).HasColumnName("ThreadID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.IsOpen)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('EM')");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblView>(entity =>
        {
            entity.HasKey(e => e.ViewId);

            entity.ToTable("tblViews");

            entity.Property(e => e.ViewId).HasColumnName("ViewID");
            entity.Property(e => e.AddinfoUrl)
                .HasMaxLength(2000)
                .HasColumnName("addinfoURL");
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ExposeRss)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ExposeRSS");
            entity.Property(e => e.ListId).HasColumnName("ListID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OutputFormat).HasColumnType("ntext");
            entity.Property(e => e.Rssxslt)
                .HasColumnType("ntext")
                .HasColumnName("RSSXSLT");
            entity.Property(e => e.Sqlcode)
                .HasColumnType("ntext")
                .HasColumnName("SQLCode");
            entity.Property(e => e.ViewName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblViewCode>(entity =>
        {
            entity.HasKey(e => e.Vcid);

            entity.ToTable("tblViewCode");

            entity.Property(e => e.Vcid).HasColumnName("VCID");
            entity.Property(e => e.ColumnName).HasMaxLength(50);
            entity.Property(e => e.Operator).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);
            entity.Property(e => e.ValueFrom).HasMaxLength(50);
            entity.Property(e => e.ViewId).HasColumnName("ViewID");
        });

        modelBuilder.Entity<TblVisitorTracking>(entity =>
        {
            entity.HasKey(e => e.VisitId);

            entity.ToTable("tblVisitorTracking");

            entity.Property(e => e.VisitId).HasColumnName("VisitID");
            entity.Property(e => e.Browser).HasMaxLength(100);
            entity.Property(e => e.RefererUrl)
                .HasMaxLength(2000)
                .HasColumnName("RefererURL");
            entity.Property(e => e.TimeOfVisit).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(2000)
                .HasColumnName("URL");
            entity.Property(e => e.UserAgent).HasMaxLength(1000);
            entity.Property(e => e.UserHostAddress).HasMaxLength(500);
        });

        modelBuilder.Entity<TblWorkFlowAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);

            entity.ToTable("tblWorkFlowAssignment");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Wflid).HasColumnName("WFLID");
        });

        modelBuilder.Entity<TblWorkFlowItem>(entity =>
        {
            entity.HasKey(e => e.Wflid);

            entity.ToTable("tblWorkFlowItems");

            entity.Property(e => e.Wflid).HasColumnName("WFLID");
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<TblvisitorPageHit>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("tblvisitorPageHits");

            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.ElapsedTime)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.PageUrl)
                .HasMaxLength(500)
                .HasColumnName("PageURL");
            entity.Property(e => e.VisitId).HasColumnName("VisitID");
        });

        modelBuilder.Entity<TmpMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpMaster");

            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RecId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RecID");
            entity.Property(e => e.StudyCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudyName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwMediaResource>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_vwMediaResources");

            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemID");
        });

        modelBuilder.Entity<XxcustomMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("xxcustomMaster");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RecId).HasColumnName("RecID");
            entity.Property(e => e.StudyName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
