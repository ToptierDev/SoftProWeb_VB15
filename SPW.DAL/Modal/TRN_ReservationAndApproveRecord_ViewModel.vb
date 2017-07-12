
Public Class TRN_ReservationAndApproveRecord_ViewModel
    Public Sub New()

        'บันทึกประวัติผู้มาติดต่อ SL01
        DataDescription.Add("SL01", "บันทึกประวัติผู้มาติดต่อ")
        DataDescription.Add("SL01.OD50RCVD", "ผู้ติดต่อ")

        'บันทึกการจอง SL02
        DataDescription.Add("SL02", "บันทึกการจอง")
        DataDescription.Add("SL02.OD11BKT1", "ใบจอง")
        DataDescription.Add("SL02.List_OD21LAGD", "รายการโปรโมชั่น")
        DataDescription.Add("SL02.List_OD21LAGD2", "รายการสินค้าโปรโมชัน")
        DataDescription.Add("SL02.List_OD21LAPM", "ประวัติการเปลี่ยนแปลงรายการโปรโมชั่น")

        'รับชำระเงิน SL03
        DataDescription.Add("SL03", "รับชำระเงิน")
        DataDescription.Add("SL03.BD24CRRG", "รายละเอียดการรับเงิน")
        DataDescription.Add("SL03.List_RD26ORRG", "ใบเสร็จรับเงิน")
        DataDescription.Add("SL03.List_REPRINTLOG", "ประวัติการพิมพ์ใบเสร็จรับเงิน,ปลดล็อค")

        'เปลี่ยนผู้ติดต่อเป็นลูกค้า SL05
        DataDescription.Add("SL05", "เปลี่ยนผู้ติดต่อเป็นลูกค้า")
        DataDescription.Add("SL05.RD01CUST", "ทะเบียนลูกค้า")
        'สัญญาลูกค้า
        DataDescription.Add("SL05.OD20LAGR", "สัญญาลูกค้า")
        DataDescription.Add("SL05.OD20BKLG", "Bookledg")
        DataDescription.Add("SL05.OD23ADDJ", "โอน")


        'รับชำระเงินทำสัญญาพร้อมออกใบเสร็จ SL06_SL09
        DataDescription.Add("SL06", "SL06_SL09รับชำระเงินทำสัญญาพร้อมออกใบเสร็จ")
        DataDescription.Add("SL06.RD11CHR1", "รายการรับชำระเงิน")
        DataDescription.Add("SL06.RD11CHR3", "รายการรับชำระเงิน อ้างอิงตัดชำระค่า..")
        DataDescription.Add("SL06.BD24CRRG", "รายละเอียดการรับชำระเงิน")
        DataDescription.Add("SL06.RD22ARWK", "การ์ดลูกหนี้")
        DataDescription.Add("SL06.RD21ARDL", "เดินรายการ การ์ดลูกหนี้ ")
        DataDescription.Add("SL06.RD26ORRG", "ใบเสร็จรับเงิน")
        DataDescription.Add("SL06.REPRINTLOG", "ประวัติการพิมพ์ใบเสร็จรับเงิน,ปลดล็อค")

        'เตรียมเอกสาร ตั้งเรื่องโอนกรรมสิทธ์ พร้อมนัดลูกค้า SL11
        DataDescription.Add("SL11", "เตรียมเอกสาร ตั้งเรื่องโอนกรรมสิทธ์ พร้อมนัดลูกค้า")
        DataDescription.Add("SL11.OD23ADDJ", "โอน")
        DataDescription.Add("SL11.OD21LAGD", "รายการโปรโมชั่น")
        DataDescription.Add("SL11.OD21LAGD2", "รายการสินค้าโปรโมชัน")
        DataDescription.Add("SL11.OD21LAPM", "ประวัติการเปลี่ยนแปลงรายการโปรโมชั่น")


    End Sub

    Public Property DataDescription As New Dictionary(Of String, String)()
    'SL01
    Public Property OD50RCVD() As OD50RCVD
    'SL02
    Public Property OD11BKT1() As OD11BKT1
    Public Property List_OD21LAGD() As List(Of OD21LAGD)
    Public Property List_OD21LAGD2() As List(Of OD21LAGD2)
    Public Property List_OD21LAPM() As List(Of OD21LAPM)
    'SL03
    Public Property BD24CRRG() As BD24CRRG
    Public Property List_RD26ORRG() As List(Of RD26ORRG)
    Public Property List_REPRINTLOG() As List(Of REPRINTLOG)






End Class

