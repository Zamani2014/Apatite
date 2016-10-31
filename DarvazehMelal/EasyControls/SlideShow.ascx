<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SlideShow.ascx.cs" Inherits="SlideShowControl_SlideShow" %>
<!--
<script type="text/javascript">
    var myArray = [];

    $(document).ready(function() {
        $.ajax({
            type: "GET",
            url: "xml/sites.xml",
            dataType: "xml",
            success: function(xml) {
                var count = 0;
                $(xml).find('site').each(function() {

                    var url = $(this).find('url').text();
                    var target = $(this).find('target').text();
                    var imageURL = $(this).find('imageURL').text();
                    var alt = $(this).find('alt').text();

                    myArray[parseInt(count)] = new Array(imageURL, url, target, alt);  //[imageURL, url, '', ''];
                    count++;
                });

                var mygallery2 = new simpleGallery({
                    wrapperid: "simplegallery2", //ID of main gallery container,
                    dimensions: [728, 95], //width/height of gallery in pixels. Should reflect dimensions of the images exactly
                    imagearray: myArray,
                    autoplay: [true, 10000, 95], //[auto_play_boolean, delay_btw_slide_millisec, cycles_before_stopping_int]
                    persist: true,
                    test: "moo",
                    fadeduration: 2000, //transition duration (milliseconds)
                    oninit: function() { //event that fires when gallery has initialized/ ready to run
                    },
                    onslide: function(curslide, i) { //event that fires after each slide is shown
                        //curslide: returns DOM reference to current slide's DIV (ie: try alert(curslide.innerHTML)
                        //i: integer reflecting current image within collection being shown (0=1st image, 1=2nd etc)
                    }
                })
            }
        });

    });
</script>
-->


<div  style="background: white none repeat scroll 0% 0%; overflow: hidden; 
position: relative; visibility: visible; -moz-background-clip: border; -moz-background-origin: 
padding; -moz-background-inline-policy: continuous;" id="simplegallery2"></div>

