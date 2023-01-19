using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class db_hotelContext : DbContext
    {
        public db_hotelContext()
        {
        }

        public db_hotelContext(DbContextOptions<db_hotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TDictionary> TDictionaries { get; set; }
        public virtual DbSet<TOrder> TOrders { get; set; }
        public virtual DbSet<TRoom> TRooms { get; set; }
        public virtual DbSet<TTest> TTests { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<TWorker> TWorkers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=db_hotel;uid=root;pwd=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<TDictionary>(entity =>
            {
                entity.ToTable("t_dictionary");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Actualname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("actualname");

                entity.Property(e => e.Createtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createtime");

                entity.Property(e => e.Deleteflag)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("deleteflag");

                entity.Property(e => e.Displayname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("displayname");

                entity.Property(e => e.Sx)
                    .HasMaxLength(50)
                    .HasColumnName("sx");

                entity.Property(e => e.Typeid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("typeid");
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.ToTable("t_order");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Appointtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("appointtime");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("birthday");

                entity.Property(e => e.Checkintime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("checkintime");

                entity.Property(e => e.Checkouttime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("checkouttime");

                entity.Property(e => e.Createtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createtime");

                entity.Property(e => e.Deleteflag)
                    .HasColumnType("int(11)")
                    .HasColumnName("deleteflag");

                entity.Property(e => e.Qzlx)
                    .HasMaxLength(50)
                    .HasColumnName("qzlx");

                entity.Property(e => e.Roomid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("roomid");

                entity.Property(e => e.Sex)
                    .HasColumnType("int(11)")
                    .HasColumnName("sex");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.Property(e => e.Xm)
                    .HasMaxLength(50)
                    .HasColumnName("xm");

                entity.Property(e => e.Ywm)
                    .HasMaxLength(100)
                    .HasColumnName("ywm");

                entity.Property(e => e.Ywx)
                    .HasMaxLength(100)
                    .HasColumnName("ywx");

                entity.Property(e => e.Zjhm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("zjhm");

                entity.Property(e => e.Zjlx)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("zjlx");

                entity.Property(e => e.Zjz)
                    .HasMaxLength(50)
                    .HasColumnName("zjz");
            });

            modelBuilder.Entity<TRoom>(entity =>
            {
                entity.ToTable("t_room");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Createtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createtime");

                entity.Property(e => e.Deleteflag)
                    .HasColumnType("int(11)")
                    .HasColumnName("deleteflag");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Number)
                    .HasColumnType("int(11)")
                    .HasColumnName("number");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("status")
                    .HasComment("房间状态（包括：空闲，占用，脏房，维修中）");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type")
                    .HasComment("房间类型（包括：单人间，双人间，棋牌室，会议室，工具间，竞技房）");
            });

            modelBuilder.Entity<TTest>(entity =>
            {
                entity.ToTable("t_test");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.ToTable("t_user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Createtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createtime");

                entity.Property(e => e.Deleteflag)
                    .HasColumnType("int(11)")
                    .HasColumnName("deleteflag");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Workerid)
                    .HasMaxLength(50)
                    .HasColumnName("workerid");
            });

            modelBuilder.Entity<TWorker>(entity =>
            {
                entity.ToTable("t_worker");

                entity.Property(e => e.Id)
                    .HasColumnType("int(32)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("birthday");

                entity.Property(e => e.Createtime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("createtime");

                entity.Property(e => e.Deleteflag)
                    .HasColumnType("int(11)")
                    .HasColumnName("deleteflag");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Quittime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("quittime");

                entity.Property(e => e.Sex)
                    .HasColumnType("int(11)")
                    .HasColumnName("sex");

                entity.Property(e => e.Zjhm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("zjhm");

                entity.Property(e => e.Zjlx)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("zjlx");

                entity.Property(e => e.Zjz)
                    .HasMaxLength(100)
                    .HasColumnName("zjz")
                    .HasComment("证件照 - 存储的是照片在服务器的相对地址");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
