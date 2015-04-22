if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"User"') and o.name = 'FK_USER_USER_DEPA_DEPARTME')
alter table "User"
   drop constraint FK_USER_USER_DEPA_DEPARTME
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Department')
            and   type = 'U')
   drop table Department
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"User"')
            and   type = 'U')
   drop table "User"
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table Department (
   Id                   int                  identity,
   Name                 nvarchar(64)         not null,
   Description          nvarchar(512)        null,
   constraint PK_DEPARTMENT primary key (Id)
)
go

/*==============================================================*/
/* Table: "User"                                                */
/*==============================================================*/
create table "User" (
   Id                   bigint               not null,
   FamilyName           nvarchar(32)         not null,
   LastName             nvarchar(32)         not null,
   CreationTime         datetime             not null,
   EmployeeNumber       nvarchar(16)         not null,
   DepartmentId         int                  not null,
   Gender               tinyint              not null,
   constraint PK_USER primary key (Id)
)
go

alter table "User"
   add constraint FK_USER_USER_DEPA_DEPARTME foreign key (DepartmentId)
      references Department (Id)
go
