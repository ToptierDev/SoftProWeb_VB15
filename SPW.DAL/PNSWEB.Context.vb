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

Partial Public Class PNSWEBEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=PNSWEBEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property AD01VEN1() As DbSet(Of AD01VEN1)
    Public Property AD01VEN2() As DbSet(Of AD01VEN2)
    Public Property AD02CATE() As DbSet(Of AD02CATE)
    Public Property AD11INV1() As DbSet(Of AD11INV1)
    Public Property AD11INV2() As DbSet(Of AD11INV2)
    Public Property AD11INV3() As DbSet(Of AD11INV3)
    Public Property AD12CDN1() As DbSet(Of AD12CDN1)
    Public Property AD12CDN2() As DbSet(Of AD12CDN2)
    Public Property AD13PAY1() As DbSet(Of AD13PAY1)
    Public Property AD13PAY2() As DbSet(Of AD13PAY2)
    Public Property AD14ADJ1() As DbSet(Of AD14ADJ1)
    Public Property AD14ADJ2() As DbSet(Of AD14ADJ2)
    Public Property AD23APLG() As DbSet(Of AD23APLG)
    Public Property AD24APWK() As DbSet(Of AD24APWK)
    Public Property APLEDGERs() As DbSet(Of APLEDGER)
    Public Property ARLEDGERs() As DbSet(Of ARLEDGER)
    Public Property BD01STB1() As DbSet(Of BD01STB1)
    Public Property BD01STB2() As DbSet(Of BD01STB2)
    Public Property BD01STB3() As DbSet(Of BD01STB3)
    Public Property BD01STB4() As DbSet(Of BD01STB4)
    Public Property BD01STB5() As DbSet(Of BD01STB5)
    Public Property BD02HOLI() As DbSet(Of BD02HOLI)
    Public Property BD02SMPL() As DbSet(Of BD02SMPL)
    Public Property BD03CTRY() As DbSet(Of BD03CTRY)
    Public Property BD04BKBR() As DbSet(Of BD04BKBR)
    Public Property BD04CASH() As DbSet(Of BD04CASH)
    Public Property BD05ALIN() As DbSet(Of BD05ALIN)
    Public Property BD05CURR() As DbSet(Of BD05CURR)
    Public Property BD05SEXR() As DbSet(Of BD05SEXR)
    Public Property BD06PSET() As DbSet(Of BD06PSET)
    Public Property BD10DIVI() As DbSet(Of BD10DIVI)
    Public Property BD11DEPT() As DbSet(Of BD11DEPT)
    Public Property BD12SECT() As DbSet(Of BD12SECT)
    Public Property BD21CHLG() As DbSet(Of BD21CHLG)
    Public Property BD22BKLG() As DbSet(Of BD22BKLG)
    Public Property BD24CRRG() As DbSet(Of BD24CRRG)
    Public Property BD25CFA1() As DbSet(Of BD25CFA1)
    Public Property BD25CFA2() As DbSet(Of BD25CFA2)
    Public Property BD26CFE1() As DbSet(Of BD26CFE1)
    Public Property BD26CFE2() As DbSet(Of BD26CFE2)
    Public Property BD40RUNN() As DbSet(Of BD40RUNN)
    Public Property BD41CRDT() As DbSet(Of BD41CRDT)
    Public Property BD42CQPC() As DbSet(Of BD42CQPC)
    Public Property BG01PN01() As DbSet(Of BG01PN01)
    Public Property BG01TR01() As DbSet(Of BG01TR01)
    Public Property BG03ALBG() As DbSet(Of BG03ALBG)
    Public Property BG03ALBG_HIS() As DbSet(Of BG03ALBG_HIS)
    Public Property BG03ALBG_T_ADISAK1() As DbSet(Of BG03ALBG_T_ADISAK1)
    Public Property BG03IND1() As DbSet(Of BG03IND1)
    Public Property BG03INDH() As DbSet(Of BG03INDH)
    Public Property CD11PAJ1() As DbSet(Of CD11PAJ1)
    Public Property CD11PAJ2() As DbSet(Of CD11PAJ2)
    Public Property CD11PAJ3() As DbSet(Of CD11PAJ3)
    Public Property CD20CTLG() As DbSet(Of CD20CTLG)
    Public Property CD21MEXA() As DbSet(Of CD21MEXA)
    Public Property CD40CTTY() As DbSet(Of CD40CTTY)
    Public Property COMMISSIONs() As DbSet(Of COMMISSION)
    Public Property COMMISSION_AA() As DbSet(Of COMMISSION_AA)
    Public Property COMMISSION_BALANCE() As DbSet(Of COMMISSION_BALANCE)
    Public Property COMMISSION_CT() As DbSet(Of COMMISSION_CT)
    Public Property COMMISSION_FN() As DbSet(Of COMMISSION_FN)
    Public Property COMMISSION_TEMP_BALANCE() As DbSet(Of COMMISSION_TEMP_BALANCE)
    Public Property COMMISSIONCARDs() As DbSet(Of COMMISSIONCARD)
    Public Property COMMLEVELs() As DbSet(Of COMMLEVEL)
    Public Property COMMPROJs() As DbSet(Of COMMPROJ)
    Public Property COMMSALEs() As DbSet(Of COMMSALE)
    Public Property COMMSALESGROUPs() As DbSet(Of COMMSALESGROUP)
    Public Property COMMSTAGEs() As DbSet(Of COMMSTAGE)
    Public Property COMMTAGSALEGROUPs() As DbSet(Of COMMTAGSALEGROUP)
    Public Property CustomerVisit_Storeporc() As DbSet(Of CustomerVisit_Storeporc)
    Public Property DE00AROLE1() As DbSet(Of DE00AROLE1)
    Public Property DE00BANNER() As DbSet(Of DE00BANNER)
    Public Property DE00CHOICE() As DbSet(Of DE00CHOICE)
    Public Property DE00FINDGP() As DbSet(Of DE00FINDGP)
    Public Property DE00GROUP() As DbSet(Of DE00GROUP)
    Public Property DE00ROADM() As DbSet(Of DE00ROADM)
    Public Property DE00ROADS() As DbSet(Of DE00ROADS)
    Public Property DE01QUES0() As DbSet(Of DE01QUES0)
    Public Property DE01QUES1() As DbSet(Of DE01QUES1)
    Public Property DE01QUES2() As DbSet(Of DE01QUES2)
    Public Property DE02CONT() As DbSet(Of DE02CONT)
    Public Property DE02CONT2() As DbSet(Of DE02CONT2)
    Public Property DE02QUSN1() As DbSet(Of DE02QUSN1)
    Public Property DE02QUSN2() As DbSet(Of DE02QUSN2)
    Public Property DE02QUSN3() As DbSet(Of DE02QUSN3)
    Public Property dtproperties() As DbSet(Of dtproperty)
    Public Property ED01COSTLIST() As DbSet(Of ED01COSTLIST)
    Public Property ED01PRJTYPE() As DbSet(Of ED01PRJTYPE)
    Public Property ED01PROJ() As DbSet(Of ED01PROJ)
    Public Property ED01PROJLAND() As DbSet(Of ED01PROJLAND)
    Public Property ED01TEAC() As DbSet(Of ED01TEAC)
    Public Property ED02PHAS() As DbSet(Of ED02PHAS)
    Public Property ED03MAPPSAP() As DbSet(Of ED03MAPPSAP)
    Public Property ED03UNIT() As DbSet(Of ED03UNIT)
    Public Property ED03UNITSTATUS() As DbSet(Of ED03UNITSTATUS)
    Public Property ED03UNLN() As DbSet(Of ED03UNLN)
    Public Property ED03UNQC() As DbSet(Of ED03UNQC)
    Public Property ED04BLOK() As DbSet(Of ED04BLOK)
    Public Property ED04RECF() As DbSet(Of ED04RECF)
    Public Property ED05CMT1() As DbSet(Of ED05CMT1)
    Public Property ED05CMT2() As DbSet(Of ED05CMT2)
    Public Property ED11COST() As DbSet(Of ED11COST)
    Public Property ED11PAJ1() As DbSet(Of ED11PAJ1)
    Public Property ED11PAJ2() As DbSet(Of ED11PAJ2)
    Public Property ED11PAJ4() As DbSet(Of ED11PAJ4)
    Public Property ED20ESLS() As DbSet(Of ED20ESLS)
    Public Property ED20RELG() As DbSet(Of ED20RELG)
    Public Property ED21LNDL() As DbSet(Of ED21LNDL)
    Public Property FD01PERS() As DbSet(Of FD01PERS)
    Public Property FD01TYAS() As DbSet(Of FD01TYAS)
    Public Property FD02LOCA() As DbSet(Of FD02LOCA)
    Public Property FD11PROP() As DbSet(Of FD11PROP)
    Public Property FD21FALG() As DbSet(Of FD21FALG)
    Public Property FD22FADP() As DbSet(Of FD22FADP)
    Public Property FD23FAMM() As DbSet(Of FD23FAMM)
    Public Property GD00CHRT() As DbSet(Of GD00CHRT)
    Public Property GD01MAST() As DbSet(Of GD01MAST)
    Public Property GD02CSCN() As DbSet(Of GD02CSCN)
    Public Property GD03BUDG() As DbSet(Of GD03BUDG)
    Public Property GD11GLDT() As DbSet(Of GD11GLDT)
    Public Property GD20TRAN() As DbSet(Of GD20TRAN)
    Public Property GD21GLSM() As DbSet(Of GD21GLSM)
    Public Property GD41ACGR() As DbSet(Of GD41ACGR)
    Public Property GD42ALTB() As DbSet(Of GD42ALTB)
    Public Property GD44ACPR() As DbSet(Of GD44ACPR)
    Public Property GLLEDGERs() As DbSet(Of GLLEDGER)
    Public Property GROUPACCs() As DbSet(Of GROUPACC)
    Public Property GROUPACCIDs() As DbSet(Of GROUPACCID)
    Public Property HD01GPRT() As DbSet(Of HD01GPRT)
    Public Property HD01SHFT() As DbSet(Of HD01SHFT)
    Public Property HD02POSI() As DbSet(Of HD02POSI)
    Public Property HD03DISE() As DbSet(Of HD03DISE)
    Public Property HD041TNG() As DbSet(Of HD041TNG)
    Public Property HD042TNT() As DbSet(Of HD042TNT)
    Public Property HD04EVL1() As DbSet(Of HD04EVL1)
    Public Property HD04EVL2() As DbSet(Of HD04EVL2)
    Public Property HD05BSSP() As DbSet(Of HD05BSSP)
    Public Property HD05EQPM() As DbSet(Of HD05EQPM)
    Public Property HD13RTER() As DbSet(Of HD13RTER)
    Public Property HD26EVL1() As DbSet(Of HD26EVL1)
    Public Property HD26EVL2() As DbSet(Of HD26EVL2)
    Public Property ICLedgers() As DbSet(Of ICLedger)
    Public Property ICREQDs() As DbSet(Of ICREQD)
    Public Property ICREQHs() As DbSet(Of ICREQH)
    Public Property ICRETDs() As DbSet(Of ICRETD)
    Public Property ICRETHs() As DbSet(Of ICRETH)
    Public Property ICSHIPDs() As DbSet(Of ICSHIPD)
    Public Property ICSHIPHs() As DbSet(Of ICSHIPH)
    Public Property ICSTOCKs() As DbSet(Of ICSTOCK)
    Public Property ICSTOCK2() As DbSet(Of ICSTOCK2)
    Public Property ICTRANS() As DbSet(Of ICTRAN)
    Public Property ICTXRATEs() As DbSet(Of ICTXRATE)
    Public Property ITDASSETs() As DbSet(Of ITDASSET)
    Public Property ITDCTRNOTEs() As DbSet(Of ITDCTRNOTE)
    Public Property ITLAWDOCs() As DbSet(Of ITLAWDOC)
    Public Property ITLAWDOC1() As DbSet(Of ITLAWDOC1)
    Public Property KD01MACH() As DbSet(Of KD01MACH)
    Public Property KD02WKCN() As DbSet(Of KD02WKCN)
    Public Property KD02WKPS() As DbSet(Of KD02WKPS)
    Public Property KD05MOLD() As DbSet(Of KD05MOLD)
    Public Property KD06MNSB() As DbSet(Of KD06MNSB)
    Public Property KD06MNWL() As DbSet(Of KD06MNWL)
    Public Property LD02COLC() As DbSet(Of LD02COLC)
    Public Property LD02GCOM() As DbSet(Of LD02GCOM)
    Public Property LD02NCCM() As DbSet(Of LD02NCCM)
    Public Property LD02RTCM() As DbSet(Of LD02RTCM)
    Public Property LD02TCOM() As DbSet(Of LD02TCOM)
    Public Property LD03CMRG() As DbSet(Of LD03CMRG)
    Public Property LD04PRMT() As DbSet(Of LD04PRMT)
    Public Property LD06SLAR() As DbSet(Of LD06SLAR)
    Public Property LD07AZIP() As DbSet(Of LD07AZIP)
    Public Property LD07SLRT() As DbSet(Of LD07SLRT)
    Public Property LD08SLTY() As DbSet(Of LD08SLTY)
    Public Property LD08SMCT() As DbSet(Of LD08SMCT)
    Public Property LD09CACT() As DbSet(Of LD09CACT)
    Public Property LD09CISL() As DbSet(Of LD09CISL)
    Public Property LD09MEDA() As DbSet(Of LD09MEDA)
    Public Property LD20SAL1() As DbSet(Of LD20SAL1)
    Public Property LD20SAL2() As DbSet(Of LD20SAL2)
    Public Property LD21SASM() As DbSet(Of LD21SASM)
    Public Property LD23RFCP() As DbSet(Of LD23RFCP)
    Public Property LD421PTR() As DbSet(Of LD421PTR)
    Public Property LD421PTY() As DbSet(Of LD421PTY)
    Public Property LD422PTR() As DbSet(Of LD422PTR)
    Public Property LD422TSC() As DbSet(Of LD422TSC)
    Public Property LD423SCT() As DbSet(Of LD423SCT)
    Public Property LD42PRMS() As DbSet(Of LD42PRMS)
    Public Property LD42TPUBSALE() As DbSet(Of LD42TPUBSALE)
    Public Property LD42TPUBTRAN() As DbSet(Of LD42TPUBTRAN)
    Public Property LD43PDOR() As DbSet(Of LD43PDOR)
    Public Property MASTERTYPEs() As DbSet(Of MASTERTYPE)
    Public Property MDCONFIGs() As DbSet(Of MDCONFIG)
    Public Property MDOPERATs() As DbSet(Of MDOPERAT)
    Public Property MIS_Transaction() As DbSet(Of MIS_Transaction)
    Public Property MKCHECKRECIVEs() As DbSet(Of MKCHECKRECIVE)
    Public Property MKTOD11REA() As DbSet(Of MKTOD11REA)
    Public Property MKTPLANEXPs() As DbSet(Of MKTPLANEXP)
    Public Property ND01PLAN() As DbSet(Of ND01PLAN)
    Public Property ND03HDCT() As DbSet(Of ND03HDCT)
    Public Property ND11VOLB() As DbSet(Of ND11VOLB)
    Public Property ND12PBG1() As DbSet(Of ND12PBG1)
    Public Property ND12PBG2() As DbSet(Of ND12PBG2)
    Public Property ND13PUS1() As DbSet(Of ND13PUS1)
    Public Property ND13PUS2() As DbSet(Of ND13PUS2)
    Public Property ND21IBUD() As DbSet(Of ND21IBUD)
    Public Property ND22BGLG() As DbSet(Of ND22BGLG)
    Public Property ND23BUDG() As DbSet(Of ND23BUDG)
    Public Property ND24BUDG() As DbSet(Of ND24BUDG)
    Public Property OD10QUO1() As DbSet(Of OD10QUO1)
    Public Property OD10QUO2() As DbSet(Of OD10QUO2)
    Public Property OD11BKT1() As DbSet(Of OD11BKT1)
    Public Property OD11DOCU() As DbSet(Of OD11DOCU)
    Public Property OD12ORD1() As DbSet(Of OD12ORD1)
    Public Property OD12ORD2() As DbSet(Of OD12ORD2)
    Public Property OD13INV1() As DbSet(Of OD13INV1)
    Public Property OD13INV2() As DbSet(Of OD13INV2)
    Public Property OD14CRN1() As DbSet(Of OD14CRN1)
    Public Property OD14CRN2() As DbSet(Of OD14CRN2)
    Public Property OD15CSH1() As DbSet(Of OD15CSH1)
    Public Property OD15CSH2() As DbSet(Of OD15CSH2)
    Public Property OD16DBN1() As DbSet(Of OD16DBN1)
    Public Property OD16DBN2() As DbSet(Of OD16DBN2)
    Public Property OD20BKLG() As DbSet(Of OD20BKLG)
    Public Property OD20DLPN() As DbSet(Of OD20DLPN)
    Public Property OD20LAGR() As DbSet(Of OD20LAGR)
    Public Property OD20LPRT() As DbSet(Of OD20LPRT)
    Public Property OD20RESALEITEM() As DbSet(Of OD20RESALEITEM)
    Public Property OD20SORD() As DbSet(Of OD20SORD)
    Public Property OD212AGL() As DbSet(Of OD212AGL)
    Public Property OD213CRD() As DbSet(Of OD213CRD)
    Public Property OD21BODS() As DbSet(Of OD21BODS)
    Public Property OD21LAGD() As DbSet(Of OD21LAGD)
    Public Property OD21LAGD2() As DbSet(Of OD21LAGD2)
    Public Property OD21LAPM() As DbSet(Of OD21LAPM)
    Public Property OD21PMUT() As DbSet(Of OD21PMUT)
    Public Property OD21PMUTL() As DbSet(Of OD21PMUTL)
    Public Property OD22CLSH() As DbSet(Of OD22CLSH)
    Public Property OD22PKL1() As DbSet(Of OD22PKL1)
    Public Property OD22PKL2() As DbSet(Of OD22PKL2)
    Public Property OD23AADJ() As DbSet(Of OD23AADJ)
    Public Property OD23SHMT() As DbSet(Of OD23SHMT)
    Public Property OD50BUDGET() As DbSet(Of OD50BUDGET)
    Public Property OD50CIAC() As DbSet(Of OD50CIAC)
    Public Property OD50CISL() As DbSet(Of OD50CISL)
    Public Property OD50INCOME() As DbSet(Of OD50INCOME)
    Public Property OD50RCVD() As DbSet(Of OD50RCVD)
    Public Property OD50RLABEL() As DbSet(Of OD50RLABEL)
    Public Property OD51CNPF() As DbSet(Of OD51CNPF)
    Public Property ODLOCKPROMOTIONs() As DbSet(Of ODLOCKPROMOTION)
    Public Property PD20APWO() As DbSet(Of PD20APWO)
    Public Property PD20WOI1() As DbSet(Of PD20WOI1)
    Public Property PD20WOI2() As DbSet(Of PD20WOI2)
    Public Property PD20WOI3() As DbSet(Of PD20WOI3)
    Public Property PD21WOO1() As DbSet(Of PD21WOO1)
    Public Property PD21WOO2() As DbSet(Of PD21WOO2)
    Public Property PD22WOBM() As DbSet(Of PD22WOBM)
    Public Property PD23JBO1() As DbSet(Of PD23JBO1)
    Public Property PD23JBO2() As DbSet(Of PD23JBO2)
    Public Property PD23JBO3() As DbSet(Of PD23JBO3)
    Public Property PD23JBO4() As DbSet(Of PD23JBO4)
    Public Property PD24RSL1() As DbSet(Of PD24RSL1)
    Public Property PD24RSL2() As DbSet(Of PD24RSL2)
    Public Property PD24RSL3() As DbSet(Of PD24RSL3)
    Public Property PD27QCDS() As DbSet(Of PD27QCDS)
    Public Property PD27QCJB() As DbSet(Of PD27QCJB)
    Public Property PD50PDPN() As DbSet(Of PD50PDPN)
    Public Property PD51PDPH() As DbSet(Of PD51PDPH)
    Public Property PD51PDPN() As DbSet(Of PD51PDPN)
    Public Property PD52MRPN() As DbSet(Of PD52MRPN)
    Public Property PD52MRPZ() As DbSet(Of PD52MRPZ)
    Public Property PD53CRPN() As DbSet(Of PD53CRPN)
    Public Property PD53CRPP() As DbSet(Of PD53CRPP)
    Public Property PD54RETN() As DbSet(Of PD54RETN)
    Public Property QD01GSPC() As DbSet(Of QD01GSPC)
    Public Property QD02SINS() As DbSet(Of QD02SINS)
    Public Property RD01CUST() As DbSet(Of RD01CUST)
    Public Property RD01CXRF() As DbSet(Of RD01CXRF)
    Public Property RD01SHRT() As DbSet(Of RD01SHRT)
    Public Property RD02CATE() As DbSet(Of RD02CATE)
    Public Property RD04CULC() As DbSet(Of RD04CULC)
    Public Property RD05BANK() As DbSet(Of RD05BANK)
    Public Property RD06ROUT() As DbSet(Of RD06ROUT)
    Public Property RD07MEMB() As DbSet(Of RD07MEMB)
    Public Property RD11CHR1() As DbSet(Of RD11CHR1)
    Public Property RD11CHR2() As DbSet(Of RD11CHR2)
    Public Property RD11CHR3() As DbSet(Of RD11CHR3)
    Public Property RD12PIN1() As DbSet(Of RD12PIN1)
    Public Property RD12PIN2() As DbSet(Of RD12PIN2)
    Public Property RD13RAJ1() As DbSet(Of RD13RAJ1)
    Public Property RD13RAJ2() As DbSet(Of RD13RAJ2)
    Public Property RD14RST1() As DbSet(Of RD14RST1)
    Public Property RD14RST2() As DbSet(Of RD14RST2)
    Public Property RD16INVO() As DbSet(Of RD16INVO)
    Public Property RD17RCT1() As DbSet(Of RD17RCT1)
    Public Property RD17RCT2() As DbSet(Of RD17RCT2)
    Public Property RD18CTR1() As DbSet(Of RD18CTR1)
    Public Property RD18CTR2() As DbSet(Of RD18CTR2)
    Public Property RD21ARDL() As DbSet(Of RD21ARDL)
    Public Property RD22ARWK() As DbSet(Of RD22ARWK)
    Public Property RD24CLRG() As DbSet(Of RD24CLRG)
    Public Property RD26ORRG() As DbSet(Of RD26ORRG)
    Public Property RD40CRT1() As DbSet(Of RD40CRT1)
    Public Property RD40CRT2() As DbSet(Of RD40CRT2)
    Public Property REPRINTLOGs() As DbSet(Of REPRINTLOG)
    Public Property SAP_POOUT_DETAIL() As DbSet(Of SAP_POOUT_DETAIL)
    Public Property SAP_POOUT_HEAD() As DbSet(Of SAP_POOUT_HEAD)
    Public Property SAP_UNIT() As DbSet(Of SAP_UNIT)
    Public Property SAPConvCusts() As DbSet(Of SAPConvCust)
    Public Property SAPConvItems() As DbSet(Of SAPConvItem)
    Public Property SAPConvRecvs() As DbSet(Of SAPConvRecv)
    Public Property SAPDailyDoors() As DbSet(Of SAPDailyDoor)
    Public Property SAPInfCusts() As DbSet(Of SAPInfCust)
    Public Property SAPInfElines() As DbSet(Of SAPInfEline)
    Public Property SAPInfPostMonthEnds() As DbSet(Of SAPInfPostMonthEnd)
    Public Property SAPInfRecvs() As DbSet(Of SAPInfRecv)
    Public Property SAPInfRecvPostLines() As DbSet(Of SAPInfRecvPostLine)
    Public Property SCANASSETs() As DbSet(Of SCANASSET)
    Public Property SCANPRINTs() As DbSet(Of SCANPRINT)
    Public Property SD01CTRY() As DbSet(Of SD01CTRY)
    Public Property SD02GODN() As DbSet(Of SD02GODN)
    Public Property SD03TYPE() As DbSet(Of SD03TYPE)
    Public Property SD03TYPE1() As DbSet(Of SD03TYPE1)
    Public Property SD04INMT() As DbSet(Of SD04INMT)
    Public Property SD04PDBG() As DbSet(Of SD04PDBG)
    Public Property SD05ACMK() As DbSet(Of SD05ACMK)
    Public Property SD05PDDS() As DbSet(Of SD05PDDS)
    Public Property SD05PDSP() As DbSet(Of SD05PDSP)
    Public Property SD05PROM1() As DbSet(Of SD05PROM1)
    Public Property SD05PROM2() As DbSet(Of SD05PROM2)
    Public Property SD05PROM3() As DbSet(Of SD05PROM3)
    Public Property SD06PDLN() As DbSet(Of SD06PDLN)
    Public Property SD07PRJPRICE() As DbSet(Of SD07PRJPRICE)
    Public Property SD08CLAS() As DbSet(Of SD08CLAS)
    Public Property SD09BNLC() As DbSet(Of SD09BNLC)
    Public Property SD11ICTR() As DbSet(Of SD11ICTR)
    Public Property SD19PHY1() As DbSet(Of SD19PHY1)
    Public Property SD22FDLY() As DbSet(Of SD22FDLY)
    Public Property SD23AVCT() As DbSet(Of SD23AVCT)
    Public Property SD24STRG() As DbSet(Of SD24STRG)
    Public Property SD25STSR() As DbSet(Of SD25STSR)
    Public Property SD26ICBK() As DbSet(Of SD26ICBK)
    Public Property SD26TRBK() As DbSet(Of SD26TRBK)
    Public Property SD42ACTB() As DbSet(Of SD42ACTB)
    Public Property SM_BONUS() As DbSet(Of SM_BONUS)
    Public Property SM_CALCHIST() As DbSet(Of SM_CALCHIST)
    Public Property SM_COM_QUARTER() As DbSet(Of SM_COM_QUARTER)
    Public Property SM_COMM() As DbSet(Of SM_COMM)
    Public Property SM_CONTRACT() As DbSet(Of SM_CONTRACT)
    Public Property SM_HEADTARGET() As DbSet(Of SM_HEADTARGET)
    Public Property SM_MENU2() As DbSet(Of SM_MENU2)
    Public Property SM_MGRTARGET() As DbSet(Of SM_MGRTARGET)
    Public Property SM_PERFORM() As DbSet(Of SM_PERFORM)
    Public Property SM_PERFORM_Q() As DbSet(Of SM_PERFORM_Q)
    Public Property SM_RATE() As DbSet(Of SM_RATE)
    Public Property SM_SALESTEAM() As DbSet(Of SM_SALESTEAM)
    Public Property SM_TARGET() As DbSet(Of SM_TARGET)
    Public Property SM_TRANSFER() As DbSet(Of SM_TRANSFER)
    Public Property SM_USR() As DbSet(Of SM_USR)
    Public Property SV_ADDCHANGE() As DbSet(Of SV_ADDCHANGE)
    Public Property SV_CHOICE() As DbSet(Of SV_CHOICE)
    Public Property SV_FORMPRINT() As DbSet(Of SV_FORMPRINT)
    Public Property SV_QUESTFORM() As DbSet(Of SV_QUESTFORM)
    Public Property SV_QUESTHISR() As DbSet(Of SV_QUESTHISR)
    Public Property SV_QUESTITEM() As DbSet(Of SV_QUESTITEM)
    Public Property SV_QUESTRAN1() As DbSet(Of SV_QUESTRAN1)
    Public Property SV_QUESTRAN2() As DbSet(Of SV_QUESTRAN2)
    Public Property SY01LOG1() As DbSet(Of SY01LOG1)
    Public Property SY02TYPY() As DbSet(Of SY02TYPY)
    Public Property sysdiagrams() As DbSet(Of sysdiagram)
    Public Property T_CAL_COUNT() As DbSet(Of T_CAL_COUNT)
    Public Property T_CAL_SUMMARY_COUNT() As DbSet(Of T_CAL_SUMMARY_COUNT)
    Public Property T_COMMISSION() As DbSet(Of T_COMMISSION)
    Public Property T_HISTORY() As DbSet(Of T_HISTORY)
    Public Property T_LEVEL() As DbSet(Of T_LEVEL)
    Public Property T_MENU2() As DbSet(Of T_MENU2)
    Public Property T_PERFORMANCE() As DbSet(Of T_PERFORMANCE)
    Public Property T_RATE() As DbSet(Of T_RATE)
    Public Property T_SUMMARY_SALES_COMMISSION() As DbSet(Of T_SUMMARY_SALES_COMMISSION)
    Public Property T_TEAM() As DbSet(Of T_TEAM)
    Public Property T_TRANSACTION() As DbSet(Of T_TRANSACTION)
    Public Property TBL_CheckOverWO() As DbSet(Of TBL_CheckOverWO)
    Public Property TD01LVRC() As DbSet(Of TD01LVRC)
    Public Property TD11WKTM() As DbSet(Of TD11WKTM)
    Public Property TD21SVAT() As DbSet(Of TD21SVAT)
    Public Property TD32BPDW() As DbSet(Of TD32BPDW)
    Public Property UD11PRQ1() As DbSet(Of UD11PRQ1)
    Public Property UD11PRQ2() As DbSet(Of UD11PRQ2)
    Public Property UD12POR1() As DbSet(Of UD12POR1)
    Public Property UD12POR2() As DbSet(Of UD12POR2)
    Public Property UD13APO1() As DbSet(Of UD13APO1)
    Public Property UD13APO2() As DbSet(Of UD13APO2)
    Public Property UD14RRF1() As DbSet(Of UD14RRF1)
    Public Property UD14RRF2() As DbSet(Of UD14RRF2)
    Public Property UD15RTV1() As DbSet(Of UD15RTV1)
    Public Property UD15RTV2() As DbSet(Of UD15RTV2)
    Public Property UD21PCPO() As DbSet(Of UD21PCPO)
    Public Property UD22PCLG() As DbSet(Of UD22PCLG)
    Public Property UD23RCL1() As DbSet(Of UD23RCL1)
    Public Property UDPRJLOCKs() As DbSet(Of UDPRJLOCK)
    Public Property VATTRANS() As DbSet(Of VATTRAN)
    Public Property WD22FDLY() As DbSet(Of WD22FDLY)
    Public Property YD02HOLI() As DbSet(Of YD02HOLI)
    Public Property YD03WORK() As DbSet(Of YD03WORK)
    Public Property YD04MGNM() As DbSet(Of YD04MGNM)
    Public Property YD05EMP1() As DbSet(Of YD05EMP1)
    Public Property YD05EMP2() As DbSet(Of YD05EMP2)
    Public Property YD05EMP3() As DbSet(Of YD05EMP3)
    Public Property YD05EMP4() As DbSet(Of YD05EMP4)
    Public Property YD05EMRC() As DbSet(Of YD05EMRC)
    Public Property YD06EMLV() As DbSet(Of YD06EMLV)
    Public Property YD07ACTB() As DbSet(Of YD07ACTB)
    Public Property YD11PRTR() As DbSet(Of YD11PRTR)
    Public Property YD12SMSH() As DbSet(Of YD12SMSH)
    Public Property YD13BONU() As DbSet(Of YD13BONU)
    Public Property YD14PRDC() As DbSet(Of YD14PRDC)
    Public Property YD20EML1() As DbSet(Of YD20EML1)
    Public Property YD20EML2() As DbSet(Of YD20EML2)
    Public Property ZFINBANKs() As DbSet(Of ZFINBANK)
    Public Property ZFINREAS() As DbSet(Of ZFINREA)
    Public Property ZFINTRANs() As DbSet(Of ZFINTRAN)
    Public Property ZFTRBANKFLOGs() As DbSet(Of ZFTRBANKFLOG)
    Public Property ZITAudit_OD23AADJ() As DbSet(Of ZITAudit_OD23AADJ)
    Public Property ZMAPINFOProjects() As DbSet(Of ZMAPINFOProject)
    Public Property ZMAPINFOPuchases() As DbSet(Of ZMAPINFOPuchase)
    Public Property ZTypeHouseCons() As DbSet(Of ZTypeHouseCon)
    Public Property BG03BGIN() As DbSet(Of BG03BGIN)
    Public Property BG03IND1_T_ADISAK() As DbSet(Of BG03IND1_T_ADISAK)
    Public Property KD20RPJB() As DbSet(Of KD20RPJB)
    Public Property MP01CONS() As DbSet(Of MP01CONS)
    Public Property MP01PURC() As DbSet(Of MP01PURC)
    Public Property OD50BUDGET2() As DbSet(Of OD50BUDGET2)
    Public Property OD50INCOME2() As DbSet(Of OD50INCOME2)
    Public Property OSALECUS() As DbSet(Of OSALECU)
    Public Property OSALEPRJs() As DbSet(Of OSALEPRJ)
    Public Property OSALEUNTs() As DbSet(Of OSALEUNT)
    Public Property SD03TYPE_Copy() As DbSet(Of SD03TYPE_Copy)
    Public Property SD05PDDS_20150115() As DbSet(Of SD05PDDS_20150115)
    Public Property SD42ACTB_Copy() As DbSet(Of SD42ACTB_Copy)
    Public Property Snapt0606_SAPBI_TargetSaleUnit() As DbSet(Of Snapt0606_SAPBI_TargetSaleUnit)
    Public Property Snapt0606_SAPBI_TargetSaleValue() As DbSet(Of Snapt0606_SAPBI_TargetSaleValue)
    Public Property Snapt0606_SAPBI_TargetTransferUnit() As DbSet(Of Snapt0606_SAPBI_TargetTransferUnit)
    Public Property Snapt0606_SAPBI_TargetTransferValue() As DbSet(Of Snapt0606_SAPBI_TargetTransferValue)
    Public Property Snapt0606_SAPBI_Unit() As DbSet(Of Snapt0606_SAPBI_Unit)
    Public Property Snapt0606_SAPBI_UnitCancellation() As DbSet(Of Snapt0606_SAPBI_UnitCancellation)
    Public Property TEMPSD05PDDS() As DbSet(Of TEMPSD05PDDS)
    Public Property ZTypeHouses() As DbSet(Of ZTypeHouse)
    Public Property ED03UNITTYPE() As DbSet(Of ED03UNITTYPE)
    Public Property KD05RSCD() As DbSet(Of KD05RSCD)
    Public Property LD01SMAN() As DbSet(Of LD01SMAN)

End Class