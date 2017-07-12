<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Displaymap.aspx.vb" Inherits="SPW.Web.UI.Displaymap" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Soft Pro Web Log in</title>

    <style>
        /* Loading Spinner */
        .spinner {
            margin: 0;
            width: 70px;
            height: 18px;
            margin: -35px 0 0 -9px;
            position: absolute;
            top: 50%;
            left: 50%;
            text-align: center;
        }

            .spinner > div {
                width: 18px;
                height: 18px;
                background-color: #333;
                border-radius: 100%;
                display: inline-block;
                -webkit-animation: bouncedelay 1.4s infinite ease-in-out;
                animation: bouncedelay 1.4s infinite ease-in-out;
                -webkit-animation-fill-mode: both;
                animation-fill-mode: both;
            }

            .spinner .bounce1 {
                -webkit-animation-delay: -.32s;
                animation-delay: -.32s;
            }

            .spinner .bounce2 {
                -webkit-animation-delay: -.16s;
                animation-delay: -.16s;
            }

        @-webkit-keyframes bouncedelay {
            0%,80%,100% {
                -webkit-transform: scale(0.0);
            }

            40% {
                -webkit-transform: scale(1.0);
            }
        }

        @keyframes bouncedelay {
            0%,80%,100% {
                transform: scale(0.0);
                -webkit-transform: scale(0.0);
            }

            40% {
                transform: scale(1.0);
                -webkit-transform: scale(1.0);
            }
        }
    </style>

    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/images/icons/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/images/icons/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/images/icons/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/images/icons/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="assets/images/icons/favicon.png">

    <link rel="stylesheet" type="text/css" href="assets/bootstrap/css/bootstrap.css">

    <!-- HELPERS -->

    <link rel="stylesheet" type="text/css" href="assets/helpers/animate.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/backgrounds.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/boilerplate.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/border-radius.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/grid.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/page-transitions.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/spacing.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/typography.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/utils.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/colors.css">

    <!-- ELEMENTS -->

    <link rel="stylesheet" type="text/css" href="assets/elements/badges.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/buttons.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/content-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/dashboard-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/forms.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/images.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/info-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/invoice.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/loading-indicators.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/menus.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/panel-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/response-messages.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/responsive-tables.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/ribbon.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/social-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/tables.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/tile-box.css">
    <link rel="stylesheet" type="text/css" href="assets/elements/timeline.css">



    <!-- ICONS -->

    <link rel="stylesheet" type="text/css" href="assets/icons/fontawesome/fontawesome.css">
    <link rel="stylesheet" type="text/css" href="assets/icons/linecons/linecons.css">
    <link rel="stylesheet" type="text/css" href="assets/icons/spinnericon/spinnericon.css">


    <!-- WIDGETS -->

    <link rel="stylesheet" type="text/css" href="assets/widgets/accordion-ui/accordion.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/calendar/calendar.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/carousel/carousel.css">

    <link rel="stylesheet" type="text/css" href="assets/widgets/charts/justgage/justgage.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/charts/morris/morris.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/charts/piegage/piegage.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/charts/xcharts/xcharts.css">

    <link rel="stylesheet" type="text/css" href="assets/widgets/chosen/chosen.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/colorpicker/colorpicker.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/datatable/datatable.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/datepicker/datepicker.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/datepicker-ui/datepicker.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/dialog/dialog.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/dropdown/dropdown.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/dropzone/dropzone.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/file-input/fileinput.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/input-switch/inputswitch.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/input-switch/inputswitch-alt.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/ionrangeslider/ionrangeslider.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/jcrop/jcrop.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/jgrowl-notifications/jgrowl.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/loading-bar/loadingbar.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/maps/vector-maps/vectormaps.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/markdown/markdown.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/modal/modal.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/multi-select/multiselect.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/multi-upload/fileupload.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/nestable/nestable.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/noty-notifications/noty.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/popover/popover.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/pretty-photo/prettyphoto.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/progressbar/progressbar.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/range-slider/rangeslider.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/slidebars/slidebars.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/slider-ui/slider.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/summernote-wysiwyg/summernote-wysiwyg.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/tabs-ui/tabs.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/theme-switcher/themeswitcher.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/timepicker/timepicker.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/tocify/tocify.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/tooltip/tooltip.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/touchspin/touchspin.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/uniform/uniform.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/wizard/wizard.css">
    <link rel="stylesheet" type="text/css" href="assets/widgets/xeditable/xeditable.css">

    <!-- SNIPPETS -->

    <link rel="stylesheet" type="text/css" href="assets/snippets/chat.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/files-box.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/login-box.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/notification-box.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/progress-box.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/todo.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/user-profile.css">
    <link rel="stylesheet" type="text/css" href="assets/snippets/mobile-navigation.css">

    <!-- APPLICATIONS -->

    <link rel="stylesheet" type="text/css" href="assets/applications/mailbox.css">

    <!-- Admin theme -->

    <link rel="stylesheet" type="text/css" href="assets/themes/admin/layout.css">
    <link rel="stylesheet" type="text/css" href="assets/themes/admin/color-schemes/default.css">

    <!-- Components theme -->

    <link rel="stylesheet" type="text/css" href="assets/themes/components/default.css">
    <link rel="stylesheet" type="text/css" href="assets/themes/components/border-radius.css">

    <!-- Admin responsive -->

    <link rel="stylesheet" type="text/css" href="assets/helpers/responsive-elements.css">
    <link rel="stylesheet" type="text/css" href="assets/helpers/admin-responsive.css">

    <!-- JS Core -->

    <script type="text/javascript" src="assets/js-core/jquery-core.js"></script>
    <script type="text/javascript" src="assets/js-core/jquery-ui-core.js"></script>
    <script type="text/javascript" src="assets/js-core/jquery-ui-widget.js"></script>
    <script type="text/javascript" src="assets/js-core/jquery-ui-mouse.js"></script>
    <script type="text/javascript" src="assets/js-core/jquery-ui-position.js"></script>
    <!--<script type="text/javascript" src="assets/js-core/transition.js"></script>-->
    <script type="text/javascript" src="assets/js-core/modernizr.js"></script>
    <script type="text/javascript" src="assets/js-core/jquery-cookie.js"></script>

    <script type="text/javascript" src="assets/bootstrap/js/bootstrap.js"></script>

    <!-- Bootstrap Dropdown -->

    <!-- <script type="text/javascript" src="assets/widgets/dropdown/dropdown.js"></script> -->

    <!-- Bootstrap Tooltip -->

    <!-- <script type="text/javascript" src="assets/widgets/tooltip/tooltip.js"></script> -->

    <!-- Bootstrap Popover -->

    <!-- <script type="text/javascript" src="assets/widgets/popover/popover.js"></script> -->

    <!-- Bootstrap Progress Bar -->

    <script type="text/javascript" src="assets/widgets/progressbar/progressbar.js"></script>

    <!-- Bootstrap Buttons -->

    <!-- <script type="text/javascript" src="assets/widgets/button/button.js"></script> -->

    <!-- Bootstrap Collapse -->

    <!-- <script type="text/javascript" src="assets/widgets/collapse/collapse.js"></script> -->

    <!-- Superclick -->

    <script type="text/javascript" src="assets/widgets/superclick/superclick.js"></script>

    <!-- Input switch alternate -->

    <script type="text/javascript" src="assets/widgets/input-switch/inputswitch-alt.js"></script>

    <!-- Slim scroll -->

    <script type="text/javascript" src="assets/widgets/slimscroll/slimscroll.js"></script>

    <!-- Slidebars -->

    <script type="text/javascript" src="assets/widgets/slidebars/slidebars.js"></script>
    <script type="text/javascript" src="assets/widgets/slidebars/slidebars-demo.js"></script>

    <!-- PieGage -->

    <script type="text/javascript" src="assets/widgets/charts/piegage/piegage.js"></script>
    <script type="text/javascript" src="assets/widgets/charts/piegage/piegage-demo.js"></script>

    <!-- Screenfull -->

    <script type="text/javascript" src="assets/widgets/screenfull/screenfull.js"></script>

    <!-- Content box -->

    <script type="text/javascript" src="assets/widgets/content-box/contentbox.js"></script>

    <!-- Overlay -->

    <script type="text/javascript" src="assets/widgets/overlay/overlay.js"></script>

    <!-- Widgets init for demo -->

    <script type="text/javascript" src="assets/js-init/widgets-init.js"></script>

    <!-- Theme layout -->

    <script type="text/javascript" src="assets/themes/admin/layout.js"></script>

    <!-- Theme switcher -->

    <script type="text/javascript" src="assets/widgets/theme-switcher/themeswitcher.js"></script>

    <!-- jGrowl notifications -->

    <!--<link rel="stylesheet" type="text/css" href="assets/widgets/jgrowl-notifications/jgrowl.css">-->
    <script type="text/javascript" src="assets/widgets/jgrowl-notifications/jgrowl.js"></script>
    <script type="text/javascript" src="assets/widgets/jgrowl-notifications/jgrowl-demo.js"></script>

    <!-- Noty notifications -->

    <!--<link rel="stylesheet" type="text/css" href="assets/widgets/noty-notifications/noty.css">-->
    <script type="text/javascript" src="assets/widgets/noty-notifications/noty.js"></script>
    <script type="text/javascript" src="assets/widgets/noty-notifications/noty-demo.js"></script>

    <%--<link href="Style/jquery-ui.css" rel="stylesheet" />
    <script src="Script/jquery-1.12.4.js"></script>
    <script src="Script/jquery-ui.js"></script>--%>

    <script type="text/javascript">
        $(window).load(function () {
            setTimeout(function () {
                $('#loading').fadeOut(400, "linear");
            }, 300);
        });
    </script>
</head>
<body>
    <div id="loading">
        <div class="spinner">
            <div class="bounce1"></div>
            <div class="bounce2"></div>
            <div class="bounce3"></div>
        </div>
    </div>
    <style type="text/css">
        html, body {
            height: 100%;
            background: #fff;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript" src="assets/widgets/wow/wow.js"></script>
    <script type="text/javascript">
        /* WOW animations */

        wow = new WOW({
            animateClass: 'animated',
            offset: 100
        });
        wow.init();
    </script>
    <%--<img src="assets/image-resources/blurred-bg/blurred-bg-2.jpg" class="login-img wow fadeIn" alt="">--%>
    <div class="center-vertical">
        <div class="center-content row">
            <form id="from1" runat="server" class="col-md-4 col-sm-5 col-xs-11 col-lg-4 center-margin">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="86400"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <ContentTemplate>
                        <%--<h3 class="text-center pad25B font-gray text-transform-upr font-size-23">Soft Pro Web</h3>--%>
                        <div id="login-form" class="content-box bg-default">
                            <div class="content-box-wrapper pad20A">
                                <img class="col-lg-12 col-md-12 col-sm-12 mrg25B center-margin display-block" style="width: 100%; height: 100%;" src="image/Logo-Pinsiris.png" alt="">
                                <table style="width:100%">
                                    <tr>
                                        <td style="vertical-align: top;width:25%">Employee No :&nbsp;
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtaUserName" autocomplete="off" runat="server" class="form-control"
                                                        onkeypress="return removespacial(this, event);" placeholder="Enter Employee No" onblur="CallReload();"></asp:TextBox>
                                                    <span class="input-group-addon bg-blue-alt">
                                                        <i class="glyph-icon icon-child"></i>
                                                    </span>
                                                </div>
                                                <asp:Label ID="lblMassage1" runat="server" Text="This value is required" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                        <td style="vertical-align: top;width:25%">Password :&nbsp;
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtaPassword" autocomplete="off" runat="server" class="form-control"
                                                        placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                                    <span class="input-group-addon bg-blue-alt">
                                                        <i class="glyph-icon icon-unlock-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:Label ID="lblMassage2" runat="server" Text="This value is required" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                        <td style="vertical-align: top;width:25%">Company :&nbsp;
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlsCompany" runat="server" class="form-control"></asp:DropDownList>
                                                    <span class="input-group-addon bg-blue-alt">
                                                        <i class="glyph-icon icon-institution"></i>
                                                    </span>
                                                </div>
                                                <asp:Label ID="lblMassage3" runat="server" Text="This value is required" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                        <td style="vertical-align: top;width:25%">Module :&nbsp;
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlsModule" runat="server" class="form-control"></asp:DropDownList>
                                                    <span class="input-group-addon bg-blue-alt">
                                                        <i class="glyph-icon icon-laptop"></i>
                                                    </span>
                                                </div>
                                                <asp:Label ID="lblMassage4" runat="server" Text="This value is required" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                        <td style="vertical-align: top;width:25%">Language :&nbsp;
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlaWebLang" runat="server" class="form-control"></asp:DropDownList>
                                                    <span class="input-group-addon bg-blue-alt">
                                                        <i class="glyph-icon icon-flag"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div class="form-group">
                                    <asp:Button ID="btnLoginTemp" runat="server" Text="Sign In"
                                        class="btn btn-block btn-blue-alt" OnClientClick="javascript:return CheckData();" />
                                    <asp:Button ID="btnLogin" runat="server" class="hide" />
                                    <asp:Label ID="Label1" runat="server" Text="Please check User ID or Password" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="row">
                                    <%--<div class="col-lg-8 col-md-8 col-sm-8"></div>--%>
                                    <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: right;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="#3333CC" NavigateUrl="#"><u>Forget</u></asp:HyperLink>&nbsp;
                                                
                                                    <asp:HyperLink ID="hplRegister" runat="server" ForeColor="#3333CC" NavigateUrl="~/Register.aspx"><u>Register</u></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnReload" runat="server" CssClass="hide" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <script type="text/javascript">
                    $(document).ready(function () {
                        SetInitial();
                    });

                    function SetInitial() {

                    }

                    function CheckData() {
                        document.getElementById('Label1').style.display = "none";
                        if ($('#<%=txtaUserName.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaUserName.ClientID%>').addClass("parsley-error")
                            document.getElementById('lblMassage1').style.display = "";
                            return false;
                        } else {
                            $('#<%=txtaUserName.ClientID%>').removeClass("parsley-error")
                            document.getElementById('lblMassage1').style.display = "none";
                        }
                        if ($('#<%=txtaPassword.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaPassword.ClientID%>').addClass("parsley-error")
                            document.getElementById('lblMassage2').style.display = "";
                            return false;
                        } else {
                            $('#<%=txtaPassword.ClientID%>').removeClass("parsley-error")
                            document.getElementById('lblMassage2').style.display = "none";
                        }
                        if ($('#<%=ddlsCompany.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=ddlsCompany.ClientID%>').addClass("parsley-error")
                            document.getElementById('lblMassage3').style.display = "";
                            return false;
                        } else {
                            $('#<%=ddlsCompany.ClientID%>').removeClass("parsley-error")
                            document.getElementById('lblMassage3').style.display = "none";
                        }
                        if ($('#<%=ddlsModule.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=ddlsModule.ClientID%>').addClass("parsley-error")
                            document.getElementById('lblMassage4').style.display = "";
                            return false;
                        } else {
                            $('#<%=ddlsModule.ClientID%>').removeClass("parsley-error")
                            document.getElementById('lblMassage4').style.display = "none";
                        }

                        __doPostBack('<%= btnLogin.UniqueID%>');
                        return false;
                    }

                    function CallReload() {
                        __doPostBack('<%= btnReload.UniqueID%>');
                        return;
                    }

                    function OpenDialogText() {
                        //$.jGrowl(Msg, {
                        //    sticky: false,
                        //    position: 'top-right',
                        //    theme: 'bg-red'
                        //});
                        //noty({
                        //    text: Msg,
                        //    type: 'error',
                        //    dismissQueue: true,
                        //    theme: 'agileUI',
                        //    layout: 'bottom'
                        //});
                        document.getElementById('Label1').style.display = "";
                    }

                        function removespacial(txt, e) {
                            var key;
                            if (document.all) {
                                key = window.event.keyCode;
                            }
                            else {
                                key = e.which;
                            }

                            if (key == 47 ||
                                key == 91 ||
                                key == 38 ||
                                key == 92 ||
                                key == 35 ||
                                key == 44 ||
                                key == 43 ||
                                key == 40 ||
                                key == 41 ||
                                key == 36 ||
                                key == 126 ||
                                key == 37 ||
                                key == 46 ||
                                key == 39 ||
                                key == 34 ||
                                key == 58 ||
                                key == 42 ||
                                key == 63 ||
                                key == 60 ||
                                key == 62 ||
                                key == 123 ||
                                key == 125 ||
                                key == 93 ||
                                key == 33 ||
                                key == 64 ||
                                key == 94) {
                                return false;
                            } else {
                                return true;
                            }
                        }
                </script>
            </form>
        </div>
    </div>
</body>
</html>
