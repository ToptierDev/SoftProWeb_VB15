﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class PNSDBWEBEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=PNSDBWEBEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property CoreAccessLogs() As DbSet(Of CoreAccessLog)
    Public Property CoreDivisions() As DbSet(Of CoreDivision)
    Public Property CoreErrorLogs() As DbSet(Of CoreErrorLog)
    Public Property CoreMenus() As DbSet(Of CoreMenu)
    Public Property CoreModules() As DbSet(Of CoreModule)
    Public Property CorePositions() As DbSet(Of CorePosition)
    Public Property CoreRoleMenus() As DbSet(Of CoreRoleMenu)
    Public Property CoreWebResources() As DbSet(Of CoreWebResource)
    Public Property EMPLOYEE_INFO() As DbSet(Of EMPLOYEE_INFO)
    Public Property ITDMENU0() As DbSet(Of ITDMENU0)
    Public Property ITSTRUCAs() As DbSet(Of ITSTRUCA)
    Public Property ITSTRUTAs() As DbSet(Of ITSTRUTA)
    Public Property MD00SITE() As DbSet(Of MD00SITE)
    Public Property MD00VARL() As DbSet(Of MD00VARL)
    Public Property MD02PRG1() As DbSet(Of MD02PRG1)
    Public Property MD02PRG2() As DbSet(Of MD02PRG2)
    Public Property MD02PRG3() As DbSet(Of MD02PRG3)
    Public Property MD04MENU() As DbSet(Of MD04MENU)
    Public Property MD05USR1() As DbSet(Of MD05USR1)
    Public Property MD05USR2() As DbSet(Of MD05USR2)
    Public Property MD07COMP() As DbSet(Of MD07COMP)
    Public Property MD07ERRO() As DbSet(Of MD07ERRO)
    Public Property MD08MDLS() As DbSet(Of MD08MDLS)
    Public Property MD100STN() As DbSet(Of MD100STN)
    Public Property MD101URG() As DbSet(Of MD101URG)
    Public Property MD102SMM() As DbSet(Of MD102SMM)
    Public Property MD10PACC() As DbSet(Of MD10PACC)
    Public Property MD11DBFD() As DbSet(Of MD11DBFD)
    Public Property MD11DBRL() As DbSet(Of MD11DBRL)
    Public Property MD12INDD() As DbSet(Of MD12INDD)
    Public Property MD13DBFS() As DbSet(Of MD13DBFS)
    Public Property MD14POPU() As DbSet(Of MD14POPU)
    Public Property MD15SLBL() As DbSet(Of MD15SLBL)
    Public Property MD16ALER() As DbSet(Of MD16ALER)
    Public Property MD17PBUF() As DbSet(Of MD17PBUF)
    Public Property ITDMENU1() As DbSet(Of ITDMENU1)
    Public Property MD04MENU_Backup() As DbSet(Of MD04MENU_Backup)
    Public Property MD05USR1BeforChangeDeptcode() As DbSet(Of MD05USR1BeforChangeDeptcode)
    Public Property MD05USR2_Backup20131216() As DbSet(Of MD05USR2_Backup20131216)
    Public Property MD07COMP_Backup() As DbSet(Of MD07COMP_Backup)
    Public Property MD09PLV1() As DbSet(Of MD09PLV1)
    Public Property MD09PLV2() As DbSet(Of MD09PLV2)
    Public Property VW_MenuFRM() As DbSet(Of VW_MenuFRM)
    Public Property VW_MenuRPT() As DbSet(Of VW_MenuRPT)
    Public Property CoreUsers() As DbSet(Of CoreUser)
    Public Property CoreUsersCompanies() As DbSet(Of CoreUsersCompany)
    Public Property CoreUsersMenus() As DbSet(Of CoreUsersMenu)
    Public Property CoreUsersProjects() As DbSet(Of CoreUsersProject)

End Class
