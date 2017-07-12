<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DatePicker.ascx.vb" Inherits="SPW.Web.UI.DatePicker" %>

<asp:HiddenField ID="hddMasterLG" runat="server" />
<script type="text/javascript">

    function attachDatePicker() {
        var hddMasterLG = $('[id$=hddMasterLG]');
        LoadClearSession();
        callLoadHide();
        if (hddMasterLG.val() == "TH") {
            $('.datepicker').datepicker({
                language: 'th-th',
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true

            }).on('changeDate', function (selected) {
                var inputs = $(this).closest('form').find(':input:visible');
                var nextele = inputs.eq(inputs.index(this) + 1);
                if (nextele.hasClass('datepicker') && nextele.hasClass('enddate')) {
                    var minDate = new Date(selected.date.valueOf());
                    $(nextele).datepicker('setStartDate', minDate);
                    $(nextele).val('');
                }
              
            });
        } else {
            $('.datepicker').datepicker({
                language: 'en',
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true

            }).on('changeDate', function (selected) {
                var inputs = $(this).closest('form').find(':input:visible');
                var nextele = inputs.eq(inputs.index(this) + 1);
                if (nextele.hasClass('datepicker') && nextele.hasClass('enddate')) {
                    var minDate = new Date(selected.date.valueOf());
                    $(nextele).datepicker('setStartDate', minDate);
                    $(nextele).val('');
                }
            });
        }

    }
    $(document).ready(function () {
        attachDatePicker();
    });

    function Check_Key_Date(txt, e) {
        var Dates = document.getElementById(txt.id);
        var key;
        if (document.all) {
            key = window.event.keyCode;
        }
        else {
            key = e.which;
        }

        if (key != 47) {
            if (key < 48 || key > 57) {
                if (key == 0 || key == 8) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (String(Dates.value).length > 9) {
                    return false;
                }
                else {
                    return true;
                }
            }
        } else {
            if (key == 47) {
                if (Dates.value.indexOf("/") > -1) {
                    if (Dates.value.split("/").length > 2) {
                        return false;
                    }
                } else {
                    return true;
                }
            }
        }
    }

</script>