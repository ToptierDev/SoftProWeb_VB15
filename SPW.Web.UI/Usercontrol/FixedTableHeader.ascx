<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FixedTableHeader.ascx.vb" Inherits="SPW.Web.UI.FixedTableHeader" %>
  <script>
        window.onscroll = function () {
            toggleFixheader($('.fixed-header'));
            toggleFixheader($('.fixed-header2'));
        };
        function toggleFixheader(el) {
            var pageOffset = document.documentElement.scrollTop || document.body.scrollTop;
            var tableOffset = el.offset().top;
            if (pageOffset + 100 > tableOffset) {
                el.addClass('active');
            } else {
                el.removeClass('active');
            }
        };
        

        $(function () {
            $('.fixed-header thead').scroll(function () {
                $('.detailTable-fixheader').scrollLeft($('.fixed-header thead').scrollLeft());
            });
            $('.detailTable-fixheader').scroll(function () {
                $('.fixed-header thead').scrollLeft($('.detailTable-fixheader').scrollLeft());
            });
            $('.fixed-header2 thead').scroll(function () {
                $('.detailTable-fixheader2').scrollLeft($('.fixed-header2 thead').scrollLeft());
            });
            $('.detailTable-fixheader2').scroll(function () {
                $('.fixed-header2 thead').scrollLeft($('.detailTable-fixheader2').scrollLeft());
            });

            $('#close-sidebar').click(function () {
                setTimeout(function () {
                    $('.fixed-header thead').width($('.detailTable').width());
                    $('.fixed-header2 thead').width($('.detailTable2').width());
                }, 1000);
            });
            toggleFixheader($('.fixed-header'));
            toggleFixheader($('.fixed-header2'));
        });
        function setTableWidth() {
            toggleFixheader($('.fixed-header'));
            toggleFixheader($('.fixed-header2'));
            $('.fixed-header thead').width($('.detailTable').width());
            $('.detailTable-fixheader thead tr').width($('.detailTable-fixheader tbody').outerWidth());
            $('.detailTable-fixheader tbody tr').width($('.detailTable-fixheader tbody').outerWidth());
            $('.detailTable-fixheader tbody>tr:first td').each(function (index, el) {
                $('.detailTable-fixheader thead>tr:first th').eq(index).outerWidth($(el).outerWidth());
            });

            $('.fixed-header2 thead').width($('.detailTable2').width());
            $('.detailTable-fixheader2 thead tr').width($('.detailTable-fixheader2 tbody').outerWidth());
            $('.detailTable-fixheader2 tbody tr').width($('.detailTable-fixheader2 tbody').outerWidth());
            $('.detailTable-fixheader2 tbody>tr:first td').each(function (index, el) {
                $('.detailTable-fixheader2 thead>tr:first th').eq(index).outerWidth($(el).outerWidth());
            });
          //  $('.detailTable-fixheader thead>tr:first th').eq(0).outerWidth($('.detailTable-fixheader tbody>tr:first td').eq(0).outerWidth())
        }
        setTableWidth();

    </script>
    <style>
       .detailTable-fixheader,.detailTable-fixheader2{
          width: 100%; overflow-x: auto; overflow-y: hidden;
      }

        .detailTable-fixheader thead tr,.detailTable-fixheader2 thead tr{
          
            display:block;
        }
        .detailTable-fixheader tbody tr,.detailTable-fixheader2 tbody tr{
         
            display:block;
        }
        .fixed-header thead,.fixed-header2 thead{
            overflow-x: scroll;
    z-index: 2;
        }
        .fixed-header.active thead,.fixed-header2.active thead{
             position: fixed;
            top: 77px;
        }

    </style>