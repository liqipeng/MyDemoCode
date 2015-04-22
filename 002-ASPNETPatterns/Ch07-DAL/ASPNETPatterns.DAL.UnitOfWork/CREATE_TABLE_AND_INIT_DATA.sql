if exists (select 1
            from  sysobjects
           where  id = object_id('Account')
            and   type = 'U')
   drop table Account
go

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table Account (
   Id                   int                  identity,
   Balance              decimal(18,2)        not null default 0,
   constraint PK_ACCOUNT primary key (Id)
)
go

TRUNCATE TABLE Account
INSERT INTO Account VALUES(1000)
INSERT INTO Account VALUES(1000)