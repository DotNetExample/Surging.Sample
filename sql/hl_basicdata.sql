/*==============================================================*/
/* DB name:      hl_basicdata                                */
/* Created on:     2019/4/21 星期日 1:15:16                        */
/*==============================================================*/

CREATE DATABASE hl_basicdata DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE hl_basicdata;

drop table if exists bd_dictionary;

/*==============================================================*/
/* Table: bd_dictionary                                         */
/*==============================================================*/
create table bd_dictionary
(
   Id                   bigint not null auto_increment comment '主键',
   Code                 varchar(50) not null comment '唯一编码',
   Value                varchar(50) not null comment '字典值',
   ParentId             bigint not null comment '父级Id',
   Seq                  int not null comment '序号',
   TypeName             varchar(50) comment '分类名称',
   HasChild             int not null comment '0.没有 1.有',
   IsSysPreSet          int not null comment '0. 否 1.是',
   CreateBy             bigint comment '创建人',
   CreateTime           datetime comment '创建日期',
   UpdateBy             bigint comment '修改人',
   UpdateTime           datetime comment '修改日期',
   IsDeleted            int comment '软删除标识',
   DeleteBy             bigint comment '删除用户',
   DeleteTime           datetime comment '删除时间',
   primary key (Id)
);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Value`, `ParentId`, `Seq`, `TypeName`, `HasChild`, `IsSysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (1, 'systemconf', '系统设置', 0, 1, '系统设置', 1, 1, NULL, '2019-05-01 22:05:47', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Value`, `ParentId`, `Seq`, `TypeName`, `HasChild`, `IsSysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (2, 'systemconf_userpdwmode', '密码生成模式', 1, 1, '系统设置', 1, 1, NULL, '2019-05-01 22:07:37', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Value`, `ParentId`, `Seq`, `TypeName`, `HasChild`, `IsSysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (3, 'systemconf_userpdwmode_random', '随机密码生成模式', 2, 1, '系统设置', 0, 1, NULL, '2019-05-01 22:10:41', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Value`, `ParentId`, `Seq`, `TypeName`, `HasChild`, `IsSysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (4, 'systemconf_userpdwmode_fixed', '固定密码生成模式', 2, 2, '系统设置', 0, 1, NULL, '2019-05-01 22:11:37', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Value`, `ParentId`, `Seq`, `TypeName`, `HasChild`, `IsSysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (5, 'systemconf_userpwdfixed_val', '123qwe', 2, 1, '系统设置', 0, 1, NULL, '2019-05-01 22:16:06', NULL, NULL, 0, NULL, NULL);

alter table bd_dictionary comment '字典表';


