using Hx.BgApp.Layout;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Hx.BgApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BgAppDbContext :
    AbpDbContext<BgAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Project> Projects { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Page> Pages { get; set; }

    public BgAppDbContext(DbContextOptions<BgAppDbContext> options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(options =>
        {
            options.UseNetTopologySuite();
        });
        //.HasConversion(
        //    point => new NetTopologySuite.IO.PostGisWriter().Write(point),
        //    point => (Geometry)new NetTopologySuite.IO.PostGisReader().Read(point)
        //);
        //sAppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");
        base.OnModelCreating(builder);
        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Project>(b =>
        {
            b.ToTable(BgAppConsts.DbTablePrefix + "PROJECTS", BgAppConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(p => p.Id).HasColumnName("ID");
            b.Property(p => p.Title).HasColumnName("TITLE").HasMaxLength(ProjectConsts.MaxTitleLength);
            b.Property(p => p.Logo).HasColumnName("LOGO").HasMaxLength(ProjectConsts.MaxLogoLength);
            b.Property(p => p.DefaultMenuExpandedList).HasColumnName("DEFAULTMENUEXPANDEDLIST").HasMaxLength(ProjectConsts.MaxDefaultMenuExpandedListLength);
            b.Property(p => p.Current).HasColumnName("CURRENT");

            b.Property(p => p.CreationTime).HasColumnName("CREATIONTIME").HasColumnType("timestamp without time zone");
            b.Property(p => p.CreatorId).HasColumnName("CREATORID");

            b.HasMany(p => p.Menus).WithOne().HasForeignKey(p => p.ProjectId).HasConstraintName("MENU_PROJECTID");
            b.HasMany(p => p.Pages).WithOne().HasForeignKey(p => p.ProjectId).HasConstraintName("PAGE_PROJECTID");
        });
        builder.Entity<Menu>(b =>
        {
            b.ToTable(BgAppConsts.DbTablePrefix + "MENUS", BgAppConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(p => p.Id).HasColumnName("ID");
            b.Property(p => p.Title).HasColumnName("TITLE").HasMaxLength(MenuConsts.MaxTitleLength);
            b.Property(p => p.Icon).HasColumnName("ICON").HasMaxLength(MenuConsts.MaxIconLength);
            b.Property(p => p.Disabled).HasColumnName("DISABLED");
            b.Property(p => p.Selected).HasColumnName("SELECTED");
            b.Property(p => p.Display).HasColumnName("DISPLAY");
            b.Property(p => p.PageId).HasColumnName("PAGEID");
            b.Property(p => p.PagePath).HasColumnName("PAGEPATH").HasMaxLength(MenuConsts.MaxPagePathLength);
            b.Property(p => p.ProjectId).HasColumnName("PROJECTID");
            b.Property(p => p.Code).HasColumnName("CODE").HasMaxLength(MenuConsts.MaxCodeLength);
            b.Property(p => p.SerialNumber).HasColumnName("SERIALNUMBER").HasMaxLength(5);
            b.Property(p => p.ParentId).HasColumnName("PARENTID");

            b.Property(p => p.CreationTime).HasColumnName("CREATIONTIME").HasColumnType("timestamp without time zone");
            b.Property(p => p.CreatorId).HasColumnName("CREATORID");

            b.HasOne<Page>().WithOne().HasForeignKey<Menu>(p => p.PageId).HasConstraintName("MENU_PAGEID");
            b.HasMany(d => d.Children).WithOne().HasForeignKey(d => d.ParentId).HasConstraintName("MENU_PARENTID").OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<Page>(b =>
        {
            b.ToTable(BgAppConsts.DbTablePrefix + "PAGES", BgAppConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(p => p.Id).HasColumnName("ID");
            b.Property(p => p.Title).HasColumnName("TITLE").HasMaxLength(PageConsts.MaxTitleLength);
            b.Property(p => p.Code).HasColumnName("CODE").HasMaxLength(PageConsts.MaxCodeLength);
            b.Property(p => p.Path).HasColumnName("PATH").HasMaxLength(PageConsts.MaxPathLength);
            b.Property(p => p.ProjectId).HasColumnName("PROJECTID");
            b.Property(p => p.Disabled).HasColumnName("DISABLED");

            b.Property(p => p.CreationTime).HasColumnName("CREATIONTIME").HasColumnType("timestamp without time zone");
            b.Property(p => p.CreatorId).HasColumnName("CREATORID");
        });
    }
}